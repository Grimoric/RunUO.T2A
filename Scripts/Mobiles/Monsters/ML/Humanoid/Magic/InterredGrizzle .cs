namespace Server.Mobiles
{
    [CorpseName("an interred grizzle corpse")]
	public class InterredGrizzle  : BaseCreature
	{
		[Constructable]
		public  InterredGrizzle () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an interred grizzle";
			Body = 259;

			SetStr( 451, 500 );
			SetDex( 201, 250 );
			SetInt( 801, 850 );

			SetHits( 1500 );
			SetStam( 150 );

			SetDamage( 16, 19 );

			SetSkill(SkillName.Meditation, 77.7, 84.0 );
			SetSkill(SkillName.EvalInt, 72.2, 79.6 );
			SetSkill(SkillName.Magery, 83.7, 89.6);
			SetSkill(SkillName.Poisoning, 0 );
			SetSkill(SkillName.Anatomy, 0 );
			SetSkill( SkillName.MagicResist, 80.2, 87.3 );
			SetSkill( SkillName.Tactics, 104.5, 105.1 );
			SetSkill( SkillName.Wrestling, 105.1, 109.4 );

			Fame = 3700;  // Guessed
			Karma = -3700;  // Guessed
		}

		public override void GenerateLoot() // -- Need to verify
		{
			AddLoot( LootPack.FilthyRich );
		}

		// TODO: Acid Blood
		/*
		 * Message: 1070820
		 * Spits pool of acid (blood, hue 0x3F), hits lost 6-10 per second/step
		 * Damage is resistable (physical)
		 * Acid last 10 seconds
		 */
		 
		public override int GetAngerSound()
		{
			return 0x581;
		}

		public override int GetIdleSound()
		{
			return 0x582;
		}

		public override int GetAttackSound()
		{
			return 0x580;
		}

		public override int GetHurtSound()
		{
			return 0x583;
		}

		public override int GetDeathSound()
		{
			return 0x584;
		}

		private int RandomPoint( int mid )
		{
			return mid + Utility.RandomMinMax( -2, 2 );
		}

		public virtual Point3D GetSpawnPosition( int range )
		{
			return GetSpawnPosition( Location, Map, range );
		}

		public virtual Point3D GetSpawnPosition( Point3D from, Map map, int range )
		{
			if( map == null )
				return from;

			Point3D loc = new Point3D( RandomPoint( X ), RandomPoint( Y ), Z );

			loc.Z = Map.GetAverageZ( loc.X, loc.Y );

			return loc;
		}

		public  InterredGrizzle ( Serial serial ) : base( serial )
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
