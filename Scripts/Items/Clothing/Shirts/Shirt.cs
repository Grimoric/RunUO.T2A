namespace Server.Items
{
    [Flipable( 0x1517, 0x1518 )]
    public class Shirt : BaseShirt
    {
        [Constructable]
        public Shirt() : this( 0 )
        {
        }

        [Constructable]
        public Shirt( int hue ) : base( 0x1517, hue )
        {
            Weight = 1.0;
        }

        public Shirt( Serial serial ) : base( serial )
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

            if ( Weight == 2.0 )
                Weight = 1.0;
        }
    }
}