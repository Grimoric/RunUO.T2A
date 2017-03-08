using System;
using Server.Items;
using Server.Guilds;
using Server.Multis;
using Server.Regions;
using Server.Mobiles;
using Server.Targeting;
using Server.Engines.PartySystem;
using Server.Misc;

namespace Server
{
    public class DefensiveSpell
	{
		public static void Nullify( Mobile from )
		{
			if( !from.CanBeginAction( typeof( DefensiveSpell ) ) )
				new InternalTimer( from ).Start();
		}

		private class InternalTimer : Timer
		{
			private Mobile m_Mobile;

			public InternalTimer( Mobile m )
				: base( TimeSpan.FromMinutes( 1.0 ) )
			{
				m_Mobile = m;

				Priority = TimerPriority.OneSecond;
			}

			protected override void OnTick()
			{
				m_Mobile.EndAction( typeof( DefensiveSpell ) );
			}
		}
	}
}

namespace Server.Spells
{
    public enum TravelCheckType
	{
		RecallFrom,
		RecallTo,
		GateFrom,
		GateTo,
		Mark,
		TeleportFrom,
		TeleportTo
	}

	public class SpellHelper
	{
		private static TimeSpan OldDamageDelay = TimeSpan.FromSeconds( 0.5 );

		public static TimeSpan GetDamageDelayForSpell( Spell sp )
		{
			if( !sp.DelayedDamage )
				return TimeSpan.Zero;

			return OldDamageDelay;
		}

		public static bool CheckMulti( Point3D p, Map map )
		{
			return CheckMulti( p, map, true, 0);
		}
		
		public static bool CheckMulti(Point3D p, Map map, bool houses)
		{
			return CheckMulti(p, map, houses, 0);
		}
		
		public static bool CheckMulti( Point3D p, Map map, bool houses, int housingrange )
		{
			if( map == null || map == Map.Internal )
				return false;

			Sector sector = map.GetSector( p.X, p.Y );

			for( int i = 0; i < sector.Multis.Count; ++i )
			{
				BaseMulti multi = sector.Multis[i];

				if( multi is BaseHouse )
				{
					BaseHouse bh = (BaseHouse)multi;

					if( houses && bh.IsInside( p, 16 ) || housingrange > 0 && bh.InRange( p, housingrange ) )
						return true;
				}
				else if( multi.Contains( p ))
				{
					return true;
				}
			}

			return false;
		}

		public static void Turn( Mobile from, object to )
		{
			IPoint3D target = to as IPoint3D;

			if( target == null )
				return;

			if( target is Item )
			{
				Item item = (Item)target;

				if( item.RootParent != from )
					from.Direction = from.GetDirectionTo( item.GetWorldLocation() );
			}
			else if( from != target )
			{
				from.Direction = from.GetDirectionTo( target );
			}
		}

		private static TimeSpan CombatHeatDelay = TimeSpan.FromSeconds( 30.0 );
		private static bool RestrictTravelCombat = true;

		public static bool CheckCombat( Mobile m )
		{
			if( !RestrictTravelCombat )
				return false;

			for( int i = 0; i < m.Aggressed.Count; ++i )
			{
				AggressorInfo info = m.Aggressed[i];

				if( info.Defender.Player && DateTime.Now - info.LastCombatTime < CombatHeatDelay )
					return true;
			}

			return false;
		}

		public static bool AdjustField( ref Point3D p, Map map, int height, bool mobsBlock )
		{
			if( map == null )
				return false;

			for( int offset = 0; offset < 10; ++offset )
			{
				Point3D loc = new Point3D( p.X, p.Y, p.Z - offset );

				if( map.CanFit( loc, height, true, mobsBlock ) )
				{
					p = loc;
					return true;
				}
			}

			return false;
		}

		public static bool CanRevealCaster( Mobile m )
		{
			if ( m is BaseCreature )
			{
				BaseCreature c = (BaseCreature)m;
						
				if ( !c.Controlled )
					return true;
			}
			
			return false;
		}

		public static void GetSurfaceTop( ref IPoint3D p )
		{
			if( p is Item )
			{
				p = ((Item)p).GetSurfaceTop();
			}
			else if( p is StaticTarget )
			{
				StaticTarget t = (StaticTarget)p;
				int z = t.Z;

				if( (t.Flags & TileFlag.Surface) == 0 )
					z -= TileData.ItemTable[t.ItemID & TileData.MaxItemValue].CalcHeight;

				p = new Point3D( t.X, t.Y, z );
			}
		}

