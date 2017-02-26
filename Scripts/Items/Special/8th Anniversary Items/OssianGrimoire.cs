namespace Server.Items
{
    public class OssianGrimoire : Spellbook, ITokunoDyable
	{
		public override int LabelNumber { get { return 1078148; } } // Ossian Grimoire

		[Constructable]
		public OssianGrimoire()
		{
			LootType = LootType.Blessed;
		}

		public OssianGrimoire( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 1 ); //version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}
