using System;
using System.Collections.Generic;
using System.Reflection;
using Server.Mobiles;
using Server.Items;
using Server.Network;
using Server.HuePickers;

namespace Server.Gumps
{
    public class PlayerVendorBuyGump : Gump
	{
		private PlayerVendor m_Vendor;
		private VendorItem m_VI;

		public PlayerVendorBuyGump( PlayerVendor vendor, VendorItem vi ) : base( 100, 200 )
		{
			m_Vendor = vendor;
			m_VI = vi;

			AddBackground( 100, 10, 300, 150, 5054 );

			AddHtmlLocalized( 125, 20, 250, 24, 1019070, false, false ); // You have agreed to purchase:

			if ( !String.IsNullOrEmpty( vi.Description ) )
				AddLabel( 125, 45, 0, vi.Description );
			else
				AddHtmlLocalized( 125, 45, 250, 24, 1019072, false, false ); // an item without a description

			AddHtmlLocalized( 125, 70, 250, 24, 1019071, false, false ); // for the amount of:
			AddLabel( 125, 95, 0, vi.Price.ToString() );

			AddButton( 250, 130, 4005, 4007, 0, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 282, 130, 100, 24, 1011012, false, false ); // CANCEL

			AddButton( 120, 130, 4005, 4007, 1, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 152, 130, 100, 24, 1011036, false, false ); // OKAY
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			if ( !m_Vendor.CanInteractWith( from, false ) )
				return;

			if ( m_Vendor.IsOwner( from ) )
			{
				m_Vendor.SayTo( from, 503212 ); // You own this shop, just take what you want.
				return;
			}

			if ( info.ButtonID == 1 )
			{
				m_Vendor.Say( from.Name );

				if ( !m_VI.Valid || !m_VI.Item.IsChildOf( m_Vendor.Backpack ) )
				{
					m_Vendor.SayTo( from, 503216 ); // You can't buy that.
					return;
				}

				int totalGold = 0;

				if ( from.Backpack != null )
					totalGold += from.Backpack.GetAmount( typeof( Gold ) );
				
				totalGold += Banker.GetBalance( from );

				if ( totalGold < m_VI.Price )
				{
					m_Vendor.SayTo( from, 503205 ); // You cannot afford this item.
				}
				else if ( !from.PlaceInBackpack( m_VI.Item ) )
				{
					m_Vendor.SayTo( from, 503204 ); // You do not have room in your backpack for this.
				}
				else
				{
					int leftPrice = m_VI.Price;

					if ( from.Backpack != null )
						leftPrice -= from.Backpack.ConsumeUpTo( typeof( Gold ), leftPrice );

					if ( leftPrice > 0 )
						Banker.Withdraw( from, leftPrice );

					m_Vendor.HoldGold += m_VI.Price;

					from.SendLocalizedMessage( 503201 ); // You take the item.
				}
			}
			else
			{
				from.SendLocalizedMessage( 503207 ); // Cancelled purchase.
			}
		}
	}

	public class PlayerVendorOwnerGump : Gump
	{
		private PlayerVendor m_Vendor;

		public PlayerVendorOwnerGump( PlayerVendor vendor ) : base( 50, 200 )
		{
			m_Vendor = vendor;

			int perDay = m_Vendor.ChargePerDay;

			AddPage( 0 );
			AddBackground( 25, 10, 530, 140, 5054 );

			AddHtmlLocalized( 425, 25, 120, 20, 1019068, false, false ); // See goods
			AddButton( 390, 25, 4005, 4007, 1, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 425, 48, 120, 20, 1019069, false, false ); // Customize
			AddButton( 390, 48, 4005, 4007, 2, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 425, 72, 120, 20, 1011012, false, false ); // CANCEL
			AddButton( 390, 71, 4005, 4007, 0, GumpButtonType.Reply, 0 );

			AddHtmlLocalized( 40, 72, 260, 20, 1038321, false, false ); // Gold held for you:
			AddLabel( 300, 72, 0, m_Vendor.HoldGold.ToString() );
			AddHtmlLocalized( 40, 96, 260, 20, 1038322, false, false ); // Gold held in my account:
			AddLabel( 300, 96, 0, m_Vendor.BankAccount.ToString() );

			//AddHtmlLocalized( 40, 120, 260, 20, 1038324, false, false ); // My charge per day is:
			// Localization has changed, we must use a string here
			AddHtml( 40, 120, 260, 20, "My charge per day is:", false, false );
			AddLabel( 300, 120, 0, perDay.ToString() );

			double days = (m_Vendor.HoldGold + m_Vendor.BankAccount) / (double)perDay;

			AddHtmlLocalized( 40, 25, 260, 20, 1038318, false, false ); // Amount of days I can work: 
			AddLabel( 300, 25, 0, ((int)days).ToString() );
			AddHtmlLocalized( 40, 48, 260, 20, 1038319, false, false ); // Earth days: 
			AddLabel( 300, 48, 0, ((int)(days / 12.0)).ToString() );
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			if ( !m_Vendor.CanInteractWith( from, true ) )
				return;

			switch ( info.ButtonID )
			{
				case 1:
				{
					m_Vendor.OpenBackpack( from );

					break;
				}
				case 2:
				{
					from.SendGump( new PlayerVendorCustomizeGump( m_Vendor, from ) );

					break;
				}
			}
		}
	}

