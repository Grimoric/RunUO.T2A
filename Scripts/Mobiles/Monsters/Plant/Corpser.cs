using Server.Items;

namespace Server.Mobiles
{
    [CorpseName( "a corpser corpse" )]
	public class Corpser : BaseCreature
	{
		[Constructable]
		public Corpser() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a corpser";
			Body = 8;
			BaseSoundID = 684;

			SetStr( 156, 180 );
			SetDex( 26, 45 );
			SetInt( 26, 40 );

			SetHits( 94, 108 );
			SetMana( 0 );

			SetDamage( 10, 23 );

			SetSkill( SkillName.MagicResist, 15.1, 20.0 );
			SetSkill( SkillName.Tactics, 45.1, 60.0 );
			SetSkill( SkillName.Wrestling, 45.1, 60.0 );

			Fame = 1000;
			Karma = -1000;

			VirtualArmor = 18;

			if ( 0.25 > Utility.RandomDouble() )
				PackItem( new Board( 10 ) );
			else
				PackItem( new Log( 10 ) );

			PackItem( new MandrakeRoot( 3 ) );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override Poison PoisonImmune{ get{ return Poison.Lesser; } }
		public override bool DisallowAllMoves{ get{ return true; } }

		public Corpser( Serial serial ) : base( serial )
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

			if ( BaseSoundID == 352 )
				BaseSoundID = 684;
		}
	}
}
