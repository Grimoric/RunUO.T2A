namespace Server.Items
{
    public class RawRibs : CookableFood
    {
        [Constructable]
        public RawRibs() : this( 1 )
        {
        }

        [Constructable]
        public RawRibs( int amount ) : base( 0x9F1, 10 )
        {
            Weight = 1.0;
            Stackable = true;
            Amount = amount;
        }

        public RawRibs( Serial serial ) : base( serial )
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

        public override Food Cook()
        {
            return new Ribs();
        }
    }
}