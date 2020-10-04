using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    public class SBWeaponSmith: SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBWeaponSmith() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{ 
				Add( new GenericBuyInfo( "Black staff", typeof( BlackStaff ), 22, 20, 0xDF1, 0 ) );
				Add( new GenericBuyInfo( "Club", typeof( Club ), 16, 20, 0x13B4, 0 ) );
				Add( new GenericBuyInfo( "Gnarled staff", typeof( GnarledStaff ), 16, 20, 0x13F8, 0 ) );
				Add( new GenericBuyInfo( "Mace", typeof( Mace ), 28, 20, 0xF5C, 0 ) );
				Add( new GenericBuyInfo( "Maul", typeof( Maul ), 21, 20, 0x143B, 0 ) );
				Add( new GenericBuyInfo( "Quarter staff", typeof( QuarterStaff ), 19, 20, 0xE89, 0 ) );
				Add( new GenericBuyInfo( "Shepherd's crook", typeof( ShepherdsCrook ), 20, 20, 0xE81, 0 ) );
				Add( new GenericBuyInfo( "Smith's hammer", typeof( SmithHammer ), 21, 20, 0x13E3, 0 ) );
				Add( new GenericBuyInfo( "Short spear", typeof( ShortSpear ), 23, 20, 0x1403, 0 ) );
				Add( new GenericBuyInfo( "Spear", typeof( Spear ), 31, 20, 0xF62, 0 ) );
				Add( new GenericBuyInfo( "War hammer", typeof( WarHammer ), 25, 20, 0x1439, 0 ) );
				Add( new GenericBuyInfo( "War mace", typeof( WarMace ), 31, 20, 0x1407, 0 ) );
				Add( new GenericBuyInfo( "Hatchet", typeof( Hatchet ), 25, 20, 0xF44, 0 ) );
				Add( new GenericBuyInfo( "Hatchet", typeof( Hatchet ), 27, 20, 0xF43, 0 ) );
				Add( new GenericBuyInfo( "War fork", typeof( WarFork ), 32, 20, 0x1405, 0 ) );

            	switch ( Utility.Random( 3 )) 
            	{ 
					case 0:
					{
						Add( new GenericBuyInfo( "Executioner's axe", typeof( ExecutionersAxe ), 30, 20, 0xF45, 0 ) );
						Add( new GenericBuyInfo( "Bardiche", typeof( Bardiche ), 60, 20, 0xF4D, 0 ) );
						Add( new GenericBuyInfo( "Battle axe", typeof( BattleAxe ), 26, 20, 0xF47, 0 ) );
						Add( new GenericBuyInfo( "Two handed axe", typeof( TwoHandedAxe ), 32, 20, 0x1443, 0 ) );

						Add( new GenericBuyInfo( "Bow", typeof( Bow ), 35, 20, 0x13B2, 0 ) );

						Add( new GenericBuyInfo( "Butcher knife", typeof( ButcherKnife ), 14, 20, 0x13F6, 0 ) );

						Add( new GenericBuyInfo( "Crossbow", typeof( Crossbow ), 46, 20, 0xF50, 0 ) );
						Add( new GenericBuyInfo( "Heavy crossbow", typeof( HeavyCrossbow ), 55, 20, 0x13FD, 0 ) );

						Add( new GenericBuyInfo( "Cutlass", typeof( Cutlass ), 24, 20, 0x1441, 0 ) );
						Add( new GenericBuyInfo( "Dagger", typeof( Dagger ), 21, 20, 0xF52, 0 ) );
						Add( new GenericBuyInfo( "Halberd", typeof( Halberd ), 42, 20, 0x143E, 0 ) );

						Add( new GenericBuyInfo( "Hammer pick", typeof( HammerPick ), 26, 20, 0x143D, 0 ) );

						Add( new GenericBuyInfo( "Katana", typeof( Katana ), 33, 20, 0x13FF, 0 ) );
						Add( new GenericBuyInfo( "Kryss", typeof( Kryss ), 32, 20, 0x1401, 0 ) );
						Add( new GenericBuyInfo( "Broadsword", typeof( Broadsword ), 35, 20, 0xF5E, 0 ) );
						Add( new GenericBuyInfo( "Longsword", typeof( Longsword ), 55, 20, 0xF61, 0 ) );
						Add( new GenericBuyInfo( "Long sword", typeof( ThinLongsword ), 27, 20, 0x13B8, 0 ) );
						Add( new GenericBuyInfo( "Viking sword", typeof( VikingSword ), 55, 20, 0x13B9, 0 ) );

						Add( new GenericBuyInfo( "Cleaver", typeof( Cleaver ), 15, 20, 0xEC3, 0 ) );
						Add( new GenericBuyInfo( "Axe", typeof( Axe ), 40, 20, 0xF49, 0 ) );
						Add( new GenericBuyInfo( "Double axe", typeof( DoubleAxe ), 52, 20, 0xF4B, 0 ) );
						Add( new GenericBuyInfo( "Pickaxe", typeof( Pickaxe ), 22, 20, 0xE86, 0 ) );

						Add( new GenericBuyInfo( "Pitchfork", typeof( Pitchfork ), 19, 20, 0xE87, 0 ) );

						Add( new GenericBuyInfo( "Scimitar", typeof( Scimitar ), 36, 20, 0x13B6, 0 ) );

						Add( new GenericBuyInfo( "Skinning knife", typeof( SkinningKnife ), 14, 20, 0xEC4, 0 ) );

						Add( new GenericBuyInfo( "Large battle axe", typeof( LargeBattleAxe ), 33, 20, 0x13FB, 0 ) );
						Add( new GenericBuyInfo( "War axe", typeof( WarAxe ), 29, 20, 0x13B0, 0 ) );

						break;
					}

				}
	
			}
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{ 	
				Add( typeof( BattleAxe ), 13 );
				Add( typeof( DoubleAxe ), 26 );
				Add( typeof( ExecutionersAxe ), 15 );
				Add( typeof( LargeBattleAxe ),16 );
				Add( typeof( Pickaxe ), 11 );
				Add( typeof( TwoHandedAxe ), 16 );
				Add( typeof( WarAxe ), 14 );
				Add( typeof( Axe ), 20 );

				Add( typeof( Bardiche ), 30 );
				Add( typeof( Halberd ), 21 );

				Add( typeof( ButcherKnife ), 7 );
				Add( typeof( Cleaver ), 7 );
				Add( typeof( Dagger ), 10 );
				Add( typeof( SkinningKnife ), 7 );

				Add( typeof( Club ), 8 );
				Add( typeof( HammerPick ), 13 );
				Add( typeof( Mace ), 14 );
				Add( typeof( Maul ), 10 );
				Add( typeof( WarHammer ), 12 );
				Add( typeof( WarMace ), 15 );

				Add( typeof( HeavyCrossbow ), 27 );
				Add( typeof( Bow ), 17 );
				Add( typeof( Crossbow ), 23 ); 

				Add( typeof( Spear ), 15 );
				Add( typeof( Pitchfork ), 9 );
				Add( typeof( ShortSpear ), 11 );

				Add( typeof( BlackStaff ), 11 );
				Add( typeof( GnarledStaff ), 8 );
				Add( typeof( QuarterStaff ), 9 );
				Add( typeof( ShepherdsCrook ), 10 );

				Add( typeof( SmithHammer ), 10 );

				Add( typeof( Broadsword ), 17 );
				Add( typeof( Cutlass ), 12 );
				Add( typeof( Katana ), 16 );
				Add( typeof( Kryss ), 16 );
				Add( typeof( Longsword ), 27 );
				Add( typeof( Scimitar ), 18 );
				Add( typeof( ThinLongsword ), 13 );
				Add( typeof( VikingSword ), 27 );

				Add( typeof( Hatchet ), 13 );
				Add( typeof( WarFork ), 16 );
			} 
		} 
	} 
}
