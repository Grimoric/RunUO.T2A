namespace Server.Items
{
    public class GuardianAxe : OrnateAxe
	{
		public override int LabelNumber{ get{ return 1073545; } } // guardian axe

		[Constructable]
		public GuardianAxe()
		{
		}

		public GuardianAxe( Serial serial ) : base( serial )
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
