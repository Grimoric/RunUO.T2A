namespace Server.Items
{
    public class OrcChieftainHelm : OrcHelm
	{
		public override int LabelNumber{ get{ return 1094924; } } // Orc Chieftain Helm [Replica]

		public override int InitMinHits{ get{ return 150; } }
		public override int InitMaxHits{ get{ return 150; } }

		public override bool CanFortify{ get{ return false; } }

		[Constructable]
		public OrcChieftainHelm()
		{
			Hue = 0x2a3;
		}

		public OrcChieftainHelm( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if (version < 1 && Hue == 0x3f) /* Pigmented? */
			{
				Hue = 0x2a3;
			}
		}
	}
}
