namespace Server.Items
{
    public class HatOfTheMagi : WizardsHat
	{
		public override int LabelNumber{ get{ return 1061597; } } // Hat of the Magi

		public override int ArtifactRarity{ get{ return 11; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public HatOfTheMagi()
		{
			Hue = 0x481;
		}

		public HatOfTheMagi( Serial serial ) : base( serial )
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