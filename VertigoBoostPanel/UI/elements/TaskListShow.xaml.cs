using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using VertigoBoostPanel.Core.TaskList;

namespace VertigoBoostPanel.UI.elements
{
	// Token: 0x02000072 RID: 114
	public partial class TaskListShow : UserControl
	{
		// Token: 0x060002FC RID: 764 RVA: 0x00015874 File Offset: 0x00013A74
		public TaskListShow(BoostTask boostTask)
		{
			base.DataContext = boostTask;
			this.InitializeComponent();
		}
	}
}
