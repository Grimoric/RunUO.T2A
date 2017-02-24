using System;
using System.Collections;

namespace Server.Engines.ConPVP
{
    public class RulesetLayout
	{
		private static RulesetLayout m_Root;

		public static RulesetLayout Root
		{
			get
			{
				if ( m_Root == null )
				{
					ArrayList entries = new ArrayList();

					entries.Add( new RulesetLayout( "Spells", new RulesetLayout[]
						{
							new RulesetLayout( "1st Circle", "Spells", new string[]
							{
								"Reactive Armor", "Clumsy",
								"Create Food", "Feeblemind",
								"Heal", "Magic Arrow",
								"Night Sight", "Weaken"
							} ),
							new RulesetLayout( "2nd Circle", "Spells", new string[]
							{
								"Agility", "Cunning",
								"Cure", "Harm",
								"Magic Trap", "Untrap",
								"Protection", "Strength"
							} ),
							new RulesetLayout( "3rd Circle", "Spells", new string[]
							{
								"Bless", "Fireball",
								"Magic Lock", "Poison",
								"Telekinesis", "Teleport",
								"Unlock Spell", "Wall of Stone"
							} ),
							new RulesetLayout( "4th Circle", "Spells", new string[]
							{
								"Arch Cure", "Arch Protection",
								"Curse", "Fire Field",
								"Greater Heal", "Lightning",
								"Mana Drain", "Recall"
							} ),
							new RulesetLayout( "5th Circle", "Spells", new string[]
							{
								"Blade Spirits", "Dispel Field",
								"Incognito", "Magic Reflection",
								"Mind Blast", "Paralyze",
								"Poison Field", "Summon Creature"
							} ),
							new RulesetLayout( "6th Circle", "Spells", new string[]
							{
								"Dispel", "Energy Bolt",
								"Explosion", "Invisibility",
								"Mark", "Mass Curse",
								"Paralyze Field", "Reveal"
							} ),
							new RulesetLayout( "7th Circle", "Spells", new string[]
							{
								"Chain Lightning", "Energy Field",
								"Flame Strike", "Gate Travel",
								"Mana Vampire", "Mass Dispel",
								"Meteor Swarm", "Polymorph"
							} ),
							new RulesetLayout( "8th Circle", "Spells", new string[]
							{
								"Earthquake", "Energy Vortex",
								"Resurrection", "Air Elemental",
								"Summon Daemon", "Earth Elemental",
								"Fire Elemental", "Water Elemental"
							} )
						} ) );

					entries.Add( new RulesetLayout( "Combat Abilities", new string[]
						{
							"Stun",
							"Disarm",
							"Concussion Blow",
							"Crushing Blow",
							"Paralyzing Blow"
						} ) );

					entries.Add( new RulesetLayout( "Skills", new string[]
						{
							"Anatomy",
							"Detect Hidden",
							"Evaluating Intelligence",
							"Hiding",
							"Poisoning",
							"Snooping",
							"Stealing",
							"Spirit Speak",
							"Stealth"
						} ) );

					entries.Add( new RulesetLayout( "Weapons", new string[]
					{
						"Magical",
						"Melee",
						"Ranged",
						"Poisoned",
						"Wrestling",
						"Runics"
					} ) );

                    entries.Add( new RulesetLayout( "Armor", new string[]
						{
							"Magical",
							"Shields",
							"Colored"
						} ) );

					entries.Add( new RulesetLayout( "Items", new RulesetLayout[]
					{
						new RulesetLayout( "Potions", new string[]
						{
							"Agility",
							"Cure",
							"Explosion",
							"Heal",
							"Nightsight",
							"Poison",
							"Refresh",
							"Strength"
						} )
					},
						new string[]
					{
						"Bandages",
						"Wands",
						"Trapped Containers",
						"Bolas",
						"Mounts",
						"Orange Petals",
						"Fire Horns"
					} ) );

					m_Root = new RulesetLayout( "Rules", (RulesetLayout[])entries.ToArray( typeof( RulesetLayout ) ) );
					m_Root.ComputeOffsets();

					// Set up default rulesets

						#region Mage 5x
						Ruleset m5x = new Ruleset( m_Root );

						m5x.Title = "Mage 5x";

						m5x.SetOptionRange( "Spells", true );

						m5x.SetOption( "Spells", "Wall of Stone", false );
						m5x.SetOption( "Spells", "Fire Field", false );
						m5x.SetOption( "Spells", "Poison Field", false );
						m5x.SetOption( "Spells", "Energy Field", false );
						m5x.SetOption( "Spells", "Reactive Armor", false );
						m5x.SetOption( "Spells", "Protection", false );
						m5x.SetOption( "Spells", "Teleport", false );
						m5x.SetOption( "Spells", "Wall of Stone", false );
						m5x.SetOption( "Spells", "Arch Protection", false );
						m5x.SetOption( "Spells", "Recall", false );
						m5x.SetOption( "Spells", "Blade Spirits", false );
						m5x.SetOption( "Spells", "Incognito", false );
						m5x.SetOption( "Spells", "Magic Reflection", false );
						m5x.SetOption( "Spells", "Paralyze", false );
						m5x.SetOption( "Spells", "Summon Creature", false );
						m5x.SetOption( "Spells", "Invisibility", false );
						m5x.SetOption( "Spells", "Mark", false );
						m5x.SetOption( "Spells", "Paralyze Field", false );
						m5x.SetOption( "Spells", "Energy Field", false );
						m5x.SetOption( "Spells", "Gate Travel", false );
						m5x.SetOption( "Spells", "Polymorph", false );
						m5x.SetOption( "Spells", "Energy Vortex", false );
						m5x.SetOption( "Spells", "Air Elemental", false );
						m5x.SetOption( "Spells", "Summon Daemon", false );
						m5x.SetOption( "Spells", "Earth Elemental", false );
						m5x.SetOption( "Spells", "Fire Elemental", false );
						m5x.SetOption( "Spells", "Water Elemental", false );
						m5x.SetOption( "Spells", "Earthquake", false );
						m5x.SetOption( "Spells", "Meteor Swarm", false );
						m5x.SetOption( "Spells", "Chain Lightning", false );
						m5x.SetOption( "Spells", "Resurrection", false );

						m5x.SetOption( "Weapons", "Wrestling", true );

						m5x.SetOption( "Skills", "Anatomy", true );
						m5x.SetOption( "Skills", "Detect Hidden", true );
						m5x.SetOption( "Skills", "Evaluating Intelligence", true );

						m5x.SetOption( "Items", "Trapped Containers", true );
						#endregion

						#region Mage 7x
						Ruleset m7x = new Ruleset( m_Root );

						m7x.Title = "Mage 7x";

						m7x.SetOptionRange( "Spells", true );

						m7x.SetOption( "Spells", "Wall of Stone", false );
						m7x.SetOption( "Spells", "Fire Field", false );
						m7x.SetOption( "Spells", "Poison Field", false );
						m7x.SetOption( "Spells", "Energy Field", false );
						m7x.SetOption( "Spells", "Reactive Armor", false );
						m7x.SetOption( "Spells", "Protection", false );
						m7x.SetOption( "Spells", "Teleport", false );
						m7x.SetOption( "Spells", "Wall of Stone", false );
						m7x.SetOption( "Spells", "Arch Protection", false );
						m7x.SetOption( "Spells", "Recall", false );
						m7x.SetOption( "Spells", "Blade Spirits", false );
						m7x.SetOption( "Spells", "Incognito", false );
						m7x.SetOption( "Spells", "Magic Reflection", false );
						m7x.SetOption( "Spells", "Paralyze", false );
						m7x.SetOption( "Spells", "Summon Creature", false );
						m7x.SetOption( "Spells", "Invisibility", false );
						m7x.SetOption( "Spells", "Mark", false );
						m7x.SetOption( "Spells", "Paralyze Field", false );
						m7x.SetOption( "Spells", "Energy Field", false );
						m7x.SetOption( "Spells", "Gate Travel", false );
						m7x.SetOption( "Spells", "Polymorph", false );
						m7x.SetOption( "Spells", "Energy Vortex", false );
						m7x.SetOption( "Spells", "Air Elemental", false );
						m7x.SetOption( "Spells", "Summon Daemon", false );
						m7x.SetOption( "Spells", "Earth Elemental", false );
						m7x.SetOption( "Spells", "Fire Elemental", false );
						m7x.SetOption( "Spells", "Water Elemental", false );
						m7x.SetOption( "Spells", "Earthquake", false );
						m7x.SetOption( "Spells", "Meteor Swarm", false );
						m7x.SetOption( "Spells", "Chain Lightning", false );
						m7x.SetOption( "Spells", "Resurrection", false );

						m7x.SetOption( "Combat Abilities", "Stun", true );

						m7x.SetOption( "Skills", "Anatomy", true );
						m7x.SetOption( "Skills", "Detect Hidden", true );
						m7x.SetOption( "Skills", "Poisoning", true );
						m7x.SetOption( "Skills", "Evaluating Intelligence", true );

						m7x.SetOption( "Weapons", "Wrestling", true );

						m7x.SetOption( "Potions", "Refresh", true );
						m7x.SetOption( "Items", "Trapped Containers", true );
						m7x.SetOption( "Items", "Bandages", true );
						#endregion

						#region Standard 7x
						Ruleset s7x = new Ruleset( m_Root );

						s7x.Title = "Standard 7x";

						s7x.SetOptionRange( "Spells", true );

						s7x.SetOption( "Spells", "Wall of Stone", false );
						s7x.SetOption( "Spells", "Fire Field", false );
						s7x.SetOption( "Spells", "Poison Field", false );
						s7x.SetOption( "Spells", "Energy Field", false );
						s7x.SetOption( "Spells", "Teleport", false );
						s7x.SetOption( "Spells", "Wall of Stone", false );
						s7x.SetOption( "Spells", "Arch Protection", false );
						s7x.SetOption( "Spells", "Recall", false );
						s7x.SetOption( "Spells", "Blade Spirits", false );
						s7x.SetOption( "Spells", "Incognito", false );
						s7x.SetOption( "Spells", "Magic Reflection", false );
						s7x.SetOption( "Spells", "Paralyze", false );
						s7x.SetOption( "Spells", "Summon Creature", false );
						s7x.SetOption( "Spells", "Invisibility", false );
						s7x.SetOption( "Spells", "Mark", false );
						s7x.SetOption( "Spells", "Paralyze Field", false );
						s7x.SetOption( "Spells", "Energy Field", false );
						s7x.SetOption( "Spells", "Gate Travel", false );
						s7x.SetOption( "Spells", "Polymorph", false );
						s7x.SetOption( "Spells", "Energy Vortex", false );
						s7x.SetOption( "Spells", "Air Elemental", false );
						s7x.SetOption( "Spells", "Summon Daemon", false );
						s7x.SetOption( "Spells", "Earth Elemental", false );
						s7x.SetOption( "Spells", "Fire Elemental", false );
						s7x.SetOption( "Spells", "Water Elemental", false );
						s7x.SetOption( "Spells", "Earthquake", false );
						s7x.SetOption( "Spells", "Meteor Swarm", false );
						s7x.SetOption( "Spells", "Chain Lightning", false );
						s7x.SetOption( "Spells", "Resurrection", false );

						s7x.SetOptionRange( "Combat Abilities", true );

						s7x.SetOption( "Skills", "Anatomy", true );
						s7x.SetOption( "Skills", "Detect Hidden", true );
						s7x.SetOption( "Skills", "Poisoning", true );
						s7x.SetOption( "Skills", "Evaluating Intelligence", true );

						s7x.SetOptionRange( "Weapons", true );
						s7x.SetOption( "Weapons", "Runics", false );
						s7x.SetOptionRange( "Armor", true );

						s7x.SetOption( "Potions", "Refresh", true );
						s7x.SetOption( "Items", "Bandages", true );
						s7x.SetOption( "Items", "Trapped Containers", true );
						#endregion

						m_Root.Defaults = new Ruleset[] { m5x, m7x, s7x };

					// Set up flavors

					Ruleset pots = new Ruleset( m_Root );

					pots.Title = "Potions";

					pots.SetOptionRange( "Potions", true );
					pots.SetOption( "Potions", "Explosion", false );

					Ruleset para = new Ruleset( m_Root );

					para.Title = "Paralyze";
					para.SetOption( "Spells", "Paralyze", true );
					para.SetOption( "Spells", "Paralyze Field", true );
					para.SetOption( "Combat Abilities", "Paralyzing Blow", true );

					Ruleset fields = new Ruleset( m_Root );

					fields.Title = "Fields";
					fields.SetOption( "Spells", "Wall of Stone", true );
					fields.SetOption( "Spells", "Fire Field", true );
					fields.SetOption( "Spells", "Poison Field", true );
					fields.SetOption( "Spells", "Energy Field", true );
					fields.SetOption( "Spells", "Wildfire", true );

					Ruleset area = new Ruleset( m_Root );

					area.Title = "Area Effect";
					area.SetOption( "Spells", "Earthquake", true );
					area.SetOption( "Spells", "Meteor Swarm", true );
					area.SetOption( "Spells", "Chain Lightning", true );
					area.SetOption( "Necromancy", "Wither", true );
					area.SetOption( "Necromancy", "Poison Strike", true );

					Ruleset summons = new Ruleset( m_Root );

					summons.Title = "Summons";
					summons.SetOption( "Spells", "Blade Spirits", true );
					summons.SetOption( "Spells", "Energy Vortex", true );
					summons.SetOption( "Spells", "Air Elemental", true );
					summons.SetOption( "Spells", "Summon Daemon", true );
					summons.SetOption( "Spells", "Earth Elemental", true );
					summons.SetOption( "Spells", "Fire Elemental", true );
					summons.SetOption( "Spells", "Water Elemental", true );
					summons.SetOption( "Necromancy", "Summon Familiar", true );
					summons.SetOption( "Necromancy", "Vengeful Spirit", true );
					summons.SetOption( "Necromancy", "Animate Dead", true );
					summons.SetOption( "Ninjitsu", "Mirror Image", true );
					summons.SetOption( "Spellweaving", "Summon Fey", true );
					summons.SetOption( "Spellweaving", "Summon Fiend", true );
					summons.SetOption( "Spellweaving", "Nature's Fury", true );

					m_Root.Flavors = new Ruleset[]{ pots, para, fields, area, summons };
				}

				return m_Root;
			}
		}

