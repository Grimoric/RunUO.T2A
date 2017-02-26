namespace Server.Items
{
    public class ArmsOfArmstrong : LeatherArms
	{
		public override int LabelNumber{ get{ return 1077675; } } // Arms of Armstrong

		[Constructable]
		public ArmsOfArmstrong()
		{
			LootType = LootType.Blessed;
		}

		public ArmsOfArmstrong( Serial serial ) : base( serial )
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
