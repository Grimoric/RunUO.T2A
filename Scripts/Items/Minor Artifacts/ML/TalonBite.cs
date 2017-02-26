namespace Server.Items
{
    public class TalonBite : OrnateAxe
	{
		public override int LabelNumber{ get{ return 1075029; } } // Talon Bite

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public TalonBite()
		{
			ItemID = 0x2D34;
			Hue = 0x47E;
		}

		public TalonBite( Serial serial ) : base( serial )
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