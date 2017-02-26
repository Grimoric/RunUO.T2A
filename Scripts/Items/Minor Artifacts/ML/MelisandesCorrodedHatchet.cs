namespace Server.Items
{
    public class MelisandesCorrodedHatchet : Hatchet
	{
		public override int LabelNumber{ get{ return 1072115; } } // Melisande's Corroded Hatchet

		[Constructable]
		public MelisandesCorrodedHatchet()
		{
			Hue = 0x494;
		}

		public MelisandesCorrodedHatchet( Serial serial ) : base( serial )
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