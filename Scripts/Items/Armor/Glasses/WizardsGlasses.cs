namespace Server.Items
{
    public class WizardsGlasses : ElvenGlasses
	{
		public override int LabelNumber{ get{ return 1073374; } } //Wizard's Crystal Reading Glasses

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public WizardsGlasses()
		{
			Hue = 0x2B0;
		}
		public WizardsGlasses( Serial serial ) : base( serial )
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

			if ( version == 0 && Hue == 0 )
				Hue = 0x2B0;
		}
	}
}
