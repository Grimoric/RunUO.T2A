namespace Server.Items
{
    public class LeatherDyeTub : DyeTub
	{
		public override bool AllowDyables{ get{ return false; } }
		public override bool AllowLeather{ get{ return true; } }
		public override int TargetMessage{ get{ return 1042416; } } // Select the leather item to dye.
		public override int FailMessage{ get{ return 1042418; } } // You can only dye leather with this tub.
		public override int LabelNumber{ get{ return 1041284; } } // Leather Dye Tub
		public override CustomHuePicker CustomHuePicker { get { return CustomHuePicker.LeatherDyeTub; } }

		[Constructable]
		public LeatherDyeTub()
		{
			LootType = LootType.Blessed;
		}

		public LeatherDyeTub( Serial serial ) : base( serial )
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