namespace Server.Items
{
    [FlipableAttribute( 0x13be, 0x13c3 )]
	public class ChainLegs : BaseArmor
	{
		public override int InitMinHits{ get{ return 45; } }
		public override int InitMaxHits{ get{ return 60; } }

		public override int OldStrReq{ get{ return 20; } }

		public override int OldDexBonus{ get{ return -3; } }

		public override int ArmorBase{ get{ return 28; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Chainmail; } }

		[Constructable]
		public ChainLegs() : base( 0x13BE )
		{
			Weight = 7.0;
		}

		public ChainLegs( Serial serial ) : base( serial )
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