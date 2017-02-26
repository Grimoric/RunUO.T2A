namespace Server.Items
{
    public class LeggingsOfEmbers : PlateLegs
	{
		public override int LabelNumber{ get{ return 1062911; } } // Royal Leggings of Embers

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public LeggingsOfEmbers()
		{
			Hue = 0x2C;
			LootType = LootType.Blessed;
		}

		public LeggingsOfEmbers( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}