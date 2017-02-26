namespace Server.Items
{
    public class BrambleCoat : WoodlandChest
	{
		public override int LabelNumber{ get{ return 1072925; } } // Bramble Coat

		[Constructable]
		public BrambleCoat()
		{
			Hue = 0x1;
		}

		public BrambleCoat( Serial serial ) : base( serial )
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