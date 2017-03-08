using Server.Targeting;

namespace Server.Items
{
    public class Dough : Item
    {
        [Constructable]
        public Dough() : base( 0x103d )
        {
            Stackable = false;
            Weight = 1.0;
        }

        public Dough( Serial serial ) : base( serial )
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

#if false
		public override void OnDoubleClick( Mobile from )
		{
			if ( !Movable )
				return;

			from.Target = new InternalTarget( this );
		}
#endif

        private class InternalTarget : Target
        {
            private Dough m_Item;

            public InternalTarget( Dough item ) : base( 1, false, TargetFlags.None )
            {
                m_Item = item;
            }

            protected override void OnTarget( Mobile from, object targeted )
            {
                if ( m_Item.Deleted ) return;

                if ( targeted is Eggs )
                {
                    m_Item.Delete();

                    ((Eggs)targeted).Consume();

                    from.AddToBackpack( new UnbakedQuiche() );
                    from.AddToBackpack( new Eggshells() );
                }
                else if ( targeted is CheeseWheel )
                {
                    m_Item.Delete();

                    ((CheeseWheel)targeted).Consume();

                    from.AddToBackpack( new CheesePizza() );
                }
                else if ( targeted is Sausage )
                {
                    m_Item.Delete();

                    ((Sausage)targeted).Consume();

                    from.AddToBackpack( new SausagePizza() );
                }
                else if ( targeted is Apple )
                {
                    m_Item.Delete();

                    ((Apple)targeted).Consume();

                    from.AddToBackpack( new UnbakedApplePie() );
                }

                else if ( targeted is Peach )
                {
                    m_Item.Delete();

                    ((Peach)targeted).Consume();

                    from.AddToBackpack( new UnbakedPeachCobbler() );
                }
            }
        }
    }
}