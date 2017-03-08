namespace Server.Items
{
    [Flipable( 0x1efd, 0x1efe )]
    public class FancyShirt : BaseShirt
    {
        [Constructable]
        public FancyShirt() : this( 0 )
        {
        }

        [Constructable]
        public FancyShirt( int hue ) : base( 0x1EFD, hue )
        {
            Weight = 2.0;
        }

        public FancyShirt( Serial serial ) : base( serial )
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