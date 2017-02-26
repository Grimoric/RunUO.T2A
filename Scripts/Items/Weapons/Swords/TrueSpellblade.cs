namespace Server.Items
{
    public class TrueSpellblade : ElvenSpellblade
	{
		public override int LabelNumber{ get{ return 1073513; } } // true spellblade

		[Constructable]
		public TrueSpellblade()
		{
		}

		public TrueSpellblade( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}
