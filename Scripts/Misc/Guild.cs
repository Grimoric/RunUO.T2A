using System;
using System.Collections.Generic;
using Server.Items;
using Server.Mobiles;
using Server.Gumps;
using Server.Network;
using Server.Targeting;
using Server.Commands;
using Server.Commands.Generic;

namespace Server.Guilds
{
    #region Ranks
    [Flags]
	public enum RankFlags
	{
		None				= 0x00000000,
		CanInvitePlayer		= 0x00000001,
		AccessGuildItems	= 0x00000002,
		RemoveLowestRank	= 0x00000004,
		RemovePlayers		= 0x00000008,
		CanPromoteDemote	= 0x00000010,
		ControlWarStatus	= 0x00000020,
		AllianceControl		= 0x00000040,
		CanSetGuildTitle	= 0x00000080,
		CanVote				= 0x00000100,

		All	= Member | CanInvitePlayer | RemovePlayers | CanPromoteDemote | ControlWarStatus | AllianceControl | CanSetGuildTitle,
		Member = RemoveLowestRank | AccessGuildItems | CanVote
	}

	public class RankDefinition
	{
		public static RankDefinition[] Ranks = new RankDefinition[]
			{
				new RankDefinition( 1062963, 0, RankFlags.None ),	//Ronin
				new RankDefinition( 1062962, 1, RankFlags.Member ),	//Member
				new RankDefinition( 1062961, 2, RankFlags.Member | RankFlags.RemovePlayers | RankFlags.CanInvitePlayer | RankFlags.CanSetGuildTitle | RankFlags.CanPromoteDemote ),	//Emmissary
				new RankDefinition( 1062960, 3, RankFlags.Member | RankFlags.ControlWarStatus ),	//Warlord
				new RankDefinition( 1062959, 4, RankFlags.All )	//Leader
			};
		public static RankDefinition Leader{ get{ return Ranks[4]; } }
		public static RankDefinition Member{ get{ return Ranks[1]; } }
		public static RankDefinition Lowest{ get{ return Ranks[0]; } }

		private TextDefinition m_Name;
		private int m_Rank;
		private RankFlags m_Flags;

		public TextDefinition Name{ get{ return m_Name; } }
		public int Rank{ get{ return m_Rank; } }
		public RankFlags Flags{ get{ return m_Flags; } }

		public RankDefinition( TextDefinition name, int rank, RankFlags flags )
		{
			m_Name = name; 
			m_Rank = rank;
			m_Flags = flags;
		}

		public bool GetFlag( RankFlags flag )
		{
			return (m_Flags & flag) != 0;
		}

		public void SetFlag( RankFlags flag, bool value )
		{
			if ( value )
				m_Flags |= flag;
			else
				m_Flags &= ~flag;
		}
	}

	#endregion

	public class Guild : BaseGuild
	{
		public static void Configure()
		{
			EventSink.CreateGuild += new CreateGuildHandler( EventSink_CreateGuild );
			EventSink.GuildGumpRequest += new GuildGumpRequestHandler( EventSink_GuildGumpRequest );

			CommandSystem.Register( "GuildProps", AccessLevel.Counselor, new CommandEventHandler( GuildProps_OnCommand ) );
		}

		#region GuildProps
		[Usage( "GuildProps" )]
		[Description( "Opens a menu where you can view and edit guild properties of a targeted player or guild stone.  If the new Guild system is active, also brings up the guild gump." )]
		private static void GuildProps_OnCommand( CommandEventArgs e )
		{
			string arg = e.ArgString.Trim();
			Mobile from = e.Mobile;

			if( arg.Length == 0 )
			{
				e.Mobile.Target = new GuildPropsTarget();
			}
			else
			{
				Guild g = null;

                int id;

                if( int.TryParse( arg, out id ) )
                    g = Guild.Find( id ) as Guild;

				if( g == null )
				{
					g = Guild.FindByAbbrev( arg ) as Guild;

					if( g == null )
						g = Guild.FindByName( arg ) as Guild;
				}

				if ( g != null )
				{
					from.SendGump( new PropertiesGump( from, g ) );
				}
			}

		}