		public static bool AddStatOffset( Mobile m, StatType type, int offset, TimeSpan duration )
		{
			if( offset > 0 )
				return AddStatBonus( m, m, type, offset, duration );
			else if( offset < 0 )
				return AddStatCurse( m, m, type, -offset, duration );

			return true;
		}

		public static bool AddStatBonus( Mobile caster, Mobile target, StatType type )
		{
			return AddStatBonus( caster, target, type, GetOffset( caster, target, type, false ), GetDuration( caster, target ) );
		}

		public static bool AddStatBonus( Mobile caster, Mobile target, StatType type, int bonus, TimeSpan duration )
		{
			int offset = bonus;
			string name = String.Format( "[Magic] {0} Offset", type );

			StatMod mod = target.GetStatMod( name );

			if( mod != null && mod.Offset < 0 )
			{
				target.AddStatMod( new StatMod( type, name, mod.Offset + offset, duration ) );
				return true;
			}
			else if( mod == null || mod.Offset < offset )
			{
				target.AddStatMod( new StatMod( type, name, offset, duration ) );
				return true;
			}

			return false;
		}

		public static bool AddStatCurse( Mobile caster, Mobile target, StatType type )
		{
			return AddStatCurse( caster, target, type, GetOffset( caster, target, type, true ), GetDuration( caster, target ) );
		}

		public static bool AddStatCurse( Mobile caster, Mobile target, StatType type, int curse, TimeSpan duration )
		{
			int offset = -curse;
			string name = String.Format( "[Magic] {0} Offset", type );

			StatMod mod = target.GetStatMod( name );

			if( mod != null && mod.Offset > 0 )
			{
				target.AddStatMod( new StatMod( type, name, mod.Offset + offset, duration ) );
				return true;
			}
			else if( mod == null || mod.Offset > offset )
			{
				target.AddStatMod( new StatMod( type, name, offset, duration ) );
				return true;
			}

			return false;
		}

		public static TimeSpan GetDuration( Mobile caster, Mobile target )
		{
			return TimeSpan.FromSeconds( caster.Skills[SkillName.Magery].Value * 1.2 );
		}

		private static bool m_DisableSkillCheck;

		public static bool DisableSkillCheck
		{
			get { return m_DisableSkillCheck; }
			set { m_DisableSkillCheck = value; }
		}

		public static double GetOffsetScalar( Mobile caster, Mobile target, bool curse )
		{
			double percent;

			if( curse )
				percent = 8 + caster.Skills.EvalInt.Fixed / 100 - target.Skills.MagicResist.Fixed / 100;
			else
				percent = 1 + caster.Skills.EvalInt.Fixed / 100;

			percent *= 0.01;

			if( percent < 0 )
				percent = 0;

			return percent;
		}

		public static int GetOffset( Mobile caster, Mobile target, StatType type, bool curse )
		{
			return 1 + (int)(caster.Skills[SkillName.Magery].Value * 0.1);
		}

		public static Guild GetGuildFor( Mobile m )
		{
			Guild g = m.Guild as Guild;

			if( g == null && m is BaseCreature )
			{
				BaseCreature c = (BaseCreature)m;
				m = c.ControlMaster;

				if( m != null )
					g = m.Guild as Guild;

				if( g == null )
				{
					m = c.SummonMaster;

					if( m != null )
						g = m.Guild as Guild;
				}
			}

			return g;
		}

		public static bool ValidIndirectTarget( Mobile from, Mobile to )
		{
			if( from == to )
				return true;

			if( to.Hidden && to.AccessLevel > from.AccessLevel )
				return false;

			Guild fromGuild = GetGuildFor( from );
			Guild toGuild = GetGuildFor( to );

			if( fromGuild != null && toGuild != null && (fromGuild == toGuild || fromGuild.IsAlly( toGuild )) )
				return false;

			Party p = Party.Get( from );

			if( p != null && p.Contains( to ) )
				return false;

			if( to is BaseCreature )
			{
				BaseCreature c = (BaseCreature)to;

				if( c.Controlled || c.Summoned )
				{
					if( c.ControlMaster == from || c.SummonMaster == from )
						return false;

					if( p != null && (p.Contains( c.ControlMaster ) || p.Contains( c.SummonMaster )) )
						return false;
				}
			}

			if( from is BaseCreature )
			{
				BaseCreature c = (BaseCreature)from;

				if( c.Controlled || c.Summoned )
				{
					if( c.ControlMaster == to || c.SummonMaster == to )
						return false;

					p = Party.Get( to );

					if( p != null && (p.Contains( c.ControlMaster ) || p.Contains( c.SummonMaster )) )
						return false;
				}
			}

			if( to is BaseCreature && !((BaseCreature)to).Controlled && ((BaseCreature)to).InitialInnocent )
				return true;

			int noto = Notoriety.Compute( from, to );

			return noto != Notoriety.Innocent || @from.Kills >= 5;
		}

