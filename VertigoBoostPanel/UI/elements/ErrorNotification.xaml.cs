using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace VertigoBoostPanel.UI.elements
{
	// Token: 0x0200006E RID: 110
	public partial class ErrorNotification : UserControl
	{
		// Token: 0x060002E3 RID: 739 RVA: 0x000152C0 File Offset: 0x000134C0
		public ErrorNotification(string text)
		{
			this.InitializeComponent();
			this.NotificationText.Content = text;
		}
	}
}
