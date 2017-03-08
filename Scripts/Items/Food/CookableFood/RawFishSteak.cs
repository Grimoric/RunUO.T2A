namespace Server.Items
{
    public class RawFishSteak : CookableFood
    {
        public override double DefaultWeight
        {
            get { return 0.1; }
        }

        [Constructable]
        public RawFishSteak() : this( 1 )
        {
        }

        [Constructable]
        public RawFishSteak( int amount ) : base( 0x097A, 10 )
        {
            Stackable = true;
            Amount = amount;
        }

        public RawFishSteak( Serial serial ) : base( serial )
        {
        }

        public override Food Cook()
        {
            return new FishSteak();
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