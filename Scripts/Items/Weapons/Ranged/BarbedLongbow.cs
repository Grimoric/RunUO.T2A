namespace Server.Items
{
    public class BarbedLongbow : ElvenCompositeLongbow
	{
		public override int LabelNumber{ get{ return 1073505; } } // barbed longbow

		[Constructable]
		public BarbedLongbow()
		{
		}

		public BarbedLongbow( Serial serial ) : base( serial )
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
