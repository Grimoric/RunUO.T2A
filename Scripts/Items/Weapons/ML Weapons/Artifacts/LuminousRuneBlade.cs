namespace Server.Items
{
    public class LuminousRuneBlade : RuneBlade
	{
		public override int LabelNumber{ get{ return 1072922; } } // Luminous Rune Blade

		[Constructable]
		public LuminousRuneBlade()
		{
		}

		public LuminousRuneBlade( Serial serial ) : base( serial )
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