namespace Server.Items
{
    [Flipable( 0x1541, 0x1542 )]
    public class BodySash : BaseMiddleTorso
    {
        [Constructable]
        public BodySash() : this( 0 )
        {
        }

        [Constructable]
        public BodySash( int hue ) : base( 0x1541, hue )
        {
            Weight = 1.0;
        }

        public BodySash( Serial serial ) : base( serial )
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