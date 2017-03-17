using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    public class Banker : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		public override NpcGuild NpcGuild{ get{ return NpcGuild.MerchantsGuild; } }

		[Constructable]
		public Banker() : base( "the banker" )
		{
		}

        public override bool IsInvulnerable { get { return true; } }

        public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBBanker() );
		}

		public static int GetBalance( Mobile from )
		{
			Item[] gold;

			return GetBalance( from, out gold );
		}

		public static int GetBalance( Mobile from, out Item[] gold )
		{
			int balance = 0;

			Container bank = from.FindBankNoCreate();

			if ( bank != null )
			{
				gold = bank.FindItemsByType( typeof( Gold ) );

				for ( int i = 0; i < gold.Length; ++i )
					balance += gold[i].Amount;
			}
			else
			{
				gold = new Item[0];
			}

			return balance;
		}

		public static bool Withdraw( Mobile from, int amount )
		{
			Item[] gold;
			int balance = GetBalance( from, out gold );

			if ( balance < amount )
				return false;

			for ( int i = 0; amount > 0 && i < gold.Length; ++i )
			{
				if ( gold[i].Amount <= amount )
				{
					amount -= gold[i].Amount;
					gold[i].Delete();
				}
				else
				{
					gold[i].Amount -= amount;
					amount = 0;
				}
			}

			return true;
		}

		public static bool Deposit( Mobile from, int amount )
		{
			BankBox box = from.FindBankNoCreate();
			if ( box == null )
				return false;

			List<Item> items = new List<Item>();

			while ( amount > 0 )
			{
				Item item;
				if ( amount <= 60000 )
				{
					item = new Gold( amount );
					amount = 0;
				}
				else
				{
                    item = new Gold( 60000 );
                    amount -= 60000;
				}

				if ( box.TryDropItem( from, item, false ) )
				{
					items.Add( item );
				}
				else
				{
					item.Delete();
					foreach ( Item curItem in items )
					{
						curItem.Delete();
					}

					return false;
				}
			}

			return true;
		}

		public static int DepositUpTo( Mobile from, int amount )
		{
			BankBox box = from.FindBankNoCreate();
			if ( box == null )
				return 0;

			int amountLeft = amount;
			while ( amountLeft > 0 )
			{
				Item item;
				int amountGiven;

				if ( amountLeft <= 60000 )
				{
					item = new Gold( amountLeft );
					amountGiven = amountLeft;
				}
				else
				{
					item = new Gold( 60000 );
					amountGiven = 60000;
				}

				if ( box.TryDropItem( from, item, false ) )
				{
					amountLeft -= amountGiven;
				}
				else
				{
					item.Delete();
					break;
				}
			}

			return amount - amountLeft;
		}

		public static void Deposit( Container cont, int amount )
		{
			while ( amount > 0 )
			{
				Item item;

				if ( amount <= 60000 )
				{
					item = new Gold( amount );
					amount = 0;
				}
				else
				{
                    item = new Gold(60000);
                    amount -= 60000;
                }

				cont.DropItem( item );
			}
		}

		public Banker( Serial serial ) : base( serial )
		{
		}

		public override bool HandlesOnSpeech( Mobile from )
		{
			if ( from.InRange( this.Location, 12 ) )
				return true;

			return base.HandlesOnSpeech( from );
		}

		public override void OnSpeech( SpeechEventArgs e )
		{
			if ( !e.Handled && e.Mobile.InRange( this.Location, 12 ) )
			{
				for ( int i = 0; i < e.Keywords.Length; ++i )
				{
					int keyword = e.Keywords[i];

					switch ( keyword )
					{
						case 0x0000: // *withdraw*
						{
							e.Handled = true;

							if ( e.Mobile.Criminal )
							{
								this.Say( 500389 ); // I will not do business with a criminal!
								break;
							}

							string[] split = e.Speech.Split( ' ' );

							if ( split.Length >= 2 )
							{
								int amount;

								Container pack = e.Mobile.Backpack;

								if ( !int.TryParse( split[1], out amount ) )
									break;

								if ( amount > 5000 )
								{
									this.Say( 500381 ); // Thou canst not withdraw so much at one time!
								}
								else if (pack == null || pack.Deleted || !(pack.TotalWeight < pack.MaxWeight) || !(pack.TotalItems < pack.MaxItems))
								{
									this.Say(1048147); // Your backpack can't hold anything else.
								}
								else if (amount > 0)
								{
									BankBox box = e.Mobile.FindBankNoCreate();

									if (box == null || !box.ConsumeTotal(typeof(Gold), amount))
									{
										this.Say(500384); // Ah, art thou trying to fool me? Thou hast not so much gold!
									}
									else
									{
										pack.DropItem(new Gold(amount));

										this.Say(1010005); // Thou hast withdrawn gold from thy account.
									}
								}
							}

							break;
						}
						case 0x0001: // *balance*
						{
							e.Handled = true;

							if ( e.Mobile.Criminal )
							{
								this.Say( 500389 ); // I will not do business with a criminal!
								break;
							}

							BankBox box = e.Mobile.FindBankNoCreate();

							if ( box != null )
								this.Say( 1042759, box.TotalGold.ToString() ); // Thy current bank balance is ~1_AMOUNT~ gold.
							else
								this.Say( 1042759, "0" ); // Thy current bank balance is ~1_AMOUNT~ gold.

							break;
						}
						case 0x0002: // *bank*
						{
							e.Handled = true;

							if ( e.Mobile.Criminal )
							{
								this.Say( 500378 ); // Thou art a criminal and cannot access thy bank box.
								break;
							}

							e.Mobile.BankBox.Open();

							break;
						}
					}
				}
			}

			base.OnSpeech( e );
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