		private string m_Title, m_Description;
		private string[] m_Options;

		private int m_Offset, m_TotalLength;

		private Ruleset[] m_Defaults;
		private Ruleset[] m_Flavors;

		private RulesetLayout m_Parent;
		private RulesetLayout[] m_Children;

		public string Title{ get{ return m_Title; } }
		public string Description{ get{ return m_Description; } }
		public string[] Options{ get{ return m_Options; } }

		public int Offset{ get{ return m_Offset; } }
		public int TotalLength{ get{ return m_TotalLength; } }

		public RulesetLayout Parent{ get{ return m_Parent; } }
		public RulesetLayout[] Children{ get{ return m_Children; } }

		public Ruleset[] Defaults{ get{ return m_Defaults; } set{ m_Defaults = value; } }
		public Ruleset[] Flavors{ get{ return m_Flavors; } set{ m_Flavors = value; } }

		public RulesetLayout FindByTitle( string title )
		{
			if ( m_Title == title )
				return this;

			for ( int i = 0; i < m_Children.Length; ++i )
			{
				RulesetLayout layout = m_Children[i].FindByTitle( title );

				if ( layout != null )
					return layout;
			}

			return null;
		}

		public string FindByIndex( int index )
		{
			if ( index >= m_Offset && index < m_Offset + m_Options.Length )
				return m_Description + ": " + m_Options[index - m_Offset];

			for ( int i = 0; i < m_Children.Length; ++i )
			{
				string opt = m_Children[i].FindByIndex( index );

				if ( opt != null )
					return opt;
			}

			return null;
		}

