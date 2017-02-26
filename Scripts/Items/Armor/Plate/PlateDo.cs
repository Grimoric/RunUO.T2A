namespace Server.Items
{
    public class PlateDo : BaseArmor
	{
		public override int InitMinHits{ get{ return 60; } }
		public override int InitMaxHits{ get{ return 70; } }

		public override int OldStrReq{ get{ return 85; } }

		public override int ArmorBase{ get{ return 3; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		[Constructable]
		public PlateDo() : base( 0x277D )
		{
			Weight = 10.0;
		}

		public PlateDo( Serial serial ) : base( serial )
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