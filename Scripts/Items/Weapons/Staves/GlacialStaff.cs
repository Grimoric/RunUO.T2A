namespace Server.Items
{
    public class GlacialStaff : BlackStaff
	{
		//TODO: Pre-AoS stuff
		public override int LabelNumber{ get{ return 1017413; } } // Glacial Staff

		[Constructable]
		public GlacialStaff()
		{
			Hue = 0x480;
		}

		public GlacialStaff( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
