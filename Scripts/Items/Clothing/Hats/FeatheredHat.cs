namespace Server.Items
{
    public class FeatheredHat : BaseHat
    {
        public override int InitMinHits{ get{ return 20; } }
        public override int InitMaxHits{ get{ return 30; } }

        [Constructable]
        public FeatheredHat() : this( 0 )
        {
        }

        [Constructable]
        public FeatheredHat( int hue ) : base( 0x171A, hue )
        {
            Weight = 1.0;
        }

        public FeatheredHat( Serial serial ) : base( serial )
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