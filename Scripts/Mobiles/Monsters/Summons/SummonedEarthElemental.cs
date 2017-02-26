namespace Server.Mobiles
{
    [CorpseName( "an earth elemental corpse" )]
	public class SummonedEarthElemental : BaseCreature
	{
		public override double DispelDifficulty{ get{ return 117.5; } }
		public override double DispelFocus{ get{ return 45.0; } }

		[Constructable]
		public SummonedEarthElemental() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an earth elemental";
			Body = 14;
			BaseSoundID = 268;

			SetStr( 200 );
			SetDex( 70 );
			SetInt( 70 );

			SetHits( 180 );

			SetDamage( 14, 21 );

			SetSkill( SkillName.MagicResist, 65.0 );
			SetSkill( SkillName.Tactics, 100.0 );
			SetSkill( SkillName.Wrestling, 90.0 );

			VirtualArmor = 34;
			ControlSlots = 2;
		}

		public SummonedEarthElemental( Serial serial ) : base( serial )
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