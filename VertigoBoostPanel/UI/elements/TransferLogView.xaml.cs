using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace VertigoBoostPanel.UI.elements
{
	// Token: 0x02000076 RID: 118
	public partial class TransferLogView : UserControl
	{
		// Token: 0x0600032B RID: 811 RVA: 0x00016CEC File Offset: 0x00014EEC
		public TransferLogView(string logContent, string logGroup)
		{
			this.InitializeComponent();
			this.logContent.Text = logContent;
			this.logGroup.Text = logGroup.ToUpper();
		}
	}
}
