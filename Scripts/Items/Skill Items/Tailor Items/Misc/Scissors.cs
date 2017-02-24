using Server.Targeting;

namespace Server.Items
{
    public interface IScissorable
	{
		bool Scissor( Mobile from, Scissors scissors );
	}

	[FlipableAttribute( 0xf9f, 0xf9e )]
	public class Scissors : Item
	{
		[Constructable]
		public Scissors() : base( 0xF9F )
		{
			Weight = 1.0;
		}

		public Scissors( Serial serial ) : base( serial )
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

		public override void OnDoubleClick( Mobile from )
		{
			from.SendLocalizedMessage( 502434 ); // What should I use these scissors on?

			from.Target = new InternalTarget( this );
		}

		private class InternalTarget : Target
		{
			private Scissors m_Item;

			public InternalTarget( Scissors item ) : base( 2, false, TargetFlags.None )
			{
				m_Item = item;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Item.Deleted )
					return;

				if ( targeted is Item && !((Item)targeted).IsStandardLoot() )
				{
					from.SendLocalizedMessage( 502440 ); // Scissors can not be used on that to produce anything.
				}
				else if( targeted is Item && !((Item)targeted).Movable ) 
				{
					if( targeted is IScissorable && ( targeted is PlagueBeastInnard || targeted is PlagueBeastMutationCore ) )
					{
						IScissorable obj = (IScissorable) targeted;

						if ( CanScissor( from, obj ) && obj.Scissor( from, m_Item ) )
							from.PlaySound( 0x248 );
					}
				}
				else if ( targeted is IScissorable )
				{
					IScissorable obj = (IScissorable)targeted;

					if ( CanScissor( from, obj ) && obj.Scissor( from, m_Item ) )
						from.PlaySound( 0x248 );
				}
				else
				{
					from.SendLocalizedMessage( 502440 ); // Scissors can not be used on that to produce anything.
				}
			}

			protected override void OnNonlocalTarget( Mobile from, object targeted )
			{
				if ( targeted is IScissorable && ( targeted is PlagueBeastInnard || targeted is PlagueBeastMutationCore ) )
				{
					IScissorable obj = (IScissorable) targeted;

					if ( CanScissor( from, obj ) && obj.Scissor( from, m_Item ) )
						from.PlaySound( 0x248 );
				}
				else
					base.OnNonlocalTarget( from, targeted );
			}
		}

		public static bool CanScissor( Mobile from, IScissorable obj )
		{
			if ( obj is Item && ( (Item)obj ).Nontransferable )
			{
				from.SendLocalizedMessage( 502440 ); // Scissors can not be used on that to produce anything.
				return false;
			}

			// TODO: Move other general checks from the different implementations here

			return true;
		}
	}
}
