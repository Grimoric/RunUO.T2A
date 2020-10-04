using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    public class SBFarmer : SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBFarmer() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{ 
				Add( new GenericBuyInfo( "Head of cabbage", typeof( Cabbage ), 5, 20, 0xC7B, 0 ) );
				Add( new GenericBuyInfo( "Canteloupe", typeof( Cantaloupe ), 6, 20, 0xC79, 0 ) );
				Add( new GenericBuyInfo( "Carrot", typeof( Carrot ), 3, 20, 0xC78, 0 ) );
				Add( new GenericBuyInfo( "Honeydew melon", typeof( HoneydewMelon ), 7, 20, 0xC74, 0 ) );
				Add( new GenericBuyInfo( "Squash", typeof( Squash ), 3, 20, 0xC72, 0 ) );
				Add( new GenericBuyInfo( "Head of lettuce", typeof( Lettuce ), 5, 20, 0xC70, 0 ) );
				Add( new GenericBuyInfo( "Onion", typeof( Onion ), 3, 20, 0xC6D, 0 ) );
				Add( new GenericBuyInfo( "Pumpkin", typeof( Pumpkin ), 11, 20, 0xC6A, 0 ) );
				Add( new GenericBuyInfo( "Gourd", typeof( GreenGourd ), 3, 20, 0xC66, 0 ) );
				Add( new GenericBuyInfo( "Gourd", typeof( YellowGourd ), 3, 20, 0xC64, 0 ) );
			//	Add( new GenericBuyInfo( "Turnip", typeof( Turnip ), 6, 20, 0x0C61, 0 ) );
				Add( new GenericBuyInfo( "Watermelon", typeof( Watermelon ), 7, 20, 0xC5C, 0 ) );
			//	Add( new GenericBuyInfo( "Ear of corn", typeof( EarOfCorn ), 3, 20, 0x0C7F, 0 ) );
				Add( new GenericBuyInfo( "Eggs", typeof( Eggs ), 3, 20, 0x9B5, 0 ) );
				Add( new BeverageBuyInfo( "Pitcher of milk", typeof( Pitcher ), BeverageType.Milk, 7, 20, 0x9AD, 0 ) );
				Add( new GenericBuyInfo( "Peach", typeof( Peach ), 3, 20, 0x9D2, 0 ) );
				Add( new GenericBuyInfo( "Pear", typeof( Pear ), 3, 20, 0x994, 0 ) );
				Add( new GenericBuyInfo( "Lemon", typeof( Lemon ), 3, 20, 0x1728, 0 ) );
				Add( new GenericBuyInfo( "Lime", typeof( Lime ), 3, 20, 0x172A, 0 ) );
				Add( new GenericBuyInfo( "Grape bunch", typeof( Grapes ), 3, 20, 0x9D1, 0 ) );
				Add( new GenericBuyInfo( "Apple", typeof( Apple ), 3, 20, 0x9D0, 0 ) );
				Add( new GenericBuyInfo( "Sheaf of hay", typeof( SheafOfHay ), 2, 20, 0xF36, 0 ) );

			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{ 
				Add( typeof( Pitcher ), 5 );
				Add( typeof( Eggs ), 1 );
				Add( typeof( Apple ), 1 );
				Add( typeof( Grapes ), 1 );
				Add( typeof( Watermelon ), 3 );
				Add( typeof( YellowGourd ), 1 );
				Add( typeof( GreenGourd ), 1 );
				Add( typeof( Pumpkin ), 5 );
				Add( typeof( Onion ), 1 );
				Add( typeof( Lettuce ), 2 );
				Add( typeof( Squash ), 1 );
				Add( typeof( Carrot ), 1 );
				Add( typeof( HoneydewMelon ), 3 );
				Add( typeof( Cantaloupe ), 3 );
				Add( typeof( Cabbage ), 2 );
				Add( typeof( Lemon ), 1 );
				Add( typeof( Lime ), 1 );
				Add( typeof( Peach ), 1 );
				Add( typeof( Pear ), 1 );
				Add( typeof( SheafOfHay ), 1 );
			} 
		} 
	} 
}