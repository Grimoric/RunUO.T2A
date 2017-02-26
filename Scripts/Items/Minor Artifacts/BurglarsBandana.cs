namespace Server.Items
{
    public class BurglarsBandana : Bandana
	{
		public override int LabelNumber{ get{ return 1063473; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public BurglarsBandana()
		{
			Hue = Utility.RandomBool() ? 0x58C : 0x10;
		}

		public BurglarsBandana( Serial serial ) : base( serial )
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