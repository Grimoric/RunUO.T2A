using Server.Items;

namespace Server.Mobiles
{
    [CorpseName( "a Miasma corpse" )]
	public class Miasma : Scorpion
	{
		[Constructable]
		public Miasma()
		{
			IsParagon = true;

			Name = "Miasma";
			Hue = 0x8FD;

			SetStr( 255, 847 );
			SetDex( 145, 428 );
			SetInt( 26, 380 );

			SetHits( 750, 2000 );
			SetMana( 5, 60 );

			SetDamage( 20, 30 );

			SetSkill( SkillName.Wrestling, 84.9, 103.3 );
			SetSkill( SkillName.Tactics, 98.4, 110.6 );
			SetSkill( SkillName.Anatomy, 0 );
			SetSkill( SkillName.MagicResist, 74.4, 77.7 );
			SetSkill( SkillName.Poisoning, 128.5, 143.6 );

			Fame = 21000;
			Karma = -21000;
		}

			/* yes, this is OSI style */
		public override double HitPoisonChance { get { return 0.35; } }
		public override Poison HitPoison { get { return Poison.Lethal; } }
		public override bool HasManaOveride { get { return true; } }
		public override int TreasureMapLevel { get { return 5; } }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 4 );
		}

		public Miasma( Serial serial )
			: base( serial )
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
