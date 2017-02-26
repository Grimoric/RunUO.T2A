namespace Server.Items
{
    public class EnchantedTitanLegBone : ShortSpear
	{
		public override int LabelNumber{ get{ return 1063482; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public EnchantedTitanLegBone()
		{
			Hue = 0x8A5;
		}

		public EnchantedTitanLegBone( Serial serial ) : base( serial )
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