		private static int[] m_Offsets = new int[]
			{
				-1, -1,
				-1,  0,
				-1,  1,
				0, -1,
				0,  1,
				1, -1,
				1,  0,
				1,  1
			};

		public static void Summon( BaseCreature creature, Mobile caster, int sound, TimeSpan duration, bool scaleDuration, bool scaleStats )
		{
			Map map = caster.Map;

			if( map == null )
				return;

			double scale = 1.0 + (caster.Skills[SkillName.Magery].Value - 100.0) / 200.0;

			if( scaleDuration )
				duration = TimeSpan.FromSeconds( duration.TotalSeconds * scale );

			if( scaleStats )
			{
				creature.RawStr = (int)(creature.RawStr * scale);
				creature.Hits = creature.HitsMax;

				creature.RawDex = (int)(creature.RawDex * scale);
				creature.Stam = creature.StamMax;

				creature.RawInt = (int)(creature.RawInt * scale);
				creature.Mana = creature.ManaMax;
			}

			Point3D p = new Point3D( caster );

			if( SpellHelper.FindValidSpawnLocation( map, ref p, true ) )
			{
				BaseCreature.Summon( creature, caster, p, sound, duration );
				return;
			}


			/*
			int offset = Utility.Random( 8 ) * 2;

			for( int i = 0; i < m_Offsets.Length; i += 2 )
			{
				int x = caster.X + m_Offsets[(offset + i) % m_Offsets.Length];
				int y = caster.Y + m_Offsets[(offset + i + 1) % m_Offsets.Length];

				if( map.CanSpawnMobile( x, y, caster.Z ) )
				{
					BaseCreature.Summon( creature, caster, new Point3D( x, y, caster.Z ), sound, duration );
					return;
				}
				else
				{
					int z = map.GetAverageZ( x, y );

					if( map.CanSpawnMobile( x, y, z ) )
					{
						BaseCreature.Summon( creature, caster, new Point3D( x, y, z ), sound, duration );
						return;
					}
				}
			}
			 * */

			creature.Delete();
			caster.SendLocalizedMessage( 501942 ); // That location is blocked.
		}

		public static bool FindValidSpawnLocation( Map map, ref Point3D p, bool surroundingsOnly )
		{
			if( map == null )	//sanity
				return false;

			if( !surroundingsOnly )
			{
				if( map.CanSpawnMobile( p ) )	//p's fine.
				{
					p = new Point3D( p );
					return true;
				}

				int z = map.GetAverageZ( p.X, p.Y );

				if( map.CanSpawnMobile( p.X, p.Y, z ) )
				{
					p = new Point3D( p.X, p.Y, z );
					return true;
				}
			}

			int offset = Utility.Random( 8 ) * 2;

			for( int i = 0; i < m_Offsets.Length; i += 2 )
			{
				int x = p.X + m_Offsets[(offset + i) % m_Offsets.Length];
				int y = p.Y + m_Offsets[(offset + i + 1) % m_Offsets.Length];

				if( map.CanSpawnMobile( x, y, p.Z ) )
				{
					p = new Point3D( x, y, p.Z );
					return true;
				}
				else
				{
					int z = map.GetAverageZ( x, y );

					if( map.CanSpawnMobile( x, y, z ) )
					{
						p = new Point3D( x, y, z );
						return true;
					}
				}
			}

			return false;
		}

		private delegate bool TravelValidator( Map map, Point3D loc );

		private static TravelValidator[] m_Validators = new TravelValidator[]
			{
				new TravelValidator( IsFeluccaT2A ),
				new TravelValidator( IsFeluccaWind ),
				new TravelValidator( IsFeluccaDungeon ),
				new TravelValidator( IsSafeZone ),
			};

