namespace Server.Mobiles
{
    [CorpseName( "a Master Mikael corpse" )]
	public class MasterMikael : BoneMagi
	{
		[Constructable]
		public MasterMikael()
		{
			IsParagon = true;

			Name = "Master Mikael";
			Hue = 0x8FD;

			SetStr( 93, 122 );
			SetDex( 91, 100 );
			SetInt( 252, 271 );

			SetHits( 789, 1014 );

			SetDamage( 11, 19 );

			SetSkill( SkillName.Wrestling, 80.1, 87.2 );
			SetSkill( SkillName.Tactics, 79.0, 90.9 );
			SetSkill( SkillName.MagicResist, 90.3, 106.9 );
			SetSkill( SkillName.Magery, 103.8, 108.0 );
			SetSkill( SkillName.EvalInt, 96.1, 105.3 );
			SetSkill( SkillName.Necromancy, 103.8, 108.0 );
			SetSkill( SkillName.SpiritSpeak, 96.1, 105.3 );

			Fame = 18000;
			Karma = -18000;

			if ( Utility.RandomBool() )
				PackNecroScroll( Utility.RandomMinMax( 5, 9 ) );
			else
				PackScroll( 4, 7 );

			PackReg( 3 );
			PackNecroReg( 1, 10 );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 2 );
		}

		public MasterMikael( Serial serial )
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
