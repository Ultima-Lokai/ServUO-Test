//....................................//
//...................................//
//....Originally created by Arya....//       
//....Updated by Lucid Nagual......//
//................................//
//...............................//

//Now saves item names and hues.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Commands;


namespace Server.Arya.Misc
{
    public class AddonGenerator
    {
        /// <summary>
        /// Set this value if you wish the scripts to be output somewhere else rather than in the default RunUO\TheBox
        /// directory. This should be a full valid path on your computer
        /// 
        /// Example:
        /// 
        /// private static string m_CustomOutputDirector = @"C:\Program Files\RunUO\Scripts\Custom\Addons";
        /// </summary>
        private static string m_CustomOutputDirectory = @"C:\RunUO\Addons Made with Addon Generator";

        #region Template

        private const string m_Template = @"using System;
using Server;
using Server.Items;

namespace {namespace}
{
    public class {name}Addon : BaseAddon
    {
        public override BaseAddonDeed Deed
        {
            get
            {
                return new {name}AddonDeed();
            }
        }

        [ Constructable ]
        public {name}Addon()
        {
{components}
        }

        public {name}Addon( Serial serial ) : base( serial )
        {
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );
            writer.Write( 0 ); // Version
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );
            int version = reader.ReadInt();
        }
    }

    public class {name}AddonDeed : BaseAddonDeed
    {
        public override BaseAddon Addon
        {
            get
            {
                return new {name}Addon();
            }
        }

        [Constructable]
        public {name}AddonDeed()
        {
            Name = ""{name}"";
        }

        public {name}AddonDeed( Serial serial ) : base( serial )
        {
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );
            writer.Write( 0 ); // Version
        }

