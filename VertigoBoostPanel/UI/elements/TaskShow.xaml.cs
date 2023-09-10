using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using VertigoBoostPanel.Core;
using VertigoBoostPanel.Core.TaskList;

namespace VertigoBoostPanel.UI.elements
{
	// Token: 0x02000075 RID: 117
	public partial class TaskShow : UserControl
	{
		// Token: 0x06000320 RID: 800 RVA: 0x000168F4 File Offset: 0x00014AF4
		public TaskShow(BoostTask boostTask)
		{
			base.DataContext = boostTask;
			this.InitializeComponent();
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00016914 File Offset: 0x00014B14
		private void MenuButton_MouseEnter(object sender, MouseEventArgs e)
		{
			((Button)sender).Opacity = 1.0;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00016938 File Offset: 0x00014B38
		private void MenuButton_MouseLeave(object sender, MouseEventArgs e)
		{
			((Button)sender).Opacity = 0.5;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0001695C File Offset: 0x00014B5C
		private void Grid_MouseEnter(object sender, MouseEventArgs e)
		{
			DoubleAnimation doubleAnimation = new DoubleAnimation();
			doubleAnimation.From = new double?(0.0);
			doubleAnimation.To = new double?(0.9);
			doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.1));
			doubleAnimation.AutoReverse = false;
			this.OpacityEffect.BeginAnimation(UIElement.OpacityProperty, doubleAnimation);
			this.AddButton.Visibility = Visibility.Visible;
			this.SettingsButton.Visibility = Visibility.Visible;
			this.DeleteButton.Visibility = Visibility.Visible;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x000169F4 File Offset: 0x00014BF4
		private void Grid_MouseLeave(object sender, MouseEventArgs e)
		{
			DoubleAnimation doubleAnimation = new DoubleAnimation();
			doubleAnimation.From = new double?(0.9);
			doubleAnimation.To = new double?(0.0);
			doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.1));
			doubleAnimation.AutoReverse = false;
			this.OpacityEffect.BeginAnimation(UIElement.OpacityProperty, doubleAnimation);
			this.AddButton.Visibility = Visibility.Hidden;
			this.SettingsButton.Visibility = Visibility.Hidden;
			this.DeleteButton.Visibility = Visibility.Hidden;
			this.CancelButton.Visibility = Visibility.Hidden;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00003148 File Offset: 0x00001348
		private void CancelTask_Click(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00016A98 File Offset: 0x00014C98
		private void AddTask_Click(object sender, RoutedEventArgs e)
		{
			((BoostTask)base.DataContext).AddTaskToQueue();
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00015C18 File Offset: 0x00013E18
		private void DeleteTask_Click(object sender, RoutedEventArgs e)
		{
			Program.GetInstance.DeleteTask((BoostTask)base.DataContext);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00016AB8 File Offset: 0x00014CB8
		private void SetupTask_Click(object sender, RoutedEventArgs e)
		{
			((BoostTask)base.DataContext).OpenEditTask("edit");
		}
	}
}
