namespace Server.Items
{
    public class Heartseeker : CompositeBow
	{
		public override int LabelNumber{ get{ return 1078210; } } // Heartseeker

		[Constructable]
		public Heartseeker()
		{
			LootType = LootType.Blessed;
		}

		public Heartseeker( Serial serial ) : base( serial )
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