		private class GuildPropsTarget : Target
		{
			public GuildPropsTarget() : base( -1, true, TargetFlags.None )
			{
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if( !BaseCommand.IsAccessible( from, o ) )
				{
					from.SendMessage( "That is not accessible." );
					return;
				}

				Guild g = null;

				if( o is Guildstone )
				{
					Guildstone stone = o as Guildstone;
					if( stone.Guild == null || stone.Guild.Disbanded )
					{
						from.SendMessage( "The guild associated with that Guildstone no longer exists" );
						return;
					}
					else
						g = stone.Guild;
				}
				else if( o is Mobile )
				{
					g = ((Mobile)o).Guild as Guild;
				}

				if( g != null )
				{
					from.SendGump( new PropertiesGump( from, g ) );
				}
				else
				{
					from.SendMessage( "That is not in a guild!" );
				}
			}
		}
		#endregion

		#region EventSinks
		public static void EventSink_GuildGumpRequest( GuildGumpRequestArgs args )
		{
			return;
		}

		public static BaseGuild EventSink_CreateGuild( CreateGuildEventArgs args )
		{
			return (BaseGuild)new Guild( args.Id );
		}
		#endregion

		public static readonly int RegistrationFee = 25000;
		public static readonly int AbbrevLimit = 4;
		public static readonly int NameLimit = 40;
		public static readonly int MajorityPercentage = 66;
		public static readonly TimeSpan InactiveTime = TimeSpan.FromDays( 30 );

		#region Var declarations
		private Mobile m_Leader;

		private string m_Name;
		private string m_Abbreviation;

		private List<Guild> m_Allies;
		private List<Guild> m_Enemies;

		private List<Mobile> m_Members;

		private Item m_Guildstone;
		private Item m_Teleporter;

		private string m_Charter;
		private string m_Website;

		private DateTime m_LastFealty;

		private GuildType m_Type;
		private DateTime m_TypeLastChange;

		private List<Guild> m_AllyDeclarations, m_AllyInvitations;

		private List<Guild> m_WarDeclarations, m_WarInvitations;
		private List<Mobile> m_Candidates, m_Accepted;
		#endregion

		public Guild( Mobile leader, string name, string abbreviation )
		{
			#region Ctor mumbo-jumbo
			m_Leader = leader;

			m_Members = new List<Mobile>();
			m_Allies = new List<Guild>();
			m_Enemies = new List<Guild>();
			m_WarDeclarations = new List<Guild>();
			m_WarInvitations = new List<Guild>();
			m_AllyDeclarations = new List<Guild>();
			m_AllyInvitations = new List<Guild>();
			m_Candidates = new List<Mobile>();
			m_Accepted = new List<Mobile>();

			m_LastFealty = DateTime.Now;

			m_Name = name;
			m_Abbreviation = abbreviation;

			m_TypeLastChange = DateTime.MinValue;

			AddMember( m_Leader );

			if( m_Leader is PlayerMobile )
				((PlayerMobile)m_Leader).GuildRank = RankDefinition.Leader;
			#endregion
		}

		public Guild( int id ) : base( id )//serialization ctor
		{
		}

		public void InvalidateMemberProperties()
		{
			InvalidateMemberProperties( false );
		}

		public void InvalidateMemberProperties( bool onlyOPL )
		{
			if ( m_Members != null )
			{
				for ( int i = 0; i < m_Members.Count; i++ )
				{
					Mobile m = m_Members[i];
					m.InvalidateProperties();

					if ( !onlyOPL )
						m.Delta( MobileDelta.Noto );
				}
			}
		}

