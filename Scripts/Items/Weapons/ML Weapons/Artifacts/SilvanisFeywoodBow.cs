namespace Server.Items
{
    public class SilvanisFeywoodBow : ElvenCompositeLongbow
	{
		public override int LabelNumber{ get{ return 1072955; } } // Silvani's Feywood Bow

		[Constructable]
		public SilvanisFeywoodBow()
		{
			Hue = 0x1A;
		}

		public SilvanisFeywoodBow( Serial serial ) : base( serial )
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