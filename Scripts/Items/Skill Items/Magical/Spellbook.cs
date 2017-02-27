using System;
using System.Collections.Generic;
using Server.Commands;
using Server.Engines.Craft;
using Server.Network;
using Server.Spells;
using Server.Targeting;

namespace Server.Items
{
    public enum SpellbookType
	{
		Invalid = -1,
		Regular
	}

	public enum BookQuality
	{
		Regular,
		Exceptional,
	}

	public class Spellbook : Item, ICraftable, ISlayer
	{
		private string m_EngravedText;
		private BookQuality m_Quality;
				
		[CommandProperty( AccessLevel.GameMaster )]		
		public string EngravedText
		{
			get{ return m_EngravedText; }
			set{ m_EngravedText = value; InvalidateProperties(); }
		}
				
		[CommandProperty( AccessLevel.GameMaster )]		
		public BookQuality Quality
		{
			get{ return m_Quality; }
			set{ m_Quality = value; InvalidateProperties(); }
		}

		public static void Initialize()
		{
			EventSink.OpenSpellbookRequest += new OpenSpellbookRequestEventHandler( EventSink_OpenSpellbookRequest );
			EventSink.CastSpellRequest += new CastSpellRequestEventHandler( EventSink_CastSpellRequest );

			CommandSystem.Register( "AllSpells", AccessLevel.GameMaster, new CommandEventHandler( AllSpells_OnCommand ) );
		}

		[Usage( "AllSpells" )]
		[Description( "Completely fills a targeted spellbook with scrolls." )]
		private static void AllSpells_OnCommand( CommandEventArgs e )
		{
			e.Mobile.BeginTarget( -1, false, TargetFlags.None, new TargetCallback( AllSpells_OnTarget ) );
			e.Mobile.SendMessage( "Target the spellbook to fill." );
		}

		private static void AllSpells_OnTarget( Mobile from, object obj )
		{
			if ( obj is Spellbook )
			{
				Spellbook book = (Spellbook)obj;

				if ( book.BookCount == 64 )
					book.Content = ulong.MaxValue;
				else
					book.Content = (1ul << book.BookCount) - 1;

				from.SendMessage( "The spellbook has been filled." );

				CommandLogging.WriteLine( from, "{0} {1} filling spellbook {2}", from.AccessLevel, CommandLogging.Format( from ), CommandLogging.Format( book ) );
			}
			else
			{
				from.BeginTarget( -1, false, TargetFlags.None, new TargetCallback( AllSpells_OnTarget ) );
				from.SendMessage( "That is not a spellbook. Try again." );
			}
		}

		private static void EventSink_OpenSpellbookRequest( OpenSpellbookRequestEventArgs e )
		{
			Mobile from = e.Mobile;

			Spellbook.Find( from, -1, SpellbookType.Regular).DisplayTo( from );
		}

		private static void EventSink_CastSpellRequest( CastSpellRequestEventArgs e )
		{
			Mobile from = e.Mobile;

			Spellbook book = e.Spellbook as Spellbook;
			int spellID = e.SpellID;

			if ( book == null || !book.HasSpell( spellID ) )
				book = Find( from, spellID );

			if ( book != null && book.HasSpell( spellID ) )
			{
				Spell spell = SpellRegistry.NewSpell( spellID, from, null );
	
				if ( spell != null )
					spell.Cast();
				else
					from.SendLocalizedMessage( 502345 ); // This spell has been temporarily disabled.
			}
			else
			{
				from.SendLocalizedMessage( 500015 ); // You do not have that spell!
			}
		}

		private static Dictionary<Mobile, List<Spellbook>> m_Table = new Dictionary<Mobile, List<Spellbook>>();

		public static SpellbookType GetTypeForSpell( int spellID )
		{
			if ( spellID >= 0 && spellID < 64 )
				return SpellbookType.Regular;

			return SpellbookType.Invalid;
		}

		public static Spellbook Find( Mobile from, int spellID )
		{
			return Find( from, spellID, GetTypeForSpell( spellID ) );
		}

		public static Spellbook Find( Mobile from, int spellID, SpellbookType type )
		{
			if ( from == null )
				return null;

			if ( from.Deleted )
			{
				m_Table.Remove( from );
				return null;
			}

			List<Spellbook> list = null;

			m_Table.TryGetValue( from, out list );

			bool searchAgain = false;

			if ( list == null )
				m_Table[from] = list = FindAllSpellbooks( from );
			else
				searchAgain = true;

			Spellbook book = FindSpellbookInList( list, from, spellID, type );

			if ( book == null && searchAgain )
			{
				m_Table[from] = list = FindAllSpellbooks( from );

				book = FindSpellbookInList( list, from, spellID, type );
			}

			return book;
		}

