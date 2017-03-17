using Server.Mobiles;
using Server.Network;

namespace Server.Menus.ItemLists
{
    public class BlacksmithMenuTest : ItemListMenu
    {
        private Mobile m_Mobile;
        private string m_Menu;
        private ItemListEntry[] m_Entries;

        public BlacksmithMenuTest(Mobile mobile, ItemListEntry[] entries, string menu) : base("What would you like to make?", entries)
        {
            m_Mobile = mobile;
            m_Menu = menu;
            m_Entries = entries;
        }

        public static ItemListEntry[] Main(Mobile from)
        {
            ItemListEntry[] entries = new ItemListEntry[5];

            entries[0] = new ItemListEntry("Repair",  4015);
            entries[1] = new ItemListEntry("Shields", 7026);
            entries[2] = new ItemListEntry("Armor",   5141);
            entries[3] = new ItemListEntry("Weapons", 5049);
            entries[4] = new ItemListEntry("a horse", 8479);

            return entries;
        }

        public override void OnResponse(NetState state, int index)
        {
            if (m_Menu == "Main")
            {
                if (m_Entries[index].Name == "Repair")
                {
                    //          Repair.Do(m_Mobile, DefBlacksmithy.CraftSystem, m_Tool);
                    m_Mobile.SendAsciiMessage("a repair function is called from here!");
                }
                else if (m_Entries[index].Name == "Shields")
                {
                    m_Mobile.SendAsciiMessage("Crafting Shields!");
                }
                else if (m_Entries[index].Name == "Armor")
                {
                    m_Mobile.SendAsciiMessage("Crafting Armor!");
                }
                else if (m_Entries[index].Name == "Weapons")
                {
                    m_Mobile.SendAsciiMessage("Crafting Weapons!");
                }
                else if (m_Entries[index].Name == "a horse")
                {
                    m_Mobile.SendAsciiMessage("You have spawned a Horse!");
                    new Horse().MoveToWorld(m_Mobile.Location,Map.Felucca);
                }
            }
        }

    }
}