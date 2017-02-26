namespace Server.Items
{
    public class HelmOfSwiftness : WingedHelm
	{
		public override int LabelNumber{ get{ return 1075037; } } // Helm of Swiftness
		
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public HelmOfSwiftness() : base()
		{
			Hue = 0x592;
		}

		public HelmOfSwiftness( Serial serial ) : base( serial )
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
