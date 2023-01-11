using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeoMapConverter.GeoRam.ConsoleCommand;
using GeoMapConverter.GeoRam.GeoMap;
using GeoMapConverter.Utils;
using GeoMapConverter.Vatsim;

namespace GeoMapConverter
{
    public partial class MainForm : Form
    {
        private readonly SynchronizationContext mContext;
        private readonly List<Vatsim.BcgMenu> mVatsimBcgMenus = new List<BcgMenu>();
        private readonly List<Vatsim.FilterMenu> mVatsimFilterMenus = new List<FilterMenu>();
        private Vatsim.GeoMapSet mVatsimGeoMapSet = new GeoMapSet();
        private GeoMapRecords mGeoMaps = new GeoMapRecords();

        public MainForm()
        {
            InitializeComponent();

            mContext = SynchronizationContext.Current;
        }

        private async void BtnOpenGeoMaps_Click(object sender, EventArgs e)
        {
            dlgGeoMaps.Filter = "Geomaps.xml|Geomaps.xml|All Files (*.*)|*.*";

            if (dlgGeoMaps.ShowDialog(this) == DialogResult.OK)
            {
                btnOpenGeoMaps.Enabled = false;
                btnOpenConsoleCommand.Enabled = false;
                btnExport.Enabled = false;
                myTreeView.Enabled = false;

                mGeoMaps = new GeoMapRecords();
                mVatsimGeoMapSet = new GeoMapSet();
                mVatsimBcgMenus.Clear();
                mVatsimFilterMenus.Clear();
                myTreeView.Nodes.Clear();

                txtStatus.Visible = true;
                txtStatus.Text = "Loading GeoMaps...";
                btnOpenGeoMaps.Enabled = false;

                await Task.Run(ProcessGeoMapsFile);

                txtStatus.Text = "GeoMaps loaded. Please open the ConsoleCommandControl XML file.";
                btnOpenGeoMaps.Enabled = false;
                btnOpenConsoleCommand.Enabled = true;
            }
        }

        private async void btnOpenConsoleCommand_Click(object sender, EventArgs e)
        {
            dlgConsoleCommand.Filter = "ConsoleCommandControl.xml|ConsoleCommandControl.xml|All Files (*.*)|*.*";

            if (dlgConsoleCommand.ShowDialog(this) == DialogResult.OK)
            {
                btnOpenConsoleCommand.Enabled = false;
                btnOpenGeoMaps.Enabled = false;
                btnExport.Enabled = false;

                await Task.Run(ProcessConsoleCommandFile);

                btnOpenGeoMaps.Enabled = true;
                btnOpenConsoleCommand.Enabled = false;
                myTreeView.Enabled = true;
                btnExport.Enabled = true;
                txtStatus.Text = "ConsoleCommandControl loaded. Please select GeoMaps to export.";
            }
        }

        private async void BtnExport_Click(object sender, EventArgs e)
        {
            txtStatus.Text = "Processing... Please wait";
            btnExport.Enabled = false;

            var filtered = await Task.Run(CreateExportFile);

            txtStatus.Text = "GeoMap conversion finished successfully.";
            btnExport.Enabled = true;

            dlgSaveMaps.InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Path.Combine("vERAM", "GeoMaps"));

            if (dlgSaveMaps.ShowDialog(this) == DialogResult.OK)
            {
                SerializationUtils.SerializeObjectToFile(filtered, dlgSaveMaps.FileName);
            }
        }

