namespace Server.Items
{
    [FlipableAttribute( 0x2B72, 0x3169 )]
	public class VultureHelm : BaseArmor
	{
		public override Race RequiredRace { get { return Race.Elf; } }

		public override int InitMinHits{ get{ return 50; } }
		public override int InitMaxHits{ get{ return 65; } }

		public override int OldStrReq{ get{ return 25; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		[Constructable]
		public VultureHelm() : base( 0x2B72 )
		{
			Weight = 5.0;
		}

		public VultureHelm( Serial serial ) : base( serial )
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