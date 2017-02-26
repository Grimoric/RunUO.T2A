namespace Server.Items
{
    public class GlovesOfSafeguarding : LeatherGloves
	{
		public override int LabelNumber{ get{ return 1077614; } } // Gloves of Safeguarding

		[Constructable]
		public GlovesOfSafeguarding()
		{
			LootType = LootType.Blessed;
		}

		public GlovesOfSafeguarding( Serial serial ) : base( serial )
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
