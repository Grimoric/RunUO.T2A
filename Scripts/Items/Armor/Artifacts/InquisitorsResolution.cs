namespace Server.Items
{
    public class InquisitorsResolution : PlateGloves
	{
		public override int LabelNumber{ get{ return 1060206; } } // The Inquisitor's Resolution
		public override int ArtifactRarity{ get{ return 10; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public InquisitorsResolution()
		{
			Hue = 0x4F2;
		}

		public InquisitorsResolution( Serial serial ) : base( serial )
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
		}
	}
}