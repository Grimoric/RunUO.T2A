using Server.Items;

namespace Server.Mobiles
{
    [CorpseName( "an impaler corpse" )]
	public class Impaler : BaseCreature
	{
		public override bool IgnoreYoungProtection { get { return false; } }

		[Constructable]
		public Impaler() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "impaler" );
			Body = 306;
			BaseSoundID = 0x2A7;

			SetStr( 190 );
			SetDex( 45 );
			SetInt( 190 );

			SetHits( 5000 );

			SetDamage( 31, 35 );

			SetSkill( SkillName.DetectHidden, 80.0 );
			SetSkill( SkillName.Meditation, 120.0 );
			SetSkill( SkillName.Poisoning, 160.0 );
			SetSkill( SkillName.MagicResist, 100.0 );
			SetSkill( SkillName.Tactics, 100.0 );
			SetSkill( SkillName.Wrestling, 80.0 );

			Fame = 24000;
			Karma = -24000;

			VirtualArmor = 49;
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

		public override bool AutoDispel{ get{ return true; } }
		public override bool BardImmune{ get{ return true; } }
		public override bool Unprovokable{ get{ return false; } }
		public override bool AreaPeaceImmune { get { return false; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override Poison HitPoison{ get{ return 0.8 >= Utility.RandomDouble() ? Poison.Greater : Poison.Deadly; } }

		public override int TreasureMapLevel{ get{ return 1; } }

		public Impaler( Serial serial ) : base( serial )
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

			if ( BaseSoundID == 1200 )
				BaseSoundID = 0x2A7;
		}
	}
}