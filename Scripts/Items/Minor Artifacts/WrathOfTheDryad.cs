namespace Server.Items
{
    public class WrathOfTheDryad : GnarledStaff
	{
		public override int LabelNumber{ get{ return 1070853; } } // Wrath of the Dryad

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public WrathOfTheDryad()
		{
			Hue = 0x29C;
		}

		public WrathOfTheDryad( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}