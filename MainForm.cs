using System;
using System.Collections.Generic;
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
        private readonly List<Vatsim.BcgMenu> mBcgMenus = new List<BcgMenu>();
        private readonly List<Vatsim.FilterMenu> mFilterMenus = new List<FilterMenu>();
        private readonly Vatsim.GeoMapSet mGeoMapSet = new GeoMapSet();

        public MainForm()
        {
            InitializeComponent();

            mContext = SynchronizationContext.Current;
        }

        private async void btnOpenConsoleCommand_Click(object sender, EventArgs e)
        {
            if (dlgConsoleCommand.ShowDialog(this) == DialogResult.OK)
            {
                btnOpenConsoleCommand.Enabled = false;
                btnOpenGeoMaps.Enabled = false;
                btnExport.Enabled = false;

                mBcgMenus.Clear();
                mFilterMenus.Clear();
                myTreeView.Nodes.Clear();

                await Task.Run(ProcessConsoleCommandFile);

                btnOpenGeoMaps.Enabled = true;
                btnOpenConsoleCommand.Enabled = true;
            }
        }

        private async void BtnOpenGeoMaps_Click(object sender, EventArgs e)
        {
            if (dlgGeoMaps.ShowDialog(this) == DialogResult.OK)
            {
                btnExport.Enabled = false;
                btnExport.Text = "Loading GeoMaps...";
                btnOpenConsoleCommand.Enabled = false;
                btnOpenGeoMaps.Enabled = false;

                await Task.Run(ProcessGeoMapsFile);

                btnExport.Text = "Export Selected GeoMaps";
                btnExport.Enabled = true;
                btnOpenGeoMaps.Enabled = true;
                btnOpenConsoleCommand.Enabled = true;
            }
        }

        private async void BtnExport_Click(object sender, EventArgs e)
        {
            btnExport.Text = "Processing... Please Wait";
            btnExport.Enabled = false;

            var filtered = await Task.Run(CreateExportFile);

            btnExport.Text = "Export Selected GeoMaps";
            btnExport.Enabled = true;

            if (dlgSaveMaps.ShowDialog(this) == DialogResult.OK)
            {
                SerializationUtils.SerializeObjectToFile(filtered, dlgSaveMaps.FileName);
            }
        }

        private void ProcessConsoleCommandFile()
        {
            var obj =
                SerializationUtils.DeserializeObjectFromFile<ConsoleCommandControlRecords>(dlgConsoleCommand
                    .FileName);

            obj.MapBrightnessMenus.ForEach(menu =>
            {
                var bcg = new Vatsim.BcgMenu
                {
                    Name = menu.BcgMenuName
                };

                var id = 1;
                foreach (var btn in menu.MapBcgButtons)
                {
                    bcg.Items.Add(new Vatsim.BcgMenuItem()
                    {
                        Label = btn.Label,
                        MenuPosition = btn.MenuPosition,
                        BcgGroups = btn.MapBcgGroups.BcgGroup,
                        OurId = id++
                    });
                }

                mBcgMenus.Add(bcg);
            });

            obj.MapFilterMenus.ForEach(menu =>
            {
                var filter = new Vatsim.FilterMenu()
                {
                    Name = menu.FilterMenuName
                };

                mContext.Post(t =>
                {
                    if (!myTreeView.Nodes.ContainsKey(filter.Name))
                    {
                        myTreeView.Nodes.Add(filter.Name, filter.Name);
                    }
                }, null);

                var id = 1;
                foreach (var btn in menu.MapFilterButtons)
                {
                    filter.Items.Add(new FilterMenuItem()
                    {
                        LabelLine1 = btn.LabelLine1,
                        LabelLine2 = btn.LabelLine2,
                        MenuPosition = btn.MenuPosition,
                        FilterGroup = btn.MapFilterGroup.Group,
                        OurId = id++
                    });

                    mContext.Post(t =>
                    {
                        var node = new TreeNode(btn.FilterButtonName)
                        {
                            Tag = btn.MapFilterGroup.Group
                        };
                        myTreeView.Nodes[filter.Name].Nodes.Add(node);
                    }, null);
                }

                mFilterMenus.Add(filter);
            });
        }

        private void ProcessGeoMapsFile()
        {
            var xml = SerializationUtils.DeserializeObjectFromFile<GeoMapRecords>(dlgGeoMaps.FileName);

            mGeoMapSet.BcgMenus = mBcgMenus;
            mGeoMapSet.FilterMenus = mFilterMenus;

            xml.GeoMapRecordList.ForEach(geomap =>
            {
                var map = new Vatsim.GeoMap
                {
                    Name = geomap.GeomapId,
                    LabelLine1 = geomap.LabelLine1,
                    LabelLine2 = geomap.LabelLine2,
                    BcgMenuName = geomap.BcgMenuName,
                    FilterMenuName = geomap.FilterMenuName
                };

                geomap.GeoMapObjectList.ForEach(obj =>
                {
                    var mapobj = new Vatsim.GeoMapObject();

                    if (obj.DefaultLineProperties != null)
                    {
                        mapobj.LineDefaults = new LineDefaults
                        {
                            Style = obj.DefaultLineProperties.LineStyle,
                            Thickness = obj.DefaultLineProperties.Thickness,
                            BcgGroup = obj.DefaultLineProperties.BcgGroup,
                            Filters = obj.DefaultLineProperties.GeoLineFilters.Id
                        };
                    }

                    if (obj.DefaultSymbolProperties != null)
                    {
                        mapobj.SymbolDefaults = new SymbolDefaults()
                        {
                            Style = obj.DefaultSymbolProperties.SymbolStyle,
                            Size = obj.DefaultSymbolProperties.FontSize,
                            BcgGroup = obj.DefaultSymbolProperties.BcgGroup,
                            Filters = obj.DefaultSymbolProperties.GeoSymbolFilters.Id
                        };
                    }

                    if (obj.TextDefaultProperties != null)
                    {
                        mapobj.TextDefaults = new TextDefaults()
                        {
                            Opaque = false,
                            Size = obj.TextDefaultProperties.FontSize,
                            Underline = obj.TextDefaultProperties.Underline,
                            XOffset = obj.TextDefaultProperties.XPixelOffset,
                            YOffset = obj.TextDefaultProperties.YPixelOffset,
                            BcgGroup = obj.TextDefaultProperties.BcgGroup,
                            Filters = obj.TextDefaultProperties.GeoTextFilters.Id
                        };
                    }

                    obj.GeoMapSymbolList.ForEach(geosymbol =>
                    {
                        if (geosymbol.GeoMapText != null)
                        {
                            var offset = 0;
                            foreach (var text in geosymbol.GeoMapText.GeoTextStrings.Select(line => new Vatsim.Text
                            {
                                Lat = geosymbol.Latitude.ToDecimalDegrees(),
                                Lon = geosymbol.Longitude.ToDecimalDegrees(false),
                                Lines = line,
                                Size = geosymbol.FontSize,
                                YOffset = offset += (5 * geosymbol.FontSize)
                            }))
                            {
                                mapobj.Elements.Add(text);
                            }
                        }

                        var symbol = new Vatsim.Symbol()
                        {
                            Lat = geosymbol.Latitude.ToDecimalDegrees(),
                            Lon = geosymbol.Longitude.ToDecimalDegrees(false),
                            Size = geosymbol.FontSize,
                            Style = (SymbolStyle) geosymbol.SymbolStyle
                        };
                        mapobj.Elements.Add(symbol);
                    });

                    obj.GeoMapLineList.ForEach(geoline =>
                    {
                        var line = new Vatsim.Line
                        {
                            StartLat = geoline.StartLatitude.ToDecimalDegrees(),
                            StartLon = geoline.StartLongitude.ToDecimalDegrees(false),
                            EndLat = geoline.EndLatitude.ToDecimalDegrees(),
                            EndLon = geoline.EndLongitude.ToDecimalDegrees(false)
                        };
                        mapobj.Elements.Add(line);
                    });

                    obj.GeoMapTextList.ForEach(geotext =>
                    {
                        foreach (var text in geotext.GeoTextStrings.Select(line => new Vatsim.Text
                        {
                            Lat = geotext.Latitude.ToDecimalDegrees(),
                            Lon = geotext.Longitude.ToDecimalDegrees(false),
                            Lines = line,
                            Underline = geotext.Underline,
                            Size = geotext.FontSize
                        }))
                        {
                            mapobj.Elements.Add(text);
                        }
                    });

                    mapobj.Description = $"Object #{obj.MapGroupId} ({obj.MapObjectType})";

                    map.Objects.Add(mapobj);
                });

                mGeoMapSet.GeoMaps.Add(map);
            });
        }

        private GeoMapSet CreateExportFile()
        {
            var filtered = new GeoMapSet();

            var checkedMaps = new Dictionary<string, List<string>>();

            var x = myTreeView.Nodes.Cast<TreeNode>()
                .Sum(node => node.Nodes.Cast<TreeNode>().Count(child => child.Checked));

            if (x == 0) return null;

            foreach (TreeNode node in myTreeView.Nodes)
            {
                var key = node.Text;
                if (node.Checked)
                {
                    checkedMaps.Add(key, new List<string>());
                    foreach (TreeNode child in node.Nodes)
                    {
                        if (child.Checked)
                        {
                            checkedMaps[key].Add(child.Tag.ToString());
                        }
                    }
                }
            }

            var clone = mGeoMapSet.DeepClone();

            foreach (var chk in checkedMaps)
            {
                filtered.BcgMenus.AddRange(clone.BcgMenus.Where(t => t.Name == chk.Key));
                filtered.FilterMenus.AddRange(clone.FilterMenus.Where(t => t.Name == chk.Key));

                var maps = clone.GeoMaps.Where(t => t.FilterMenuName == chk.Key);
                foreach (var map in maps)
                {
                    var filteredMap = new GeoMap
                    {
                        Name = map.Name,
                        BcgGroup = map.BcgGroup,
                        BcgMenuName = map.BcgMenuName,
                        FilterMenuName = map.FilterMenuName,
                        LabelLine1 = map.LabelLine1,
                        LabelLine2 = map.LabelLine2,
                        Objects = new List<Vatsim.GeoMapObject>()
                    };

                    foreach (var obj in map.Objects)
                    {
                        if (chk.Value.Contains(obj.Filter))
                        {
                            var mapObj = new Vatsim.GeoMapObject()
                            {
                                Description = obj.Description,
                                TdmOnly = obj.TdmOnly,
                            };

                            if (obj.TextDefaults != null)
                            {
                                if (chk.Value.Contains(obj.TextDefaults.Filters.ToString()))
                                {
                                    mapObj.TextDefaults = obj.TextDefaults;
                                }
                            }

                            if (obj.SymbolDefaults != null)
                            {
                                if (chk.Value.Contains(obj.SymbolDefaults.Filters.ToString()))
                                {
                                    mapObj.SymbolDefaults = obj.SymbolDefaults;
                                }
                            }

                            if (obj.LineDefaults != null)
                            {
                                if (chk.Value.Contains(obj.LineDefaults.Filters.ToString()))
                                {
                                    mapObj.LineDefaults = obj.LineDefaults;
                                }
                            }

                            foreach (Element elem in obj.Elements)
                            {
                                if (mapObj.LineDefaults != null && elem is Line)
                                {
                                    mapObj.Elements.Add(elem);
                                }

                                if (mapObj.TextDefaults != null && elem is Text)
                                {
                                    mapObj.Elements.Add(elem);
                                }

                                if (mapObj.SymbolDefaults != null && elem is Symbol)
                                {
                                    mapObj.Elements.Add(elem);
                                }
                            }

                            filteredMap.Objects.Add(mapObj);
                        }
                    }

                    if (filteredMap.Objects.Count > 0)
                    {
                        filtered.GeoMaps.Add(filteredMap);
                    }
                }
            }

            foreach (var map in filtered.GeoMaps)
            {
                foreach (var obj in map.Objects)
                {
                    if (obj.LineDefaults != null)
                    {
                        var newFilter = filtered.FilterMenus.Where(t => t.Name == map.FilterMenuName)
                            .SelectMany(t => t.Items).FirstOrDefault(t => t.FilterGroup == obj.LineDefaults.Filters);
                        if (newFilter != null)
                        {
                            obj.LineDefaults.Filters = newFilter.OurId;
                        }

                        var newBcgFilter = filtered.BcgMenus.Where(t => t.Name == map.BcgMenuName)
                            .SelectMany(t => t.Items).FirstOrDefault(t => t.BcgGroups == obj.LineDefaults.BcgGroup);
                        if (newBcgFilter != null)
                        {
                            obj.LineDefaults.BcgGroup = newBcgFilter.OurId;
                        }
                    }

                    if (obj.SymbolDefaults != null)
                    {
                        var newFilter = filtered.FilterMenus.Where(t => t.Name == map.FilterMenuName)
                            .SelectMany(t => t.Items).FirstOrDefault(t => t.FilterGroup == obj.SymbolDefaults.Filters);
                        if (newFilter != null)
                        {
                            obj.SymbolDefaults.Filters = newFilter.OurId;
                        }

                        var newBcgFilter = filtered.BcgMenus.Where(t => t.Name == map.BcgMenuName)
                            .SelectMany(t => t.Items).FirstOrDefault(t => t.BcgGroups == obj.SymbolDefaults.BcgGroup);
                        if (newBcgFilter != null)
                        {
                            obj.SymbolDefaults.BcgGroup = newBcgFilter.OurId;
                        }
                    }

                    if (obj.TextDefaults != null)
                    {
                        var newFilter = filtered.FilterMenus.Where(t => t.Name == map.FilterMenuName)
                            .SelectMany(t => t.Items).FirstOrDefault(t => t.FilterGroup == obj.TextDefaults.Filters);
                        if (newFilter != null)
                        {
                            obj.TextDefaults.Filters = newFilter.OurId;
                        }

                        var newBcgFilter = filtered.BcgMenus.Where(t => t.Name == map.BcgMenuName)
                            .SelectMany(t => t.Items).FirstOrDefault(t => t.BcgGroups == obj.TextDefaults.BcgGroup);
                        if (newBcgFilter != null)
                        {
                            obj.TextDefaults.BcgGroup = newBcgFilter.OurId;
                        }
                    }
                }
            }

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