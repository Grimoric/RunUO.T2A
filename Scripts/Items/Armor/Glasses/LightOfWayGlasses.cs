namespace Server.Items
{
    public class LightOfWayGlasses : ElvenGlasses
	{
		public override int LabelNumber{ get{ return 1073378; } } //Light Of Way Reading Glasses

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public LightOfWayGlasses()
		{
			Hue = 0x256;
		}
		public LightOfWayGlasses( Serial serial ) : base( serial )
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

			if ( version == 0 && Hue == 0 )
				Hue = 0x256;
		}
	}
}
