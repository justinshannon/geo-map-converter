using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GeoMapConverter.GeoRam.ConsoleCommand;
using GeoMapConverter.GeoRam.GeoMap;
using GeoMapConverter.Utils;
using GeoMapConverter.VatsimGeoMapSet;

namespace GeoMapConverter
{
    public partial class MainForm : Form
    {
        private string _defaultMap = "";

        private List<string> _usedFilterGroups = new List<string>();
        private List<string> _usedBcgGroups = new List<string>();

        private List<Element> _geoLines = new List<Element>();
        private List<Element> _geoSymbols = new List<Element>();
        private List<Element> _geoText = new List<Element>();

        private readonly List<BcgMenu> _bcgMenuList = new List<BcgMenu>();
        private readonly List<FilterMenu> _filterMenuList = new List<FilterMenu>();
        private readonly GeoMapSet _geoMapSet = new GeoMapSet();
        private readonly List<GeoMap> _geoMapList = new List<GeoMap>();
        private readonly List<VatsimGeoMapSet.GeoMapObject> _geoMapObjects = new List<VatsimGeoMapSet.GeoMapObject>();

        private readonly System.Threading.SynchronizationContext mSyncContext;

        public MainForm()
        {
            InitializeComponent();

            mSyncContext = System.Threading.SynchronizationContext.Current;
        }

        /// <summary>
        /// Opens and parses the ConsoleCommandControl.xml file
        /// The BCG (Brightness Control Groups) and Menu Filter buttons
        /// are parsed and added to the treeview.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOpenConsoleCommand_Click(object sender, EventArgs e)
        {
            if (dlgConsoleCommand.ShowDialog(this) == DialogResult.OK)
            {
                btnOpenConsoleCommand.Enabled = false;
                btnExport.Enabled = false;
                btnOpenGeoMaps.Enabled = false;

                myTreeView.Nodes.Clear();
                _bcgMenuList.Clear();
                _filterMenuList.Clear();

                if (!bwLoadConsoleCommand.IsBusy)
                {
                    bwLoadConsoleCommand.RunWorkerAsync();
                }
            }
        }

        /// <summary>
        /// Opens and parses the GeoMaps.xml file.
        /// The GeoMap records are parsed so they can be later added to the exported file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOpenGeoMaps_Click(object sender, EventArgs e)
        {
            if (dlgGeoMaps.ShowDialog(this) == DialogResult.OK)
            {
                btnExport.Enabled = false;
                btnExport.Text = "Loading GeoMaps...";
                btnOpenConsoleCommand.Enabled = false;
                btnOpenGeoMaps.Enabled = false;

                _geoMapList.Clear();
                _geoMapObjects.Clear();
                _usedFilterGroups.Clear();
                _usedBcgGroups.Clear();
                _geoLines.Clear();
                _geoSymbols.Clear();
                _geoText.Clear();

                if (!bwLoadGeoMaps.IsBusy)
                {
                    bwLoadGeoMaps.RunWorkerAsync();
                }
            }
        }