        private void ProcessConsoleCommandFile()
        {
            var obj = SerializationUtils.DeserializeObjectFromFile<ConsoleCommandControlRecords>(dlgConsoleCommand.FileName);

            obj.MapBrightnessMenus.ForEach(menu =>
            {
                var bcg = new Vatsim.BcgMenu
                {
                    Name = menu.BcgMenuName
                };

                var id = 1;
                menu.MapBcgButtons.ForEach(button =>
                {
                    bcg.Items.Add(new BcgMenuItem
                    {
                        Label = button.Label,
                        MenuPosition = button.MenuPosition,
                        BcgGroups = button.MapBcgGroups.BcgGroup,
                        LocalId = id++
                    });
                });

                mVatsimBcgMenus.Add(bcg);
            });

            obj.MapFilterMenus.ForEach(menu =>
            {
                var filter = new Vatsim.FilterMenu()
                {
                    Name = menu.FilterMenuName
                };

                var id = 1;
                foreach (var mapFilterButton in menu.MapFilterButtons)
                {
                    filter.Items.Add(new FilterMenuItem()
                    {
                        LabelLine1 = mapFilterButton.LabelLine1,
                        LabelLine2 = mapFilterButton.LabelLine2,
                        MenuPosition = mapFilterButton.MenuPosition,
                        FilterGroup = mapFilterButton.MapFilterGroup.Group,
                        LocalId = id++
                    });

                    mContext.Post(t =>
                    {
                        var node = new TreeNode(mapFilterButton.FilterButtonName);
                        node.Tag = mapFilterButton;
                        myTreeView.Nodes[filter.Name].Nodes.Add(node);
                    }, null);
                }

                mVatsimFilterMenus.Add(filter);
            });

            mVatsimGeoMapSet.BcgMenus = mVatsimBcgMenus;
            mVatsimGeoMapSet.FilterMenus = mVatsimFilterMenus;
        }

        private void ProcessGeoMapsFile()
        {
            mGeoMaps = SerializationUtils.DeserializeObjectFromFile<GeoMapRecords>(dlgGeoMaps.FileName);

            mGeoMaps.GeoMapRecordList.ForEach(map =>
            {
                mContext.Post(t =>
                {
                    if (!myTreeView.Nodes.ContainsKey(map.FilterMenuName))
                    {
                        var node = myTreeView.Nodes.Add(map.FilterMenuName, map.GeomapId);
                        node.Tag = map;
                    }
                }, null);
            });
        }