        public override void    Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );
            int version = reader.ReadInt();
        }
    }
}";

        #endregion

        public static void Initialize()
        {
            CommandSystem.Register("AddonGen", AccessLevel.Administrator, new CommandEventHandler(OnAddonGen));
        }

        [Usage("AddonGen [<name> [namespace]]"),
        Description("Brings up the addon script generator gump. When used with the name (and eventually namespace) parameter generates an addon script from the targeted region.")]
        private static void OnAddonGen(CommandEventArgs e)
        {
            //
            // State object:
            // 0: Name
            // 1: Namespace (Server.Items)
            // 2: Items (true)
            // 3: Statics (false)
            // 4: Use range (false)
            // 5: Min Z (-128)
            // 6: Max Z (127)

            object[] state = new object[7];

            state[0] = "";
            state[1] = "Server.Items";
            state[2] = true;
            state[3] = false;
            state[4] = false;
            state[5] = -128;
            state[6] = 127;

            if (e.Arguments.Length > 0)
            {
                state[0] = e.Arguments[0];

                if (e.Arguments.Length > 1)
                {
                    state[1] = e.Arguments[1];
                }

                BoundingBoxPicker.Begin(e.Mobile, new BoundingBoxCallback(PickerCallback), state);
            }
            else
            {
                // Send gump
                e.Mobile.SendGump(new InternalGump(e.Mobile, state));
            }
        }

        private static void PickerCallback(Mobile from, Map map, Point3D start, Point3D end, object state)
        {
            object[] args = state as object[];

            if (start.X > end.X)
            {
                int x = start.X;
                start.X = end.X;
                end.X = x;
            }

            if (start.Y > end.Y)
            {
                int y = start.Y;
                start.Y = end.Y;
                end.Y = y;
            }

            Rectangle2D bounds = new Rectangle2D(start, end);

            string name = args[0] as string;
            string ns = args[1] as string;

            bool items = (bool)args[2];
            bool statics = (bool)args[3];
            bool range = (bool)args[4];

            sbyte min = sbyte.MinValue;
            sbyte max = sbyte.MaxValue;

            try { min = sbyte.Parse(args[5] as string); }
            catch { }
            try { max = sbyte.Parse(args[6] as string); }
            catch { }

            if (max < min)
            {
                sbyte temp = max;
                max = min;
                min = temp;
            }

            Hashtable tiles = new Hashtable();

            if (statics)
            {
                for (int x = start.X; x <= end.X; x++)
                {
                    for (int y = start.Y; y <= end.Y; y++)
                    {
                        List<StaticTile> list = GetTilesAt(map, new Point2D(x, y), items, false, statics);

                        if (range)
                        {
                            List<StaticTile> remove = new List<StaticTile>();

                            foreach (StaticTile t in list)
                            {
                                if (t.Z < min || t.Z > max)
                                    remove.Add(t);
                            }

                            foreach (StaticTile t in remove)
                                list.Remove(t);
                        }

                        if (list != null && list.Count > 0)
                        {
                            tiles[new Point2D(x, y)] = list;
                        }
                    }
                }
            }

            IPooledEnumerable en = map.GetItemsInBounds(bounds);
            List<Object> target = new List<Object>();
            bool fail = false;

            try
            {
                foreach (object o in en)
                {
                    Static s = o as Static;

                    if (s == null)
                        continue;

                    if (range && (s.Z < min || s.Z > max))
                        continue;

                    target.Add(o);
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.ToString());
                from.SendMessage(0x40, "The targeted items have been modified. Please retry.");
                fail = true;
            }
            finally
            {
                en.Free();
            }

            if (fail)
                return;

            if (target.Count == 0 && tiles.Keys.Count == 0)
            {
                from.SendMessage(0x40, "No items have been selected");
                return;
            }

            // Get center
            Point3D center = new Point3D();
            center.Z = 127;

            int x1 = bounds.End.X;
            int y1 = bounds.End.Y;
            int x2 = bounds.Start.X;
            int y2 = bounds.Start.Y;

            // Get correct bounds
            foreach (Static item in target)
            {
                if (item.Z < center.Z)
                {
                    center.Z = item.Z;
                }

                x1 = Math.Min(x1, item.X);
                y1 = Math.Min(y1, item.Y);
                x2 = Math.Max(x2, item.X);
                y2 = Math.Max(y2, item.Y);
            }

            foreach (Point2D p in tiles.Keys)
            {
                List<StaticTile> list = tiles[p] as List<StaticTile>;

                foreach (StaticTile t in list)
                {
                    if (t.Z < center.Z)
                    {
                        center.Z = t.Z;
                    }
                }

                x1 = Math.Min(x1, p.X);
                y1 = Math.Min(y1, p.Y);
                x2 = Math.Max(x2, p.X);
                y2 = Math.Max(y2, p.Y);
            }

            center.X = x1 + ((x2 - x1) / 2);
            center.Y = y1 + ((y2 - y1) / 2);

            // Build items
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            // Statics
            foreach (Point2D p in tiles.Keys)
            {
                List<StaticTile> list = tiles[p] as List<StaticTile>;

                int xOffset = p.X - center.X;
                int yOffset = p.Y - center.Y;

                foreach (StaticTile t in list)
                {
                    int zOffset = t.Z - center.Z;
                    int id = t.ID - 16384;

                    sb.AppendFormat("\t\t\tAddComponent( new AddonComponent( {0} ), {1}, {2}, {3} );\n", id, xOffset, yOffset, zOffset);
                }
            }

            sb.AppendFormat("\t\t\tAddonComponent ac;\n");

            foreach (Static item in target)
            {
                int xOffset = item.X - center.X;
                int yOffset = item.Y - center.Y;
                int zOffset = item.Z - center.Z;
                int id = item.ItemID;

                sb.AppendFormat("\t\t\tac = new AddonComponent( {0} );\n", item.ItemID);

                if ((item.ItemData.Flags & TileFlag.LightSource) == TileFlag.LightSource)
                {
                    sb.AppendFormat("\t\t\tac.Light = LightType.{0};\n", item.Light.ToString());
                }

                if (item.Hue != 0)
                {
                    sb.AppendFormat("\t\t\tac.Hue = {0};\n", item.Hue);
                }

                if (item.Name != null)
                {
                    sb.AppendFormat("\t\t\tac.Name = \"{0}\";\n", item.Name);
                }

                sb.AppendFormat("\t\t\tAddComponent( ac, {0}, {1}, {2} );\n", xOffset, yOffset, zOffset);
            }

            string output = m_Template.Replace("{name}", name);
            output = output.Replace("{namespace}", ns);
            output = output.Replace("{components}", sb.ToString());

            StreamWriter writer = null;
            string path = null;

            if (m_CustomOutputDirectory != null)
                path = Path.Combine(m_CustomOutputDirectory, string.Format(@"{0}Addon.cs", name));
            else
                path = Path.Combine(Core.BaseDirectory, string.Format(@"TheBox\{0}Addon.cs", name));

            fail = false;

            try
            {
                string folder = Path.GetDirectoryName(path);

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                writer = new StreamWriter(path, false);
                writer.Write(output);
            }
            catch
            {
                from.SendMessage(0x40, "An error occurred when writing the file.");
                fail = true;
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }

            if (!fail)
            {
                from.SendMessage(0x40, "Script saved to {0}", path);
            }
        }

        public static List<StaticTile> GetTilesAt(Map map, Point2D p, bool items, bool land, bool statics)
        {
            List<StaticTile> list = new List<StaticTile>();

            if (map == Map.Internal)
                return list;

            if (statics)
                list.AddRange(map.Tiles.GetStaticTiles(p.X, p.Y, true));

            if (items)
            {
                Sector sector = map.GetSector(p);

                foreach (Item item in sector.Items)
                    if (item.AtWorldPoint(p.X, p.Y))
                        list.Add(new StaticTile((ushort)item.ItemID, (sbyte)item.Z));
            }

            return list;
        }


        #region Gump
        private class InternalGump : Gump
        {
            private const int LabelHue = 0x480;
            private const int GreenHue = 0x40;
            private object[] m_State;

            public InternalGump(Mobile m, object[] state)
                : base(100, 50)
            {
                m.CloseGump(typeof(InternalGump));
                m_State = state;
                MakeGump();
            }

            private void MakeGump()
            {
                this.Closable = true;
                this.Disposable = true;
                this.Dragable = true;
                this.Resizable = false;
                this.AddPage(0);
                this.AddBackground(0, 0, 280, 225, 9270);
                this.AddAlphaRegion(10, 10, 260, 205);
                this.AddLabel(64, 15, GreenHue, @"Addon Script Generator");
                this.AddLabel(20, 40, LabelHue, @"Name");
                this.AddImageTiled(95, 55, 165, 1, 9304);

                // Name: 0
                this.AddTextEntry(95, 35, 165, 20, LabelHue, 0, m_State[0] as string);

                this.AddLabel(20, 60, LabelHue, @"Namespace");
                this.AddImageTiled(95, 75, 165, 1, 9304);

                // Namespace: 1
                this.AddTextEntry(95, 55, 165, 20, LabelHue, 1, m_State[1] as string);

                // Items: Check 0
                this.AddCheck(20, 85, 2510, 2511, ((bool)m_State[2]), 0);
                this.AddLabel(40, 85, LabelHue, @"Export Items");

                // Statics: Check 1
                this.AddCheck(20, 110, 2510, 2511, ((bool)m_State[3]), 1);
                this.AddLabel(40, 110, LabelHue, @"Export Statics");

                // Range: Check 2
                this.AddCheck(20, 135, 2510, 2511, ((bool)m_State[4]), 2);
                this.AddLabel(40, 135, LabelHue, @"Specify Z Range");

                // Min Z: Text 2
                this.AddLabel(50, 160, LabelHue, @"min.");
                this.AddImageTiled(85, 175, 60, 1, 9304);
                this.AddTextEntry(85, 155, 60, 20, LabelHue, 2, m_State[5].ToString());

                // Max Z: Text 3
                this.AddLabel(160, 160, LabelHue, @"max.");
                this.AddImageTiled(200, 175, 60, 1, 9304);
                this.AddTextEntry(200, 155, 60, 20, LabelHue, 3, m_State[6].ToString());

                // Cancel: B0
                this.AddButton(20, 185, 4020, 4021, 0, GumpButtonType.Reply, 0);
                this.AddLabel(55, 185, LabelHue, @"Cancel");

                // Generate: B1
                this.AddButton(155, 185, 4005, 4006, 1, GumpButtonType.Reply, 0);
                this.AddLabel(195, 185, LabelHue, @"Generate");
            }

            public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
            {
                if (info.ButtonID == 0)
                    return;

                foreach (TextRelay text in info.TextEntries)
                {
                    switch (text.EntryID)
                    {
                        case 0: // Name, 0

                            m_State[0] = text.Text.Replace(" ", "");

                            break;

                        case 1: // Namespace, 1

                            m_State[1] = text.Text;

                            break;

                        case 2: // Min Z, 5

                            m_State[5] = text.Text;

                            break;

                        case 3: // Max Z, 6

                            m_State[6] = text.Text;

                            break;
                    }
                }

                // Reset checks
                m_State[2] = false;
                m_State[3] = false;
                m_State[4] = false;

                foreach (int check in info.Switches)
                {
                    m_State[check + 2] = true; // Offset by 2 in the state object
                }

                if (Verify(m_State))
                {
                    BoundingBoxPicker.Begin(sender.Mobile, new BoundingBoxCallback(AddonGenerator.PickerCallback), m_State);
                }
                else
                {
                    sender.Mobile.SendMessage(0x40, "Please review the generation parameters, some are invalid.");
                    sender.Mobile.SendGump(new InternalGump(sender.Mobile, m_State));
                }
            }

            private static bool Verify(object[] state)
            {
                if (state[0] == null || (state[0] as string).Length == 0)
                    return false;

                if (state[1] == null || (state[1] as string).Length == 0)
                    return false;

                bool items = (bool)state[2];
                bool statics = (bool)state[3];
                bool range = (bool)state[4];
                sbyte min = sbyte.MaxValue;
                sbyte max = sbyte.MinValue;
                bool fail = false;

                try
                {
                    min = sbyte.Parse(state[5] as string);
                }
                catch { fail = true; }

                try
                {
                    max = sbyte.Parse(state[6] as string);
                }
                catch { fail = true; }

                if (!(items || statics))
                    return false;

                if (range && fail)
                    return false;

                return true;
            }
        }
        #endregion
    }
}