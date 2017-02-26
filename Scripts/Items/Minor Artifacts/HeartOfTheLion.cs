namespace Server.Items
{
    public class HeartOfTheLion : PlateChest
	{
		public override int LabelNumber{ get{ return 1070817; } } // Heart of the Lion

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public HeartOfTheLion()
		{
			Hue = 0x501;
		}

		public HeartOfTheLion( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}