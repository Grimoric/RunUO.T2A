namespace Server.Items
{
    public class TheTaskmaster : WarFork
	{
		public override int LabelNumber{ get{ return 1061110; } } // The Taskmaster
		public override int ArtifactRarity{ get{ return 10; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public TheTaskmaster()
		{
			Hue = 0x4F8;
		}

		public TheTaskmaster( Serial serial ) : base( serial )
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