namespace Server.Items
{
    public class BladeOfTheRighteous : Longsword
	{
		public override int LabelNumber{ get{ return 1061107; } } // Blade of the Righteous
		public override int ArtifactRarity{ get{ return 10; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public BladeOfTheRighteous()
		{
			Hue = 0x47E;
			Slayer = SlayerName.Exorcism;
		}

		public BladeOfTheRighteous( Serial serial ) : base( serial )
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

			if ( Slayer == SlayerName.None )
				Slayer = SlayerName.Exorcism;
		}
	}
}