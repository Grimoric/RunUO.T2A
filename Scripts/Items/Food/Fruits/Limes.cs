namespace Server.Items
{
    public class Limes : Food
    {
        [Constructable]
        public Limes() : this( 1 )
        {
        }

        [Constructable]
        public Limes( int amount ) : base( amount, 0x172B )
        {
            this.Weight = 1.0;
            this.FillFactor = 1;
        }

        public Limes( Serial serial ) : base( serial )
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