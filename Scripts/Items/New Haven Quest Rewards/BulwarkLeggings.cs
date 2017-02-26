namespace Server.Items
{
    public class BulwarkLeggings : RingmailLegs
	{
		public override int LabelNumber{ get{ return 1077727; } } // Bulwark Leggings

		[Constructable]
		public BulwarkLeggings()
		{
			LootType = LootType.Blessed;
		}

		public BulwarkLeggings( Serial serial ) : base( serial )
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
