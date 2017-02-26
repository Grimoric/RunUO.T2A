namespace Server.Items
{
    public class RecarosRiposte : WarFork
	{
		public override int LabelNumber{ get{ return 1078195; } } // Recaro's Riposte

		[Constructable]
		public RecarosRiposte()
		{
			LootType = LootType.Blessed;
		}

		public RecarosRiposte( Serial serial ) : base( serial )
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
