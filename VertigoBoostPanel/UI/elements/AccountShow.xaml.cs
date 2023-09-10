using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VertigoBoostPanel.Core;
using VertigoBoostPanel.Core.Player;

namespace VertigoBoostPanel.UI.elements
{
	// Token: 0x0200006B RID: 107
	public partial class AccountShow : UserControl
	{
		// Token: 0x060002C2 RID: 706 RVA: 0x000144E8 File Offset: 0x000126E8
		public AccountShow(Player player)
		{
			base.DataContext = player;
			this.InitializeComponent();
			this._currentPlayer = player;
			this.UpdateStat();
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x000038F4 File Offset: 0x00001AF4
		public bool IsChecked
		{
			get
			{
				return this._checked;
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00014518 File Offset: 0x00012718
		public void UpdateStat()
		{
			base.Dispatcher.Invoke(delegate
			{
				this.label_Copy4.Content = this._currentPlayer.Login.Replace("_", "__");
				this.label_Copy3.Content = this._currentPlayer.Lvl.ToString();
				this.label_Copy2.Content = this._currentPlayer.Wins5x5.ToString();
				this.label_Copy1.Content = this._currentPlayer.Wins2x2.ToString();
				if (this._currentPlayer.Prime == 1)
				{
					this.primeLabel.Content = "Yes";
					this.primeLabel.Foreground = Brushes.Green;
				}
				else
				{
					this.primeLabel.Content = "No";
					this.primeLabel.Foreground = Brushes.Red;
				}
				if (this._currentPlayer.Started && this._currentPlayer.WindowLoaded)
				{
					this.StatusOvalchik.Fill = Brushes.Green;
				}
				if (this._currentPlayer.Started && !this._currentPlayer.WindowLoaded)
				{
					this.StatusOvalchik.Fill = Brushes.Yellow;
				}
				if (!this._currentPlayer.Started && !this._currentPlayer.WindowLoaded)
				{
					this.StatusOvalchik.Fill = Brushes.Red;
				}
				BitmapImage bitmapImage = new BitmapImage();
				bitmapImage.BeginInit();
				bitmapImage.UriSource = new Uri(string.Format("/Resources/img/ranks/{0}.png", this._currentPlayer.Rank5x5), UriKind.Relative);
				bitmapImage.EndInit();
				this.rank5x5.Stretch = Stretch.Fill;
				this.rank5x5.Source = bitmapImage;
				this.rank5x5.Width = 40.0;
				this.rank5x5.Height = 16.0;
				BitmapImage bitmapImage2 = new BitmapImage();
				bitmapImage2.BeginInit();
				bitmapImage2.UriSource = new Uri(string.Format("/Resources/img/ranks/{0}.png", this._currentPlayer.Rank2x2), UriKind.Relative);
				bitmapImage2.EndInit();
				this.rank2x2.Stretch = Stretch.Fill;
				this.rank2x2.Source = bitmapImage2;
				this.rank2x2.Width = 40.0;
				this.rank2x2.Height = 16.0;
				BitmapImage bitmapImage3 = new BitmapImage();
				bitmapImage3.BeginInit();
				bitmapImage3.UriSource = new Uri(string.Format("/Resources/marks/{0}.png", this._currentPlayer.Mark), UriKind.Relative);
				bitmapImage3.EndInit();
				this.mark_0.Source = bitmapImage3;
			});
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0001453C File Offset: 0x0001273C
		public void DeletePlayer()
		{
			this._currentPlayer.DeletePlayer();
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00014554 File Offset: 0x00012754
		public void StartPlayer()
		{
			this._currentPlayer.StartPlayer(false);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00014570 File Offset: 0x00012770
		public void StopPlayer()
		{
			this._currentPlayer.StopPlayer();
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00014588 File Offset: 0x00012788
		public void OpenSteam()
		{
			this._currentPlayer.OpenSteam();
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x000145A0 File Offset: 0x000127A0
		public void OpenSteamClient()
		{
			this._currentPlayer.OpenSteamClient();
		}

		// Token: 0x060002CA RID: 714 RVA: 0x000145B8 File Offset: 0x000127B8
		public void CheckEnterToSteam()
		{
			this._currentPlayer.CheckEnterToSteam();
		}

		// Token: 0x060002CB RID: 715 RVA: 0x000145D0 File Offset: 0x000127D0
		private void Grid_MouseLeave(object sender, MouseEventArgs e)
		{
			DoubleAnimation doubleAnimation = new DoubleAnimation();
			doubleAnimation.From = new double?(0.05);
			doubleAnimation.To = new double?(0.0);
			doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
			doubleAnimation.AutoReverse = false;
			this.naVodka.BeginAnimation(UIElement.OpacityProperty, doubleAnimation);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00014644 File Offset: 0x00012844
		private void Grid_MouseEnter(object sender, MouseEventArgs e)
		{
			DoubleAnimation doubleAnimation = new DoubleAnimation();
			doubleAnimation.From = new double?(0.0);
			doubleAnimation.To = new double?(0.05);
			doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.1));
			doubleAnimation.AutoReverse = false;
			this.naVodka.BeginAnimation(UIElement.OpacityProperty, doubleAnimation);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x000146B8 File Offset: 0x000128B8
		private void Checked(object sender, RoutedEventArgs e)
		{
			e.Handled = true;
			this._checked = true;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x000146D4 File Offset: 0x000128D4
		private void Unchecked(object sender, RoutedEventArgs e)
		{
			e.Handled = true;
			this._checked = false;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x000146F0 File Offset: 0x000128F0
		public void EditCheck(bool isChecked)
		{
			this.checkBox_0.IsChecked = new bool?(isChecked);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00014710 File Offset: 0x00012910
		private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (Program.GetInstance.accPageActive)
			{
				e.Handled = true;
			}
			if (Program.GetInstance.accPageActive)
			{
				Program.GetInstance.AccountsPage.SetAccountData(this._currentPlayer.Login, this._currentPlayer.Password);
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00014764 File Offset: 0x00012964
		private void mark_MouseDown(object sender, MouseButtonEventArgs e)
		{
			e.Handled = true;
			Image image = sender as Image;
			if (!this.IsInMarkChoice && image.Name == "mark_0")
			{
				BitmapImage bitmapImage = new BitmapImage();
				bitmapImage.BeginInit();
				bitmapImage.UriSource = new Uri(string.Format("/Resources/marks/{0}.png", 0), UriKind.Relative);
				bitmapImage.EndInit();
				this.mark_0.Source = bitmapImage;
				this.IsInMarkChoice = true;
				this.label_Copy4.Visibility = Visibility.Hidden;
				this.mark_1.Visibility = Visibility.Visible;
				this.mark_2.Visibility = Visibility.Visible;
				this.mark_3.Visibility = Visibility.Visible;
				this.mark_4.Visibility = Visibility.Visible;
				this.mark_5.Visibility = Visibility.Visible;
				return;
			}
			int num = int.Parse(image.Name.Split(new char[] { '_' })[1]);
			BitmapImage bitmapImage2 = new BitmapImage();
			bitmapImage2.BeginInit();
			bitmapImage2.UriSource = new Uri(string.Format("/Resources/marks/{0}.png", num), UriKind.Relative);
			bitmapImage2.EndInit();
			this.mark_0.Source = bitmapImage2;
			this._currentPlayer.Mark = num;
			this._currentPlayer.SaveInfo();
			this.IsInMarkChoice = false;
			this.label_Copy4.Visibility = Visibility.Visible;
			this.mark_1.Visibility = Visibility.Hidden;
			this.mark_2.Visibility = Visibility.Hidden;
			this.mark_3.Visibility = Visibility.Hidden;
			this.mark_4.Visibility = Visibility.Hidden;
			this.mark_5.Visibility = Visibility.Hidden;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x000148F8 File Offset: 0x00012AF8
		private void hideMarkChoice()
		{
			this.IsInMarkChoice = false;
			this.label_Copy4.Visibility = Visibility.Visible;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00014918 File Offset: 0x00012B18
		private void checkBox_0_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			bool? isChecked = checkBox.IsChecked;
			checkBox.IsChecked = ((isChecked == null) ? null : new bool?(!isChecked.GetValueOrDefault()));
		}

		// Token: 0x04000284 RID: 644
		private bool _checked;

		// Token: 0x04000285 RID: 645
		public Player _currentPlayer;

		// Token: 0x04000286 RID: 646
		private bool IsInMarkChoice;
	}
}
