using System;
using Server.Items;

namespace Server.SkillHandlers
{
	class Meditation
	{
		public static void Initialize()
		{
			SkillInfo.Table[46].Callback = new SkillUseCallback( OnUse );
		}

		public static bool CheckOkayHolding( Item item )
		{
			if ( item == null )
				return true;

			if ( item is Spellbook || item is Runebook )
				return true;

			return false;
		}

		public static TimeSpan OnUse( Mobile m )
		{
			m.RevealingAction();

			if ( m.Target != null )
			{
				m.SendLocalizedMessage( 501845 ); // You are busy doing something else and cannot focus.

				return TimeSpan.FromSeconds( 5.0 );
			} 
			else if (  m.Hits < m.HitsMax / 10 ) // Less than 10% health
			{
				m.SendLocalizedMessage( 501849 ); // The mind is strong but the body is weak.

				return TimeSpan.FromSeconds( 5.0 );
			}
			else if ( m.Mana >= m.ManaMax )
			{
				m.SendLocalizedMessage( 501846 ); // You are at peace.

				return TimeSpan.FromSeconds( 5.0 );
			}
			else 
			{
				Item oneHanded = m.FindItemOnLayer( Layer.OneHanded );
				Item twoHanded = m.FindItemOnLayer( Layer.TwoHanded );

				if ( !CheckOkayHolding( oneHanded ) || !CheckOkayHolding( twoHanded ) )
				{
					m.SendLocalizedMessage( 502626 ); // Your hands must be free to cast spells or meditate.

					return TimeSpan.FromSeconds( 2.5 );
				}

				double skillVal = m.Skills[SkillName.Meditation].Value;
				double chance = (50.0 + ( skillVal - ( m.ManaMax - m.Mana ) ) * 2) / 100;

				if ( chance > Utility.RandomDouble() )
				{
					m.CheckSkill( SkillName.Meditation, 0.0, 100.0 );

					m.SendLocalizedMessage( 501851 ); // You enter a meditative trance.
					m.Meditating = true;

					if ( m.Player || m.Body.IsHuman )
						m.PlaySound( 0xF9 );
				} 
				else 
				{
					m.SendLocalizedMessage( 501850 ); // You cannot focus your concentration.
				}

				return TimeSpan.FromSeconds( 10.0 );
			}
		}
	}
}