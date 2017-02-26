namespace Server.Items
{
    public class StitchersMittens : LeafGloves
	{
		public override int LabelNumber{ get{ return 1072932; } } // Stitcher's Mittens

		[Constructable]
		public StitchersMittens()
		{
			Hue = 0x481;
		}

		public StitchersMittens( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();

		}
	}
}