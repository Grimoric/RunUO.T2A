namespace Server.Items
{
    public class PolarBearMask : BearMask
	{
		public override int LabelNumber{ get{ return 1070637; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public PolarBearMask()
		{
			Hue = 0x481;
		}

		public PolarBearMask( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 2 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}