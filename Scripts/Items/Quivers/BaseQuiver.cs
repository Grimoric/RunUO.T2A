using System;
using Server.Engines.Craft;

namespace Server.Items
{
    public class BaseQuiver : Container, ICraftable
	{
		public override int DefaultGumpID{ get{ return 0x108; } }
		public override int DefaultMaxItems{ get{ return 1; } }
		public override int DefaultMaxWeight{ get{ return 50; } }
		public override double DefaultWeight{ get{ return 2.0; } }

		private int m_Capacity;
		private int m_LowerAmmoCost;
		private int m_WeightReduction;
		private int m_DamageIncrease;

		[CommandProperty( AccessLevel.GameMaster)]
		public int Capacity
		{
			get{ return m_Capacity; }
			set{ m_Capacity = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster)]
		public int LowerAmmoCost
		{
			get{ return m_LowerAmmoCost; }
			set{ m_LowerAmmoCost = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster)]
		public int WeightReduction
		{
			get{ return m_WeightReduction; }
			set{ m_WeightReduction = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster)]
		public int DamageIncrease
		{
			get{ return m_DamageIncrease; }
			set{ m_DamageIncrease = value; InvalidateProperties(); }
		}

		private Mobile m_Crafter;
		private ClothingQuality m_Quality;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Crafter
		{
			get{ return m_Crafter; }
			set{ m_Crafter = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public ClothingQuality Quality
		{
			get{ return m_Quality; }
			set{ m_Quality = value; InvalidateProperties(); }
		}
		
		public Item Ammo
		{
			get{ return Items.Count > 0 ? Items[ 0 ] : null; }
		}

		public BaseQuiver() : this( 0x2FB7 )
		{
		}

		public BaseQuiver( int itemID ) : base( itemID )
		{
			Weight = 2.0;
			Capacity = 500;
			Layer = Layer.Cloak;

			DamageIncrease = 10;
		}

		public BaseQuiver( Serial serial ) : base( serial )
		{
		}

		public override void  UpdateTotal( Item sender, TotalType type, int delta )
		{
			InvalidateProperties();

 			base.UpdateTotal(sender, type, delta);
		}

		public override int GetTotal( TotalType type )
		{
			int total = base.GetTotal( type );

			if ( type == TotalType.Weight )
				total -= total * m_WeightReduction / 100;

			return total;
		}

		private static Type[] m_Ammo = new Type[]
		{
			typeof( Arrow ), typeof( Bolt )
		};

		public bool CheckType( Item item )
		{
			Type type = item.GetType();
			Item ammo = Ammo;

			if ( ammo != null )
			{
				if ( ammo.GetType() == type )
					return true;
			}
			else
			{
				for ( int i = 0; i < m_Ammo.Length; i++ )
				{
					if ( type == m_Ammo[ i ] )
						return true;
				}
			}

			return false;
		}

		public override bool CheckHold( Mobile m, Item item, bool message, bool checkItems, int plusItems, int plusWeight )
		{
			if ( !CheckType( item ) )
			{
				if ( message )
					m.SendLocalizedMessage( 1074836 ); // The container can not hold that type of object.

				return false;
			}

			if ( Items.Count < DefaultMaxItems )
			{
				if ( item.Amount <= m_Capacity )
					return base.CheckHold( m, item, message, checkItems, plusItems, plusWeight );

				return false;
			}
			else if ( checkItems )
				return false;

			Item ammo = Ammo;

			if ( ammo == null || ammo.Deleted )
				return false;

			if ( ammo.Amount + item.Amount <= m_Capacity )
				return true;

			return false;
		}

		public override void AddItem( Item dropped )
		{
			base.AddItem( dropped );

			InvalidateWeight();
		}
		
		public override void RemoveItem( Item dropped )
		{
			base.RemoveItem( dropped );

			InvalidateWeight();
		}
		
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
				
			if ( m_Crafter != null )
				list.Add( 1050043, m_Crafter.Name ); // crafted by ~1_NAME~

			if ( m_Quality == ClothingQuality.Exceptional )
				list.Add( 1063341 ); // exceptional

			Item ammo = Ammo;

			if ( ammo != null )
			{
				if ( ammo is Arrow )
					list.Add( 1075265, "{0}\t{1}", ammo.Amount, Capacity ); // Ammo: ~1_QUANTITY~/~2_CAPACITY~ arrows
				else if ( ammo is Bolt )
					list.Add( 1075266, "{0}\t{1}", ammo.Amount, Capacity ); // Ammo: ~1_QUANTITY~/~2_CAPACITY~ bolts
			}
			else
				list.Add( 1075265, "{0}\t{1}", 0, Capacity ); // Ammo: ~1_QUANTITY~/~2_CAPACITY~ arrows

			int prop;

			if ( (prop = m_DamageIncrease) != 0 )
				list.Add( 1074762, prop.ToString() ); // Damage modifier: ~1_PERCENT~%
			
			if ( (prop = m_LowerAmmoCost) > 0 )
				list.Add( 1075208, prop.ToString() ); // Lower Ammo Cost ~1_Percentage~%

			double weight = 0;

			if ( ammo != null )
				weight = ammo.Weight * ammo.Amount;

			list.Add( 1072241, "{0}\t{1}\t{2}\t{3}", Items.Count, DefaultMaxItems, (int) weight, DefaultMaxWeight ); // Contents: ~1_COUNT~/~2_MAXCOUNT items, ~3_WEIGHT~/~4_MAXWEIGHT~ stones

			if ( (prop = m_WeightReduction) != 0 )
				list.Add( 1072210, prop.ToString() ); // Weight reduction: ~1_PERCENTAGE~%	
		}
		
		private static void SetSaveFlag( ref SaveFlag flags, SaveFlag toSet, bool setIf )
		{
			if ( setIf )
				flags |= toSet;
		}

		private static bool GetSaveFlag( SaveFlag flags, SaveFlag toGet )
		{
			return (flags & toGet) != 0;
		}

		[Flags]
		private enum SaveFlag
		{
			None				= 0x00000000,
			Attributes			= 0x00000001,
			DamageModifier		= 0x00000002,
			LowerAmmoCost		= 0x00000004,
			WeightReduction		= 0x00000008,
			Crafter				= 0x00000010,
			Quality				= 0x00000020,
			Capacity			= 0x00000040,
			DamageIncrease		= 0x00000080
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( 0 ); // version

			SaveFlag flags = SaveFlag.None;

			SetSaveFlag( ref flags, SaveFlag.LowerAmmoCost,		m_LowerAmmoCost != 0 );
			SetSaveFlag( ref flags, SaveFlag.WeightReduction,	m_WeightReduction != 0 );
			SetSaveFlag( ref flags, SaveFlag.DamageIncrease,	m_DamageIncrease != 0 );
			SetSaveFlag( ref flags, SaveFlag.Crafter,			m_Crafter != null );
			SetSaveFlag( ref flags, SaveFlag.Quality,			true );
			SetSaveFlag( ref flags, SaveFlag.Capacity,			m_Capacity > 0 );

			writer.WriteEncodedInt( (int) flags );

			if ( GetSaveFlag( flags, SaveFlag.LowerAmmoCost ) )
				writer.Write( (int) m_LowerAmmoCost );

			if ( GetSaveFlag( flags, SaveFlag.WeightReduction ) )
				writer.Write( (int) m_WeightReduction );

			if ( GetSaveFlag( flags, SaveFlag.DamageIncrease ) )
				writer.Write( (int) m_DamageIncrease );

			if ( GetSaveFlag( flags, SaveFlag.Crafter ) )
				writer.Write( (Mobile) m_Crafter );

			if ( GetSaveFlag( flags, SaveFlag.Quality ) )
				writer.Write( (int) m_Quality );

			if ( GetSaveFlag( flags, SaveFlag.Capacity ) )
				writer.Write( (int) m_Capacity );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			SaveFlag flags = (SaveFlag) reader.ReadEncodedInt();

			if ( GetSaveFlag( flags, SaveFlag.LowerAmmoCost ) )
				m_LowerAmmoCost = reader.ReadInt();

			if ( GetSaveFlag( flags, SaveFlag.WeightReduction ) )
				m_WeightReduction = reader.ReadInt();

			if ( GetSaveFlag( flags, SaveFlag.DamageIncrease ) )
				m_DamageIncrease = reader.ReadInt();

			if ( GetSaveFlag( flags, SaveFlag.Crafter ) )
				m_Crafter = reader.ReadMobile();

			if ( GetSaveFlag( flags, SaveFlag.Quality ) )
				m_Quality = (ClothingQuality) reader.ReadInt();

			if ( GetSaveFlag( flags, SaveFlag.Capacity ) )
				m_Capacity = reader.ReadInt();
		}

		public void InvalidateWeight()
		{
			if ( RootParent is Mobile )
			{
				Mobile m = (Mobile) RootParent;

				m.UpdateTotals();
			}
		}
		
		#region ICraftable
		public virtual int OnCraft( int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, BaseTool tool, CraftItem craftItem, int resHue )
		{
			Quality = (ClothingQuality) quality;

			if ( makersMark )
				Crafter = from;

			return quality;
		}
		#endregion
	}
}