		private static bool[,] m_Rules = new bool[,]
			{
					/*T2A(Fel),	Wind(Fel),	Dungeons(Fel),	SafeZone */
/* Recall From */	{ false,	false,		false,			true },
/* Recall To */		{ false,	false,		false,			false },
/* Gate From */		{ false,	false,		false,			false },
/* Gate To */		{ false,	false,		false,			false },
/* Mark In */		{ false,	false,		false,			false },
/* Tele From */		{ true,		true,		true,			true },
/* Tele To */		{ true,		true,		true,			false },
			};

		public static void SendInvalidMessage( Mobile caster, TravelCheckType type )
		{
			if( type == TravelCheckType.RecallTo || type == TravelCheckType.GateTo )
				caster.SendLocalizedMessage( 1019004 ); // You are not allowed to travel there.
			else if( type == TravelCheckType.TeleportTo )
				caster.SendLocalizedMessage( 501035 ); // You cannot teleport from here to the destination.
			else
				caster.SendLocalizedMessage( 501802 ); // Thy spell doth not appear to work...
		}

		public static bool CheckTravel( Mobile caster, TravelCheckType type )
		{
			return CheckTravel( caster, caster.Map, caster.Location, type );
		}

		public static bool CheckTravel( Map map, Point3D loc, TravelCheckType type )
		{
			return CheckTravel( null, map, loc, type );
		}

		private static Mobile m_TravelCaster;
		private static TravelCheckType m_TravelType;

		public static bool CheckTravel( Mobile caster, Map map, Point3D loc, TravelCheckType type )
		{
			if( IsInvalid( map, loc ) ) // null, internal, out of bounds
			{
				if( caster != null )
					SendInvalidMessage( caster, type );

				return false;
			}

			if( caster != null && caster.AccessLevel == AccessLevel.Player && caster.Region.IsPartOf( typeof( Regions.Jail ) ) )
			{
				caster.SendLocalizedMessage( 1114345 ); // You'll need a better jailbreak plan than that!
				return false;
			}

			// Always allow monsters to teleport
			if ( caster is BaseCreature && ( type == TravelCheckType.TeleportTo || type == TravelCheckType.TeleportFrom ) )
			{
				BaseCreature bc = (BaseCreature)caster;

				if ( !bc.Controlled && !bc.Summoned )
					return true;
			}

			m_TravelCaster = caster;
			m_TravelType = type;

			int v = (int)type;
			bool isValid = true;

			for( int i = 0; isValid && i < m_Validators.Length; ++i )
				isValid = m_Rules[v, i] || !m_Validators[i]( map, loc );

			if( !isValid && caster != null )
				SendInvalidMessage( caster, type );

			return isValid;
		}

		public static bool IsWindLoc( Point3D loc )
		{
			int x = loc.X, y = loc.Y;

			return x >= 5120 && y >= 0 && x < 5376 && y < 256;
		}

		public static bool IsFeluccaWind( Map map, Point3D loc )
		{
			return map == Map.Felucca && IsWindLoc( loc );
		}

		public static bool IsFeluccaT2A( Map map, Point3D loc )
		{
			int x = loc.X, y = loc.Y;

			return map == Map.Felucca && x >= 5120 && y >= 2304 && x < 6144 && y < 4096;
		}

		public static bool IsFeluccaDungeon( Map map, Point3D loc )
		{
			Region region = Region.Find( loc, map );
			return region.IsPartOf( typeof( DungeonRegion ) ) && region.Map == Map.Felucca;
		}

		public static bool IsSafeZone( Map map, Point3D loc )
		{
			return false;
		}

		public static bool IsInvalid( Map map, Point3D loc )
		{
			if( map == null || map == Map.Internal )
				return true;

			int x = loc.X, y = loc.Y;

			return x < 0 || y < 0 || x >= map.Width || y >= map.Height;
		}

		//towns
		public static bool IsTown( IPoint3D loc, Mobile caster )
		{
			if( loc is Item )
				loc = ((Item)loc).GetWorldLocation();

			return IsTown( new Point3D( loc ), caster );
		}

		public static bool IsTown( Point3D loc, Mobile caster )
		{
			Map map = caster.Map;

			if( map == null )
				return false;

			GuardedRegion reg = (GuardedRegion) Region.Find( loc, map ).GetRegion( typeof( GuardedRegion ) );

			return reg != null && !reg.IsDisabled();
		}

		public static bool CheckTown( IPoint3D loc, Mobile caster )
		{
			if( loc is Item )
				loc = ((Item)loc).GetWorldLocation();

			return CheckTown( new Point3D( loc ), caster );
		}

