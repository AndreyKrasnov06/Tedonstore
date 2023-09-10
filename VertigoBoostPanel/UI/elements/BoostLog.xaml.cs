using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using VertigoBoostPanel.Model;

namespace VertigoBoostPanel.UI.elements
{
	// Token: 0x0200006D RID: 109
	public partial class BoostLog : UserControl
	{
		// Token: 0x060002E0 RID: 736 RVA: 0x0001520C File Offset: 0x0001340C
		public BoostLog(BoostLog boostLog)
		{
			base.DataContext = boostLog;
			this.InitializeComponent();
		}
	}
}
