namespace Server.Items
{
    public class StuddedSuneate : BaseArmor
	{
		public override int InitMinHits{ get{ return 35; } }
		public override int InitMaxHits{ get{ return 50; } }

		public override int OldStrReq{ get{ return 30; } }

		public override int ArmorBase{ get{ return 3; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Studded; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		[Constructable]
		public StuddedSuneate() : base( 0x27D2 )
		{
			Weight = 5.0;
		}

		public StuddedSuneate( Serial serial ) : base( serial )
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