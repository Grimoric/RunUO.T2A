namespace Server.Items
{
    public class VioletCourage : FemalePlateChest
	{
		public override int LabelNumber{ get{ return 1063471; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public VioletCourage()
		{
			Hue = 0x486;
		}

		public VioletCourage( Serial serial ) : base( serial )
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