	public class PlayerVendorCustomizeGump : Gump
	{
		private Mobile m_Vendor;

		private class CustomItem
		{
			private Type m_Type;
			private int m_ItemID;
			private int m_LocNum;
			private int m_ArtNum;
			private bool m_LongText;

			public CustomItem( int itemID, int loc ) : this( null, itemID, loc, 0, false )
			{
			}

			public CustomItem( int itemID, int loc, bool longText ) : this( null, itemID, loc, 0, longText )
			{
			}

			public CustomItem( Type type, int loc ) : this( type, loc, 0 )
			{
			}

			public CustomItem( Type type, int loc, int art ) : this( type, 0, loc, art, false )
			{
			}

			public CustomItem( Type type, int itemID, int loc, int art, bool longText )
			{
				m_Type = type;
				m_ItemID = itemID;
				m_LocNum = loc;
				m_ArtNum = art;
				m_LongText = longText;
			}

			public Item Create()
			{
				if ( m_Type == null )
					return null;

				Item i = null;
				
				try
				{
					ConstructorInfo ctor = m_Type.GetConstructor( new Type[0] );
					if ( ctor != null )
						i = ctor.Invoke( null ) as Item;
				}
				catch
				{
				}
				
				return i;
			}

			public Type Type{ get{ return m_Type; } }
			public int ItemID{ get{ return m_ItemID; } }
			public int LocNumber{ get{ return m_LocNum; } }
			public int ArtNumber{ get{ return m_ArtNum; } }
			public bool LongText{ get{ return m_LongText; } }
		}

		private class CustomCategory
		{
			private CustomItem[] m_Entries;
			private Layer m_Layer;
			private bool m_CanDye;
			private int m_LocNum;

			public CustomCategory( Layer layer, int loc, bool canDye, CustomItem[] items )
			{
				m_Entries = items;
				m_CanDye = canDye;
				m_Layer = layer;
				m_LocNum = loc;
			}

			public bool CanDye{ get{ return m_CanDye; } }
			public CustomItem[] Entries{ get{ return m_Entries; } }
			public Layer Layer{ get{ return m_Layer; } }
			public int LocNumber{ get{ return m_LocNum; } }
		}

