namespace Server.Items
{
    public abstract class BaseShoes : BaseClothing
    {
        public BaseShoes( int itemID ) : this( itemID, 0 )
        {
        }

        public BaseShoes( int itemID, int hue ) : base( itemID, Layer.Shoes, hue )
        {
        }

        public BaseShoes( Serial serial ) : base( serial )
        {
        }

        public override bool Scissor( Mobile from, Scissors scissors )
        {
            if( DefaultResource == CraftResource.None )
                return base.Scissor( from, scissors );

            from.SendLocalizedMessage( 502440 ); // Scissors can not be used on that to produce anything.
            return false;
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );

            writer.Write( (int) 2 ); // version
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );

            int version = reader.ReadInt();

            switch ( version )
            {
                case 2: break; // empty, resource removed
                case 1:
                {
                    m_Resource = (CraftResource)reader.ReadInt();
                    break;
                }
                case 0:
                {
                    m_Resource = DefaultResource;
                    break;
                }
            }
        }
    }
}