		public static Spellbook FindSpellbookInList( List<Spellbook> list, Mobile from, int spellID, SpellbookType type )
		{
			Container pack = from.Backpack;

			for ( int i = list.Count - 1; i >= 0; --i )
			{
				if ( i >= list.Count )
					continue;

				Spellbook book = list[i];

				if ( !book.Deleted && (book.Parent == from || pack != null && book.Parent == pack) && ValidateSpellbook( book, spellID, type ) )
					return book;

				list.RemoveAt( i );
			}

			return null;
		}

		public static List<Spellbook> FindAllSpellbooks( Mobile from )
		{
			List<Spellbook> list = new List<Spellbook>();

			Item item = from.FindItemOnLayer( Layer.OneHanded );

			if ( item is Spellbook )
				list.Add( (Spellbook)item );

			Container pack = from.Backpack;

			if ( pack == null )
				return list;

			for ( int i = 0; i < pack.Items.Count; ++i )
			{
				item = pack.Items[i];

				if ( item is Spellbook )
					list.Add( (Spellbook)item );
			}

			return list;
		}

		public static Spellbook FindEquippedSpellbook( Mobile from )
		{
			return @from.FindItemOnLayer( Layer.OneHanded ) as Spellbook;
		}

		public static bool ValidateSpellbook( Spellbook book, int spellID, SpellbookType type )
		{
			return book.SpellbookType == type && ( spellID == -1 || book.HasSpell( spellID ) );
		}

		public override bool DisplayWeight { get { return false; } }

		public virtual SpellbookType SpellbookType{ get{ return SpellbookType.Regular; } }
		public virtual int BookOffset{ get{ return 0; } }
		public virtual int BookCount{ get{ return 64; } }

		private ulong m_Content;
		private int m_Count;

		public override bool CanEquip( Mobile from )
		{
			if ( !from.CanBeginAction( typeof( BaseWeapon ) ) )
			{
				return false;
			}

			return base.CanEquip( from );
		}

