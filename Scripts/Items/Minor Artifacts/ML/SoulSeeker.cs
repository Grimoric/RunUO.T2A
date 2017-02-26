namespace Server.Items
{
    public class SoulSeeker : RadiantScimitar
	{
		public override int LabelNumber{ get{ return 1075046; } } // Soul Seeker

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public SoulSeeker()
		{
			Hue = 0x38C;
			Slayer = SlayerName.Repond;
		}

		public SoulSeeker( Serial serial ) : base( serial )
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