namespace Server.Items
{
    public class RunebookDyeTub : DyeTub
	{
		public override bool AllowDyables{ get{ return false; } }
		public override bool AllowRunebooks{ get{ return true; } }
		public override int TargetMessage{ get{ return 1049774; } } // Target the runebook or runestone to dye
		public override int FailMessage{ get{ return 1049775; } } // You can only dye runestones or runebooks with this tub.
		public override int LabelNumber{ get{ return 1049740; } } // Runebook Dye Tub
		public override CustomHuePicker CustomHuePicker{ get{ return CustomHuePicker.LeatherDyeTub; } }

		[Constructable]
		public RunebookDyeTub()
		{
			LootType = LootType.Blessed;
		}

		public RunebookDyeTub( Serial serial ) : base( serial )
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