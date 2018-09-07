namespace Server.Items
{
    public class SmallFish : Item
	{
		[Constructable]
		public SmallFish() : this( 1 )
		{
		}

		[Constructable]
		public SmallFish( int amount ) : base( Utility.Random(0x0DD6, 1 ) )
		{
			Stackable = true;
			Weight = 5.0;
			Amount = amount;
		}
        
		public SmallFish( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
