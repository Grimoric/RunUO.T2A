namespace Server.Items
{
    public class StrawHat : BaseHat
    {
        public override int InitMinHits{ get{ return 20; } }
        public override int InitMaxHits{ get{ return 30; } }

        [Constructable]
        public StrawHat() : this( 0 )
        {
        }

        [Constructable]
        public StrawHat( int hue ) : base( 0x1717, hue )
        {
            Weight = 1.0;
        }

        public StrawHat( Serial serial ) : base( serial )
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