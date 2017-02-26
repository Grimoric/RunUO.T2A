namespace Server.Items
{
    public class SpellWovenBritches : LeafLegs
	{
		public override int LabelNumber{ get{ return 1072929; } } // Spell Woven Britches

		[Constructable]
		public SpellWovenBritches()
		{
			Hue = 0x487;
		}

		public SpellWovenBritches( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}