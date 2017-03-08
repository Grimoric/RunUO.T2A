namespace Server.Items
{
    public class CheeseSlice : Food
    {
        public override double DefaultWeight
        {
            get { return 0.1; }
        }

        [Constructable]
        public CheeseSlice() : this( 1 )
        {
        }

        [Constructable]
        public CheeseSlice( int amount ) : base( amount, 0x97C )
        {
            this.FillFactor = 1;
        }

        public CheeseSlice( Serial serial ) : base( serial )
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