		public void InvalidateMemberNotoriety()
		{
			if ( m_Members != null )
			{
				for (int i=0;i<m_Members.Count;i++)
					m_Members[i].Delta( MobileDelta.Noto );
			}
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Leader
		{
			get
			{
				if ( m_Leader == null || m_Leader.Deleted || m_Leader.Guild != this )
					CalculateGuildmaster();

				return m_Leader;
			}
			set
			{
				if( value != null )
					this.AddMember( value ); //Also removes from old guild.

				if( m_Leader is PlayerMobile && m_Leader.Guild == this )
					((PlayerMobile)m_Leader).GuildRank = RankDefinition.Member;

				m_Leader = value;

				if( m_Leader is PlayerMobile )
					((PlayerMobile)m_Leader).GuildRank = RankDefinition.Leader;
			}
		}


		public override bool Disbanded
		{
			get
			{
				return m_Leader == null || m_Leader.Deleted;
			}
		}

		public override void OnDelete( Mobile mob )
		{
			RemoveMember( mob );
		}


		public void Disband()
		{
			m_Leader = null;

			BaseGuild.List.Remove( this.Id );

			foreach ( Mobile m in m_Members )
			{
				m.SendLocalizedMessage( 502131 ); // Your guild has disbanded.

				if( m is PlayerMobile )
					((PlayerMobile)m).GuildRank = RankDefinition.Lowest;

				m.Guild = null;
			}

			m_Members.Clear();

			for ( int i = m_Allies.Count - 1; i >= 0; --i )
				if ( i < m_Allies.Count )
					RemoveAlly( m_Allies[i] );

			for ( int i = m_Enemies.Count - 1; i >= 0; --i )
				if ( i < m_Enemies.Count )
					RemoveEnemy( m_Enemies[i] );

			if ( m_Guildstone != null )
				m_Guildstone.Delete();

			m_Guildstone = null;
		}

		#region Is<something>(...)
		public bool IsMember( Mobile m )
		{
			return m_Members.Contains( m );
		}

		public bool IsAlly( Guild g )
		{
			return m_Allies.Contains( g );
		}

		public bool IsEnemy( Guild g )
		{
			if( Type != GuildType.Regular && g.Type != GuildType.Regular && Type != g.Type )
				return true;

			return IsWar( g );
		}

		public bool IsWar( Guild g )
		{
			if( g == null )
				return false;

			return m_Enemies.Contains( g );
		}
		#endregion

		#region Serialization
		public override void Serialize( GenericWriter writer )
		{
			if ( this.LastFealty+TimeSpan.FromDays( 1.0 ) < DateTime.Now )
				this.CalculateGuildmaster();

			writer.Write( (int) 5 );//version

			writer.WriteGuildList( m_AllyDeclarations, true );
			writer.WriteGuildList( m_AllyInvitations, true );

			writer.Write( m_TypeLastChange );

			writer.Write( (int)m_Type );

			writer.Write( m_LastFealty );

			writer.Write( m_Leader );
			writer.Write( m_Name );
			writer.Write( m_Abbreviation );

			writer.WriteGuildList<Guild>( m_Allies, true );
			writer.WriteGuildList<Guild>( m_Enemies, true );
			writer.WriteGuildList( m_WarDeclarations, true );
			writer.WriteGuildList( m_WarInvitations, true );

			writer.Write( m_Members, true );
			writer.Write( m_Candidates, true );
			writer.Write( m_Accepted, true );

			writer.Write( m_Guildstone );
			writer.Write( m_Teleporter );

			writer.Write( m_Charter );
			writer.Write( m_Website );
		}

		public override void Deserialize( GenericReader reader )
		{
			int version = reader.ReadInt();

			switch ( version )
			{
				case 5:
			    case 4:
				{
					m_AllyDeclarations = reader.ReadStrongGuildList<Guild>();
					m_AllyInvitations = reader.ReadStrongGuildList<Guild>();

					goto case 3;
				}
				case 3:
				{
					m_TypeLastChange = reader.ReadDateTime();

					goto case 2;
				}
				case 2:
				{
					m_Type = (GuildType)reader.ReadInt();

					goto case 1;
				}
				case 1:
				{
					m_LastFealty = reader.ReadDateTime();

					goto case 0;
				}
				case 0:
				{
					m_Leader = reader.ReadMobile();

					if( m_Leader is PlayerMobile )
						((PlayerMobile)m_Leader).GuildRank = RankDefinition.Leader;

					m_Name = reader.ReadString();
					m_Abbreviation = reader.ReadString();

					m_Allies = reader.ReadStrongGuildList<Guild>();
					m_Enemies = reader.ReadStrongGuildList<Guild>();
					m_WarDeclarations = reader.ReadStrongGuildList<Guild>();
					m_WarInvitations = reader.ReadStrongGuildList<Guild>();

					m_Members = reader.ReadStrongMobileList();
					m_Candidates = reader.ReadStrongMobileList();
					m_Accepted = reader.ReadStrongMobileList(); 

					m_Guildstone = reader.ReadItem();
					m_Teleporter = reader.ReadItem();

					m_Charter = reader.ReadString();
					m_Website = reader.ReadString();

					break;
				}
			}

			if ( m_AllyDeclarations == null )
				m_AllyDeclarations = new List<Guild>();

			if ( m_AllyInvitations == null )
				m_AllyInvitations = new List<Guild>();

			Timer.DelayCall( TimeSpan.Zero, new TimerCallback( VerifyGuild_Callback ) );
		}

		private void VerifyGuild_Callback()
		{
			if( m_Guildstone == null || m_Members.Count == 0 )
				Disband();
		}

		#endregion

		#region Add/Remove Member/Old Ally/Old Enemy
		public void AddMember( Mobile m )
		{
			if ( !m_Members.Contains( m ) )
			{
				if ( m.Guild != null && m.Guild != this )
					((Guild)m.Guild).RemoveMember( m );

				m_Members.Add( m );
				m.Guild = this;
				m.GuildFealty = m_Leader;

				if( m is PlayerMobile )
					((PlayerMobile)m).GuildRank = RankDefinition.Lowest;
			}
		}

		public void RemoveMember( Mobile m )
		{
			RemoveMember( m, 1018028 ); // You have been dismissed from your guild.
		}
		public void RemoveMember( Mobile m, int message )
		{
			if ( m_Members.Contains( m ) )
			{
				m_Members.Remove( m );
				
				Guild guild = m.Guild as Guild;
				
				m.Guild = null;

				if( m is PlayerMobile )
					((PlayerMobile)m).GuildRank = RankDefinition.Lowest;

				if( message > 0 )
					m.SendLocalizedMessage( message );

				if ( m == m_Leader )
				{
					CalculateGuildmaster();

					if ( m_Leader == null )
						Disband();
				}

				if ( m_Members.Count == 0 )
					Disband();
				
				m.Delta( MobileDelta.Noto );
			}
		}

		public void AddAlly( Guild g )
		{
			if ( !m_Allies.Contains( g ) )
			{
				m_Allies.Add( g );

				g.AddAlly( this );
			}
		}

		public void RemoveAlly( Guild g )
		{
			if ( m_Allies.Contains( g ) )
			{
				m_Allies.Remove( g );

				g.RemoveAlly( this );
			}
		}

		public void AddEnemy( Guild g )
		{
			if ( !m_Enemies.Contains( g ) )
			{
				m_Enemies.Add( g );

				g.AddEnemy( this );
			}
		}

		public void RemoveEnemy( Guild g )
		{
			if ( m_Enemies != null && m_Enemies.Contains( g ) )
			{
				m_Enemies.Remove( g );

				g.RemoveEnemy( this );
			}
		}

		#endregion

		#region Guild[Text]Message(...)
		public void GuildMessage( int num, bool append, string format, params object[] args )
		{
			GuildMessage( num, append, String.Format( format, args) );
		}
		public void GuildMessage( int number )
		{
			for ( int i = 0; i < m_Members.Count; ++i )
				m_Members[i].SendLocalizedMessage( number );
		}
		public void GuildMessage( int number, string args )
		{
			GuildMessage( number, args, 0x3B2 );
		}
		public void GuildMessage( int number, string args, int hue )
		{
			for ( int i = 0; i < m_Members.Count; ++i )
				m_Members[i].SendLocalizedMessage( number, args, hue );
		}
		public void GuildMessage( int number, bool append, string affix )
		{
			GuildMessage( number, append, affix, "", 0x3B2 );
		}
		public void GuildMessage( int number, bool append, string affix, string args )
		{
			GuildMessage( number, append, affix, args, 0x3B2 );
		}
		public void GuildMessage( int number, bool append, string affix, string args, int hue )
		{
			for ( int i = 0; i < m_Members.Count; ++i )
				m_Members[i].SendLocalizedMessage( number, append, affix, args, hue );
		}

		public void GuildTextMessage( string text )
		{
			GuildTextMessage( 0x3B2, text );
		}
		public void GuildTextMessage( string format, params object[] args )
		{
			GuildTextMessage( 0x3B2, String.Format( format, args ) );
		}
		public void GuildTextMessage( int hue, string text )
		{
			for( int i = 0; i < m_Members.Count; ++i )
				m_Members[i].SendMessage( hue, text );
		}
		public void GuildTextMessage( int hue, string format, params object[] args )
		{
			GuildTextMessage( hue, String.Format( format, args ) );
		}

		public void GuildChat( Mobile from, int hue, string text )
		{
			Packet p = null;
			for( int i = 0; i < m_Members.Count; i++ )
			{
				Mobile m = m_Members[i];

				NetState state = m.NetState;

				if( state != null )
				{
					if( p == null )
						p = Packet.Acquire( new UnicodeMessage( from.Serial, from.Body, MessageType.Guild, hue, 3, from.Language, from.Name, text ) );

					state.Send( p );
				}
			}

			Packet.Release( p );
		}

		public void GuildChat( Mobile from, string text )
		{
			PlayerMobile pm = from as PlayerMobile;

			GuildChat( from, pm == null ? 0x3B2 : pm.GuildMessageHue, text );
		}
		#endregion

		#region Voting
		public bool CanVote( Mobile m )
		{
			return m != null && !m.Deleted && m.Guild == this;
		}
		public bool CanBeVotedFor( Mobile m )
		{
			return m != null && !m.Deleted && m.Guild == this;
		}

		public void CalculateGuildmaster()
		{
			Dictionary<Mobile, int> votes = new Dictionary<Mobile, int>();

			int votingMembers = 0;

			for ( int i = 0; m_Members != null && i < m_Members.Count; ++i )
			{
				Mobile memb = m_Members[i];

				if ( !CanVote( memb ) )
					continue;

				Mobile m = memb.GuildFealty;

				if( !CanBeVotedFor( m ) )
				{
					if ( m_Leader != null && !m_Leader.Deleted && m_Leader.Guild == this )
						m = m_Leader;
					else 
						m = memb;
				}

				if ( m == null )
					continue;

				int v;

				if( !votes.TryGetValue( m, out v ) )
					votes[m] = 1;
				else
					votes[m] = v + 1;
				
				votingMembers++;
			}

			Mobile winner = null;
			int highVotes = 0;

			foreach ( KeyValuePair<Mobile, int> kvp in votes )
			{
				Mobile m = (Mobile)kvp.Key;
				int val = (int)kvp.Value;

				if ( winner == null || val > highVotes )
				{
					winner = m;
					highVotes = val;
				}
			}

			if ( m_Leader != winner && winner != null )
				GuildMessage( 1018015, true, winner.Name ); // Guild Message: Guildmaster changed to:

			Leader = winner;
			m_LastFealty = DateTime.Now;
		}

		#endregion

		#region Getters & Setters
		[CommandProperty( AccessLevel.GameMaster )]
		public Item Guildstone
		{
			get
			{
				return m_Guildstone;
			}
			set
			{
				m_Guildstone = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Item Teleporter
		{
			get
			{
				return m_Teleporter;
			}
			set
			{
				m_Teleporter = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public override string Name
		{
			get
			{
				return m_Name;
			}
			set
			{
				m_Name = value;

				InvalidateMemberProperties( true );

				if ( m_Guildstone != null )
					m_Guildstone.InvalidateProperties();
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string Website
		{
			get
			{
				return m_Website;
			}
			set
			{
				m_Website = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public override string Abbreviation
		{
			get
			{
				return m_Abbreviation;
			}
			set
			{
				m_Abbreviation = value;

				InvalidateMemberProperties( true );

				if( m_Guildstone != null )
					m_Guildstone.InvalidateProperties();
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string Charter
		{
			get
			{
				return m_Charter;
			}
			set
			{
				m_Charter = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public override GuildType Type
		{
			get
			{
				return GuildType.Regular;
			}
			set
			{
				if ( m_Type != value )
				{
					m_Type = value;
					m_TypeLastChange = DateTime.Now;

					InvalidateMemberProperties();
				}
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime LastFealty
		{
			get
			{
				return m_LastFealty;
			}
			set
			{
				m_LastFealty = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime TypeLastChange
		{
			get
			{
				return m_TypeLastChange;
			}
		}

		public List<Guild> Allies
		{
			get
			{
				return m_Allies;
			}
		}

		public List<Guild> Enemies
		{
			get
			{
				return m_Enemies;
			}
		}

		public List<Guild> AllyDeclarations
		{
			get
			{
				return m_AllyDeclarations;
			}
		}

		public List<Guild> AllyInvitations
		{
			get
			{
				return m_AllyInvitations;
			}
		}

		public List<Guild> WarDeclarations
		{
			get
			{
				return m_WarDeclarations;
			}
		}

		public List<Guild> WarInvitations
		{
			get
			{
				return m_WarInvitations;
			}
		}

		public List<Mobile> Candidates
		{
			get
			{
				return m_Candidates;
			}
		}

		public List<Mobile> Accepted
		{
			get
			{
				return m_Accepted;
			}
		}

		public List<Mobile> Members
		{
			get
			{
				return m_Members;
			}
		}

		#endregion
	}
}