		public RulesetLayout FindByOption( string title, string option, ref int index )
		{
			if ( title == null || m_Title == title )
			{
				index = GetOptionIndex( option );

				if ( index >= 0 )
					return this;

				title = null;
			}

			for ( int i = 0; i < m_Children.Length; ++i )
			{
				RulesetLayout layout = m_Children[i].FindByOption( title, option, ref index );

				if ( layout != null )
					return layout;
			}

			return null;
		}

		public int GetOptionIndex( string option )
		{
			return Array.IndexOf( m_Options, option );
		}

		public void ComputeOffsets()
		{
			int offset = 0;

			RecurseComputeOffsets( ref offset );
		}

		private int RecurseComputeOffsets( ref int offset )
		{
			m_Offset = offset;

			offset += m_Options.Length;
			m_TotalLength += m_Options.Length;

			for ( int i = 0; i < m_Children.Length; ++i )
				m_TotalLength += m_Children[i].RecurseComputeOffsets( ref offset );

			return m_TotalLength;
		}

		public RulesetLayout( string title, string[] options ) : this( title, title, new RulesetLayout[0], options )
		{
		}

		public RulesetLayout( string title, string description, string[] options ) : this( title, description, new RulesetLayout[0], options )
		{
		}

		public RulesetLayout( string title, RulesetLayout[] children ) : this( title, title, children, new string[0] )
		{
		}

		public RulesetLayout( string title, string description, RulesetLayout[] children ) : this( title, description, children, new string[0] )
		{
		}

		public RulesetLayout( string title, RulesetLayout[] children, string[] options ) : this( title, title, children, options )
		{
		}

		public RulesetLayout( string title, string description, RulesetLayout[] children, string[] options )
		{
			m_Title = title;
			m_Description = description;
			m_Children = children;
			m_Options = options;

			for ( int i = 0; i < children.Length; ++i )
				children[i].m_Parent = this;
		}
	}
}