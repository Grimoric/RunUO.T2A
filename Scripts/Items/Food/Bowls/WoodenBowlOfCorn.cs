namespace Server.Items
{
    public class WoodenBowlOfCorn : Food
    {
        [Constructable]
        public WoodenBowlOfCorn() : base( 0x15FA )
        {
            Stackable = false;
            Weight = 1.0;
            FillFactor = 2;
        }

        public override bool Eat( Mobile from )
        {
            if ( !base.Eat( from ) )
                return false;

            from.AddToBackpack( new EmptyWoodenBowl() );
            return true;
        }

        public WoodenBowlOfCorn( Serial serial ) : base( serial )
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