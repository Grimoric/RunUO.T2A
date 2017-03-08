namespace Server.Items
{
    public class SplitCoconut : Food
    {
        [Constructable]
        public SplitCoconut() : this( 1 )
        {
        }

        [Constructable]
        public SplitCoconut( int amount ) : base( amount, 0x1725 )
        {
            this.Weight = 1.0;
            this.FillFactor = 1;
        }

        public SplitCoconut( Serial serial ) : base( serial )
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