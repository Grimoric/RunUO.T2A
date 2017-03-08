namespace Server.Items
{
    public class OpenCoconut : Food
    {
        [Constructable]
        public OpenCoconut() : this( 1 )
        {
        }

        [Constructable]
        public OpenCoconut( int amount ) : base( amount, 0x1723 )
        {
            this.Weight = 1.0;
            this.FillFactor = 1;
        }

        public OpenCoconut( Serial serial ) : base( serial )
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