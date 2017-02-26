namespace Server.Items
{
    public class TunicOfGuarding : LeatherChest
	{
		public override int LabelNumber{ get{ return 1077693; } } // Tunic of Guarding

		[Constructable]
		public TunicOfGuarding()
		{
			LootType = LootType.Blessed;
		}

		public TunicOfGuarding( Serial serial ) : base( serial )
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
