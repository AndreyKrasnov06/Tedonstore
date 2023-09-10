using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.CSharp.RuntimeBinder;
using VertigoBoostPanel.Core;
using VertigoBoostPanel.Core.ErrorHandler;
using VertigoBoostPanel.Core.Player;
using VertigoBoostPanel.Model;
using VertigoBoostPanel.Services.Files;
using VertigoBoostPanel.Static;
using VertigoBoostPanel.UI.elements;

namespace VertigoBoostPanel.UI.pages
{
	// Token: 0x0200004C RID: 76
	public partial class AccountsPage : Page
	{
		// Token: 0x060001BA RID: 442 RVA: 0x0000CC54 File Offset: 0x0000AE54
		public AccountsPage()
		{
			base.DataContext = this;
			this.InitializeComponent();
			Program.GetInstance.AccountsPage = this;
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001BB RID: 443 RVA: 0x000036A8 File Offset: 0x000018A8
		public string Yes
		{
			get
			{
				return "/Resources/img/yes.png";
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001BC RID: 444 RVA: 0x000036AF File Offset: 0x000018AF
		public string Null
		{
			get
			{
				return "/Resources/img/null.png";
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001BD RID: 445 RVA: 0x000034C4 File Offset: 0x000016C4
		public string Accounts
		{
			get
			{
				return "/Resources/img/accounts.png";
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001BE RID: 446 RVA: 0x000036B6 File Offset: 0x000018B6
		public string Trash
		{
			get
			{
				return "/Resources/img/trash.png";
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001BF RID: 447 RVA: 0x000036BD File Offset: 0x000018BD
		public string Steam
		{
			get
			{
				return "/Resources/img/steam.png";
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x000036C4 File Offset: 0x000018C4
		public string CopyIcon
		{
			get
			{
				return "/Resources/img/copyIcon.png";
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x000036CB File Offset: 0x000018CB
		public string AccountMark
		{
			get
			{
				return "/Resources/img/account_mark.png";
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x000036D2 File Offset: 0x000018D2
		public string PageLeft
		{
			get
			{
				return "/Resources/img/pageLeft.png";
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x000036D9 File Offset: 0x000018D9
		public string PageRight
		{
			get
			{
				return "/Resources/img/pageRight.png";
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000CC8C File Offset: 0x0000AE8C
		public AccountsPage ReadyToSwap()
		{
			this.ReadyToLeave();
			this.CreatePages();
			if (this.AccountsBox.Items.Count == 0)
			{
				this.checkbox.IsChecked = new bool?(false);
			}
			this.textBox.Text = "";
			this.textBox_Copy.Text = "";
			Program.GetInstance.accPageActive = true;
			return this;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000CCF4 File Offset: 0x0000AEF4
		public void ReadyToLeave()
		{
			this.AccountsBox.Items.Clear();
			Program.GetInstance.accPageActive = false;
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x000036E0 File Offset: 0x000018E0
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x0000CD1C File Offset: 0x0000AF1C
		public string LoginSearchMask
		{
			get
			{
				return this._loginSearchMask;
			}
			set
			{
				this._loginSearchMask = value;
				this.CreatePages();
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000CD38 File Offset: 0x0000AF38
		public void CreatePages()
		{
			this.AccountsBox.Items.Clear();
			this.pages.Clear();
			AccountPage accountPage = null;
			accountPage = new AccountPage();
			IEnumerable<Player> enumerable = from x in StaticData.GetInstance.Players
				where this.currentChoisedMark == 0 || x.Mark == this.currentChoisedMark
				where string.IsNullOrEmpty(this.LoginSearchMask) || x.Login.Contains(this.LoginSearchMask)
				select x;
			if (this._order_level != 0)
			{
				if (this._order_level == 1)
				{
					enumerable = enumerable.OrderBy((Player x) => x.Lvl);
				}
				else if (this._order_level == 2)
				{
					enumerable = enumerable.OrderByDescending((Player x) => x.Lvl);
				}
			}
			if (this._order_rank5 != 0)
			{
				if (this._order_rank5 == 1)
				{
					enumerable = enumerable.OrderBy((Player x) => x.Rank5x5);
				}
				else if (this._order_rank5 == 2)
				{
					enumerable = enumerable.OrderByDescending((Player x) => x.Rank5x5);
				}
			}
			if (this._order_rank2 != 0)
			{
				if (this._order_rank2 == 1)
				{
					enumerable = enumerable.OrderBy((Player x) => x.Rank2x2);
				}
				else if (this._order_rank2 == 2)
				{
					enumerable = enumerable.OrderByDescending((Player x) => x.Rank2x2);
				}
			}
			if (this._order_wins5 != 0)
			{
				if (this._order_wins5 == 1)
				{
					enumerable = enumerable.OrderBy((Player x) => x.Wins5x5);
				}
				else if (this._order_wins5 == 2)
				{
					enumerable = enumerable.OrderByDescending((Player x) => x.Wins5x5);
				}
			}
			if (this._order_wins2 != 0)
			{
				if (this._order_wins2 == 1)
				{
					enumerable = enumerable.OrderBy((Player x) => x.Wins2x2);
				}
				else if (this._order_wins2 == 2)
				{
					enumerable = enumerable.OrderByDescending((Player x) => x.Wins2x2);
				}
			}
			if (this._order_prime != 0)
			{
				if (this._order_prime == 1)
				{
					enumerable = enumerable.Where((Player x) => x.Prime == 1);
				}
				else if (this._order_prime == 2)
				{
					enumerable = enumerable.Where((Player x) => x.Prime == 0);
				}
			}
			foreach (AccountShow accountShow in enumerable.Select((Player player) => player.PlayerShow))
			{
				accountPage.players.Add(accountShow);
				if (accountPage.players.Count == 20)
				{
					this.pages.Add(accountPage);
					accountPage = new AccountPage();
				}
			}
			if (accountPage.players.Count != 0)
			{
				this.pages.Add(accountPage);
			}
			this.current_page = 0;
			this.NavigateToPage();
			GC.Collect();
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000D0E0 File Offset: 0x0000B2E0
		public void NavigateToPage()
		{
			if (this.pages.Count != 0)
			{
				this.AccountsBox.Items.Clear();
				foreach (AccountShow accountShow in this.pages[this.current_page].players)
				{
					this.AccountsBox.Items.Add(accountShow);
				}
				this.n_page.Content = string.Format("{0}/{1}", this.current_page + 1, this.pages.Count);
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000D1A4 File Offset: 0x0000B3A4
		private void rightPage_Click(object sender, RoutedEventArgs e)
		{
			if (this.current_page == this.pages.Count - 1)
			{
				this.current_page = 0;
			}
			else
			{
				this.current_page++;
			}
			this.NavigateToPage();
			this.checkbox.IsChecked = new bool?(false);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000D1F4 File Offset: 0x0000B3F4
		private void leftPage_Click(object sender, RoutedEventArgs e)
		{
			if (this.current_page == 0)
			{
				this.current_page = this.pages.Count - 1;
			}
			else
			{
				this.current_page--;
			}
			this.NavigateToPage();
			this.checkbox.IsChecked = new bool?(false);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000D244 File Offset: 0x0000B444
		private void MenuButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			System.Windows.Controls.Button button = (System.Windows.Controls.Button)sender;
			if (button.Opacity == 0.5)
			{
				button.Opacity = 1.0;
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000D27C File Offset: 0x0000B47C
		private void MenuButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
		{
			System.Windows.Controls.Button button = (System.Windows.Controls.Button)sender;
			if (button.Opacity == 1.0)
			{
				button.Opacity = 0.5;
			}
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000D2B4 File Offset: 0x0000B4B4
		private void Checked(object sender, RoutedEventArgs e)
		{
			foreach (object obj in ((IEnumerable)this.AccountsBox.Items))
			{
				((AccountShow)obj).EditCheck(true);
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000D318 File Offset: 0x0000B518
		private void Unchecked(object sender, RoutedEventArgs e)
		{
			foreach (object obj in ((IEnumerable)this.AccountsBox.Items))
			{
				((AccountShow)obj).EditCheck(false);
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000D37C File Offset: 0x0000B57C
		private void add_accounts_Copy1_Click(object sender, RoutedEventArgs e)
		{
			string text = this.textBox.Text;
			string text2 = this.textBox_Copy.Text;
			Program.GetInstance.SetupNewAccount(text, text2);
			this.ReadyToSwap();
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000D3B8 File Offset: 0x0000B5B8
		private void add_accounts_Copy6_Click(object sender, RoutedEventArgs e)
		{
			string path = string.Empty;
			OpenFileDialog OPF = new OpenFileDialog();
			Exception threadEx = null;
			Thread thread = new Thread(delegate
			{
				try
				{
					OPF.Filter = "Файлы txt|*.txt";
					OPF.InitialDirectory = "C:\\Users\\" + Environment.UserName + "\\Desktop\\";
					if (OPF.ShowDialog() == DialogResult.OK)
					{
						path = OPF.FileName;
					}
					else
					{
						path = "None";
					}
				}
				catch (Exception ex)
				{
					threadEx = ex;
					path = "None";
				}
				Console.WriteLine(path);
			});
			thread.SetApartmentState(ApartmentState.STA);
			thread.Start();
			thread.Join();
			object obj = true;
			if ((string.IsNullOrEmpty(path) || string.IsNullOrWhiteSpace(path)) && !(path == "None"))
			{
				obj = false;
			}
			else
			{
				if (!File.Exists(path))
				{
					CreateError.NewError("File does not exist", false);
					return;
				}
				string[] array = File.ReadAllLines(path);
				object obj2 = new Random();
				if (AccountsPage.<>o__42.<>p__0 == null)
				{
					AccountsPage.<>o__42.<>p__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Next", null, typeof(AccountsPage), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				object obj3 = AccountsPage.<>o__42.<>p__0.Target(AccountsPage.<>o__42.<>p__0, obj2, 10);
				string[] array2 = array;
				int i = 0;
				while (i < array2.Length)
				{
					string text = array2[i];
					if (!Program.GetInstance.LoginWindow.signInButton.IsEnabled)
					{
						goto IL_3CE;
					}
					if (AccountsPage.<>o__42.<>p__1 == null)
					{
						AccountsPage.<>o__42.<>p__1 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof(AccountsPage), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
						}));
					}
					object obj4 = AccountsPage.<>o__42.<>p__1.Target(AccountsPage.<>o__42.<>p__1, obj3, 1);
					if (AccountsPage.<>o__42.<>p__4 == null)
					{
						AccountsPage.<>o__42.<>p__4 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(AccountsPage), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
					}
					object obj5;
					if (AccountsPage.<>o__42.<>p__4.Target(AccountsPage.<>o__42.<>p__4, obj4))
					{
						obj5 = obj4;
					}
					else
					{
						if (AccountsPage.<>o__42.<>p__3 == null)
						{
							AccountsPage.<>o__42.<>p__3 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.Or, typeof(AccountsPage), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
							}));
						}
						Func<CallSite, object, object, object> target = AccountsPage.<>o__42.<>p__3.Target;
						CallSite <>p__ = AccountsPage.<>o__42.<>p__3;
						object obj6 = obj4;
						if (AccountsPage.<>o__42.<>p__2 == null)
						{
							AccountsPage.<>o__42.<>p__2 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof(AccountsPage), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
							}));
						}
						obj5 = target(<>p__, obj6, AccountsPage.<>o__42.<>p__2.Target(AccountsPage.<>o__42.<>p__2, obj3, 3));
					}
					object obj7 = obj5;
					if (AccountsPage.<>o__42.<>p__8 == null)
					{
						AccountsPage.<>o__42.<>p__8 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(AccountsPage), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
					}
					if (!AccountsPage.<>o__42.<>p__8.Target(AccountsPage.<>o__42.<>p__8, obj7))
					{
						if (AccountsPage.<>o__42.<>p__7 == null)
						{
							AccountsPage.<>o__42.<>p__7 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(AccountsPage), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
						}
						Func<CallSite, object, bool> target2 = AccountsPage.<>o__42.<>p__7.Target;
						CallSite <>p__2 = AccountsPage.<>o__42.<>p__7;
						if (AccountsPage.<>o__42.<>p__6 == null)
						{
							AccountsPage.<>o__42.<>p__6 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.Or, typeof(AccountsPage), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
							}));
						}
						Func<CallSite, object, object, object> target3 = AccountsPage.<>o__42.<>p__6.Target;
						CallSite <>p__3 = AccountsPage.<>o__42.<>p__6;
						object obj8 = obj7;
						if (AccountsPage.<>o__42.<>p__5 == null)
						{
							AccountsPage.<>o__42.<>p__5 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof(AccountsPage), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
							}));
						}
						if (!target2(<>p__2, target3(<>p__3, obj8, AccountsPage.<>o__42.<>p__5.Target(AccountsPage.<>o__42.<>p__5, obj3, 7))))
						{
							goto IL_3CE;
						}
					}
					IL_520:
					i++;
					continue;
					IL_3CE:
					if (string.IsNullOrEmpty(text) || (string.IsNullOrWhiteSpace(text) && text.Length > 2))
					{
						goto IL_520;
					}
					if (text.Contains(" "))
					{
						Program.GetInstance.SetupNewAccount(text.Split(new char[] { ' ' })[0], text.Split(new char[] { ' ' })[1]);
						goto IL_520;
					}
					if (text.Contains("\t"))
					{
						Program.GetInstance.SetupNewAccount(text.Split(new char[] { '\t' })[0], text.Split(new char[] { '\t' })[1]);
						goto IL_520;
					}
					if (text.Contains(":"))
					{
						Program.GetInstance.SetupNewAccount(text.Split(new char[] { ':' })[0], text.Split(new char[] { ':' })[1]);
						goto IL_520;
					}
					if (text.Contains(","))
					{
						Program.GetInstance.SetupNewAccount(text.Split(new char[] { ',' })[0], text.Split(new char[] { ',' })[1]);
						goto IL_520;
					}
					obj = false;
					System.Windows.Forms.MessageBox.Show("Failed to parse string " + text, "VertigoBoostPanel");
					goto IL_520;
				}
			}
			if (AccountsPage.<>o__42.<>p__9 == null)
			{
				AccountsPage.<>o__42.<>p__9 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(AccountsPage), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
			}
			if (AccountsPage.<>o__42.<>p__9.Target(AccountsPage.<>o__42.<>p__9, obj))
			{
				CreateError.NewNotification("Success import", false);
			}
			this.ReadyToSwap();
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000D964 File Offset: 0x0000BB64
		private void add_accounts_Copy5_Click(object sender, RoutedEventArgs e)
		{
			foreach (object obj in ((IEnumerable)this.AccountsBox.Items))
			{
				AccountShow accountShow = (AccountShow)obj;
				if (accountShow.IsChecked)
				{
					accountShow.DeletePlayer();
				}
			}
			this.ReadyToSwap();
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000D9DC File Offset: 0x0000BBDC
		private void add_accounts_Copy_Click(object sender, RoutedEventArgs e)
		{
			new Task(delegate
			{
				foreach (object obj in ((IEnumerable)this.AccountsBox.Items))
				{
					AccountShow accountShow = (AccountShow)obj;
					if (accountShow.IsChecked)
					{
						accountShow.CheckEnterToSteam();
					}
				}
				foreach (object obj2 in ((IEnumerable)this.AccountsBox.Items))
				{
					AccountShow accountShow2 = (AccountShow)obj2;
					if (accountShow2.IsChecked)
					{
						accountShow2.StartPlayer();
					}
				}
			}).Start();
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000DA00 File Offset: 0x0000BC00
		private void add_accounts_Copy2_Click(object sender, RoutedEventArgs e)
		{
			foreach (object obj in ((IEnumerable)this.AccountsBox.Items))
			{
				AccountShow accountShow = (AccountShow)obj;
				if (accountShow.IsChecked)
				{
					accountShow.StopPlayer();
				}
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000DA70 File Offset: 0x0000BC70
		private void add_accounts_Copy4_Click(object sender, RoutedEventArgs e)
		{
			foreach (object obj in ((IEnumerable)this.AccountsBox.Items))
			{
				AccountShow accountShow = (AccountShow)obj;
				if (accountShow.IsChecked)
				{
					accountShow.OpenSteam();
				}
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000DAE0 File Offset: 0x0000BCE0
		private void add_accounts_Copy3_Click(object sender, RoutedEventArgs e)
		{
			foreach (Player player in StaticData.GetInstance.Players)
			{
				player.StopPlayer();
			}
			foreach (Process process in Process.GetProcesses())
			{
				if (process.ProcessName.Contains("csgo") || process.ProcessName.ToLower().Contains("steam") || process.ProcessName == "VertigoBoostClient" || process.ProcessName.ToLower() == "gameoverlayui")
				{
					try
					{
						Process.Start(new ProcessStartInfo
						{
							Verb = "runas",
							FileName = "cmd.exe",
							Arguments = string.Format("/c taskkill /f /pid \"{0}\"", process.Id),
							WindowStyle = ProcessWindowStyle.Hidden
						});
					}
					catch (Exception ex)
					{
						LogService.GetInstance.LogInformation(ex.ToString());
					}
				}
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000DC1C File Offset: 0x0000BE1C
		private void loginCopy_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrEmpty(this.textBox.Text) && !string.IsNullOrWhiteSpace(this.textBox.Text))
			{
				System.Windows.Clipboard.Clear();
				System.Windows.Clipboard.SetText(this.textBox.Text);
				CreateError.NewNotification("Login copied", false);
				return;
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000DC70 File Offset: 0x0000BE70
		private void passwordCopy_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrEmpty(this.textBox_Copy.Text) && !string.IsNullOrWhiteSpace(this.textBox_Copy.Text))
			{
				System.Windows.Clipboard.Clear();
				System.Windows.Clipboard.SetText(this.textBox_Copy.Text);
				CreateError.NewNotification("Password copied", false);
				return;
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000DCC4 File Offset: 0x0000BEC4
		public void SetAccountData(string login, string password)
		{
			this.textBox.Text = login;
			this.textBox_Copy.Text = password;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000DCEC File Offset: 0x0000BEEC
		private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
		{
			this.SetAccountData(string.Empty, string.Empty);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000DD0C File Offset: 0x0000BF0C
		private void exportButton_Click(object sender, RoutedEventArgs e)
		{
			string text = Directory.GetCurrentDirectory() + "\\accounts.txt";
			if (File.Exists(text))
			{
				File.Delete(text);
			}
			string[] array = StaticData.GetInstance.Players.Select((Player player) => player.Login + ":" + player.Password).ToArray<string>();
			File.WriteAllLines(text, array);
			new Process
			{
				StartInfo = 
				{
					FileName = "notepad.exe",
					Arguments = text
				}
			}.Start();
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000DDA0 File Offset: 0x0000BFA0
		private void add_accounts_Copy4_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
			foreach (object obj in ((IEnumerable)this.AccountsBox.Items))
			{
				AccountShow accountShow = (AccountShow)obj;
				if (accountShow.IsChecked)
				{
					accountShow.OpenSteamClient();
				}
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000DE10 File Offset: 0x0000C010
		private void mark_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (this.isInMarkChoice)
			{
				this.markChoiceMenu.Visibility = Visibility.Hidden;
				return;
			}
			this.markChoiceMenu.Visibility = Visibility.Visible;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000DE40 File Offset: 0x0000C040
		private void markChoice_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Image image = sender as Image;
			this.currentChoisedMark = int.Parse(image.Name.Split(new char[] { '_' })[1]);
			BitmapImage bitmapImage = new BitmapImage();
			bitmapImage.BeginInit();
			bitmapImage.UriSource = new Uri(string.Format("/Resources/marks/{0}.png", this.currentChoisedMark), UriKind.Relative);
			bitmapImage.EndInit();
			this.mark.Source = bitmapImage;
			this.markChoiceMenu.Visibility = Visibility.Hidden;
			this.CreatePages();
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000DED0 File Offset: 0x0000C0D0
		private void Loginbox_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			this.LoginSearchMask = this.loginbox.Text;
			if (this.loginbox.Text.Length <= 0)
			{
				this.loginToolTip.Visibility = Visibility.Visible;
				return;
			}
			this.loginToolTip.Visibility = Visibility.Hidden;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000DF1C File Offset: 0x0000C11C
		private void Label_Copy5_OnMouseDown(object sender, MouseButtonEventArgs e)
		{
			if (this._order_level == 0)
			{
				this._order_level = 1;
				this.label_Copy5.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF"));
			}
			else if (this._order_level == 1)
			{
				this._order_level = 2;
				this.label_Copy5.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF"));
			}
			else if (this._order_level == 2)
			{
				this._order_level = 0;
				this.label_Copy5.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF616161"));
			}
			this.CreatePages();
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000DFC0 File Offset: 0x0000C1C0
		private void Label_Copy7_OnMouseDown(object sender, MouseButtonEventArgs e)
		{
			if (this._order_rank5 == 0)
			{
				this._order_rank5 = 1;
				this.label_Copy7.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF"));
			}
			else if (this._order_rank5 == 1)
			{
				this._order_rank5 = 2;
				this.label_Copy7.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF"));
			}
			else if (this._order_rank5 == 2)
			{
				this._order_rank5 = 0;
				this.label_Copy7.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF616161"));
			}
			this.CreatePages();
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000E064 File Offset: 0x0000C264
		private void Label_Copy6_OnMouseDown(object sender, MouseButtonEventArgs e)
		{
			if (this._order_rank2 == 0)
			{
				this._order_rank2 = 1;
				this.label_Copy6.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF"));
			}
			else if (this._order_rank2 == 1)
			{
				this._order_rank2 = 2;
				this.label_Copy6.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF"));
			}
			else if (this._order_rank2 == 2)
			{
				this._order_rank2 = 0;
				this.label_Copy6.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF616161"));
			}
			this.CreatePages();
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000E108 File Offset: 0x0000C308
		private void Label_Copy3_OnMouseDown(object sender, MouseButtonEventArgs e)
		{
			if (this._order_wins5 != 0)
			{
				if (this._order_wins5 == 1)
				{
					this._order_wins5 = 2;
					this.label_Copy3.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF"));
				}
				else if (this._order_wins5 == 2)
				{
					this._order_wins5 = 0;
					this.label_Copy3.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF616161"));
				}
			}
			else
			{
				this._order_wins5 = 1;
				this.label_Copy3.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF"));
			}
			this.CreatePages();
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000E1AC File Offset: 0x0000C3AC
		private void Label_Copy2_OnMouseDown(object sender, MouseButtonEventArgs e)
		{
			if (this._order_wins2 != 0)
			{
				if (this._order_wins2 == 1)
				{
					this._order_wins2 = 2;
					this.label_Copy2.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF"));
				}
				else if (this._order_wins2 == 2)
				{
					this._order_wins2 = 0;
					this.label_Copy2.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF616161"));
				}
			}
			else
			{
				this._order_wins2 = 1;
				this.label_Copy2.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF"));
			}
			this.CreatePages();
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000E250 File Offset: 0x0000C450
		private void Label_Copy1_OnMouseDown(object sender, MouseButtonEventArgs e)
		{
			if (this._order_prime != 0)
			{
				if (this._order_prime == 1)
				{
					this._order_prime = 2;
					this.label_Copy1.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF"));
				}
				else if (this._order_prime == 2)
				{
					this._order_prime = 0;
					this.label_Copy1.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF616161"));
				}
			}
			else
			{
				this._order_prime = 1;
				this.label_Copy1.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF"));
			}
			this.CreatePages();
		}

		// Token: 0x04000169 RID: 361
		private string _loginSearchMask;

		// Token: 0x0400016A RID: 362
		private int _order_level;

		// Token: 0x0400016B RID: 363
		private int _order_rank5;

		// Token: 0x0400016C RID: 364
		private int _order_rank2;

		// Token: 0x0400016D RID: 365
		private int _order_wins5;

		// Token: 0x0400016E RID: 366
		private int _order_wins2;

		// Token: 0x0400016F RID: 367
		private int _order_prime;

		// Token: 0x04000170 RID: 368
		public List<AccountPage> pages = new List<AccountPage>();

		// Token: 0x04000171 RID: 369
		public int current_page;

		// Token: 0x04000172 RID: 370
		private bool isInMarkChoice;

		// Token: 0x04000173 RID: 371
		private int currentChoisedMark;
	}
}
