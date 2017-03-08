namespace Server.Items
{
    public class UnbakedQuiche : CookableFood
    {
        public override int LabelNumber{ get{ return 1041339; } } // unbaked quiche

        [Constructable]
        public UnbakedQuiche() : base( 0x1042, 25 )
        {
            Weight = 1.0;
        }

        public UnbakedQuiche( Serial serial ) : base( serial )
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

        public override Food Cook()
        {
            return new Quiche();
        }
    }
}