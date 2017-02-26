namespace Server.Items
{
    public class SmallPlateJingasa : BaseArmor
	{
		public override int InitMinHits{ get{ return 55; } }
		public override int InitMaxHits{ get{ return 60; } }

		public override int OldStrReq{ get{ return 55; } }

		public override int ArmorBase{ get{ return 4; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		[Constructable]
		public SmallPlateJingasa() : base( 0x2784 )
		{
			Weight = 5.0;
		}

		public SmallPlateJingasa( Serial serial ) : base( serial )
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