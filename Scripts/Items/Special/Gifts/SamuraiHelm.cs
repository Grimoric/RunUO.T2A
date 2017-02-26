namespace Server.Items
{
    [FlipableAttribute( 0x236C, 0x236D )]
	public class SamuraiHelm : BaseArmor
	{
		public override int LabelNumber{ get{ return 1062923; } } // Ancient Samurai Helm

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		[Constructable]
		public SamuraiHelm() : base( 0x236C )
		{
			Weight = 5.0;
			LootType = LootType.Blessed;
		}

		public SamuraiHelm( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}