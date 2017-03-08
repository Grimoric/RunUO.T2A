namespace Server.Items
{
    [Flipable( 0x152e, 0x152f )]
    public class ShortPants : BasePants
    {
        [Constructable]
        public ShortPants() : this( 0 )
        {
        }

        [Constructable]
        public ShortPants( int hue ) : base( 0x152E, hue )
        {
            Weight = 2.0;
        }

        public ShortPants( Serial serial ) : base( serial )
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