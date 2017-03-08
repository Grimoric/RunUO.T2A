namespace Server.Items
{
    [Flipable( 0x1721, 0x1722 )]
    public class Bananas : Food
    {
        [Constructable]
        public Bananas() : this( 1 )
        {
        }

        [Constructable]
        public Bananas( int amount ) : base( amount, 0x1721 )
        {
            this.Weight = 1.0;
            this.FillFactor = 1;
        }

        public Bananas( Serial serial ) : base( serial )
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