        /// <summary>
        /// Exports the selected geomaps into a vERAM formatted file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnExport_Click(object sender, EventArgs e)
        {
            int x = 0;
            foreach (TreeNode node in myTreeView.Nodes)
            {
                if (node.Checked)
                {
                    x++;
                }

                foreach (TreeNode child in node.Nodes)
                {
                    if (child.Checked)
                    {
                        x++;
                    }
                }
            }

            if (x == 0) return;

            List<string> checkedGeoMaps = new List<string>();
            List<string> checkedFilters = new List<string>();

            List<string> usedBcgGroups = new List<string>();
            List<string> usedFilterGroups = new List<string>();

            if (dlgSaveMaps.ShowDialog(this) == DialogResult.OK)
            {
                _geoMapSet.GeoMaps.Clear();
                _geoMapSet.BcgMenus.Clear();
                _geoMapSet.FilterMenus.Clear();

                _geoMapSet.DefaultMap = _defaultMap;

                foreach (TreeNode node in myTreeView.Nodes)
                {
                    if (node.Checked)
                    {
                        checkedGeoMaps.Add(node.Text);

                        foreach (TreeNode child in node.Nodes)
                        {
                            if (child.Checked)
                            {
                                checkedFilters.Add(child.Tag.ToString());
                            }
                        }
                    }
                }

                foreach (GeoMap geoMap in _geoMapList.Where(n => checkedGeoMaps.Contains(n.FilterMenuName)))
                {
                    usedBcgGroups.Add(geoMap.BcgMenuName);
                    usedFilterGroups.Add(geoMap.FilterMenuName);

                    _geoMapSet.GeoMaps.Add(new GeoMap
                    {
                        GeoMapSetName = geoMap.GeoMapSetName,
                        Name = geoMap.Name,
                        BcgMenuName = geoMap.BcgMenuName,
                        FilterMenuName = geoMap.FilterMenuName,
                        LabelLine1 = geoMap.LabelLine1,
                        LabelLine2 = geoMap.LabelLine2,
                        Objects = _geoMapObjects.Where(n => checkedFilters.Contains(n.FilterGroup))
                            .ToList()
                    });
                }

                foreach (FilterMenu filterMenu in _filterMenuList.Where(n => usedFilterGroups.Contains(n.Name)))
                {
                    for (int i = 0; i < filterMenu.FilterLabels.Count; i++)
                    {
                        if (_usedFilterGroups.Contains(i.ToString()))
                        {
                            _geoMapSet.FilterMenus.Add(filterMenu);
                        }
                    }
                }

                foreach (BcgMenu bcgMenu in _bcgMenuList.Where(n => usedBcgGroups.Contains(n.Name)))
                {
                    for (int i = 0; i < bcgMenu.BcgMenuItemList.Count; i++)
                    {
                        if (_usedBcgGroups.Contains(i.ToString()))
                        {
                            _geoMapSet.BcgMenus.Add(bcgMenu);
                        }
                    }
                }

                SerializationUtils.SerializeObjectToFile(_geoMapSet, dlgSaveMaps.FileName);
            }
        }

        /// <summary>
        /// Iterates through the treeview and checks or unchecks the child and parent nodes accordingly.
        /// If no child nodes are checked, don't check the parent. If the parent node is checked, check
        /// all child nodes. If only a child node is checked, check the parent.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node.Nodes.Count > 0)
                {
                    CheckAllChildNodes(e.Node, e.Node.Checked);
                }

            }

