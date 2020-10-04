using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    public class SBAlchemist : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBAlchemist()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( "Red Potion", typeof( RefreshPotion ), 15, 10, 0xF0B, 0 ) );
				Add( new GenericBuyInfo( "Blue Potion", typeof( AgilityPotion ), 15, 10, 0xF08, 0 ) );
				Add( new GenericBuyInfo( "Black Potion", typeof( NightSightPotion ), 15, 10, 0xF06, 0 ) );
				Add( new GenericBuyInfo( "Yellow Potion", typeof( LesserHealPotion ), 15, 10, 0xF0C, 0 ) );
				Add( new GenericBuyInfo( "White Potion", typeof( StrengthPotion ), 15, 10, 0xF09, 0 ) );
				Add( new GenericBuyInfo( "Green Potion", typeof( LesserPoisonPotion ), 15, 10, 0xF0A, 0 ) );
				Add( new GenericBuyInfo( "Orange Potion", typeof( LesserCurePotion ), 15, 10, 0xF07, 0 ) );
				Add( new GenericBuyInfo( "Purple Potion", typeof( LesserExplosionPotion ), 21, 10, 0xF0D, 0 ) );
				Add( new GenericBuyInfo( "Mortar and pestle", typeof( MortarPestle ), 8, 10, 0xE9B, 0 ) );

				Add( new GenericBuyInfo( "Black Pearl", typeof( BlackPearl ), 5, 20, 0xF7A, 0 ) );
				Add( new GenericBuyInfo( "Blood Moss", typeof( Bloodmoss ), 5, 20, 0xF7B, 0 ) );
				Add( new GenericBuyInfo( "Garlic", typeof( Garlic ), 3, 20, 0xF84, 0 ) );
				Add( new GenericBuyInfo( "Ginseng", typeof( Ginseng ), 3, 20, 0xF85, 0 ) );
				Add( new GenericBuyInfo( "Mandrake Root", typeof( MandrakeRoot ), 3, 20, 0xF86, 0 ) );
				Add( new GenericBuyInfo( "Nightshade", typeof( Nightshade ), 3, 20, 0xF88, 0 ) );
				Add( new GenericBuyInfo( "Spider's Silk", typeof( SpidersSilk ), 3, 20, 0xF8D, 0 ) );
				Add( new GenericBuyInfo( "Sulfurous Ash", typeof( SulfurousAsh ), 3, 20, 0xF8C, 0 ) );

				Add( new GenericBuyInfo( "Empty bottle", typeof( Bottle ), 5, 100, 0xF0E, 0 ) ); 
//				Add( new GenericBuyInfo( "Heating stand", typeof( HeatingStand ), 2, 100, 0x1849, 0 ) );

				Add( new GenericBuyInfo( "Hair dye", typeof( HairDye ), 37, 10, 0xEFF, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( BlackPearl ), 3 ); 
				Add( typeof( Bloodmoss ), 3 ); 
				Add( typeof( MandrakeRoot ), 2 ); 
				Add( typeof( Garlic ), 2 ); 
				Add( typeof( Ginseng ), 2 ); 
				Add( typeof( Nightshade ), 2 ); 
				Add( typeof( SpidersSilk ), 2 ); 
				Add( typeof( SulfurousAsh ), 2 ); 
				Add( typeof( Bottle ), 3 );
				Add( typeof( MortarPestle ), 4 );
				Add( typeof( HairDye ), 19 );

				Add( typeof( NightSightPotion ), 7 );
				Add( typeof( AgilityPotion ), 7 );
				Add( typeof( StrengthPotion ), 7 );
				Add( typeof( RefreshPotion ), 7 );
				Add( typeof( LesserCurePotion ), 7 );
				Add( typeof( LesserHealPotion ), 7 );
				Add( typeof( LesserPoisonPotion ), 7 );
				Add( typeof( LesserExplosionPotion ), 10 );
			}
		}
	}
}
