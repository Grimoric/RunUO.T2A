namespace Server.Items
{
    public class Eggs : CookableFood
    {
        [Constructable]
        public Eggs() : this( 1 )
        {
        }

        [Constructable]
        public Eggs( int amount ) : base( 0x9B5, 15 )
        {
            Weight = 1.0;
            Stackable = true;
            Amount = amount;
        }

        public Eggs( Serial serial ) : base( serial )
        {
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );

            writer.Write( (int) 1 ); // version
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );

            int version = reader.ReadInt();

            if ( version < 1 )
            {
                Stackable = true;

                if ( Weight == 0.5 )
                    Weight = 1.0;
            }
        }

        public override Food Cook()
        {
            return new FriedEggs();
        }
    }
}