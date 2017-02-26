namespace Server.Items
{
    public class EscutcheonDeAriadne : MetalKiteShield
	{
		public override int LabelNumber{ get{ return 1077694; } } // Escutcheon de Ariadne

		[Constructable]
		public EscutcheonDeAriadne()
		{
			LootType = LootType.Blessed;
			Hue = 0x8A5;
		}

		public EscutcheonDeAriadne( Serial serial ) : base( serial )
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
