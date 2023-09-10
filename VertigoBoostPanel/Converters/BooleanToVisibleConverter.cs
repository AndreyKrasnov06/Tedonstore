using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace VertigoBoostPanel.Converters
{
	// Token: 0x02000029 RID: 41
	public class BooleanToVisibleConverter : IValueConverter
	{
		// Token: 0x0600010D RID: 269 RVA: 0x00008C00 File Offset: 0x00006E00
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			object obj;
			try
			{
				obj = ((!(bool)value) ? Visibility.Hidden : Visibility.Visible);
			}
			catch
			{
				obj = Binding.DoNothing;
			}
			return obj;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000034A8 File Offset: 0x000016A8
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Binding.DoNothing;
		}
	}
}
