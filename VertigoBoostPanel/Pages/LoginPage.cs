using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.CSharp.RuntimeBinder;
using VertigoBoostPanel.Core;
using VertigoBoostPanel.Services.Crypto;
using VertigoBoostPanel.Services.Files;
using VertigoBoostPanel.Services.RegistryInteraction;
using VertigoBoostPanel.Static;
using VertigoBoostPanel.UI.elements;

namespace VertigoBoostPanel.Pages
{
	// Token: 0x0200000A RID: 10
	public class LoginPage : Window, IComponentConnector
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00005AF4 File Offset: 0x00003CF4
		public LoginPage()
		{
			if (File.Exists("updater.exe"))
			{
				try
				{
					File.Delete("updater.exe");
				}
				catch (Exception ex)
				{
					LogService.GetInstance.LogInformation(ex.ToString());
				}
			}
			base.DataContext = this;
			Program.GetInstance.LoginWindow = this;
			this.InitializeComponent();
			try
			{
				if (string.IsNullOrEmpty(Settings.GetInstance.Version))
				{
					Settings.GetInstance.Version = Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
					RegistryService.Login = "";
					RegistryService.Password = "";
				}
				else
				{
					this.loginbox.Text = RegistryService.Login;
					this.passwordbox.Password = RegistryService.Password;
				}
			}
			catch (Exception ex2)
			{
				LogService.GetInstance.LogInformation(ex2.ToString());
			}
			if (!this.IsInternetAvailable)
			{
				this.ShowError("Unable to connect to server", "red");
				this.signInButton.IsEnabled = false;
			}
			if (!new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
			{
				this.ShowError("Run panel as administrator", "yellow");
				this.signInButton.IsEnabled = false;
			}
			if (Hardware.HWID == "error" || string.IsNullOrEmpty(Hardware.HWID) || string.IsNullOrWhiteSpace(Hardware.HWID))
			{
				this.ShowError("Run panel without sandbox", "yellow");
				this.signInButton.IsEnabled = false;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000031F9 File Offset: 0x000013F9
		public string LoginButtonImage
		{
			get
			{
				return "/Resources/img/loginbutton.png";
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00005C9C File Offset: 0x00003E9C
		public void ShowError(string message, string color = "yellow")
		{
			if (color == "red")
			{
				this.errorLabel.Foreground = Brushes.Red;
			}
			else
			{
				this.errorLabel.Foreground = Brushes.Yellow;
			}
			this.errorLabel.Content = message;
			this.errorLabel.Visibility = Visibility.Visible;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00005CF0 File Offset: 0x00003EF0
		private bool IsInternetAvailable
		{
			get
			{
				bool flag;
				try
				{
					Dns.GetHostEntry("panel.tedonstore.com");
					flag = true;
				}
				catch
				{
					flag = false;
				}
				return flag;
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00005D28 File Offset: 0x00003F28
		private void Drag_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
			{
				base.DragMove();
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00005D44 File Offset: 0x00003F44
		private void ButtonTopPanel_MouseLeave(object sender, MouseEventArgs e)
		{
			Shape shape = (Rectangle)((Button)sender).Template.FindName("Line", (Button)sender);
			Rectangle rectangle = (Rectangle)((Button)sender).Template.FindName("SecondLine", (Button)sender);
			BrushConverter brushConverter = new BrushConverter();
			shape.Fill = (Brush)brushConverter.ConvertFromString("#FF99999B");
			if (rectangle != null)
			{
				rectangle.Fill = (Brush)brushConverter.ConvertFromString("#FF99999B");
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00005DCC File Offset: 0x00003FCC
		private void ButtonTopPanel_MouseEnter(object sender, MouseEventArgs e)
		{
			Shape shape = (Rectangle)((Button)sender).Template.FindName("Line", (Button)sender);
			Rectangle rectangle = (Rectangle)((Button)sender).Template.FindName("SecondLine", (Button)sender);
			shape.Fill = Brushes.White;
			if (rectangle != null)
			{
				rectangle.Fill = Brushes.White;
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003148 File Offset: 0x00001348
		private void Window_MouseEnter(object sender, MouseEventArgs e)
		{
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00005E38 File Offset: 0x00004038
		private void Minimize_Button_Click(object sender, RoutedEventArgs e)
		{
			base.WindowState = WindowState.Minimized;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00005E4C File Offset: 0x0000404C
		private void passwordbox_PasswordChanged(object sender, RoutedEventArgs e)
		{
			if (this.passwordbox.Password.Length <= 0)
			{
				this.passwordToolTip.Visibility = Visibility.Visible;
			}
			else
			{
				this.passwordToolTip.Visibility = Visibility.Hidden;
			}
			RegistryService.Password = this.passwordbox.Password;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00005E98 File Offset: 0x00004098
		private void loginbox_TextChanged(object sender, TextChangedEventArgs e)
		{
			RegistryService.Login = this.loginbox.Text;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00005EB8 File Offset: 0x000040B8
		private void Window_ContentRendered(object sender, EventArgs e)
		{
			string languageName = Settings.GetInstance.Language;
			if (!string.IsNullOrEmpty(languageName))
			{
				if ((from obj in App.Languages
					where obj.Name == languageName
					select (obj)).FirstOrDefault<CultureInfo>() != null)
				{
					App.Language = (from obj in App.Languages
						where obj.Name == languageName
						select (obj)).FirstOrDefault<CultureInfo>();
				}
				else
				{
					App.Language = (from obj in App.Languages
						where obj.Name == "en-US"
						select (obj)).FirstOrDefault<CultureInfo>();
					Settings.GetInstance.Language = "en-US";
				}
			}
			else
			{
				App.Language = (from obj in App.Languages
					where obj.Name == "en-US"
					select (obj)).FirstOrDefault<CultureInfo>();
				Settings.GetInstance.Language = "en-US";
			}
			object obj6 = 1;
			obj6 = Process.GetProcesses();
			if (LoginPage.<>o__13.<>p__0 == null)
			{
				LoginPage.<>o__13.<>p__0 = CallSite<Func<CallSite, object, Process[]>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(Process[]), typeof(LoginPage)));
			}
			object obj2 = LoginPage.<>o__13.<>p__0.Target(LoginPage.<>o__13.<>p__0, obj6).FirstOrDefault((Process x) => x.ProcessName.ToLower() == "dnspy");
			if (LoginPage.<>o__13.<>p__1 == null)
			{
				LoginPage.<>o__13.<>p__1 = CallSite<Func<CallSite, object, Process[]>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(Process[]), typeof(LoginPage)));
			}
			object obj3 = LoginPage.<>o__13.<>p__1.Target(LoginPage.<>o__13.<>p__1, obj6).FirstOrDefault((Process x) => x.ProcessName.ToLower() == "netunpack");
			if (LoginPage.<>o__13.<>p__2 == null)
			{
				LoginPage.<>o__13.<>p__2 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof(LoginPage), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			object obj4 = LoginPage.<>o__13.<>p__2.Target(LoginPage.<>o__13.<>p__2, obj2, null);
			if (LoginPage.<>o__13.<>p__6 == null)
			{
				LoginPage.<>o__13.<>p__6 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(LoginPage), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
			}
			if (!LoginPage.<>o__13.<>p__6.Target(LoginPage.<>o__13.<>p__6, obj4))
			{
				if (LoginPage.<>o__13.<>p__5 == null)
				{
					LoginPage.<>o__13.<>p__5 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(LoginPage), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
				}
				Func<CallSite, object, bool> target = LoginPage.<>o__13.<>p__5.Target;
				CallSite <>p__ = LoginPage.<>o__13.<>p__5;
				if (LoginPage.<>o__13.<>p__4 == null)
				{
					LoginPage.<>o__13.<>p__4 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.Or, typeof(LoginPage), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Func<CallSite, object, object, object> target2 = LoginPage.<>o__13.<>p__4.Target;
				CallSite <>p__2 = LoginPage.<>o__13.<>p__4;
				object obj5 = obj4;
				if (LoginPage.<>o__13.<>p__3 == null)
				{
					LoginPage.<>o__13.<>p__3 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof(LoginPage), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				if (!target(<>p__, target2(<>p__2, obj5, LoginPage.<>o__13.<>p__3.Target(LoginPage.<>o__13.<>p__3, obj3, null))))
				{
					return;
				}
			}
			Process.GetCurrentProcess().Kill();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000062C8 File Offset: 0x000044C8
		private void Snow()
		{
			int num = this._random.Next(-500, (int)this.LayoutRoot.ActualWidth - 100);
			double num2 = (double)this._random.Next(5, 15) * 0.1;
			int num3 = this._random.Next(0, 270);
			RotateTransform rotateTransform = new RotateTransform((double)num3);
			ScaleTransform scaleTransform = new ScaleTransform(num2, num2);
			TranslateTransform translateTransform = new TranslateTransform((double)num, (double)(-100f));
			SnowFlake flake = new SnowFlake
			{
				RenderTransform = new TransformGroup
				{
					Children = new TransformCollection { rotateTransform, scaleTransform, translateTransform }
				},
				HorizontalAlignment = HorizontalAlignment.Left,
				VerticalAlignment = VerticalAlignment.Top
			};
			this.LayoutRoot.Children.Add(flake);
			Duration duration = new Duration(TimeSpan.FromSeconds(3.0));
			DoubleAnimation doubleAnimation = LoginPage.GenerateAnimation(num + this._random.Next(100, 500), duration, flake, "RenderTransform.Children[2].X");
			int num4 = -100 + (int)(this.LayoutRoot.ActualHeight + 100.0 + 100.0);
			DoubleAnimation doubleAnimation2 = LoginPage.GenerateAnimation(num4, duration, flake, "RenderTransform.Children[2].Y");
			num3 += this._random.Next(90, 360);
			DoubleAnimation doubleAnimation3 = LoginPage.GenerateAnimation(num3, duration, flake, "RenderTransform.Children[0].Angle");
			Storyboard story = new Storyboard();
			story.Completed += delegate(object sender, EventArgs e)
			{
				this.LayoutRoot.Children.Remove(flake);
			};
			story.Children.Add(doubleAnimation);
			story.Children.Add(doubleAnimation2);
			story.Children.Add(doubleAnimation3);
			flake.Loaded += delegate(object sender, RoutedEventArgs e)
			{
				story.Begin();
			};
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003200 File Offset: 0x00001400
		private static DoubleAnimation GenerateAnimation(int x, Duration duration, DependencyObject flake, string propertyPath)
		{
			DoubleAnimation doubleAnimation = new DoubleAnimation();
			doubleAnimation.To = new double?((double)x);
			doubleAnimation.Duration = duration;
			Storyboard.SetTarget(doubleAnimation, flake);
			Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(propertyPath, Array.Empty<object>()));
			return doubleAnimation;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000064D4 File Offset: 0x000046D4
		private async void signInButton_Click(object sender, RoutedEventArgs e)
		{
			this.signInButton.IsEnabled = false;
			Settings.GetInstance.PanelOffset = DateTime.Now.Ticks;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000650C File Offset: 0x0000470C
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri uri = new Uri("/VertigoBoostPanel;component/ui/windows/loginpage.xaml", UriKind.Relative);
			Application.LoadComponent(this, uri);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00006540 File Offset: 0x00004740
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				((LoginPage)target).ContentRendered += this.Window_ContentRendered;
				((LoginPage)target).MouseEnter += this.Window_MouseEnter;
				return;
			case 2:
				this.LayoutRoot = (Grid)target;
				return;
			case 3:
				((Border)target).MouseDown += this.Drag_MouseDown;
				return;
			case 4:
				this.signInButton = (Button)target;
				this.signInButton.Click += this.signInButton_Click;
				return;
			case 5:
				this.label = (Label)target;
				return;
			case 6:
				this.welcomeText = (TextBlock)target;
				return;
			case 7:
				this.b_close = (Button)target;
				this.b_close.MouseLeave += this.ButtonTopPanel_MouseLeave;
				this.b_close.MouseEnter += this.ButtonTopPanel_MouseEnter;
				return;
			case 8:
				((Button)target).MouseLeave += this.ButtonTopPanel_MouseLeave;
				((Button)target).MouseEnter += this.ButtonTopPanel_MouseEnter;
				((Button)target).Click += this.Minimize_Button_Click;
				return;
			case 9:
				this.image_Copy = (Image)target;
				return;
			case 10:
				this.errorLabel = (Label)target;
				return;
			case 11:
				this.loginToolTip = (TextBlock)target;
				return;
			case 12:
				this.loginbox = (TextBox)target;
				this.loginbox.TextChanged += this.loginbox_TextChanged;
				return;
			case 13:
				this.passwordToolTip = (TextBlock)target;
				return;
			case 14:
				this.passwordbox = (PasswordBox)target;
				this.passwordbox.PasswordChanged += this.passwordbox_PasswordChanged;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}

		// Token: 0x04000014 RID: 20
		private readonly Random _random = new Random((int)DateTime.Now.Ticks);

		// Token: 0x04000015 RID: 21
		internal Grid LayoutRoot;

		// Token: 0x04000016 RID: 22
		internal Button signInButton;

		// Token: 0x04000017 RID: 23
		internal Label label;

		// Token: 0x04000018 RID: 24
		internal TextBlock welcomeText;

		// Token: 0x04000019 RID: 25
		internal Button b_close;

		// Token: 0x0400001A RID: 26
		internal Image image_Copy;

		// Token: 0x0400001B RID: 27
		internal Label errorLabel;

		// Token: 0x0400001C RID: 28
		internal TextBlock loginToolTip;

		// Token: 0x0400001D RID: 29
		internal TextBox loginbox;

		// Token: 0x0400001E RID: 30
		internal TextBlock passwordToolTip;

		// Token: 0x0400001F RID: 31
		internal PasswordBox passwordbox;

		// Token: 0x04000020 RID: 32
		private bool _contentLoaded;
	}
}
