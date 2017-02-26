namespace Server.Items
{
    public class BrightsightLenses : ElvenGlasses
	{
		public override int LabelNumber{ get{ return 1075039; } } // Brightsight Lenses

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public BrightsightLenses() : base()
		{
			Hue = 0x501;
		}

		public BrightsightLenses( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