            SelectParents(e.Node, e.Node.Checked);
        }

        /// <summary>
        /// Checks the parent node if a child node is checked
        /// </summary>
        /// <param name="node"></param>
        /// <param name="isChecked"></param>
        private void SelectParents(TreeNode node, bool isChecked)
        {
            var parent = node.Parent;

            if (parent == null)
                return;

            if (!isChecked && HasCheckedNode(parent))
                return;

            parent.Checked = isChecked;
            SelectParents(parent, isChecked);
        }

        /// <summary>
        /// Determines if the parent node has any child nodes checked
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private bool HasCheckedNode(TreeNode node)
        {
            return node.Nodes.Cast<TreeNode>().Any(n => n.Checked);
        }

        /// <summary>
        /// Checks all child nodes
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="nodeChecked"></param>
        private void CheckAllChildNodes(TreeNode treeNode, bool nodeChecked)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                node.Checked = nodeChecked;
                if (node.Nodes.Count > 0)
                {
                    this.CheckAllChildNodes(node, nodeChecked);
                }
            }
        }

        private void BwLoadConsoleCommand_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var obj =
                SerializationUtils.DeserializeObjectFromFile<ConsoleCommandControlRecords>(dlgConsoleCommand
                    .FileName);

            // iterator through map brightness menus
            foreach (MapBrightnessMenu mapBrightnessMenu in obj.MapBrightnessMenus)
            {
                BcgMenu bcgMenu = new BcgMenu
                {
                    Name = mapBrightnessMenu.BcgMenuName,
                    BcgMenuItemList = new List<BcgMenuItem>()
                };

                foreach (MapBcgButton mapBcgButton in mapBrightnessMenu.MapBcgButtons)
                {
                    bcgMenu.BcgMenuItemList.Add(new BcgMenuItem
                    {
                        Label = mapBcgButton.Label
                    });
                }

                _bcgMenuList.Add(bcgMenu);
            }

            // iterate through map filter menus
            foreach (MapFilterMenu mapFilterMenu in obj.MapFilterMenus)
            {
                FilterMenu filterMenu = new FilterMenu
                {
                    Name = mapFilterMenu.FilterMenuName,
                    FilterLabels = new List<FilterLabel>()
                };

                string key = mapFilterMenu.FilterMenuName;

                // add geomap parent to treeview
                mSyncContext.Post(n =>
                {
                    if (!myTreeView.Nodes.ContainsKey(key))
                    {
                        myTreeView.Nodes.Add(key, key);
                    }
                }, null);

                // add geomap filters (children) to treeview
                int i = 1;
                foreach (MapFilterButton mapFilterButton in mapFilterMenu.MapFilterButtons)
                {
                    mSyncContext.Post(n =>
                    {
                        filterMenu.FilterLabels.Add(new FilterLabel
                        {
                            LabelLine1 = mapFilterButton.LabelLine1,
                            LabelLine2 = mapFilterButton.LabelLine2
                        });

                        TreeNode tn = new TreeNode(mapFilterButton.FilterButtonName)
                        {
                            Tag = i
                        };
                        myTreeView.Nodes[key].Nodes.Add(tn);

                        i++;
                    },null);
                }

                _filterMenuList.Add(filterMenu);
            }
        }

        private void BwLoadConsoleCommand_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            btnOpenGeoMaps.Enabled = true;
            btnOpenConsoleCommand.Enabled = true;
        }

        private void BwLoadGeoMaps_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var obj = SerializationUtils.DeserializeObjectFromFile<GeoMapRecords>(dlgGeoMaps.FileName);

            int i = 0;
            foreach (GeoMapRecord geoMapRecord in obj.GeoMapRecordList)
            {
                // set the default map name
                if (i == 0)
                {
                    _defaultMap = geoMapRecord.GeomapId;
                }

                // create a geomap object for each geomap record
                _geoMapList.Add(new GeoMap
                {
                    Name = geoMapRecord.GeomapId,
                    LabelLine1 = geoMapRecord.LabelLine1,
                    LabelLine2 = geoMapRecord.LabelLine2,
                    BcgMenuName = geoMapRecord.BcgMenuName,
                    FilterMenuName = geoMapRecord.FilterMenuName
                });

                // iterator through the geomap objects and parse them accordingly
                foreach (GeoRam.GeoMap.GeoMapObject geoMapObject in geoMapRecord.GeoMapObjectList)
                {
                    SymbolDefaults symbolDefaults = null;
                    TextDefaults textDefaults = null;
                    LineDefaults lineDefaults = null;

                    _geoLines = new List<Element>();
                    _geoSymbols = new List<Element>();
                    _geoText = new List<Element>();

                    _usedBcgGroups = new List<string>();
                    _usedFilterGroups = new List<string>();

                    string filterGroup = "";

                    // parse the default symbol properties
                    if (geoMapObject.DefaultSymbolProperties != null)
                    {
                        symbolDefaults = new SymbolDefaults
                        {
                            BcgGroup = geoMapObject.DefaultSymbolProperties.BcgGroup,
                            Filters = geoMapObject.DefaultSymbolProperties.GeoSymbolFilters.FirstOrDefault(),
                            Style = geoMapObject.DefaultSymbolProperties.SymbolStyle,
                            Size = geoMapObject.DefaultSymbolProperties.FontSize
                        };

                        filterGroup = geoMapObject.DefaultSymbolProperties.GeoSymbolFilters.FirstOrDefault();

                        _usedFilterGroups.Add(filterGroup);
                        _usedBcgGroups.Add(geoMapObject.DefaultSymbolProperties.BcgGroup);
                    }

                    // parse the default line properties
                    if (geoMapObject.DefaultLineProperties != null)
                    {
                        lineDefaults = new LineDefaults
                        {
                            BcgGroup = geoMapObject.DefaultLineProperties.BcgGroup,
                            Filters = geoMapObject.DefaultLineProperties.GeoLineFilters.FirstOrDefault(),
                            Style = geoMapObject.DefaultLineProperties.LineStyle,
                            Thickness = geoMapObject.DefaultLineProperties.Thickness
                        };

                        filterGroup = geoMapObject.DefaultLineProperties.GeoLineFilters.FirstOrDefault();

                        _usedFilterGroups.Add(filterGroup);
                        _usedBcgGroups.Add(geoMapObject.DefaultLineProperties.BcgGroup);
                    }

                    // parse the text default properties
                    if (geoMapObject.TextDefaultProperties != null)
                    {
                        textDefaults = new TextDefaults
                        {
                            BcgGroup = geoMapObject.TextDefaultProperties.BcgGroup,
                            Filters = geoMapObject.TextDefaultProperties.GeoTextFilters.FirstOrDefault(),
                            Underline = geoMapObject.TextDefaultProperties.Underline,
                            Opaque = geoMapObject.TextDefaultProperties.DisplaySetting,
                            XOffset = geoMapObject.TextDefaultProperties.XPixelOffset,
                            YOffset = geoMapObject.TextDefaultProperties.YPixelOffset
                        };

                        filterGroup = geoMapObject.TextDefaultProperties.GeoTextFilters.FirstOrDefault();

                        _usedFilterGroups.Add(filterGroup);
                        _usedBcgGroups.Add(geoMapObject.TextDefaultProperties.BcgGroup);
                    }

                    // parse the geomap lines
                    foreach (GeoMapLine geoMapLine in geoMapObject.GeoMapLineList)
                    {
                        _geoLines.Add(new Line
                        {
                            StartLat = StringUtils.ToDecimalDegrees(geoMapLine.StartLatitude),
                            StartLon = StringUtils.ToDecimalDegrees(geoMapLine.StartLongitude, false),
                            EndLat = StringUtils.ToDecimalDegrees(geoMapLine.EndLatitude),
                            EndLon = StringUtils.ToDecimalDegrees(geoMapLine.EndLongitude, false)
                        });
                    }

                    // parse the geomap symbols
                    foreach (GeoMapSymbol geoMapSymbol in geoMapObject.GeoMapSymbolList)
                    {
                        _geoSymbols.Add(new Symbol
                        {
                            Lat = StringUtils.ToDecimalDegrees(geoMapSymbol.Latitude),
                            Lon = StringUtils.ToDecimalDegrees(geoMapSymbol.Longitude, false)
                        });
                    }

                    // parse the geomap text objects
                    foreach (GeoMapText geoMapText in geoMapObject.GeoMapTextList)
                    {
                        _geoText.Add(new Text
                        {
                            Lat = StringUtils.ToDecimalDegrees(geoMapText.Latitude),
                            Lon = StringUtils.ToDecimalDegrees(geoMapText.Longitude, false),
                            Lines = string.Join(" ", geoMapText.GeoTextStrings)
                        });
                    }

                    // add the geomap objects to a list so they can be later referenced in a geomap record if exported
                    _geoMapObjects.Add(new VatsimGeoMapSet.GeoMapObject
                    {
                        GeoMapName = geoMapRecord.FilterMenuName,
                        Description = $"Object #{geoMapObject.MapGroupId} ({geoMapObject.MapObjectType})",
                        SymbolDefaults = symbolDefaults,
                        LineDefaults = lineDefaults,
                        TextDefaults = textDefaults,
                        Elements = _geoLines.Union(_geoSymbols).Union(_geoText).ToList(),
                        FilterGroup = filterGroup
                    });

                    i++;
                }
            }
        }

        private void BwLoadGeoMaps_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            btnExport.Text = "Export Checked GeoMaps";
            btnExport.Enabled = true;
            btnOpenGeoMaps.Enabled = true;
            btnOpenConsoleCommand.Enabled = true;
        }
    }
}