using Server.Items;

namespace Server.Mobiles
{
    [CorpseName( "a Grobu corpse" )]
	public class Grobu : BlackBear
	{
		[Constructable]
		public Grobu()
		{
			IsParagon = true;

			Name = "Grobu";
			Hue = 0x455;

			AI = AIType.AI_Melee;
			FightMode = FightMode.Closest;

			SetStr( 192, 210 );
			SetDex( 132, 150 );
			SetInt( 50, 52 );

			SetHits( 1235, 1299 );
			SetStam( 132, 150 );
			SetMana( 9 );

			SetDamage( 15, 18 );

			SetSkill( SkillName.Wrestling, 96.4, 119.0 );
			SetSkill( SkillName.Tactics, 96.2, 116.5 );
			SetSkill( SkillName.MagicResist, 66.2, 83.7 );

			Fame = 1000;
			Karma = 1000;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
		}

		public Grobu( Serial serial )
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
