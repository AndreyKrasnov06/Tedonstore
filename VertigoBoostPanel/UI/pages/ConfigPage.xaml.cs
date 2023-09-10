using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using VertigoBoostPanel.UI.ViewModels.Pages;

namespace VertigoBoostPanel.UI.pages
{
	// Token: 0x02000050 RID: 80
	public partial class ConfigPage : Page
	{
		// Token: 0x060001FD RID: 509 RVA: 0x0000EA50 File Offset: 0x0000CC50
		public ConfigPage()
		{
			base.DataContext = new ConfigPageViewModel(this);
		}
	}
}
