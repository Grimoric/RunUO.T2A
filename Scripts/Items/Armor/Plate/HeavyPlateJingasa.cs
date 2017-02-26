namespace Server.Items
{
    public class HeavyPlateJingasa : BaseArmor
	{
		public override int InitMinHits{ get{ return 50; } }
		public override int InitMaxHits{ get{ return 70; } }

		public override int OldStrReq{ get{ return 55; } }

		public override int ArmorBase{ get{ return 4; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		[Constructable]
		public HeavyPlateJingasa() : base( 0x2777 )
		{
			Weight = 5.0;
		}

		public HeavyPlateJingasa( Serial serial ) : base( serial )
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