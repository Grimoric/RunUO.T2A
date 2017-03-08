namespace Server.Items
{
    public class CheeseWheel : Food
    {
        public override double DefaultWeight
        {
            get { return 0.1; }
        }

        [Constructable]
        public CheeseWheel() : this( 1 )
        {
        }

        [Constructable]
        public CheeseWheel( int amount ) : base( amount, 0x97E )
        {
            this.FillFactor = 3;
        }

        public CheeseWheel( Serial serial ) : base( serial )
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