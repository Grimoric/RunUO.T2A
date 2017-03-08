namespace Server.Items
{
    public class SkullCap : BaseHat
    {
        public override int InitMinHits{ get{ return 7; } }
        public override int InitMaxHits{ get{ return 12; } }

        [Constructable]
        public SkullCap() : this( 0 )
        {
        }

        [Constructable]
        public SkullCap( int hue ) : base( 0x1544, hue )
        {
            Weight = 1.0;
        }

        public SkullCap( Serial serial ) : base( serial )
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