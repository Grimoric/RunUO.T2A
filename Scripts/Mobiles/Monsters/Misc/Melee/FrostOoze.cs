namespace Server.Mobiles
{
    [CorpseName( "a frost ooze corpse" )]
	public class FrostOoze : BaseCreature
	{
		[Constructable]
		public FrostOoze() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a frost ooze";
			Body = 94;
			BaseSoundID = 456;

			SetStr( 18, 30 );
			SetDex( 16, 21 );
			SetInt( 16, 20 );

			SetHits( 13, 17 );

			SetDamage( 3, 9 );

			SetSkill( SkillName.MagicResist, 5.1, 10.0 );
			SetSkill( SkillName.Tactics, 19.3, 34.0 );
			SetSkill( SkillName.Wrestling, 25.3, 40.0 );

			Fame = 450;
			Karma = -450;

			VirtualArmor = 38;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Gems, Utility.RandomMinMax( 1, 2 ) );
		}

		public FrostOoze( Serial serial ) : base( serial )
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
		}
	}
}