namespace Server.Items
{
    [Flipable( 0x153b, 0x153c )]
    public class HalfApron : BaseWaist
    {
        [Constructable]
        public HalfApron() : this( 0 )
        {
        }

        [Constructable]
        public HalfApron( int hue ) : base( 0x153b, hue )
        {
            Weight = 2.0;
        }

        public HalfApron( Serial serial ) : base( serial )
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