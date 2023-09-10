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
using VertigoBoostPanel.Core.Player;

namespace VertigoBoostPanel.UI.elements
{
	// Token: 0x0200006C RID: 108
	public partial class AccountShowInTask : UserControl
	{
		// Token: 0x060002D7 RID: 727 RVA: 0x00014E78 File Offset: 0x00013078
		public AccountShowInTask(Player player)
		{
			base.DataContext = player;
			this.InitializeComponent();
			this._currentPlayer = player;
			this.login = this._currentPlayer.Login;
			this.loginBox.Content = this._currentPlayer.Login.Replace("_", "__");
			BitmapImage bitmapImage = new BitmapImage();
			bitmapImage.BeginInit();
			bitmapImage.UriSource = new Uri(string.Format("/Resources/img/ranks/{0}.png", this._currentPlayer.Rank5x5), UriKind.Relative);
			bitmapImage.EndInit();
			this.rankImg.Stretch = Stretch.Fill;
			this.rankImg.Source = bitmapImage;
			this.rankImg.Width = 40.0;
			this.rankImg.Height = 16.0;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00014F54 File Offset: 0x00013154
		public void UpdateStat()
		{
			base.Dispatcher.Invoke(delegate
			{
				BitmapImage bitmapImage = new BitmapImage();
				bitmapImage.BeginInit();
				bitmapImage.UriSource = new Uri(string.Format("/Resources/img/ranks/{0}.png", this._currentPlayer.Rank5x5), UriKind.Relative);
				bitmapImage.EndInit();
				this.rankImg.Stretch = Stretch.Fill;
				this.rankImg.Source = bitmapImage;
				this.rankImg.Width = 40.0;
				this.rankImg.Height = 16.0;
			});
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00014F78 File Offset: 0x00013178
		private void Grid_MouseLeave(object sender, MouseEventArgs e)
		{
			DoubleAnimation doubleAnimation = new DoubleAnimation();
			doubleAnimation.From = new double?(0.05);
			doubleAnimation.To = new double?(0.0);
			doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
			doubleAnimation.AutoReverse = false;
			this.naVodka.BeginAnimation(UIElement.OpacityProperty, doubleAnimation);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00014FEC File Offset: 0x000131EC
		private void Grid_MouseEnter(object sender, MouseEventArgs e)
		{
			DoubleAnimation doubleAnimation = new DoubleAnimation();
			doubleAnimation.From = new double?(0.0);
			doubleAnimation.To = new double?(0.05);
			doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.1));
			doubleAnimation.AutoReverse = false;
			this.naVodka.BeginAnimation(UIElement.OpacityProperty, doubleAnimation);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00015060 File Offset: 0x00013260
		private void Checked(object sender, RoutedEventArgs e)
		{
			this.isChecked = true;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00015074 File Offset: 0x00013274
		private void Unchecked(object sender, RoutedEventArgs e)
		{
			this.isChecked = false;
		}

		// Token: 0x04000298 RID: 664
		public string login;

		// Token: 0x04000299 RID: 665
		public bool isChecked;

		// Token: 0x0400029A RID: 666
		private Player _currentPlayer;
	}
}
