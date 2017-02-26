namespace Server.Items
{
    public class CaptainQuacklebushsCutlass : Cutlass
	{
		public override int LabelNumber{ get{ return 1063474; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public CaptainQuacklebushsCutlass()
		{
			Hue = 0x66C;
		}

		public CaptainQuacklebushsCutlass( Serial serial ) : base( serial )
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