namespace Server.Items
{
    public class DreadPirateHat : TricorneHat
	{
		public override int LabelNumber{ get{ return 1063467; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public DreadPirateHat()
		{
			Hue = 0x497;
		}

		public DreadPirateHat( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}