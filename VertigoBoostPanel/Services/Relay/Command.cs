using System;
using System.Windows.Input;

namespace VertigoBoostPanel.Services.Relay
{
	// Token: 0x02000083 RID: 131
	public class Command : ICommand
	{
		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000372 RID: 882 RVA: 0x00017A80 File Offset: 0x00015C80
		// (remove) Token: 0x06000373 RID: 883 RVA: 0x00017A94 File Offset: 0x00015C94
		public event EventHandler CanExecuteChanged
		{
			add
			{
				CommandManager.RequerySuggested += value;
			}
			remove
			{
				CommandManager.RequerySuggested -= value;
			}
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00003A71 File Offset: 0x00001C71
		public Command(Action<object> execute, Func<object, bool> canExecute = null)
		{
			this.execute = execute;
			this.canExecute = canExecute;
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00017AA8 File Offset: 0x00015CA8
		public bool CanExecute(object parameter)
		{
			return this.canExecute == null || this.canExecute(parameter);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00017ACC File Offset: 0x00015CCC
		public void Execute(object parameter)
		{
			this.execute(parameter);
		}

		// Token: 0x040002F8 RID: 760
		private Action<object> execute;

		// Token: 0x040002F9 RID: 761
		private Func<object, bool> canExecute;
	}
}
