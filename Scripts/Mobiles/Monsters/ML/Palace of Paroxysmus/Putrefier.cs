namespace Server.Mobiles
{
    [CorpseName( "a Putrefier corpse" )]
	public class Putrefier : Balron
	{
		[Constructable]
		public Putrefier()
		{
			IsParagon = true;

			Name = "Putrefier";
			Hue = 63;

			SetStr( 1057, 1400 );
			SetDex( 232, 560 );
			SetInt( 201, 440 );

			SetHits( 3010, 4092 );

			SetDamage( 27, 34 );

			SetSkill( SkillName.Wrestling, 111.2, 128.0 );
			SetSkill( SkillName.Tactics, 115.2, 125.2 );
			SetSkill( SkillName.MagicResist, 143.4, 170.0 );
			SetSkill( SkillName.Anatomy, 44.6, 67.0 );
			SetSkill( SkillName.Magery, 117.6, 118.8 );
			SetSkill( SkillName.EvalInt, 113.0, 128.8 );
			SetSkill( SkillName.Meditation, 41.4, 85.0 );
			SetSkill( SkillName.Poisoning, 45.0, 50.0 );

			Fame = 24000;
			Karma = -24000;

			PackScroll( 4, 7 );
			PackScroll( 4, 7 );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 3 );
		}

		public override Poison HitPoison{ get{ return Poison.Deadly; } } // Becomes Lethal with Paragon bonus
		public override int TreasureMapLevel{ get{ return 5; } }

		public Putrefier( Serial serial )
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
