namespace Server.Items
{
    public class PlateSuneate : BaseArmor
	{
		public override int InitMinHits{ get{ return 55; } }
		public override int InitMaxHits{ get{ return 65; } }

		public override int OldStrReq{ get{ return 80; } }

		public override int ArmorBase{ get{ return 3; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		[Constructable]
		public PlateSuneate() : base( 0x2788 )
		{
			Weight = 7.0;
		}

		public PlateSuneate( Serial serial ) : base( serial )
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