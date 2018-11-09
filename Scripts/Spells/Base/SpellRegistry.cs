using System;
using System.Collections.Generic;

namespace Server.Spells
{
    public class SpellRegistry
	{
		private static Type[] m_Types = new Type[700];
		private static int m_Count;

		private static Dictionary<Type, Int32> m_IDsFromTypes = new Dictionary<Type, Int32>( m_Types.Length );
		
		public static void Register( int spellID, Type type )
		{
			if ( spellID < 0 || spellID >= m_Types.Length )
				return;

			if ( m_Types[spellID] == null )
				++m_Count;

			m_Types[spellID] = type;

			if( !m_IDsFromTypes.ContainsKey( type ) )
				m_IDsFromTypes.Add( type, spellID );
		}

		private static object[] m_Params = new object[2];

		public static Spell NewSpell( int spellID, Mobile caster, Item scroll )
		{
			if ( spellID < 0 || spellID >= m_Types.Length )
				return null;

			Type t = m_Types[spellID];

			if( t != null )
			{
				m_Params[0] = caster;
				m_Params[1] = scroll;

				try
				{
					return (Spell)Activator.CreateInstance( t, m_Params );
				}
				catch
				{
				}
			}

			return null;
		}

		private static string[] m_CircleNames = new string[]
			{
				"First",
				"Second",
				"Third",
				"Fourth",
				"Fifth",
				"Sixth",
				"Seventh",
				"Eighth"
			};

		public static Spell NewSpell( string name, Mobile caster, Item scroll )
		{
			for ( int i = 0; i < m_CircleNames.Length; ++i )
			{
				Type t = ScriptCompiler.FindTypeByFullName( String.Format( "Server.Spells.{0}.{1}", m_CircleNames[i], name ) );

				if ( t != null )
				{
					m_Params[0] = caster;
					m_Params[1] = scroll;

					try
					{
						return (Spell)Activator.CreateInstance( t, m_Params );
					}
					catch
					{
					}
				}
			}

			return null;
		}
	}
}