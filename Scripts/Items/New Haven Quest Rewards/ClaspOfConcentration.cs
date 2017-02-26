namespace Server.Items
{
    public class ClaspOfConcentration : SilverBracelet
	{
		public override int LabelNumber{ get{ return 1077695; } } // Clasp of Concentration

		[Constructable]
		public ClaspOfConcentration()
		{
			LootType = LootType.Blessed;
		}

		public ClaspOfConcentration( Serial serial ) : base( serial )
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
