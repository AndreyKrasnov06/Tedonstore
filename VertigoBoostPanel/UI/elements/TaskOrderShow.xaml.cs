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
using VertigoBoostPanel.Static;
using VertigoBoostPanel.UI.pages;

namespace VertigoBoostPanel.UI.elements
{
	// Token: 0x02000073 RID: 115
	public partial class TaskOrderShow : UserControl
	{
		// Token: 0x060002FF RID: 767 RVA: 0x0001593C File Offset: 0x00013B3C
		public TaskOrderShow(BoostTask boostTask)
		{
			base.DataContext = boostTask;
			this.InitializeComponent();
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000D244 File Offset: 0x0000B444
		private void MenuButton_MouseEnter(object sender, MouseEventArgs e)
		{
			Button button = (Button)sender;
			if (button.Opacity == 0.5)
			{
				button.Opacity = 1.0;
			}
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000D27C File Offset: 0x0000B47C
		private void MenuButton_MouseLeave(object sender, MouseEventArgs e)
		{
			Button button = (Button)sender;
			if (button.Opacity == 1.0)
			{
				button.Opacity = 0.5;
			}
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0001595C File Offset: 0x00013B5C
		private void Grid_MouseEnter(object sender, MouseEventArgs e)
		{
			DoubleAnimation doubleAnimation = new DoubleAnimation();
			doubleAnimation.From = new double?(0.0);
			doubleAnimation.To = new double?(0.9);
			doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.1));
			doubleAnimation.AutoReverse = false;
			this.OpacityEffect.BeginAnimation(UIElement.OpacityProperty, doubleAnimation);
			if (!(((BoostTask)base.DataContext).Status != "Working"))
			{
				this.defaultPanelFunctions.Visibility = Visibility.Visible;
				return;
			}
			if (((BoostTask)base.DataContext).SessionType.ToLower() == "transfer")
			{
				this.defaultPanelFunctions.Visibility = Visibility.Visible;
				this.SettingsButton.Visibility = Visibility.Visible;
				this.stopTask.Visibility = Visibility.Hidden;
				this.deleteTask.Visibility = Visibility.Visible;
				return;
			}
			this.CancelButton.Visibility = Visibility.Visible;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00015A58 File Offset: 0x00013C58
		private void Grid_MouseLeave(object sender, MouseEventArgs e)
		{
			DoubleAnimation doubleAnimation = new DoubleAnimation();
			doubleAnimation.From = new double?(0.9);
			doubleAnimation.To = new double?(0.0);
			doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
			doubleAnimation.AutoReverse = false;
			this.OpacityEffect.BeginAnimation(UIElement.OpacityProperty, doubleAnimation);
			this.CancelButton.Visibility = Visibility.Hidden;
			this.SettingsButton.Visibility = Visibility.Hidden;
			this.defaultPanelFunctions.Visibility = Visibility.Hidden;
			this.deleteTask.Visibility = Visibility.Hidden;
			this.stopTask.Visibility = Visibility.Visible;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00015B08 File Offset: 0x00013D08
		private void CancelTask_Click(object sender, RoutedEventArgs e)
		{
			if (((BoostTask)base.DataContext).SessionType.ToLower() == "transfer")
			{
				Program.GetInstance.DeleteTask((BoostTask)base.DataContext);
				return;
			}
			StaticData.GetInstance.taskNamesInQueue.Remove(((BoostTask)base.DataContext).Name);
			BoostTaskQueue.RemoveFromQueue((BoostTask)base.DataContext);
			TasksPage taskPage = Program.GetInstance.TaskPage;
			if (taskPage == null)
			{
				return;
			}
			taskPage.ReadyToSwap();
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00015B94 File Offset: 0x00013D94
		private void startTask_Click(object sender, RoutedEventArgs e)
		{
			((BoostTask)base.DataContext).StartSession();
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00015BB4 File Offset: 0x00013DB4
		private void stopTask_Click(object sender, RoutedEventArgs e)
		{
			((BoostTask)base.DataContext).StopSession();
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00015BD4 File Offset: 0x00013DD4
		private void SettingsButton_OnClick(object sender, RoutedEventArgs e)
		{
			if (((BoostTask)base.DataContext).SessionType.ToLower() == "transfer")
			{
				((BoostTask)base.DataContext).OpenEditTask("edit");
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00015C18 File Offset: 0x00013E18
		private void DeleteTask_OnClick(object sender, RoutedEventArgs e)
		{
			Program.GetInstance.DeleteTask((BoostTask)base.DataContext);
		}
	}
}
