using System;
using Server.Items;
using Server.Engines.CannedEvil;

namespace Server.Mobiles
{
    public class Serado : BaseChampion
	{
		public override ChampionSkullType SkullType{ get{ return ChampionSkullType.Power; } }

		public override Type[] UniqueList{ get{ return new Type[] { typeof( Pacify ) }; } }
		public override Type[] SharedList{ get{ return new Type[] { 	typeof( BraveKnightOfTheBritannia ),
										typeof( DetectiveBoots ),
										typeof( EmbroideredOakLeafCloak ),
										typeof( LieutenantOfTheBritannianRoyalGuard ) }; } }
		public override Type[] DecorativeList{ get{ return new Type[] { typeof( Futon ), typeof( SwampTile ) }; } }

		public override MonsterStatuetteType[] StatueTypes{ get{ return new MonsterStatuetteType[] { }; } }

		[Constructable]
		public Serado() : base( AIType.AI_Melee )
		{
			Name = "Serado";
			Title = "the awakened";

			Body = 249;
			Hue = 0x96C;

			SetStr( 1000 );
			SetDex( 150 );
			SetInt( 300 );

			SetHits( 9000 );
			SetMana( 300 );

			SetDamage( 29, 35 );

			SetSkill( SkillName.MagicResist, 120.0 );
			SetSkill( SkillName.Tactics, 120.0 );
			SetSkill( SkillName.Wrestling, 70.0 );
			SetSkill( SkillName.Poisoning, 150.0 );

			Fame = 22500;
			Karma = -22500;

			PackItem( Engines.Plants.Seed.RandomBonsaiSeed() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 4 );
			AddLoot( LootPack.FilthyRich );
			AddLoot( LootPack.Gems, 6 );
		}

		public override int TreasureMapLevel{ get{ return 5; } }

		public override Poison HitPoison{ get{ return Poison.Lethal; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override double HitPoisonChance{	get{ return 0.8; } }

		public override int Feathers{ get{ return 30; } }

		public override bool ShowFameTitle{ get{ return false; } }
		public override bool ClickTitle{ get{ return false; } }

		public Serado( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
