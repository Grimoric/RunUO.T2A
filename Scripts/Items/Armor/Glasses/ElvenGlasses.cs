namespace Server.Items
{
    public class ElvenGlasses : BaseArmor
	{
		public override int LabelNumber{ get{ return 1032216; } } // elven glasses

		public override int InitMinHits{ get{ return 36; } }
		public override int InitMaxHits{ get{ return 48; } }

		public override int OldStrReq{ get{ return 40; } }

		public override int ArmorBase{ get{ return 30; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Leather; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }
		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }


		[Constructable]
		public ElvenGlasses() : base( 0x2FB8 )
		{
			Weight = 2;
		}

		public ElvenGlasses( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
