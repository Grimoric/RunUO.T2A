using Server.Items;

namespace Server.Mobiles
{
    [CorpseName( "a hydra corpse" )]
	public class Hydra : BaseCreature
	{
		[Constructable]
		public Hydra()
			: base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a hydra";
			Body = 0x109;
			BaseSoundID = 0x16A;

			SetStr( 801, 828 );
			SetDex( 102, 118 );
			SetInt( 102, 120 );

			SetHits( 1480, 1500 );

			SetDamage( 21, 26 );

			SetSkill( SkillName.Wrestling, 103.5, 117.4 );
			SetSkill( SkillName.Tactics, 100.1, 109.8 );
			SetSkill( SkillName.MagicResist, 85.5, 98.5 );
			SetSkill( SkillName.Anatomy, 75.4, 79.8 );

			// TODO: Fame/Karma
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 3 );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			c.DropItem( new HydraScale() );

			/*
			// TODO: uncomment once added
			if ( Utility.RandomDouble() < 0.2 )
				c.DropItem( new ParrotItem() );

			if ( Utility.RandomDouble() < 0.05 )
				c.DropItem( new ThorvaldsMedallion() );
			*/
		}

		public override bool HasBreath { get { return true; } }
		public override int Hides { get { return 40; } }
		public override int Meat { get { return 19; } }
		public override int TreasureMapLevel { get { return 5; } }

		public Hydra( Serial serial )
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
