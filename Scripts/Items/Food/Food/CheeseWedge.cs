namespace Server.Items
{
    public class CheeseWedge : Food
    {
        public override double DefaultWeight
        {
            get { return 0.1; }
        }

        [Constructable]
        public CheeseWedge() : this( 1 )
        {
        }

        [Constructable]
        public CheeseWedge( int amount ) : base( amount, 0x97D )
        {
            this.FillFactor = 3;
        }

        public CheeseWedge( Serial serial ) : base( serial )
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