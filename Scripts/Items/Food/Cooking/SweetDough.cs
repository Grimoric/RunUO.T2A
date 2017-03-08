using System;
using Server.Targeting;

namespace Server.Items
{
    public class SweetDough : Item
    {
        public override int LabelNumber{ get{ return 1041340; } } // sweet dough

        [Constructable]
        public SweetDough() : base( 0x103d )
        {
            Stackable = false;
            Weight = 1.0;
            Hue = 150;
        }

        public SweetDough( Serial serial ) : base( serial )
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

            if ( Hue == 51 )
                Hue = 150;
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
            private SweetDough m_Item;

            public InternalTarget( SweetDough item ) : base( 1, false, TargetFlags.None )
            {
                m_Item = item;
            }

            protected override void OnTarget( Mobile from, object targeted )
            {
                if ( m_Item.Deleted ) return;

                if ( targeted is BowlFlour )
                {
                    m_Item.Delete();
                    ((BowlFlour)targeted).Delete();

                    from.AddToBackpack( new CakeMix() );
                }
                else if ( targeted is Campfire )
                {
                    from.PlaySound( 0x225 );
                    m_Item.Delete();
                    InternalTimer t = new InternalTimer( from, (Campfire)targeted );
                    t.Start();
                }
            }
			
            private class InternalTimer : Timer
            {
                private Mobile m_From;
                private Campfire m_Campfire;
			
                public InternalTimer( Mobile from, Campfire campfire ) : base( TimeSpan.FromSeconds( 5.0 ) )
                {
                    m_From = from;
                    m_Campfire = campfire;
                }

                protected override void OnTick()
                {
                    if ( m_From.GetDistanceToSqrt( m_Campfire ) > 3 )
                    {
                        m_From.SendLocalizedMessage( 500686 ); // You burn the food to a crisp! It's ruined.
                        return;
                    }

                    if ( m_From.CheckSkill( SkillName.Cooking, 0, 10 ) )
                    {
                        if ( m_From.AddToBackpack( new Muffins() ) )
                            m_From.PlaySound( 0x57 );
                    }
                    else
                    {
                        m_From.SendLocalizedMessage( 500686 ); // You burn the food to a crisp! It's ruined.
                    }
                }
            }
        }
    }
}