using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using NetFwTypeLib;

namespace VertigoBoostPanel.UI.elements
{
	// Token: 0x02000071 RID: 113
	public partial class SteamRoute : UserControl, INotifyPropertyChanged
	{
		// Token: 0x060002EC RID: 748 RVA: 0x0001542C File Offset: 0x0001362C
		public SteamRoute(string name, string ip, string portRange)
		{
			base.DataContext = this;
			this.InitializeComponent();
			this.ServerName = name;
			this._serverIP = ip;
			this._serverPortRange = portRange;
			if (this.checkIfServerBlocked())
			{
				this.blockedBox.IsChecked = new bool?(true);
			}
			Task.Run(delegate
			{
				using (Ping ping = new Ping())
				{
					PingReply pingReply = ping.Send(this._serverIP);
					if (pingReply != null && pingReply.Status == IPStatus.Success)
					{
						this.Ping = pingReply.RoundtripTime.ToString();
					}
				}
			});
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002ED RID: 749 RVA: 0x000036CB File Offset: 0x000018CB
		public string AccountMark
		{
			get
			{
				return "/Resources/img/account_mark.png";
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060002EE RID: 750 RVA: 0x0001548C File Offset: 0x0001368C
		// (remove) Token: 0x060002EF RID: 751 RVA: 0x000154CC File Offset: 0x000136CC
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0001550C File Offset: 0x0001370C
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x00015598 File Offset: 0x00013798
		public string ServerName
		{
			get
			{
				return this._serverName.Replace("(North)", "(N)").Replace("(South)", "(S)").Replace("(Unicom)", "(U)")
					.Replace("(Telecom)", "(T)")
					.Replace("(Mobile)", "(M)")
					.Replace("(Bromma)", "(B)")
					.Replace("(Kista)", "(K)")
					.Replace(" (sha-4) Backbone", "");
			}
			set
			{
				this._serverName = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("ServerName"));
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x000038FC File Offset: 0x00001AFC
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x000155C8 File Offset: 0x000137C8
		public string Ping
		{
			get
			{
				return this._ping;
			}
			set
			{
				this._ping = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged != null)
				{
					propertyChanged(this, new PropertyChangedEventArgs("Ping"));
					return;
				}
			}
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x000155F8 File Offset: 0x000137F8
		private bool checkIfServerBlocked()
		{
			using (IEnumerator enumerator = ((INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"))).Rules.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((INetFwRule)enumerator.Current).Name == "TDN-block-" + this.ServerName)
					{
						return true;
					}
				}
				return false;
			}
			bool flag;
			return flag;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0001568C File Offset: 0x0001388C
		public void blockServer()
		{
			this.blockedBox.IsChecked = new bool?(true);
			if (this.checkIfServerBlocked())
			{
				return;
			}
			INetFwRule netFwRule = (INetFwRule2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
			netFwRule.Enabled = true;
			netFwRule.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT;
			netFwRule.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;
			netFwRule.RemoteAddresses = this._serverIP;
			netFwRule.Protocol = 17;
			netFwRule.RemotePorts = this._serverPortRange;
			netFwRule.Name = "TDN-block-" + this.ServerName;
			((INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"))).Rules.Add(netFwRule);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0001573C File Offset: 0x0001393C
		public void UnblockServer()
		{
			this.blockedBox.IsChecked = new bool?(false);
			((INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"))).Rules.Remove("TDN-block-" + this.ServerName);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00003904 File Offset: 0x00001B04
		private void blockedBox_Checked(object sender, RoutedEventArgs e)
		{
			this.blockServer();
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000390C File Offset: 0x00001B0C
		private void blockedBox_Unchecked(object sender, RoutedEventArgs e)
		{
			this.UnblockServer();
		}

		// Token: 0x040002AB RID: 683
		private string _serverName;

		// Token: 0x040002AC RID: 684
		private string _ping;

		// Token: 0x040002AD RID: 685
		private string _serverIP;

		// Token: 0x040002AE RID: 686
		private string _serverPortRange;
	}
}
