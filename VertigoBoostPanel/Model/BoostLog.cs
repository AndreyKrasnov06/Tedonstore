using System;
using System.ComponentModel;
using System.Windows.Media;
using VertigoBoostPanel.Core;
using VertigoBoostPanel.Services.Api;
using VertigoBoostPanel.UI.elements;

namespace VertigoBoostPanel.Model
{
	// Token: 0x0200001C RID: 28
	public class BoostLog : INotifyPropertyChanged
	{
		// Token: 0x060000CB RID: 203 RVA: 0x00008718 File Offset: 0x00006918
		public BoostLog(string name, string type)
		{
			this._name = name;
			this._type = type;
			this._creationDate = DateTime.Now.ToLongTimeString();
			Program.GetInstance.MainWindow.Dispatcher.Invoke(delegate
			{
				this._view = new BoostLog(this);
			});
			this._sendMessageToUser();
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060000CC RID: 204 RVA: 0x00008774 File Offset: 0x00006974
		// (remove) Token: 0x060000CD RID: 205 RVA: 0x000087B4 File Offset: 0x000069B4
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000CE RID: 206 RVA: 0x000033D5 File Offset: 0x000015D5
		public BoostLog View
		{
			get
			{
				return this._view;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000CF RID: 207 RVA: 0x000033DD File Offset: 0x000015DD
		public string Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x000033E5 File Offset: 0x000015E5
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x000033ED File Offset: 0x000015ED
		public string Date
		{
			get
			{
				return this._creationDate;
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000087F4 File Offset: 0x000069F4
		public void SetLastLogLine(bool flag)
		{
			Program.GetInstance.MainWindow.Dispatcher.Invoke(delegate
			{
				if (flag)
				{
					this._view.okontovka.BorderBrush = Brushes.Green;
					return;
				}
				this._view.okontovka.BorderBrush = Brushes.Gray;
			});
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000883C File Offset: 0x00006A3C
		private void _sendMessageToUser()
		{
			SocketBackend.GetInstance.SendNotificationMessage("[" + this._name + "] " + this._type);
		}

		// Token: 0x04000091 RID: 145
		private BoostLog _view;

		// Token: 0x04000092 RID: 146
		private string _creationDate;

		// Token: 0x04000093 RID: 147
		private string _name;

		// Token: 0x04000094 RID: 148
		private string _type;
	}
}
