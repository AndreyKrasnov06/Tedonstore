using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace VertigoBoostPanel.UI.elements
{
	// Token: 0x0200006F RID: 111
	public partial class Notification : UserControl
	{
		// Token: 0x060002E6 RID: 742 RVA: 0x00015344 File Offset: 0x00013544
		public Notification(string text)
		{
			this.InitializeComponent();
			this.NotificationText.Content = text;
		}
	}
}