		public static bool CheckTown( Point3D loc, Mobile caster )
		{
			if( IsTown( loc, caster ) )
			{
				caster.SendLocalizedMessage( 500946 ); // You cannot cast this in town!
				return false;
			}

			return true;
		}

		//magic reflection
		public static void CheckReflect( int circle, Mobile caster, ref Mobile target )
		{
			CheckReflect( circle, ref caster, ref target );
		}

		public static void CheckReflect( int circle, ref Mobile caster, ref Mobile target )
		{
			if( target.MagicDamageAbsorb > 0 )
			{
				++circle;

				target.MagicDamageAbsorb -= circle;

				// This order isn't very intuitive, but you have to nullify reflect before target gets switched

				bool reflect = target.MagicDamageAbsorb >= 0;

				if( target is BaseCreature )
					((BaseCreature)target).CheckReflect( caster, ref reflect );

				if( target.MagicDamageAbsorb <= 0 )
				{
					target.MagicDamageAbsorb = 0;
					DefensiveSpell.Nullify( target );
				}

				if( reflect )
				{
					target.FixedEffect( 0x37B9, 10, 5 );

					Mobile temp = caster;
					caster = target;
					target = temp;
				}
			}
			else if( target is BaseCreature )
			{
				bool reflect = false;

				((BaseCreature)target).CheckReflect( caster, ref reflect );

				if( reflect )
				{
					target.FixedEffect( 0x37B9, 10, 5 );

					Mobile temp = caster;
					caster = target;
					target = temp;
				}
			}
		}

		public static void Damage( Spell spell, Mobile target, double damage )
		{
			TimeSpan ts = GetDamageDelayForSpell( spell );

			Damage( spell, ts, target, spell.Caster, damage );
		}

		public static void Damage( TimeSpan delay, Mobile target, double damage )
		{
			Damage( delay, target, null, damage );
		}

		public static void Damage( TimeSpan delay, Mobile target, Mobile from, double damage )
		{
			Damage( null, delay, target, from, damage );
		}

		public static void Damage( Spell spell, TimeSpan delay, Mobile target, Mobile from, double damage )
		{
			int iDamage = (int)damage;

			if( delay == TimeSpan.Zero )
			{
				if( from is BaseCreature )
					((BaseCreature)from).AlterSpellDamageTo( target, ref iDamage );

				if( target is BaseCreature )
					((BaseCreature)target).AlterSpellDamageFrom( from, ref iDamage );

				target.Damage( iDamage, from );
			}
			else
			{
				new SpellDamageTimer( spell, target, from, iDamage, delay ).Start();
			}

			if( target is BaseCreature && from != null && delay == TimeSpan.Zero )
			{
				BaseCreature c = (BaseCreature) target;

				c.OnHarmfulSpell( from );
				c.OnDamagedBySpell( from );
			}
		}

		public static void Damage( Spell spell, Mobile target, double damage, int phys, int fire, int cold, int pois, int nrgy )
		{
			TimeSpan ts = GetDamageDelayForSpell( spell );

			Damage( spell, ts, target, spell.Caster, damage, phys, fire, cold, pois, nrgy, DFAlgorithm.Standard );
		}

		public static void Damage( Spell spell, Mobile target, double damage, int phys, int fire, int cold, int pois, int nrgy, DFAlgorithm dfa )
		{
			TimeSpan ts = GetDamageDelayForSpell( spell );

			Damage( spell, ts, target, spell.Caster, damage, phys, fire, cold, pois, nrgy, dfa );
		}

		public static void Damage( TimeSpan delay, Mobile target, double damage, int phys, int fire, int cold, int pois, int nrgy )
		{
			Damage( delay, target, null, damage, phys, fire, cold, pois, nrgy );
		}

		public static void Damage( TimeSpan delay, Mobile target, Mobile from, double damage, int phys, int fire, int cold, int pois, int nrgy )
		{
			Damage( delay, target, from, damage, phys, fire, cold, pois, nrgy, DFAlgorithm.Standard );
		}

		public static void Damage( TimeSpan delay, Mobile target, Mobile from, double damage, int phys, int fire, int cold, int pois, int nrgy, DFAlgorithm dfa )
		{
			Damage( null, delay, target, from, damage, phys, fire, cold, pois, nrgy, dfa );
		}

