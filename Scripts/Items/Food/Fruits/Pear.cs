namespace Server.Items
{
    public class Pear : Food
    {
        [Constructable]
        public Pear() : this( 1 )
        {
        }

        [Constructable]
        public Pear( int amount ) : base( amount, 0x994 )
        {
            this.Weight = 1.0;
            this.FillFactor = 1;
        }

        public Pear( Serial serial ) : base( serial )
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