namespace Server.Items
{
    public class MidnightBracers : BoneArms
	{
		public override int LabelNumber{ get{ return 1061093; } } // Midnight Bracers
		public override int ArtifactRarity{ get{ return 11; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public MidnightBracers()
		{
			Hue = 0x455;
		}

		public MidnightBracers( Serial serial ) : base( serial )
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