		private static CustomCategory[] Categories = new CustomCategory[]{
			new CustomCategory( Layer.InnerTorso, 1011357, true, new CustomItem[]{// Upper Torso
				new CustomItem( typeof( Shirt ), 		1011359, 5399 ),
				new CustomItem( typeof( FancyShirt ),	1011360, 7933 ),
				new CustomItem( typeof( PlainDress ),	1011363, 7937 ),
				new CustomItem( typeof( FancyDress ),	1011364, 7935 ),
				new CustomItem( typeof( Robe ),			1011365, 7939 )
			} ),

			new CustomCategory( Layer.MiddleTorso, 1011371, true, new CustomItem[]{//Over chest
				new CustomItem( typeof( Doublet ), 		1011358, 8059 ),
				new CustomItem( typeof( Tunic ),		1011361, 8097 ),
				new CustomItem( typeof( JesterSuit ), 	1011366, 8095 ),
				new CustomItem( typeof( BodySash ),		1011372, 5441 ),
				new CustomItem( typeof( Surcoat ),		1011362, 8189 ),
				new CustomItem( typeof( HalfApron ),	1011373, 5435 ),
				new CustomItem( typeof( FullApron ),	1011374, 5437 ),
			} ),

			new CustomCategory( Layer.Shoes, 1011388, true, new CustomItem[]{//Footwear
				new CustomItem( typeof( Sandals ),		1011389, 5901 ),
				new CustomItem( typeof( Shoes ),		1011390, 5904 ),
				new CustomItem( typeof( Boots ),		1011391, 5899 ),
				new CustomItem( typeof( ThighBoots ),	1011392, 5906 ),
			} ),

			new CustomCategory( Layer.Helm, 1011375, true, new CustomItem[]{//Hats
				new CustomItem( typeof( SkullCap ),		1011376, 5444 ),
				new CustomItem( typeof( Bandana ), 		1011377, 5440 ),
				new CustomItem( typeof( FloppyHat ),	1011378, 5907 ),
				new CustomItem( typeof( WideBrimHat ),	1011379, 5908 ),
				new CustomItem( typeof( Cap ),			1011380, 5909 ),
				new CustomItem( typeof( TallStrawHat ),	1011382, 5910 )
			} ),

			new CustomCategory( Layer.Helm, 1015319, true, new CustomItem[]{//More Hats
			    new CustomItem( typeof( StrawHat ),		1011382, 5911 ),
				new CustomItem( typeof( WizardsHat ), 	1011383, 5912 ),
				new CustomItem( typeof( Bonnet ),		1011384, 5913 ),
				new CustomItem( typeof( FeatheredHat ),	1011385, 5914 ),
				new CustomItem( typeof( TricorneHat ),	1011386, 5915 ),
				new CustomItem( typeof( JesterHat ),	1011387, 5916 )
			} ),

			new CustomCategory( Layer.Pants, 1011367, true, new CustomItem[]{ //Lower Torso
				new CustomItem( typeof( LongPants ),	1011368, 5433 ),
				new CustomItem( typeof( Kilt ), 		1011369, 5431 ),
				new CustomItem( typeof( Skirt ),		1011370, 5398 ),
			} ),

			new CustomCategory( Layer.Cloak, 1011393, true, new CustomItem[]{ // Back
				new CustomItem( typeof( Cloak ),		1011394, 5397 )
			} ),

			new CustomCategory( Layer.Hair, 1011395, true, new CustomItem[]{ // Hair
				new CustomItem( 0x203B,		1011052 ),
				new CustomItem( 0x203C,		1011053 ),
				new CustomItem( 0x203D,		1011054 ),
				new CustomItem( 0x2044,		1011055 ),
				new CustomItem( 0x2045,		1011047 ),
				new CustomItem( 0x204A,		1011050 ),
				new CustomItem( 0x2047,		1011396 ),
				new CustomItem( 0x2048,		1011048 ),
				new CustomItem( 0x2049,		1011049 ),
			} ),

			new CustomCategory( Layer.FacialHair, 1015320, true, new CustomItem[]{//Facial Hair
				new CustomItem( 0x2041,		1011062 ),
				new CustomItem( 0x203F,		1011060 ),
				new CustomItem( 0x204B,		1015321, true ),
				new CustomItem( 0x203E,		1011061 ),
				new CustomItem( 0x204C,		1015322, true ),
				new CustomItem( 0x2040,		1015323 ),
				new CustomItem( 0x204D,		1011401 ),
			} ),

			new CustomCategory( Layer.FirstValid, 1011397, false, new CustomItem[]{//Held items
				new CustomItem( typeof( FishingPole ), 	1011406, 3520 ),
				new CustomItem( typeof( Pickaxe ),		1011407, 3717 ),
				new CustomItem( typeof( Pitchfork ),	1011408, 3720 ),
				new CustomItem( typeof( Cleaver ),		1015324, 3778 ),
				new CustomItem( typeof( Mace ),			1011409, 3933 ),
				new CustomItem( typeof( Torch ),		1011410, 3940 ),
				new CustomItem( typeof( Hammer ),		1011411, 4020 ),
				new CustomItem( typeof( Longsword ),	1011412, 3936 ),
				new CustomItem( typeof( GnarledStaff ), 1011413, 5113 )
			} ),

			new CustomCategory( Layer.FirstValid, 1015325, false, new CustomItem[]{//More held items
				new CustomItem( typeof( Crossbow ),		1011414, 3920 ),
				new CustomItem( typeof( WarMace ),		1011415, 5126 ),
				new CustomItem( typeof( TwoHandedAxe ),	1011416, 5186 ),
				new CustomItem( typeof( Spear ),		1011417, 3939 ),
				new CustomItem( typeof( Katana ),		1011418, 5118 ),
				new CustomItem( typeof( Spellbook ),	1011419, 3834 )
			} )
		};

