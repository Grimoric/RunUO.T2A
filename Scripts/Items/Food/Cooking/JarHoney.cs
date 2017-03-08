using Server.Targeting;

namespace Server.Items
{
    public class JarHoney : Item
    {
        [Constructable]
        public JarHoney() : base( 0x9ec )
        {
            Weight = 1.0;
            Stackable = true;
        }

        public JarHoney( Serial serial ) : base( serial )
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
            Stackable = true;
        }

        /*public override void OnDoubleClick( Mobile from )
		{
			if ( !Movable )
				return;

			from.Target = new InternalTarget( this );
		}*/

        private class InternalTarget : Target
        {
            private JarHoney m_Item;

            public InternalTarget( JarHoney item ) : base( 1, false, TargetFlags.None )
            {
                m_Item = item;
            }

            protected override void OnTarget( Mobile from, object targeted )
            {
                if ( m_Item.Deleted ) return;

                if ( targeted is Dough )
                {
                    m_Item.Delete();
                    ((Dough)targeted).Consume();

                    from.AddToBackpack( new SweetDough() );
                }
				
                if (targeted is BowlFlour)
                {
                    m_Item.Consume();
                    ((BowlFlour)targeted).Delete();

                    from.AddToBackpack( new CookieMix() );
                }
            }
        }
    }
}