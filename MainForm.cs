using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GeoMapConverter.GeoRam.ConsoleCommand;
using GeoMapConverter.GeoRam.GeoMap;
using GeoMapConverter.Utils;
using GeoMapConverter.Vatsim;

namespace GeoMapConverter
{
    public partial class MainForm : Form
    {
        private readonly System.Threading.SynchronizationContext mSyncContext;

        private string mDefaultMap = "";
        private List<string> mUsedFilterGroups = new List<string>();
        private List<string> mUsedBcgGroups = new List<string>();

        private List<Vatsim.Line> mGeoLines = new List<Vatsim.Line>();
        private List<Vatsim.Symbol> mGeoSymbols = new List<Vatsim.Symbol>();
        private List<Vatsim.Text> mGeoTexts = new List<Vatsim.Text>();
        private List<Vatsim.BcgMenu> mBcgMenuList = new List<Vatsim.BcgMenu>();
        private List<Vatsim.FilterMenu> mFilterMenuList = new List<Vatsim.FilterMenu>();
        private List<Vatsim.GeoMapObject> mGeoMapObjects = new List<Vatsim.GeoMapObject>();
        private List<Vatsim.GeoMap> mGeoMapList = new List<Vatsim.GeoMap>();
        private Vatsim.GeoMapSet mGeoMapSet = new Vatsim.GeoMapSet();

