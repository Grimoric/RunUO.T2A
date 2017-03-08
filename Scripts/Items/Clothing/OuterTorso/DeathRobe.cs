using System;

namespace Server.Items
{
    public class DeathRobe : Robe
    {
        private Timer m_DecayTimer;
        private DateTime m_DecayTime;

        private static TimeSpan m_DefaultDecayTime = TimeSpan.FromMinutes(1.0);

        public override bool DisplayLootType
        {
            get{ return false; }
        }

        [Constructable]
        public DeathRobe()
        {
            LootType = LootType.Newbied;
            Hue = 2301;
            BeginDecay( m_DefaultDecayTime );
        }

        public new bool Scissor( Mobile from, Scissors scissors )
        {
            from.SendLocalizedMessage( 502440 ); // Scissors can not be used on that to produce anything.
            return false;
        }

        public void BeginDecay()
        {
            BeginDecay( m_DefaultDecayTime );
        }

        private void BeginDecay( TimeSpan delay )
        {
            if ( m_DecayTimer != null )
                m_DecayTimer.Stop();

            m_DecayTime = DateTime.Now + delay;

            m_DecayTimer = new InternalTimer( this, delay );
            m_DecayTimer.Start();
        }

        public override bool OnDroppedToWorld( Mobile from, Point3D p )
        {
            BeginDecay( m_DefaultDecayTime );

            return true;
        }

        public override bool OnDroppedToMobile( Mobile from, Mobile target )
        {
            if (m_DecayTimer != null )
            {
                m_DecayTimer.Stop();
                m_DecayTimer = null;
            }

            return true;
        }

        public override void OnAfterDelete()
        {
            if ( m_DecayTimer != null )
                m_DecayTimer.Stop();

            m_DecayTimer = null;
        }

        private class InternalTimer : Timer
        {
            private DeathRobe m_Robe;

            public InternalTimer( DeathRobe c, TimeSpan delay ) : base( delay )
            {
                m_Robe = c;
                Priority = TimerPriority.FiveSeconds;
            }

            protected override void OnTick()
            {
                if ( m_Robe.Parent != null || m_Robe.IsLockedDown )
                    Stop();
                else
                    m_Robe.Delete();
            }
        }

        public DeathRobe( Serial serial ) : base( serial )
        {
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );

            writer.Write( (int) 2 ); // version

            writer.Write( m_DecayTimer != null );

            if( m_DecayTimer != null )
                writer.WriteDeltaTime( m_DecayTime );
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );

            int version = reader.ReadInt();

            switch ( version )
            {
                case 2:
                {
                    if( reader.ReadBool() )
                    {
                        m_DecayTime = reader.ReadDeltaTime();
                        BeginDecay( m_DecayTime - DateTime.Now );
                    }
                    break;
                }
                case 1:
                case 0:
                {
                    if ( Parent == null )
                        BeginDecay( m_DefaultDecayTime );
                    break;
                }
            }

            if ( version < 1 && Hue == 0 )
                Hue = 2301;
        }
    }
}