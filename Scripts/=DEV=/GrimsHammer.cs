using Server.Engines.Craft;
using Server.Network;
using Server.Menus.ItemLists;

namespace Server.Items
{
	[FlipableAttribute(0x13B3, 0x13B4)]
	public class GrimsHammer : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefBlacksmithy.CraftSystem; } }

	    [Constructable]
		public GrimsHammer() : base(0x13B3)
		{
            Hue = 1283;
            Layer = Layer.OneHanded;
		}


		public GrimsHammer( Serial serial ) : base( serial )
		{
		}

        public override void OnSingleClick(Mobile from)
        {
            from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Crafting Menu [Test]"));
        }

        public override void OnDoubleClick(Mobile from)
        {
            from.SendMenu(new BlacksmithMenuTest(from, BlacksmithMenuTest.Main(from), "Main"));
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}