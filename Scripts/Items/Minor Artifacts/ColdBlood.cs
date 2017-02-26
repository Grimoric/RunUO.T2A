namespace Server.Items
{
    public class ColdBlood : Cleaver
	{
		public override int LabelNumber{ get{ return 1070818; } } // Cold Blood

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public ColdBlood()
		{
			Hue = 0x4F2;
		}

		public ColdBlood( Serial serial ) : base( serial )
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