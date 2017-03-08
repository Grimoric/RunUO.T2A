namespace Server.Items
{
    public class SpecialDyeTub : DyeTub
	{
		public override CustomHuePicker CustomHuePicker{ get{ return CustomHuePicker.SpecialDyeTub; } }
		public override int LabelNumber{ get{ return 1041285; } } // Special Dye Tub

		[Constructable]
		public SpecialDyeTub()
		{
			LootType = LootType.Blessed;
		}

		public SpecialDyeTub( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}