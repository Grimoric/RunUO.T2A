using System;
using System.Collections.Generic;
using Server.Commands;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
    public class PublicMoongate : Item
    {
        [Constructable]
        public PublicMoongate() : base(0xF6C)
        {
            Movable = false;
            Light = LightType.Circle300;
        }

        public PublicMoongate(Serial serial) : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!from.Player)
                return;

            if (from.InRange(GetWorldLocation(), 1))
                UseGate(from);
            else
                from.SendLocalizedMessage(500446); // That is too far away.
        }

        public override void OnSingleClick(Mobile from)
        {
            int moonPhase = (int)Clock.GetMoonPhase(from.Map, from.X, from.Y);
            from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("Through the moongate you {0}", PMList.Felucca.Entries[moonPhase].Description)));
        }

        public override bool OnMoveOver(Mobile m)
        {
            if (m.Player)
                UseGate(m);

            return false;
        }

        public void UseGate(Mobile m)
        {
            m.Combatant = null;
            m.Warmode = false;

            int moonPhase = (int)Clock.GetMoonPhase(m.Map, m.X, m.Y);

            m.MoveToWorld(PMList.Felucca.Entries[moonPhase].Location, Map.Felucca);
            BaseCreature.TeleportPets(m, PMList.Felucca.Entries[moonPhase].Location, Map.Felucca);

            Effects.PlaySound(PMList.Felucca.Entries[moonPhase].Location, Map.Felucca, 0x1FE);

            if (!m.Hidden || m.AccessLevel == AccessLevel.Player)
                Effects.PlaySound(m.Location, m.Map, 0x20E);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public static void Initialize()
        {
            CommandSystem.Register("MoonGen", AccessLevel.Administrator, new CommandEventHandler(MoonGen_OnCommand));
        }

        [Usage("MoonGen")]
        [Description("Generates public moongates. Removes all old moongates.")]
        public static void MoonGen_OnCommand(CommandEventArgs e)
        {
            DeleteAll();

            int count = 0;

            count += MoonGen(PMList.Trammel);
            count += MoonGen(PMList.Felucca);

            World.Broadcast(0x35, true, "{0} moongates generated.", count);
        }

        private static void DeleteAll()
        {
            List<Item> list = new List<Item>();

            foreach (Item item in World.Items.Values)
            {
                if (item is PublicMoongate)
                    list.Add(item);
            }

            foreach (Item item in list)
                item.Delete();

            if (list.Count > 0)
                World.Broadcast(0x35, true, "{0} moongates removed.", list.Count);
        }

        private static int MoonGen(PMList list)
        {
            foreach (PMEntry entry in list.Entries)
            {
                Item item = new PublicMoongate();

                item.MoveToWorld(entry.Location, list.Map);
            }

            return list.Entries.Length;
        }
    }

    public class PMEntry
    {
        private Point3D m_Location;
        private string m_Description;

        public Point3D Location
        {
            get
            {
                return m_Location;
            }
        }

        public string Description
        {
            get
            {
                return m_Description;
            }
        }

        public PMEntry(Point3D loc, string des)
        {
            m_Location = loc;
            m_Description = des;
        }
    }

    public class PMList
    {
        private Map m_Map;
        private PMEntry[] m_Entries;

        public Map Map
        {
            get
            {
                return m_Map;
            }
        }

        public PMEntry[] Entries
        {
            get
            {
                return m_Entries;
            }
        }

        public PMList(Map map, PMEntry[] entries)
        {
            m_Map = map;
            m_Entries = entries;
        }

        public static readonly PMList Trammel = new PMList(Map.Trammel, new PMEntry[]
                {
                    new PMEntry( new Point3D( 1336, 1997, 5 ), "see a road to the east and mountains in the distance to the west." ), // Britain
                    new PMEntry( new Point3D( 4467, 1283, 5 ), "see a small escarpment to the south and a large city to the North." ), // Moonglow
					new PMEntry( new Point3D( 3563, 2139, Map.Trammel.GetAverageZ( 3563, 2139 ) ), "see what appears to be a small peninsula covered in lush foliage." ), // Magincia
					new PMEntry( new Point3D(  643, 2067, 5 ), "see a small city to the south, while a vast ocean lies in all other directions." ), // Skara Brae
					new PMEntry( new Point3D( 1828, 2948,-20), "see a large sandstone city standing on a far bank of the river to the north." ), // Trinsic
					new PMEntry( new Point3D( 2701,  692, 5 ), "can just make out a road to the southwest and a river to the north." ), // Minoc
					new PMEntry( new Point3D(  771,  752, 5 ), "see deep forest on all sides." ), // Yew
                    new PMEntry( new Point3D( 1499, 3771, 5 ), "see a vast body of water to the east while to the west a city can be seen nearby." ), // Jhelom
				});

        public static readonly PMList Felucca = new PMList(Map.Felucca, new PMEntry[]
                {
                    new PMEntry( new Point3D( 1336, 1997, 5 ), "see a road to the east and mountains in the distance to the west." ), // Britain
                    new PMEntry( new Point3D( 4467, 1283, 5 ), "see a small escarpment to the south and a large city to the North." ), // Moonglow
					new PMEntry( new Point3D( 3563, 2139, Map.Trammel.GetAverageZ( 3563, 2139 ) ), "see what appears to be a small peninsula covered in lush foliage." ), // Magincia
					new PMEntry( new Point3D(  643, 2067, 5 ), "see a small city to the south, while a vast ocean lies in all other directions." ), // Skara Brae
					new PMEntry( new Point3D( 1828, 2948,-20), "see a large sandstone city standing on a far bank of the river to the north." ), // Trinsic
					new PMEntry( new Point3D( 2701,  692, 5 ), "can just make out a road to the southwest and a river to the north." ), // Minoc
					new PMEntry( new Point3D(  771,  752, 5 ), "see deep forest on all sides." ), // Yew
                    new PMEntry( new Point3D( 1499, 3771, 5 ), "see a vast body of water to the east while to the west a city can be seen nearby." ), // Jhelom
				});
    }
}
