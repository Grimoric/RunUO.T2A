using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBMage : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBMage()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( "Spellbook", typeof( Spellbook ), 18, 10, 0xEFA, 0 ) );
				Add( new GenericBuyInfo( "Pen and ink", typeof( ScribesPen ), 8, 10, 0xFBF, 0 ) );
				Add( new GenericBuyInfo( "Blank scroll", typeof( BlankScroll ), 5, 20, 0x0E34, 0 ) );

				Add( new GenericBuyInfo( "A magical wizard's hat", typeof( MagicWizardsHat ), 11, 10, 0x1718, Utility.RandomDyedHue() ) );

				Add( new GenericBuyInfo( "Recall rune", typeof( RecallRune ), 15, 10, 0x1F14, 0 ) );

				Add( new GenericBuyInfo( "Red Potion", typeof( RefreshPotion ), 15, 20, 0xF0B, 0 ) );
				Add( new GenericBuyInfo( "Blue Potion", typeof( AgilityPotion ), 15, 20, 0xF08, 0 ) );
				Add( new GenericBuyInfo( "Black Potion", typeof( NightSightPotion ), 15, 20, 0xF06, 0 ) ); 
				Add( new GenericBuyInfo( "Yellow Potion", typeof( LesserHealPotion ), 15, 20, 0xF0C, 0 ) );
				Add( new GenericBuyInfo( "White Potion", typeof( StrengthPotion ), 15, 20, 0xF09, 0 ) );
				Add( new GenericBuyInfo( "Orange Potion", typeof( LesserCurePotion ), 15, 20, 0xF07, 0 ) );

				Add( new GenericBuyInfo( "Black Pearl", typeof( BlackPearl ), 5, 20, 0xF7A, 0 ) );
				Add( new GenericBuyInfo( "Blood Moss", typeof( Bloodmoss ), 5, 20, 0xF7B, 0 ) );
				Add( new GenericBuyInfo( "Garlic", typeof( Garlic ), 3, 20, 0xF84, 0 ) );
				Add( new GenericBuyInfo( "Ginseng", typeof( Ginseng ), 3, 20, 0xF85, 0 ) );
				Add( new GenericBuyInfo( "Mandrake Root", typeof( MandrakeRoot ), 3, 20, 0xF86, 0 ) );
				Add( new GenericBuyInfo( "Nightshade", typeof( Nightshade ), 3, 20, 0xF88, 0 ) );
				Add( new GenericBuyInfo( "Spider's Silk", typeof( SpidersSilk ), 3, 20, 0xF8D, 0 ) );
				Add( new GenericBuyInfo( "Sulfurous Ash", typeof( SulfurousAsh ), 3, 20, 0xF8C, 0 ) );

				Add( new GenericBuyInfo( "Reactive Armor Scroll", typeof( ReactiveArmorScroll ), 12, 20, 0x1F2D, 0 ) );
				Add( new GenericBuyInfo( "Clumsy Scroll", typeof( ClumsyScroll ), 12, 20, 0x1F2E, 0 ) );
				Add( new GenericBuyInfo( "Create Food Scroll", typeof( CreateFoodScroll ), 12, 20, 0x1F2F, 0 ) );
				Add( new GenericBuyInfo( "Feeblemind Scroll", typeof( FeeblemindScroll ), 12, 20, 0x1F30, 0 ) );
				Add( new GenericBuyInfo( "Heal Scroll", typeof( HealScroll ), 12, 20, 0x1F31, 0 ) );
				Add( new GenericBuyInfo( "Magic Arrow Scroll", typeof( MagicArrowScroll ), 12, 20, 0x1F32, 0 ) );
				Add( new GenericBuyInfo( "Night Sight Scroll", typeof( NightSightScroll ), 12, 20, 0x1F33, 0 ) );
				Add( new GenericBuyInfo( "Weaken Scroll", typeof( WeakenScroll ), 12, 20, 0x1F34, 0 ) );

				Add( new GenericBuyInfo( "Agility Scroll", typeof( AgilityScroll ), 22, 20, 0x1F35, 0 ) );
				Add( new GenericBuyInfo( "Cunning Scroll", typeof( CunningScroll ), 22, 20, 0x1F36, 0 ) );
				Add( new GenericBuyInfo( "Cure Scroll", typeof( CureScroll ), 22, 20, 0x1F37, 0 ) );
				Add( new GenericBuyInfo( "Harm Scroll", typeof( HarmScroll ), 22, 20, 0x1F38, 0 ) );
				Add( new GenericBuyInfo( "Magic Trap Scroll", typeof( MagicTrapScroll ), 22, 20, 0x1F39, 0 ) );
				Add( new GenericBuyInfo( "Magic Untrap Scroll", typeof( MagicUnTrapScroll ), 22, 20, 0x1F3A, 0 ) );
				Add( new GenericBuyInfo( "Protection Scroll", typeof( ProtectionScroll ), 22, 20, 0x1F3B, 0 ) );
				Add( new GenericBuyInfo( "Strength Scroll", typeof( StrengthScroll ), 22, 20, 0x1F3C, 0 ) );

				Add( new GenericBuyInfo( "Bless Scroll", typeof( BlessScroll ), 32, 20, 0x1F3D, 0 ) );
				Add( new GenericBuyInfo( "Fireball Scroll", typeof( FireballScroll ), 32, 20, 0x1F3E, 0 ) );
				Add( new GenericBuyInfo( "Magic Lock Scroll", typeof( MagicLockScroll ), 32, 20, 0x1F3F, 0 ) );
				Add( new GenericBuyInfo( "Poison Scroll", typeof( PoisonScroll ), 32, 20, 0x1F40, 0 ) );
				Add( new GenericBuyInfo( "Telekinisis Scroll", typeof( TelekinisisScroll ), 32, 20, 0x1F41, 0 ) );
				Add( new GenericBuyInfo( "Teleport Scroll", typeof( TeleportScroll ), 32, 20, 0x1F42, 0 ) );
				Add( new GenericBuyInfo( "Unlock Scroll", typeof( UnlockScroll ), 32, 20, 0x1F43, 0 ) );
				Add( new GenericBuyInfo( "Wall of Stone Scroll", typeof( WallOfStoneScroll ), 32, 20, 0x1F44, 0 ) );
			}
		}

	    public class InternalSellInfo : GenericSellInfo
	    {
	        public InternalSellInfo()
	        {
	            Add( typeof( WizardsHat ), 15 );
	            Add( typeof( BlackPearl ), 3 );
	            Add( typeof( Bloodmoss ), 4 );
	            Add( typeof( MandrakeRoot ), 2 );
	            Add( typeof( Garlic ), 2 );
	            Add( typeof( Ginseng ), 2 );
	            Add( typeof( Nightshade ), 2 );
	            Add( typeof( SpidersSilk ), 2 );
	            Add( typeof( SulfurousAsh ), 2 );

	            Add( typeof( RecallRune ), 13);
	            Add( typeof( Spellbook ), 25);

				Add( typeof( ReactiveArmorScroll ), 4 );
				Add( typeof( ClumsyScroll ), 4 );
				Add( typeof( FeeblemindScroll ), 4 );
				Add( typeof( HealScroll ), 4 );
				Add( typeof( MagicArrowScroll ), 4 );
				Add( typeof( NightSightScroll ), 4 );
				Add( typeof( WeakenScroll ), 4 );
				
				Add( typeof( AgilityScroll ), 6 );
				Add( typeof( CunningScroll ), 6 );
				Add( typeof( CureScroll ), 6 );
				Add( typeof( HarmScroll ), 6 );
				Add( typeof( MagicTrapScroll ), 6 );
				Add( typeof( MagicUnTrapScroll ), 6 );
				Add( typeof( ProtectionScroll ), 6 );
				Add( typeof( StrengthScroll ), 6 );
				
				Add( typeof( BlessScroll ), 8 );
				Add( typeof( FireballScroll ), 8 );
				Add( typeof( MagicLockScroll ), 8 );
				Add( typeof( PoisonScroll ), 8 );
				Add( typeof( TelekinisisScroll ), 8 );
				Add( typeof( TeleportScroll ), 8 );
				Add( typeof( UnlockScroll ), 8 );
				Add( typeof( WallOfStoneScroll ), 8 );

				Add( typeof( ArchCureScroll ), 10 );
				Add( typeof( ArchProtectionScroll ), 10 );
				Add( typeof( CurseScroll ), 10 );
				Add( typeof( FireFieldScroll ), 10 );
				Add( typeof( GreaterHealScroll ), 10 );
				Add( typeof( LightningScroll ), 10 );
				Add( typeof( ManaDrainScroll ), 10 );
				Add( typeof( RecallScroll ), 10 );

				Add( typeof( BladeSpiritsScroll ), 12 );
				Add( typeof( DispelFieldScroll ), 12 );
				Add( typeof( IncognitoScroll ), 12 );
				Add( typeof( MagicReflectScroll ), 12 );
				Add( typeof( MindBlastScroll ), 12 );
				Add( typeof( ParalyzeScroll ), 12 );
				Add( typeof( PoisonFieldScroll ), 12 );
				Add( typeof( SummonCreatureScroll ), 12 );

				Add( typeof( DispelScroll ), 14 );
				Add( typeof( EnergyBoltScroll ), 14 );
				Add( typeof( ExplosionScroll ), 14 );
				Add( typeof( InvisibilityScroll ), 14 );
				Add( typeof( MarkScroll ), 14 );
				Add( typeof( MassCurseScroll ), 14 );
				Add( typeof( ParalyzeFieldScroll ), 14 );
				Add( typeof( RevealScroll ), 14 );

				Add( typeof( ChainLightningScroll ), 16 );
				Add( typeof( EnergyFieldScroll ), 16 );
				Add( typeof( FlamestrikeScroll ), 16 );
				Add( typeof( GateTravelScroll ), 16 );
				Add( typeof( ManaVampireScroll ), 16 );
				Add( typeof( MassDispelScroll ), 16 );
				Add( typeof( MeteorSwarmScroll ), 16 );
				Add( typeof( PolymorphScroll ), 16 );

				Add( typeof( EarthquakeScroll ), 18 );
				Add( typeof( EnergyVortexScroll ), 18 );
				Add( typeof( ResurrectionScroll ), 18 );
				Add( typeof( SummonAirElementalScroll ), 18 );
				Add( typeof( SummonDaemonScroll ), 18 );
				Add( typeof( SummonEarthElementalScroll ), 18 );
				Add( typeof( SummonFireElementalScroll ), 18 );
				Add( typeof( SummonWaterElementalScroll ), 18 );
			}
		}
    }
}