        private Dictionary<string, Dictionary<string, string>> BcgTranslations = new Dictionary<string, Dictionary<string, string>>();
        private Dictionary<string, Dictionary<string, string>> FilterTranslations = new Dictionary<string, Dictionary<string, string>>();

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
            if (dlgConsoleCommand.ShowDialog(this) == DialogResult.OK) {
                btnOpenConsoleCommand.Enabled = false;
                btnExport.Enabled = false;
                btnOpenGeoMaps.Enabled = false;

                myTreeView.Nodes.Clear();
                mBcgMenuList.Clear();
                mFilterMenuList.Clear();

                if (!bwLoadConsoleCommand.IsBusy) {
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
            if (dlgGeoMaps.ShowDialog(this) == DialogResult.OK) {
                btnExport.Enabled = false;
                btnExport.Text = "Loading GeoMaps...";
                btnOpenConsoleCommand.Enabled = false;
                btnOpenGeoMaps.Enabled = false;

                mGeoMapObjects.Clear();
                mUsedFilterGroups.Clear();
                mUsedBcgGroups.Clear();
                mGeoLines.Clear();
                mGeoSymbols.Clear();
                mGeoTexts.Clear();

                if (!bwLoadGeoMaps.IsBusy) {
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
            foreach (TreeNode node in myTreeView.Nodes) {
                if (node.Checked) {
                    x++;
                }

                foreach (TreeNode child in node.Nodes) {
                    if (child.Checked) {
                        x++;
                    }
                }
            }

            if (x == 0) return;

            List<string> checkedFilters = new List<string>();
            List<string> checkedFilterGroups = new List<string>();

            List<string> mUsedBcgGroups = new List<string>();
            List<string> mUsedFilterGroups = new List<string>();

            if (dlgSaveMaps.ShowDialog(this) == DialogResult.OK) {

                mGeoMapSet.GeoMaps.Clear();
                mGeoMapSet.BcgMenus.Clear();
                mGeoMapSet.FilterMenus.Clear();
                checkedFilters.Clear();
                checkedFilterGroups.Clear();
                mUsedBcgGroups.Clear();
                mUsedFilterGroups.Clear();

                mGeoMapSet.DefaultMap = mDefaultMap;

                foreach (TreeNode node in myTreeView.Nodes) {
                    if (node.Checked) {
                        checkedFilters.Add(node.Text);
                        foreach (TreeNode child in node.Nodes) {
                            if (child.Checked) {
                                string[] split = child.Tag.ToString().Split(',');
                                foreach (var tag in split) {
                                    checkedFilterGroups.Add(tag);
                                }
                            }
                        }
                    }
                }

                //foreach(var f in checkedFilters) {
                //    Console.WriteLine(f);
                //}

                //foreach(var f in checkedFilterGroups) {
                //    Console.WriteLine("*" + f);
                //}

                //Console.WriteLine("-----");

                //foreach (KeyValuePair<string, Dictionary<string, string>> menu in BcgTranslations) {
                //    Console.WriteLine(menu.Key);
                //    foreach (KeyValuePair<string, string> item in menu.Value) {
                //        Console.WriteLine("*" + item.Key + " -> " + item.Value);
                //    }
                //}

                //Console.WriteLine("------");

                foreach (GeoMap map in mGeoMapList) {
                    if (checkedFilters.Contains(map.FilterMenuName)) {
                        var geoMap = new GeoMap
                        {
                            Name = map.Name,
                            BcgMenuName = map.BcgMenuName,
                            FilterMenuName = map.FilterMenuName,
                            LabelLine1 = map.LabelLine1,
                            LabelLine2 = map.LabelLine2
                        };

                        foreach (var mapObj in map.Objects.Where(a => a.Filters2.Any(item => checkedFilterGroups.Contains(item)))) {
                            if (mapObj.LineDefaults != null) {
                                if (!string.IsNullOrEmpty(mapObj.LineDefaults.Bcg)) {
                                    var newbcg = BcgTranslations[map.BcgMenuName][mapObj.LineDefaults.Bcg];
                                    mapObj.LineDefaults.Bcg = newbcg;
                                    mUsedBcgGroups.Add(newbcg);
                                }
                                if (!string.IsNullOrEmpty(mapObj.LineDefaults.Filters)) {
                                    var newfilter = FilterTranslations[map.FilterMenuName][mapObj.LineDefaults.Filters];
                                    mapObj.LineDefaults.Filters = newfilter;
                                    mUsedFilterGroups.Add(newfilter);
                                }
                            }
                            if (mapObj.SymbolDefaults != null) {
                                if (!string.IsNullOrEmpty(mapObj.SymbolDefaults.Bcg)) {
                                    var newbcg = BcgTranslations[map.BcgMenuName][mapObj.SymbolDefaults.Bcg];
                                    mapObj.SymbolDefaults.Bcg = newbcg;
                                    mUsedBcgGroups.Add(newbcg);
                                }
                                if (!string.IsNullOrEmpty(mapObj.SymbolDefaults.Filters)) {
                                    var newfilter = FilterTranslations[map.FilterMenuName][mapObj.SymbolDefaults.Filters];
                                    mapObj.SymbolDefaults.Filters = newfilter;
                                    mUsedFilterGroups.Add(newfilter);
                                }
                            }
                            if (mapObj.TextDefaults != null) {
                                if (!string.IsNullOrEmpty(mapObj.TextDefaults.Bcg)) {
                                    var newbcg = BcgTranslations[map.BcgMenuName][mapObj.TextDefaults.Bcg];
                                    mapObj.TextDefaults.Bcg = newbcg;
                                    mUsedBcgGroups.Add(newbcg);
                                }
                                if (!string.IsNullOrEmpty(mapObj.TextDefaults.Filters)) {
                                    var newfilter = FilterTranslations[map.FilterMenuName][mapObj.TextDefaults.Filters];
                                    mapObj.TextDefaults.Filters = newfilter;
                                    mUsedFilterGroups.Add(newfilter);
                                }
                            }
                            geoMap.Objects.Add(mapObj);
                            //Console.WriteLine(">" + mapObj.Description + " Filters: " + string.Join(",", mapObj.Filters2));
                        }

                        mGeoMapSet.GeoMaps.Add(geoMap);
                    }
                }

                foreach (BcgMenu menu in mBcgMenuList) {
                    if (checkedFilters.Contains(menu.Name)) {
                        foreach (var item in menu.Items) {
                            if (mUsedBcgGroups.Contains(item.MenuPosition.ToString())) {
                                if (!mGeoMapSet.BcgMenus.Contains(menu)) {
                                    mGeoMapSet.BcgMenus.Add(menu);
                                }
                            }
                        }
                    }
                }

                foreach (FilterMenu menu in mFilterMenuList) {
                    if (checkedFilters.Contains(menu.Name)) {
                        foreach (var item in menu.Items) {
                            if (mUsedFilterGroups.Contains(item.MenuPosition.ToString())) {
                                if (!mGeoMapSet.FilterMenus.Contains(menu)) {
                                    mGeoMapSet.FilterMenus.Add(menu);
                                }
                            }
                        }
                    }
                }

                SerializationUtils.SerializeObjectToFile(mGeoMapSet, dlgSaveMaps.FileName);
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
            if (e.Action != TreeViewAction.Unknown) {
                if (e.Node.Nodes.Count > 0) {
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
            foreach (TreeNode node in treeNode.Nodes) {
                node.Checked = nodeChecked;
                if (node.Nodes.Count > 0) {
                    this.CheckAllChildNodes(node, nodeChecked);
                }
            }
        }

        private void BwLoadConsoleCommand_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var obj = SerializationUtils.DeserializeObjectFromFile<ConsoleCommandControlRecords>(dlgConsoleCommand.FileName);

            // iterator through map brightness menus
            foreach (MapBrightnessMenu mapBrightnessMenu in obj.MapBrightnessMenus) {
                BcgMenu bcgMenu = new BcgMenu
                {
                    Name = mapBrightnessMenu.BcgMenuName
                };

                if (!BcgTranslations.ContainsKey(mapBrightnessMenu.BcgMenuName)) {
                    BcgTranslations.Add(mapBrightnessMenu.BcgMenuName, new Dictionary<string, string>());
                }

                int i = 1;
                foreach (MapBcgButton btn in mapBrightnessMenu.MapBcgButtons) {
                    bcgMenu.Items.Add(new BcgMenuItem
                    {
                        Label = btn.Label,
                        MenuPosition = btn.MenuPosition,
                        BcgGroups = btn.MapBcgGroups.ToList()
                    });

                    if (BcgTranslations.ContainsKey(mapBrightnessMenu.BcgMenuName)) {
                        BcgTranslations[mapBrightnessMenu.BcgMenuName].Add(string.Join(",", btn.MapBcgGroups), btn.MenuPosition.ToString());
                    }

                    i++;
                }

                mBcgMenuList.Add(bcgMenu);
            }

            // iterate through map filter menus
            foreach (MapFilterMenu mapFilterMenu in obj.MapFilterMenus) {
                FilterMenu filterMenu = new FilterMenu
                {
                    Name = mapFilterMenu.FilterMenuName
                };

                if (!FilterTranslations.ContainsKey(mapFilterMenu.FilterMenuName)) {
                    FilterTranslations.Add(mapFilterMenu.FilterMenuName, new Dictionary<string, string>());
                }

                string key = mapFilterMenu.FilterMenuName;
                mSyncContext.Post(n => {
                    if (!myTreeView.Nodes.ContainsKey(key)) {
                        myTreeView.Nodes.Add(key, key);
                    }
                }, null);

                int i = 0;
                foreach (MapFilterButton btn in mapFilterMenu.MapFilterButtons) {
                    var item = new FilterMenuItem
                    {
                        LabelLine1 = btn.LabelLine1,
                        LabelLine2 = btn.LabelLine2,
                        MenuPosition = btn.MenuPosition,
                        FilterGroups = btn.MapFilterGroups.ToList()
                    };
                    filterMenu.Items.Add(item);

                    if (FilterTranslations.ContainsKey(mapFilterMenu.FilterMenuName)) {
                        FilterTranslations[mapFilterMenu.FilterMenuName].Add(string.Join(",", btn.MapFilterGroups), btn.MenuPosition.ToString());
                    }

                    mSyncContext.Post(n => {
                        TreeNode tn = new TreeNode(btn.FilterButtonName)
                        {
                            Tag = string.Join(",", item.FilterGroups)
                        };
                        myTreeView.Nodes[key].Nodes.Add(tn);
                    }, null);

                    i++;
                }

                mFilterMenuList.Add(filterMenu);
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

            foreach (GeoMapRecord record in obj.GeoMapRecordList) {
                if (i == 0) {
                    mDefaultMap = record.GeomapId;
                }

                var map = new GeoMap
                {
                    Name = record.GeomapId,
                    LabelLine1 = record.LabelLine1,
                    LabelLine2 = record.LabelLine2,
                    BcgMenuName = record.BcgMenuName,
                    FilterMenuName = record.FilterMenuName
                };

                foreach (GeoRam.GeoMap.GeoMapObject geo in record.GeoMapObjectList) {

                    mGeoLines.Clear();
                    mGeoSymbols.Clear();
                    mGeoTexts.Clear();

                    SymbolDefaults symbolDefaults = null;
                    TextDefaults textDefaults = null;
                    LineDefaults lineDefaults = null;

                    if (geo.MapObjectType == GeoRam.GeoMap.Enums.ObjectTypes.SAA) {
                        continue;
                    }

                    if (geo.DefaultSymbolProperties != null) {
                        symbolDefaults = new SymbolDefaults();
                        if (!string.IsNullOrEmpty(geo.DefaultSymbolProperties.BcgGroup)) {
                            symbolDefaults.Bcg = geo.DefaultSymbolProperties.BcgGroup;
                        }
                        if (geo.DefaultSymbolProperties.GeoSymbolFilters != null && geo.DefaultSymbolProperties.GeoSymbolFilters.Length > 0) {
                            symbolDefaults.Filters = string.Join(",", geo.DefaultSymbolProperties.GeoSymbolFilters);
                        }
                        if (!string.IsNullOrEmpty(geo.DefaultSymbolProperties.FontSize)) {
                            symbolDefaults.Size = geo.DefaultSymbolProperties.FontSize;
                        }
                        symbolDefaults.Style = geo.DefaultSymbolProperties.SymbolStyle;
                    }

                    if (geo.DefaultLineProperties != null) {
                        lineDefaults = new LineDefaults();
                        if (!string.IsNullOrEmpty(geo.DefaultLineProperties.BcgGroup)) {
                            lineDefaults.Bcg = geo.DefaultLineProperties.BcgGroup;
                        }
                        if (geo.DefaultLineProperties.GeoLineFilters != null && geo.DefaultLineProperties.GeoLineFilters.Length > 0) {
                            lineDefaults.Filters = string.Join(",", geo.DefaultLineProperties.GeoLineFilters);
                        }
                        if (!string.IsNullOrEmpty(geo.DefaultLineProperties.Thickness)) {
                            lineDefaults.Thickness = geo.DefaultLineProperties.Thickness;
                        }
                        lineDefaults.Style = geo.DefaultLineProperties.LineStyle;
                    }

                    if (geo.TextDefaultProperties != null) {
                        textDefaults = new TextDefaults();
                        if (!string.IsNullOrEmpty(geo.TextDefaultProperties.BcgGroup)) {
                            textDefaults.Bcg = geo.TextDefaultProperties.BcgGroup;
                        }
                        if (geo.TextDefaultProperties.GeoTextFilters != null && geo.TextDefaultProperties.GeoTextFilters.Length > 0) {
                            textDefaults.Filters = string.Join(",", geo.TextDefaultProperties.GeoTextFilters);
                        }
                        textDefaults.Size = geo.TextDefaultProperties.FontSize;
                        textDefaults.Underline = geo.TextDefaultProperties.Underline;
                        textDefaults.XOffset = geo.TextDefaultProperties.XPixelOffset;
                        textDefaults.YOffset = geo.TextDefaultProperties.YPixelOffset;
                    }

                    foreach (GeoMapLine line in geo.GeoMapLineList) {
                        mGeoLines.Add(new Line
                        {
                            StartLat = StringUtils.ToDecimalDegrees(line.StartLatitude),
                            StartLon = StringUtils.ToDecimalDegrees(line.StartLongitude, false),
                            EndLat = StringUtils.ToDecimalDegrees(line.EndLatitude),
                            EndLon = StringUtils.ToDecimalDegrees(line.EndLongitude, false)
                        });
                    }

                    foreach (GeoMapSymbol symbol in geo.GeoMapSymbolList) {
                        mGeoSymbols.Add(new Symbol
                        {
                            Lat = StringUtils.ToDecimalDegrees(symbol.Latitude),
                            Lon = StringUtils.ToDecimalDegrees(symbol.Longitude, false)
                        });
                    }

                    foreach (GeoMapText text in geo.GeoMapTextList) {
                        mGeoTexts.Add(new Vatsim.Text
                        {
                            Lat = StringUtils.ToDecimalDegrees(text.Latitude),
                            Lon = StringUtils.ToDecimalDegrees(text.Longitude, false),
                            Lines = string.Join(" ", text.GeoTextStrings)
                        });
                    }

                    map.Objects.Add(new Vatsim.GeoMapObject
                    {
                        Description = $"Object #{ geo.MapGroupId } ({ geo.MapObjectType })",
                        SymbolDefaults = symbolDefaults,
                        LineDefaults = lineDefaults,
                        TextDefaults = textDefaults,
                        Elements = mGeoLines.Cast<Element>().Union(mGeoTexts.Cast<Element>()).Union(mGeoSymbols.Cast<Element>()).ToList()
                    });
                }

                mGeoMapList.Add(map);
                i++;
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