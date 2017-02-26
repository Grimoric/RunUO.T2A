using System.Collections;

namespace Server.Spells.First
{
    public class ReactiveArmorSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Reactive Armor", "Flam Sanct",
				236,
				9011,
				Reagent.Garlic,
				Reagent.SpidersSilk,
				Reagent.SulfurousAsh
			);

		public override SpellCircle Circle { get { return SpellCircle.First; } }

		public ReactiveArmorSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override bool CheckCast()
		{
			if ( Caster.MeleeDamageAbsorb > 0 )
			{
				Caster.SendLocalizedMessage( 1005559 ); // This spell is already in effect.
				return false;
			}
			else if ( !Caster.CanBeginAction( typeof( DefensiveSpell ) ) )
			{
				Caster.SendLocalizedMessage( 1005385 ); // The spell will not adhere to you at this time.
				return false;
			}

			return true;
		}

		private static Hashtable m_Table = new Hashtable();

		public override void OnCast()
		{
			if ( Caster.MeleeDamageAbsorb > 0 )
			{
				Caster.SendLocalizedMessage( 1005559 ); // This spell is already in effect.
			}
			else if ( !Caster.CanBeginAction( typeof( DefensiveSpell ) ) )
			{
				Caster.SendLocalizedMessage( 1005385 ); // The spell will not adhere to you at this time.
			}
			else if ( CheckSequence() )
			{
				if ( Caster.BeginAction( typeof( DefensiveSpell ) ) )
				{
					int value = (int)(Caster.Skills[SkillName.Magery].Value + Caster.Skills[SkillName.Meditation].Value + Caster.Skills[SkillName.Inscribe].Value);
					value /= 3;

					if ( value < 0 )
						value = 1;
					else if ( value > 75 )
						value = 75;

					Caster.MeleeDamageAbsorb = value;

					Caster.FixedParticles( 0x376A, 9, 32, 5008, EffectLayer.Waist );
					Caster.PlaySound( 0x1F2 );
				}
				else
				{
					Caster.SendLocalizedMessage( 1005385 ); // The spell will not adhere to you at this time.
				}
			}

			FinishSequence();
		}

		public static void EndArmor( Mobile m )
		{
			if ( m_Table.Contains( m ) )
			{
				m_Table.Remove( m );
				BuffInfo.RemoveBuff( m, BuffIcon.ReactiveArmor );
			}
		}
	}
}