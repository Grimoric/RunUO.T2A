namespace Server.Items
{
    public class FeyLeggings : ChainLegs
	{
		public override int LabelNumber{ get{ return 1075041; } } // Fey Leggings

    	public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get { return 255; } }


		[Constructable]
		public FeyLeggings()
		{
		}

		public override Race RequiredRace { get { return Race.Elf; } }

		public FeyLeggings( Serial serial ) : base( serial )
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