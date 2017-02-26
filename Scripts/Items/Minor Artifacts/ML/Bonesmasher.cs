namespace Server.Items
{
    public class Bonesmasher : DiamondMace
	{
		public override int LabelNumber{ get{ return 1075030; } } // Bonesmasher

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public Bonesmasher()
		{
			ItemID = 0x2D30;
			Hue = 0x482;
		}

		public Bonesmasher( Serial serial ) : base( serial )
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