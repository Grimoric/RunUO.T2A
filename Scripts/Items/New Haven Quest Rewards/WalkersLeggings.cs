namespace Server.Items
{
    public class WalkersLeggings : LeatherNinjaPants
	{
		public override int LabelNumber{ get{ return 1078222; } } // Walker's Leggings

		[Constructable]
		public WalkersLeggings()
		{
			LootType = LootType.Blessed;
		}

		public WalkersLeggings( Serial serial ) : base( serial )
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
