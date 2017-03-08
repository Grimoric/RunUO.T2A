namespace Server.Items
{
    public class RoastPig : Food
    {
        [Constructable]
        public RoastPig() : this( 1 )
        {
        }

        [Constructable]
        public RoastPig( int amount ) : base( amount, 0x9BB )
        {
            this.Weight = 45.0;
            this.FillFactor = 20;
        }

        public RoastPig( Serial serial ) : base( serial )
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