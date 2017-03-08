namespace Server.Items
{
    public class Sausage : Food
    {
        [Constructable]
        public Sausage() : this( 1 )
        {
        }

        [Constructable]
        public Sausage( int amount ) : base( amount, 0x9C0 )
        {
            this.Weight = 1.0;
            this.FillFactor = 4;
        }

        public Sausage( Serial serial ) : base( serial )
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