		public PlayerVendorCustomizeGump( Mobile v, Mobile from ) : base( 30, 40 )
		{
			m_Vendor = v;
			int x,y;

			from.CloseGump( typeof( PlayerVendorCustomizeGump ) );

			AddPage( 0 );
			AddBackground( 0, 0, 585, 393, 5054 );
			AddBackground( 195, 36, 387, 275, 3000 );
			AddHtmlLocalized( 10, 10, 565, 18, 1011356, false, false ); // <center>VENDOR CUSTOMIZATION MENU</center>
			AddHtmlLocalized( 60, 355, 150, 18, 1011036, false, false ); // OKAY
			AddButton( 25, 355, 4005, 4007, 1, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 320, 355, 150, 18, 1011012, false, false ); // CANCEL
			AddButton( 285, 355, 4005, 4007, 0, GumpButtonType.Reply, 0 );

			y = 35;
			for ( int i=0;i<Categories.Length;i++ )
			{
				CustomCategory cat = (CustomCategory)Categories[i];
				AddHtmlLocalized( 5, y, 150, 25, cat.LocNumber, true, false );
				AddButton( 155, y, 4005, 4007, 0, GumpButtonType.Page, i+1 );
				y += 25;
			}

			for ( int i=0;i<Categories.Length;i++ )
			{
				CustomCategory cat = (CustomCategory)Categories[i];
				AddPage( i+1 );

				for ( int c=0;c<cat.Entries.Length;c++ )
				{
					CustomItem entry = (CustomItem)cat.Entries[c];
					x = 198 + c%3*129;
					y = 38 + c/3*67;

					AddHtmlLocalized( x, y, 100, entry.LongText ? 36 : 18, entry.LocNumber, false, false );

					if ( entry.ArtNumber != 0 )
						AddItem( x+20, y+25, entry.ArtNumber );

					AddRadio( x, y + (entry.LongText ? 40 : 20), 210, 211, false, (c<<8) + i );
				}

				if ( cat.CanDye )
				{
					AddHtmlLocalized( 327, 239, 100, 18, 1011402, false, false ); // Color
					AddRadio( 327, 259, 210, 211, false, 100+i );
				}

				AddHtmlLocalized( 456, 239, 100, 18, 1011403, false, false ); // Remove
				AddRadio( 456, 259, 210, 211, false, 200+i );
			}
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			if ( m_Vendor.Deleted )
				return;

			Mobile from = state.Mobile;

			if ( m_Vendor is PlayerVendor && !((PlayerVendor)m_Vendor).CanInteractWith( from, true ) )
				return;

			if ( info.ButtonID == 0 )
			{
				if ( m_Vendor is PlayerVendor ) // do nothing for barkeeps
				{
					m_Vendor.Direction = m_Vendor.GetDirectionTo( from );
					m_Vendor.Animate( 32, 5, 1, true, false, 0 );//bow
					m_Vendor.SayTo( from, 1043310 + Utility.Random( 12 ) ); // a little random speech
				}
			}
			else if ( info.ButtonID == 1 && info.Switches.Length > 0 )
			{
				int cnum = info.Switches[0];
				int cat = cnum%256;
				int ent = cnum>>8;

				if ( cat < Categories.Length && cat >= 0 )
				{
					if ( ent < Categories[cat].Entries.Length && ent >= 0 )
					{
						Item item = m_Vendor.FindItemOnLayer( Categories[cat].Layer );

						if ( item != null )
							item.Delete();

						List<Item> items = m_Vendor.Items;

						for ( int i = 0; item == null && i < items.Count; ++i )
						{
							Item checkitem = items[i];
							Type type = checkitem.GetType();

							for ( int j = 0; item == null && j < Categories[cat].Entries.Length; ++j )
							{
								if ( type == Categories[cat].Entries[j].Type )
									item = checkitem;
							}
						}

						if ( item != null )
							item.Delete();

						if ( Categories[cat].Layer == Layer.FacialHair )
						{
							if ( m_Vendor.Female )
							{
								from.SendLocalizedMessage( 1010639 ); // You cannot place facial hair on a woman!
							}
							else
							{
								int hue = m_Vendor.FacialHairHue;

								m_Vendor.FacialHairItemID = 0;
								m_Vendor.ProcessDelta(); // invalidate item ID for clients

								m_Vendor.FacialHairItemID = Categories[cat].Entries[ent].ItemID;
								m_Vendor.FacialHairHue = hue;
							}
						}
						else if ( Categories[cat].Layer == Layer.Hair )
						{
							int hue = m_Vendor.HairHue;

							m_Vendor.HairItemID = 0;
							m_Vendor.ProcessDelta(); // invalidate item ID for clients

							m_Vendor.HairItemID = Categories[cat].Entries[ent].ItemID;
							m_Vendor.HairHue = hue;
						}
						else
						{
							item = Categories[cat].Entries[ent].Create();

							if ( item != null )
							{
								item.Layer = Categories[cat].Layer;

								if ( !m_Vendor.EquipItem( item ) )
									item.Delete();
							}
						}

						from.SendGump( new PlayerVendorCustomizeGump( m_Vendor, from ) );
					}
				}
				else
				{
					cat -= 100;

					if ( cat < 100 )
					{
						if ( cat < Categories.Length && cat >= 0 )
						{
							CustomCategory category = Categories[cat];

							if ( category.Layer == Layer.Hair )
							{
								new PVHairHuePicker( false, m_Vendor, from ).SendTo( state );
							}
							else if ( category.Layer == Layer.FacialHair )
							{
								new PVHairHuePicker( true, m_Vendor, from ).SendTo( state );
							}
							else
							{
								Item item = null;

								List<Item> items = m_Vendor.Items;

								for ( int i = 0; item == null && i < items.Count; ++i )
								{
									Item checkitem = items[i];
									Type type = checkitem.GetType();

									for ( int j = 0; item == null && j < category.Entries.Length; ++j )
									{
										if ( type == category.Entries[j].Type )
											item = checkitem;
									}
								}

								if ( item != null )
									new PVHuePicker( item, m_Vendor, from ).SendTo( state );
							}
						}
					}
					else
					{
						cat -= 100;

						if ( cat < Categories.Length && cat >= 0 )
						{
							CustomCategory category = Categories[cat];

							if ( category.Layer == Layer.Hair )
							{
								m_Vendor.HairItemID = 0;
							}
							else if ( category.Layer == Layer.FacialHair )
							{
								m_Vendor.FacialHairItemID = 0;
							}
							else
							{
								Item item = null;

								List<Item> items = m_Vendor.Items;

								for ( int i = 0; item == null && i < items.Count; ++i )
								{
									Item checkitem = items[i];
									Type type = checkitem.GetType();

									for ( int j = 0; item == null && j < category.Entries.Length; ++j )
									{
										if ( type == category.Entries[j].Type )
											item = checkitem;
									}
								}

								if ( item != null )
									item.Delete();
							}

							from.SendGump( new PlayerVendorCustomizeGump( m_Vendor, from ) );
						}
					}
				}
			}
		}