		public override bool AllowEquipedCast( Mobile from )
		{
			return true;
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( dropped is SpellScroll && dropped.Amount == 1 )
			{
				SpellScroll scroll = (SpellScroll)dropped;

				SpellbookType type = GetTypeForSpell( scroll.SpellID );

				if ( type != this.SpellbookType )
				{
					return false;
				}
				else if ( HasSpell( scroll.SpellID ) )
				{
					from.SendLocalizedMessage( 500179 ); // That spell is already present in that spellbook.
					return false;
				}
				else
				{
					int val = scroll.SpellID - BookOffset;

					if ( val >= 0 && val < BookCount )
					{
						m_Content |= (ulong)1 << val;
						++m_Count;

						InvalidateProperties();

						scroll.Delete();

						from.Send( new PlaySound( 0x249, GetWorldLocation() ) );
						return true;
					}

					return false;
				}
			}
			else
			{
				return false;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public ulong Content
		{
			get
			{
				return m_Content;
			}
			set
			{
				if ( m_Content != value )
				{
					m_Content = value;

					m_Count = 0;

					while ( value > 0 )
					{
						m_Count += (int)(value & 0x1);
						value >>= 1;
					}

					InvalidateProperties();
				}
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int SpellCount
		{
			get
			{
				return m_Count;
			}
		}

		[Constructable]
		public Spellbook() : this( (ulong)0 )
		{
		}

		[Constructable]
		public Spellbook( ulong content ) : this( content, 0xEFA )
		{
		}

		public Spellbook( ulong content, int itemID ) : base( itemID )
		{
			Weight = 3.0;
			Layer = Layer.OneHanded;
			LootType = LootType.Blessed;

			Content = content;
		}

		public bool HasSpell( int spellID )
		{
			spellID -= BookOffset;

			return spellID >= 0 && spellID < BookCount && (m_Content & ((ulong)1 << spellID)) != 0;
		}

		public Spellbook( Serial serial ) : base( serial )
		{
		}

		public void DisplayTo( Mobile to )
		{
			// The client must know about the spellbook or it will crash!

			NetState ns = to.NetState;

			if ( ns == null )
				return;

			if ( Parent == null )
			{
				to.Send( this.WorldPacket );
			}
			else if ( Parent is Item )
			{
				// What will happen if the client doesn't know about our parent?
				if ( ns.ContainerGridLines )
					to.Send( new ContainerContentUpdate6017( this ) );
				else
					to.Send( new ContainerContentUpdate( this ) );
			}
			else if ( Parent is Mobile )
			{
				// What will happen if the client doesn't know about our parent?
				to.Send( new EquipUpdate( this ) );
			}

			if ( ns.HighSeas )
				to.Send( new DisplaySpellbookHS( this ) );
			else
				to.Send( new DisplaySpellbook( this ) );

			if ( ns.ContainerGridLines )
				to.Send( new SpellbookContent6017( m_Count, BookOffset + 1, m_Content, this ) );
            else
                to.Send( new SpellbookContent( m_Count, BookOffset + 1, m_Content, this ) );
		}

		private Mobile m_Crafter;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Crafter
		{
			get{ return m_Crafter; }
			set{ m_Crafter = value; InvalidateProperties(); }
		}

		public override bool DisplayLootType{ get{ return false; } }

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
	
			if ( m_Quality == BookQuality.Exceptional )
				list.Add( 1063341 ); // exceptional
				
			if ( m_EngravedText != null )
				list.Add( 1072305, m_EngravedText ); // Engraved: ~1_INSCRIPTION~

			if ( m_Crafter != null )
				list.Add( 1050043, m_Crafter.Name ); // crafted by ~1_NAME~
				
			if( m_Slayer != SlayerName.None )
			{
				SlayerEntry entry = SlayerGroup.GetEntryByName( m_Slayer );
				if( entry != null )
					list.Add( entry.Title );
			}

			if( m_Slayer2 != SlayerName.None )
			{
				SlayerEntry entry = SlayerGroup.GetEntryByName( m_Slayer2 );
				if( entry != null )
					list.Add( entry.Title );
			}

			list.Add( 1042886, m_Count.ToString() ); // ~1_NUMBERS_OF_SPELLS~ Spells
		}

		public override void OnSingleClick( Mobile from )
		{
			base.OnSingleClick( from );

			if ( m_Crafter != null )
				this.LabelTo( from, 1050043, m_Crafter.Name ); // crafted by ~1_NAME~

			this.LabelTo( from, 1042886, m_Count.ToString() );
		}

		public override void OnDoubleClick( Mobile from )
		{
			Container pack = from.Backpack;

			if ( Parent == from || pack != null && Parent == pack )
				DisplayTo( from );
			else
				from.SendLocalizedMessage( 500207 ); // The spellbook must be in your backpack (and not in a container within) to open.
		}


		private SlayerName m_Slayer;
		private SlayerName m_Slayer2;
		//Currently though there are no dual slayer spellbooks, OSI has a habit of putting dual slayer stuff in later

		[CommandProperty( AccessLevel.GameMaster )]
		public SlayerName Slayer
		{
			get { return m_Slayer; }
			set { m_Slayer = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public SlayerName Slayer2
		{
			get { return m_Slayer2; }
			set { m_Slayer2 = value; InvalidateProperties(); }
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 5 ); // version

			writer.Write( (byte) m_Quality );	
		
			writer.Write( (string) m_EngravedText );	

			writer.Write( m_Crafter );

			writer.Write( (int)m_Slayer );
			writer.Write( (int)m_Slayer2 );

			writer.Write( m_Content );
			writer.Write( m_Count );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 5:
				{
					m_Quality = (BookQuality) reader.ReadByte();		

					goto case 4;
				}
				case 4:
				{
					m_EngravedText = reader.ReadString();		

					goto case 3;
				}
				case 3:
				{
					m_Crafter = reader.ReadMobile();
					goto case 2;
				}
				case 2:
				{
					m_Slayer = (SlayerName)reader.ReadInt();
					m_Slayer2 = (SlayerName)reader.ReadInt();
					goto case 1;
				}
				case 1:
				case 0:
				{
					m_Content = reader.ReadULong();
					m_Count = reader.ReadInt();

					break;
				}
			}

			if ( Parent is Mobile )
				((Mobile)Parent).CheckStatTimers();
		}

		private static int[] m_LegendPropertyCounts = new int[]
			{
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,	// 0 properties : 21/52 : 40%
				1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,					// 1 property   : 15/52 : 29%
				2, 2, 2, 2, 2, 2, 2, 2, 2, 2,									// 2 properties : 10/52 : 19%
				3, 3, 3, 3, 3, 3												// 3 properties :  6/52 : 12%

			};

		private static int[] m_ElderPropertyCounts = new int[]
			{
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,// 0 properties : 15/34 : 44%
				1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 				// 1 property   : 10/34 : 29%
				2, 2, 2, 2, 2, 2,							// 2 properties :  6/34 : 18%
				3, 3, 3										// 3 properties :  3/34 :  9%
			};

		private static int[] m_GrandPropertyCounts = new int[]
			{
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,	// 0 properties : 10/20 : 50%
				1, 1, 1, 1, 1, 1,				// 1 property   :  6/20 : 30%
				2, 2, 2,						// 2 properties :  3/20 : 15%
				3								// 3 properties :  1/20 :  5%
			};

		private static int[] m_MasterPropertyCounts = new int[]
			{
				0, 0, 0, 0, 0, 0,				// 0 properties : 6/10 : 60%
				1, 1, 1,						// 1 property   : 3/10 : 30%
				2								// 2 properties : 1/10 : 10%
			};

		private static int[] m_AdeptPropertyCounts = new int[]
			{
				0, 0, 0,						// 0 properties : 3/4 : 75%
				1								// 1 property   : 1/4 : 25%
			};

		public virtual int OnCraft( int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, BaseTool tool, CraftItem craftItem, int resHue )
		{
			if ( makersMark )
				Crafter = from;

			m_Quality = (BookQuality) ( quality - 1 );

			return quality;
		}
	}
}