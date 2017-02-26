namespace Server.Items
{
    public class HealersTouch : LeatherGloves
	{
		public override int LabelNumber{ get{ return 1077684; } } // Healer's Touch

		[Constructable]
		public HealersTouch()
		{
			LootType = LootType.Blessed;
		}

		public HealersTouch( Serial serial ) : base( serial )
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
