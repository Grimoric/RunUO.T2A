using Server.Items;

namespace Server.Mobiles
{
    [CorpseName( "an abyssmal horror corpse" )]
	public class AbysmalHorror : BaseCreature
	{
		public override bool IgnoreYoungProtection { get { return false; } }

		[Constructable]
		public AbysmalHorror() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an abyssmal horror";
			Body = 312;
			BaseSoundID = 0x451;

			SetStr( 401, 420 );
			SetDex( 81, 90 );
			SetInt( 401, 420 );

			SetHits( 6000 );

			SetDamage( 13, 17 );

			SetSkill( SkillName.EvalInt, 200.0 );
			SetSkill( SkillName.Magery, 112.6, 117.5 );
			SetSkill( SkillName.Meditation, 200.0 );
			SetSkill( SkillName.MagicResist, 117.6, 120.0 );
			SetSkill( SkillName.Tactics, 100.0 );
			SetSkill( SkillName.Wrestling, 84.1, 88.0 );

			Fame = 26000;
			Karma = -26000;

			VirtualArmor = 54;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 2 );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			if ( !Summoned && !NoKillAwards && DemonKnight.CheckArtifactChance( this ) )
				DemonKnight.DistributeArtifact( this );
		}

		public override bool BardImmune{ get{ return true; } }
		public override bool Unprovokable{ get{ return false; } }
		public override bool AreaPeaceImmune { get { return false; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override int TreasureMapLevel{ get{ return 1; } }

		public AbysmalHorror( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			if ( BaseSoundID == 357 )
				BaseSoundID = 0x451;
		}
	}
}