		private class PVHuePicker : HuePicker
		{
			private Item m_Item;
			private Mobile m_Vendor;
			private Mobile m_Mob;

			public PVHuePicker( Item item, Mobile v, Mobile from ) : base( item.ItemID )
			{
				m_Item = item;
				m_Vendor = v;
				m_Mob = from;
			}

			public override void OnResponse( int hue )
			{
				if ( m_Item.Deleted )
					return;

				if ( m_Vendor is PlayerVendor && !((PlayerVendor)m_Vendor).CanInteractWith( m_Mob, true ) )
					return;

				m_Item.Hue = hue;
				m_Mob.SendGump( new PlayerVendorCustomizeGump( m_Vendor, m_Mob ) );
			}
		}

		private class PVHairHuePicker : HuePicker
		{
			private bool m_FacialHair;
			private Mobile m_Vendor;
			private Mobile m_Mob;

			public PVHairHuePicker( bool facialHair, Mobile v, Mobile from ) : base( 0xFAB )
			{
				m_FacialHair = facialHair;
				m_Vendor = v;
				m_Mob = from;
			}

			public override void OnResponse( int hue )
			{
				if ( m_Vendor.Deleted )
					return;

				if ( m_Vendor is PlayerVendor && !((PlayerVendor)m_Vendor).CanInteractWith( m_Mob, true ) )
					return;

				if ( m_FacialHair )
					m_Vendor.FacialHairHue = hue;
				else
					m_Vendor.HairHue = hue;

				m_Mob.SendGump( new PlayerVendorCustomizeGump( m_Vendor, m_Mob ) );
			}
		}
	}
}
