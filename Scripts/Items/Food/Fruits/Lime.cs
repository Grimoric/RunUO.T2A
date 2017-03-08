namespace Server.Items
{
    public class Lime : Food
    {
        [Constructable]
        public Lime() : this( 1 )
        {
        }

        [Constructable]
        public Lime( int amount ) : base( amount, 0x172a )
        {
            this.Weight = 1.0;
            this.FillFactor = 1;
        }

        public Lime( Serial serial ) : base( serial )
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