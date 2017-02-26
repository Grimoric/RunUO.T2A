namespace Server.Items
{
    public class LeatherNinjaJacket : BaseArmor
	{
		public override int InitMinHits{ get{ return 55; } }
		public override int InitMaxHits{ get{ return 65; } }

		public override int OldStrReq{ get{ return 10; } }

		public override int ArmorBase{ get{ return 3; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Leather; } }
		public override CraftResource DefaultResource { get { return CraftResource.RegularLeather; } }

		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		[Constructable]
		public LeatherNinjaJacket() : base( 0x2793 )
		{
			Weight = 5.0;
		}

		public LeatherNinjaJacket( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}