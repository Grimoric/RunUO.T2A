namespace Server.Items
{
    [Flipable( 0x1ffd, 0x1ffe )]
    public class Surcoat : BaseMiddleTorso
    {
        [Constructable]
        public Surcoat() : this( 0 )
        {
        }

        [Constructable]
        public Surcoat( int hue ) : base( 0x1FFD, hue )
        {
            Weight = 6.0;
        }

        public Surcoat( Serial serial ) : base( serial )
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

            if ( Weight == 3.0 )
                Weight = 6.0;
        }
    }
}