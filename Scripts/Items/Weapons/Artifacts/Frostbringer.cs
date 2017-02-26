namespace Server.Items
{
    public class Frostbringer : Bow
	{
		public override int LabelNumber{ get{ return 1061111; } } // Frostbringer
		public override int ArtifactRarity{ get{ return 11; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public Frostbringer()
		{
			Hue = 0x4F2;
		}

		public Frostbringer( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}