        private GeoMapSet CreateExportFile()
        {
            var filtered = new GeoMapSet();

            var x = myTreeView.Nodes.Cast<TreeNode>()
                .Sum(node => node.Nodes.Cast<TreeNode>().Count(child => child.Checked));

            if (x == 0) return null;

            foreach (TreeNode node in myTreeView.Nodes)
            {
                if (node.Checked)
                {
                    var key = node.Text;
                    var tag = node.Tag as GeoMapRecord;

                    filtered.BcgMenus.AddRange(mVatsimBcgMenus.Where(m => m.Name == tag.BcgMenuName));
                    filtered.FilterMenus.AddRange(mVatsimFilterMenus.Where(m => m.Name == tag.FilterMenuName));

                    var checkedFilterGroups = new List<MapFilterButton>();

                    foreach (TreeNode child in node.Nodes)
                    {
                        if (child.Checked)
                        {
                            var mapTag = child.Tag as MapFilterButton;
                            checkedFilterGroups.Add(mapTag);
                        }
                    }

                    var geoMap = new Vatsim.GeoMap
                    {
                        Name = tag.GeomapId,
                        LabelLine1 = tag.LabelLine1,
                        LabelLine2 = tag.LabelLine2,
                        BcgMenuName = tag.BcgMenuName,
                        FilterMenuName = tag.FilterMenuName
                    };

                    foreach (var obj in tag.GeoMapObjectList)
                    {
                        var mapObj = new Vatsim.GeoMapObject();

                        if (obj.DefaultLineProperties != null)
                        {
                            var bcgGroup = obj.DefaultLineProperties.BcgGroup;
                            var filterGroups = obj.DefaultLineProperties.GeoLineFilters.Select(m => m.Id);

                            bool isUseableObject = false;
                            foreach (var filter in filterGroups)
                            {
                                if (checkedFilterGroups.Select(m => m.MapFilterGroup.Group).Contains(filter))
                                    isUseableObject = true;
                            }

                            if (isUseableObject)
                            {
                                mapObj.LineDefaults = new LineDefaults
                                {
                                    Style = obj.DefaultLineProperties.LineStyle,
                                    Thickness = obj.DefaultLineProperties.Thickness
                                };

                                obj.DefaultLineProperties.GeoLineFilters.ForEach(filter =>
                                {
                                    var filterMenu = mVatsimFilterMenus.SelectMany(t => t.Items)
                                        .FirstOrDefault(t => t.FilterGroup == filter.Id);
                                    mapObj.LineDefaults.FilterList.Add(filterMenu?.LocalId ?? filter.Id);
                                });

                                obj.GeoMapLineList.ForEach(geoLine =>
                                {
                                    var line = new Vatsim.Line
                                    {
                                        BcgGroup = bcgGroup,
                                        StartLat = geoLine.StartLatitude.ToDecimalDegrees(),
                                        StartLon = geoLine.StartLongitude.ToDecimalDegrees(false),
                                        EndLat = geoLine.EndLatitude.ToDecimalDegrees(),
                                        EndLon = geoLine.EndLongitude.ToDecimalDegrees(false)
                                    };
                                    mapObj.Elements.Add(line);
                                });

                                var bcgMenu = mVatsimBcgMenus.SelectMany(t => t.Items)
                                    .FirstOrDefault(t => t.BcgGroups == obj.DefaultLineProperties.BcgGroup);
                                mapObj.LineDefaults.BcgGroup = bcgMenu?.LocalId ?? obj.DefaultLineProperties.BcgGroup;
                            }
                        }

                        if (obj.DefaultSymbolProperties != null)
                        {
                            var filterGroups = obj.DefaultSymbolProperties.GeoSymbolFilters.Select(m => m.Id);

                            bool isUsableObject = false;
                            foreach (var filter in filterGroups)
                            {
                                if (checkedFilterGroups.Select(m => m.MapFilterGroup.Group).Contains(filter))
                                    isUsableObject = true;
                            }

                            if (isUsableObject)
                            {
                                mapObj.SymbolDefaults = new Vatsim.SymbolDefaults
                                {
                                    Style = obj.DefaultSymbolProperties.SymbolStyle,
                                    Size = obj.DefaultSymbolProperties.FontSize
                                };

                                if (mapObj.SymbolDefaults.Style == SymbolStyle.DME)
                                    mapObj.SymbolDefaults.Style = SymbolStyle.TACAN;

                                obj.DefaultSymbolProperties.GeoSymbolFilters.ForEach(filter =>
                                {
                                    var filterMenu = mVatsimFilterMenus.SelectMany(t => t.Items)
                                        .FirstOrDefault(t => t.FilterGroup == filter.Id);
                                    mapObj.SymbolDefaults.FilterList.Add(filterMenu?.LocalId ?? filter.Id);
                                });

                                obj.GeoMapSymbolList.ForEach(geoSymbol =>
                                {
                                    if (geoSymbol.GeoMapText != null)
                                    {
                                        var offset = 0;
                                        foreach (var text in geoSymbol.GeoMapText.GeoTextStrings
                                            .Select(line => new Vatsim.Text
                                        {
                                            Lat = geoSymbol.Latitude.ToDecimalDegrees(),
                                            Lon = geoSymbol.Longitude.ToDecimalDegrees(false),
                                            Lines = line,
                                            Size = geoSymbol.FontSize,
                                            YOffset = offset += (5 * geoSymbol.FontSize)
                                        }))
                                        {
                                            mapObj.Elements.Add(text);
                                        }
                                    }

                                    var symbol = new Vatsim.Symbol
                                    {
                                        Lat = geoSymbol.Latitude.ToDecimalDegrees(),
                                        Lon = geoSymbol.Longitude.ToDecimalDegrees(false),
                                        Size = geoSymbol.FontSize,
                                        Style = (SymbolStyle)geoSymbol.SymbolStyle
                                    };

                                    if (symbol.Style == SymbolStyle.DME)
                                        symbol.Style = SymbolStyle.TACAN;

                                    mapObj.Elements.Add(symbol);
                                });

                                var bcgMenu = mVatsimBcgMenus.SelectMany(t => t.Items)
                                    .FirstOrDefault(t => t.BcgGroups == obj.DefaultSymbolProperties.BcgGroup);
                                mapObj.SymbolDefaults.BcgGroup = bcgMenu?.LocalId ?? obj.DefaultSymbolProperties.BcgGroup;
                            }
                        }

                        if (obj.TextDefaultProperties != null)
                        {
                            var filterGroups = obj.TextDefaultProperties.GeoTextFilters.Select(m => m.Id);

                            bool isUsableObject = false;
                            foreach (var filter in filterGroups)
                            {
                                if (checkedFilterGroups.Select(m => m.MapFilterGroup.Group).Contains(filter))
                                    isUsableObject = true;
                            }

                            if (isUsableObject)
                            {
                                mapObj.TextDefaults = new Vatsim.TextDefaults
                                {
                                    Opaque = false,
                                    Size = obj.TextDefaultProperties.FontSize,
                                    Underline = obj.TextDefaultProperties.Underline,
                                    XOffset = obj.TextDefaultProperties.XPixelOffset,
                                    YOffset = obj.TextDefaultProperties.YPixelOffset
                                };

                                obj.TextDefaultProperties.GeoTextFilters.ForEach(filter =>
                                {
                                    var filterMenu = mVatsimFilterMenus.SelectMany(t => t.Items)
                                        .FirstOrDefault(t => t.FilterGroup == filter.Id);
                                    mapObj.TextDefaults.FilterList.Add(filterMenu?.LocalId ?? filter.Id);
                                });

                                obj.GeoMapTextList.ForEach(geoText =>
                                {
                                    foreach (var text in geoText.GeoTextStrings.Select(line => new Vatsim.Text
                                    {
                                        Lat = geoText.Latitude.ToDecimalDegrees(),
                                        Lon = geoText.Longitude.ToDecimalDegrees(false),
                                        Lines = line,
                                        Underline = geoText.Underline,
                                        Size = geoText.FontSize
                                    }))
                                    {
                                        mapObj.Elements.Add(text);
                                    }
                                });

                                var bcgMenu = mVatsimBcgMenus.SelectMany(t => t.Items)
                                    .FirstOrDefault(t => t.BcgGroups == obj.TextDefaultProperties.BcgGroup);
                                mapObj.TextDefaults.BcgGroup = bcgMenu?.LocalId ?? obj.TextDefaultProperties.BcgGroup;
                            }
                        }

                        mapObj.Description = $"Object #{obj.MapGroupId} ({obj.MapObjectType})";

                        if (mapObj.HasElements)
                        {
                            geoMap.Objects.Add(mapObj);
                        }
                    }

                    mVatsimGeoMapSet.GeoMaps.Add(geoMap);
                }
            }

            filtered.GeoMaps = mVatsimGeoMapSet.GeoMaps;
            return filtered;
        }

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

        private static void SelectParents(TreeNode node, bool isChecked)
        {
            while (true)
            {
                var parent = node.Parent;

                if (parent == null)
                {
                    return;
                }

                if (!isChecked && HasCheckedNode(parent))
                {
                    return;
                }

                parent.Checked = isChecked;
                node = parent;
            }
        }

        private static bool HasCheckedNode(TreeNode node)
        {
            return node.Nodes.Cast<TreeNode>().Any(n => n.Checked);
        }

        private static void CheckAllChildNodes(TreeNode treeNode, bool nodeChecked)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                node.Checked = nodeChecked;
                if (node.Nodes.Count > 0)
                {
                    CheckAllChildNodes(node, nodeChecked);
                }
            }
        }
    }
}