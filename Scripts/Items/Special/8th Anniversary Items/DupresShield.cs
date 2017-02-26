namespace Server.Items
{
    public class DupresShield : BaseShield, ITokunoDyable
	{
		public override int LabelNumber { get { return 1075196; } } // Dupre’s Shield

		public override int InitMinHits { get { return 255; } }
		public override int InitMaxHits { get { return 255; } }

		public override int ArmorBase { get { return 15; } }

		[Constructable]
		public DupresShield() : base( 0x2B01 )
		{
			LootType = LootType.Blessed;
			Weight = 6.0;
		}

		public DupresShield( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); //version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}
