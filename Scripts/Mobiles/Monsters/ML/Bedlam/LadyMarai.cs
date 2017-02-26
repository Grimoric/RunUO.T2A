namespace Server.Mobiles
{
    [CorpseName( "a Lady Marai corpse" )]
	public class LadyMarai : SkeletalKnight
	{
		[Constructable]
		public LadyMarai()
		{
			IsParagon = true;

			Name = "Lady Marai";
			Hue = 0x21;

			SetStr( 221, 304 );
			SetDex( 98, 138 );
			SetInt( 54, 99 );

			SetHits( 694, 846 );

			SetDamage( 15, 25 );

			SetSkill( SkillName.Wrestling, 126.6, 137.2 );
			SetSkill( SkillName.Tactics, 128.7, 134.5 );
			SetSkill( SkillName.MagicResist, 102.1, 119.1 );
			SetSkill( SkillName.Anatomy, 126.2, 136.5 );

			Fame = 18000;
			Karma = -18000;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 3 );
		}


		public LadyMarai( Serial serial )
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
