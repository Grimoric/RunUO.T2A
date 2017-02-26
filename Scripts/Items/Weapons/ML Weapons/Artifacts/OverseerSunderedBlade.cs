namespace Server.Items
{
    public class OverseerSunderedBlade : RadiantScimitar
	{
		public override int LabelNumber{ get{ return 1072920; } } // Overseer Sundered Blade

		[Constructable]
		public OverseerSunderedBlade()
		{
			ItemID = 0x2D27;
			Hue = 0x485;
		}

		public OverseerSunderedBlade( Serial serial ) : base( serial )
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