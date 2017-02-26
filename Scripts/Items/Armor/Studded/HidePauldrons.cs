namespace Server.Items
{
    [FlipableAttribute( 0x2B77, 0x316E )]
	public class HidePauldrons : BaseArmor
	{
		public override Race RequiredRace { get { return Race.Elf; } }

		public override int InitMinHits{ get{ return 35; } }
		public override int InitMaxHits{ get{ return 45; } }

		public override int OldStrReq{ get{ return 20; } }

		public override int ArmorBase{ get{ return 15; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Studded; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.Half; } }

		[Constructable]
		public HidePauldrons() : base( 0x2B77 )
		{
			Weight = 4.0;
		}

		public HidePauldrons( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}