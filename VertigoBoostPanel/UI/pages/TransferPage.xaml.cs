using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using VertigoBoostPanel.UI.ViewModels.Pages;

namespace VertigoBoostPanel.UI.pages
{
	// Token: 0x0200006A RID: 106
	public partial class TransferPage : Page
	{
		// Token: 0x060002BF RID: 703 RVA: 0x00014450 File Offset: 0x00012650
		public TransferPage()
		{
			TransferPage.Instance = this;
			base.DataContext = new TransferPageViewModel(this);
			this.InitializeComponent();
		}

		// Token: 0x04000280 RID: 640
		public static TransferPage Instance;
	}
}
