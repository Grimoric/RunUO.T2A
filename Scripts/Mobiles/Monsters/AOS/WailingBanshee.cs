using Server.Items;

namespace Server.Mobiles
{
    [CorpseName( "a wailing banshee corpse" )]
	public class WailingBanshee : BaseCreature
	{
		[Constructable]
		public WailingBanshee() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a wailing banshee";
			Body = 310;
			BaseSoundID = 0x482;

			SetStr( 126, 150 );
			SetDex( 76, 100 );
			SetInt( 86, 110 );

			SetHits( 76, 90 );

			SetDamage( 10, 14 );

			SetSkill( SkillName.MagicResist, 70.1, 95.0 );
			SetSkill( SkillName.Tactics, 45.1, 70.0 );
			SetSkill( SkillName.Wrestling, 50.1, 70.0 );

			Fame = 1500;
			Karma = -1500;

			VirtualArmor = 19;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override bool BleedImmune{ get{ return true; } }

		public WailingBanshee( Serial serial ) : base( serial )
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