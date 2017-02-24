namespace Server.Items
{
    public class LesserHealPotion : BaseHealPotion
	{
		public override int MinHeal { get { return 3; } }
		public override int MaxHeal { get { return 10; } }
		public override double Delay{ get{ return 10.0; } }

		[Constructable]
		public LesserHealPotion() : base( PotionEffect.HealLesser )
		{
		}

		public LesserHealPotion( Serial serial ) : base( serial )
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