		public static void Damage( Spell spell, TimeSpan delay, Mobile target, Mobile from, double damage, int phys, int fire, int cold, int pois, int nrgy, DFAlgorithm dfa )
		{
			int iDamage = (int)damage;

			if( delay == TimeSpan.Zero )
			{
				if( from is BaseCreature )
					((BaseCreature)from).AlterSpellDamageTo( target, ref iDamage );

				if( target is BaseCreature )
					((BaseCreature)target).AlterSpellDamageFrom( from, ref iDamage );

				WeightOverloading.DFA = dfa;

                target.Damage(iDamage, from);
			    int damageGiven = iDamage;

                if ( from != null ) // sanity check
				{
					DoLeech( damageGiven, from, target );
				}

				WeightOverloading.DFA = DFAlgorithm.Standard;
			}
			else
			{
				new SpellDamageTimerAOS( spell, target, from, iDamage, phys, fire, cold, pois, nrgy, delay, dfa ).Start();
			}

			if( target is BaseCreature && from != null && delay == TimeSpan.Zero )
			{
				BaseCreature c = (BaseCreature) target;

				c.OnHarmfulSpell( from );
				c.OnDamagedBySpell( from );
			}
		}

		public static void DoLeech( int damageGiven, Mobile from, Mobile target )
		{
		}

		public static void Heal( int amount, Mobile target, Mobile from )
		{
			Heal( amount, target, from, true );
		}
		public static void Heal( int amount, Mobile target, Mobile from, bool message )
		{
			//TODO: All Healing *spells* go through ArcaneEmpowerment
			target.Heal( amount, from, message );
		}

		private class SpellDamageTimer : Timer
		{
			private Mobile m_Target, m_From;
			private int m_Damage;
			private Spell m_Spell;

			public SpellDamageTimer( Spell s, Mobile target, Mobile from, int damage, TimeSpan delay )
				: base( delay )
			{
				m_Target = target;
				m_From = from;
				m_Damage = damage;
				m_Spell = s;

				if( m_Spell != null && m_Spell.DelayedDamage && !m_Spell.DelayedDamageStacking )
					m_Spell.StartDelayedDamageContext( target, this );

				Priority = TimerPriority.TwentyFiveMS;
			}

			protected override void OnTick()
			{
				if( m_From is BaseCreature )
					((BaseCreature)m_From).AlterSpellDamageTo( m_Target, ref m_Damage );

				if( m_Target is BaseCreature )
					((BaseCreature)m_Target).AlterSpellDamageFrom( m_From, ref m_Damage );

				m_Target.Damage( m_Damage );
				if( m_Spell != null )
					m_Spell.RemoveDelayedDamageContext( m_Target );
			}
		}

		private class SpellDamageTimerAOS : Timer
		{
			private Mobile m_Target, m_From;
			private int m_Damage;
			private int m_Phys, m_Fire, m_Cold, m_Pois, m_Nrgy;
			private DFAlgorithm m_DFA;
			private Spell m_Spell;

			public SpellDamageTimerAOS( Spell s, Mobile target, Mobile from, int damage, int phys, int fire, int cold, int pois, int nrgy, TimeSpan delay, DFAlgorithm dfa )
				: base( delay )
			{
				m_Target = target;
				m_From = from;
				m_Damage = damage;
				m_Phys = phys;
				m_Fire = fire;
				m_Cold = cold;
				m_Pois = pois;
				m_Nrgy = nrgy;
				m_DFA = dfa;
				m_Spell = s;
				if( m_Spell != null && m_Spell.DelayedDamage && !m_Spell.DelayedDamageStacking )
					m_Spell.StartDelayedDamageContext( target, this );

				Priority = TimerPriority.TwentyFiveMS;
			}

			protected override void OnTick()
			{
				if( m_From is BaseCreature && m_Target != null )
					((BaseCreature)m_From).AlterSpellDamageTo( m_Target, ref m_Damage );

				if( m_Target is BaseCreature && m_From != null )
					((BaseCreature)m_Target).AlterSpellDamageFrom( m_From, ref m_Damage );

				WeightOverloading.DFA = m_DFA;

                m_Target.Damage(m_Damage, m_From);
                int damageGiven = m_Damage;
                
                if ( m_From != null ) // sanity check
				{
					DoLeech( damageGiven, m_From, m_Target );
				}

				WeightOverloading.DFA = DFAlgorithm.Standard;

				if( m_Target is BaseCreature && m_From != null )
				{
					BaseCreature c = (BaseCreature) m_Target;

					c.OnHarmfulSpell( m_From );
					c.OnDamagedBySpell( m_From );
				}

				if( m_Spell != null )
					m_Spell.RemoveDelayedDamageContext( m_Target );

			}
		}
	}
}
