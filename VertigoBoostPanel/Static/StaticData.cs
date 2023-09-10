using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json.Linq;
using VertigoBoostPanel.Core;
using VertigoBoostPanel.Core.Player;
using VertigoBoostPanel.Core.TaskList;
using VertigoBoostPanel.Services.Files;
using VertigoBoostPanel.UI.Pages;

namespace VertigoBoostPanel.Static
{
	// Token: 0x02000016 RID: 22
	public class StaticData
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600009E RID: 158 RVA: 0x0000726C File Offset: 0x0000546C
		public static StaticData GetInstance
		{
			get
			{
				if (StaticData.Class == null)
				{
					object syncObject = StaticData.SyncObject;
					lock (syncObject)
					{
						if (StaticData.Class == null)
						{
							StaticData.Class = new StaticData();
						}
					}
				}
				return StaticData.Class;
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000072D0 File Offset: 0x000054D0
		public async Task RegisterNewSubscriber(string loginResult)
		{
			if (!string.IsNullOrEmpty(loginResult) && !string.IsNullOrWhiteSpace(loginResult))
			{
				if (loginResult == "net_error")
				{
					Program.GetInstance.LoginWindow.Dispatcher.Invoke(delegate
					{
						Program.GetInstance.LoginWindow.ShowError("Unable to connect to server", "red");
					});
				}
				else
				{
					try
					{
						object obj = JObject.Parse(loginResult);
						long num = DateTimeOffset.Now.ToUnixTimeSeconds();
						if (StaticData.<>o__7.<>p__1 == null)
						{
							StaticData.<>o__7.<>p__1 = CallSite<Func<CallSite, object, long>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(long), typeof(StaticData)));
						}
						Func<CallSite, object, long> target = StaticData.<>o__7.<>p__1.Target;
						CallSite <>p__ = StaticData.<>o__7.<>p__1;
						if (StaticData.<>o__7.<>p__0 == null)
						{
							StaticData.<>o__7.<>p__0 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetIndex(CSharpBinderFlags.None, typeof(StaticData), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
							}));
						}
						if (Math.Abs(num - target(<>p__, StaticData.<>o__7.<>p__0.Target(StaticData.<>o__7.<>p__0, obj, "server_time"))) < 86401L)
						{
							if (StaticData.<>o__7.<>p__4 == null)
							{
								StaticData.<>o__7.<>p__4 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(StaticData), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
							}
							Func<CallSite, object, bool> target2 = StaticData.<>o__7.<>p__4.Target;
							CallSite <>p__2 = StaticData.<>o__7.<>p__4;
							if (StaticData.<>o__7.<>p__3 == null)
							{
								StaticData.<>o__7.<>p__3 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof(StaticData), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							Func<CallSite, object, object, object> target3 = StaticData.<>o__7.<>p__3.Target;
							CallSite <>p__3 = StaticData.<>o__7.<>p__3;
							if (StaticData.<>o__7.<>p__2 == null)
							{
								StaticData.<>o__7.<>p__2 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetIndex(CSharpBinderFlags.None, typeof(StaticData), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							if (target2(<>p__2, target3(<>p__3, StaticData.<>o__7.<>p__2.Target(StaticData.<>o__7.<>p__2, obj, "message"), null)))
							{
								if (StaticData.<>o__7.<>p__7 == null)
								{
									StaticData.<>o__7.<>p__7 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof(StaticData), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
									}));
								}
								Func<CallSite, object, string, object> target4 = StaticData.<>o__7.<>p__7.Target;
								CallSite <>p__4 = StaticData.<>o__7.<>p__7;
								if (StaticData.<>o__7.<>p__6 == null)
								{
									StaticData.<>o__7.<>p__6 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "ToString", null, typeof(StaticData), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
								}
								Func<CallSite, object, object> target5 = StaticData.<>o__7.<>p__6.Target;
								CallSite <>p__5 = StaticData.<>o__7.<>p__6;
								if (StaticData.<>o__7.<>p__5 == null)
								{
									StaticData.<>o__7.<>p__5 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetIndex(CSharpBinderFlags.None, typeof(StaticData), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
									}));
								}
								object obj2 = target4(<>p__4, target5(<>p__5, StaticData.<>o__7.<>p__5.Target(StaticData.<>o__7.<>p__5, obj, "message")), "bad_hwid");
								if (StaticData.<>o__7.<>p__13 == null)
								{
									StaticData.<>o__7.<>p__13 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(StaticData), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
								}
								if (!StaticData.<>o__7.<>p__13.Target(StaticData.<>o__7.<>p__13, obj2))
								{
									if (StaticData.<>o__7.<>p__12 == null)
									{
										StaticData.<>o__7.<>p__12 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(StaticData), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
									}
									Func<CallSite, object, bool> target6 = StaticData.<>o__7.<>p__12.Target;
									CallSite <>p__6 = StaticData.<>o__7.<>p__12;
									if (StaticData.<>o__7.<>p__11 == null)
									{
										StaticData.<>o__7.<>p__11 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.Or, typeof(StaticData), new CSharpArgumentInfo[]
										{
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
										}));
									}
									Func<CallSite, object, object, object> target7 = StaticData.<>o__7.<>p__11.Target;
									CallSite <>p__7 = StaticData.<>o__7.<>p__11;
									object obj3 = obj2;
									if (StaticData.<>o__7.<>p__10 == null)
									{
										StaticData.<>o__7.<>p__10 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof(StaticData), new CSharpArgumentInfo[]
										{
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
										}));
									}
									Func<CallSite, object, string, object> target8 = StaticData.<>o__7.<>p__10.Target;
									CallSite <>p__8 = StaticData.<>o__7.<>p__10;
									if (StaticData.<>o__7.<>p__9 == null)
									{
										StaticData.<>o__7.<>p__9 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "ToString", null, typeof(StaticData), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
									}
									Func<CallSite, object, object> target9 = StaticData.<>o__7.<>p__9.Target;
									CallSite <>p__9 = StaticData.<>o__7.<>p__9;
									if (StaticData.<>o__7.<>p__8 == null)
									{
										StaticData.<>o__7.<>p__8 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetIndex(CSharpBinderFlags.None, typeof(StaticData), new CSharpArgumentInfo[]
										{
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
										}));
									}
									if (!target6(<>p__6, target7(<>p__7, obj3, target8(<>p__8, target9(<>p__9, StaticData.<>o__7.<>p__8.Target(StaticData.<>o__7.<>p__8, obj, "message")), "bad_machine"))))
									{
										if (StaticData.<>o__7.<>p__17 == null)
										{
											StaticData.<>o__7.<>p__17 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(StaticData), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
										}
										Func<CallSite, object, bool> target10 = StaticData.<>o__7.<>p__17.Target;
										CallSite <>p__10 = StaticData.<>o__7.<>p__17;
										if (StaticData.<>o__7.<>p__16 == null)
										{
											StaticData.<>o__7.<>p__16 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof(StaticData), new CSharpArgumentInfo[]
											{
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
											}));
										}
										Func<CallSite, object, string, object> target11 = StaticData.<>o__7.<>p__16.Target;
										CallSite <>p__11 = StaticData.<>o__7.<>p__16;
										if (StaticData.<>o__7.<>p__15 == null)
										{
											StaticData.<>o__7.<>p__15 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "ToString", null, typeof(StaticData), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
										}
										Func<CallSite, object, object> target12 = StaticData.<>o__7.<>p__15.Target;
										CallSite <>p__12 = StaticData.<>o__7.<>p__15;
										if (StaticData.<>o__7.<>p__14 == null)
										{
											StaticData.<>o__7.<>p__14 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetIndex(CSharpBinderFlags.None, typeof(StaticData), new CSharpArgumentInfo[]
											{
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
											}));
										}
										if (target10(<>p__10, target11(<>p__11, target12(<>p__12, StaticData.<>o__7.<>p__14.Target(StaticData.<>o__7.<>p__14, obj, "message")), "wrong_password_crack")))
										{
											Program.GetInstance.LoginWindow.Dispatcher.Invoke(delegate
											{
												Program.GetInstance.LoginWindow.ShowError("Wrong login or password", "red");
											});
											return;
										}
										if (StaticData.<>o__7.<>p__21 == null)
										{
											StaticData.<>o__7.<>p__21 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(StaticData), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
										}
										Func<CallSite, object, bool> target13 = StaticData.<>o__7.<>p__21.Target;
										CallSite <>p__13 = StaticData.<>o__7.<>p__21;
										if (StaticData.<>o__7.<>p__20 == null)
										{
											StaticData.<>o__7.<>p__20 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof(StaticData), new CSharpArgumentInfo[]
											{
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
											}));
										}
										Func<CallSite, object, string, object> target14 = StaticData.<>o__7.<>p__20.Target;
										CallSite <>p__14 = StaticData.<>o__7.<>p__20;
										if (StaticData.<>o__7.<>p__19 == null)
										{
											StaticData.<>o__7.<>p__19 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "ToString", null, typeof(StaticData), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
										}
										Func<CallSite, object, object> target15 = StaticData.<>o__7.<>p__19.Target;
										CallSite <>p__15 = StaticData.<>o__7.<>p__19;
										if (StaticData.<>o__7.<>p__18 == null)
										{
											StaticData.<>o__7.<>p__18 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetIndex(CSharpBinderFlags.None, typeof(StaticData), new CSharpArgumentInfo[]
											{
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
											}));
										}
										if (target13(<>p__13, target14(<>p__14, target15(<>p__15, StaticData.<>o__7.<>p__18.Target(StaticData.<>o__7.<>p__18, obj, "message")), "no_license")))
										{
											Program.GetInstance.LoginWindow.Dispatcher.Invoke(delegate
											{
												Program.GetInstance.LoginWindow.ShowError("Your subscription has ended", "red");
											});
											return;
										}
										if (StaticData.<>o__7.<>p__25 == null)
										{
											StaticData.<>o__7.<>p__25 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(StaticData), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
										}
										Func<CallSite, object, bool> target16 = StaticData.<>o__7.<>p__25.Target;
										CallSite <>p__16 = StaticData.<>o__7.<>p__25;
										if (StaticData.<>o__7.<>p__24 == null)
										{
											StaticData.<>o__7.<>p__24 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof(StaticData), new CSharpArgumentInfo[]
											{
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
											}));
										}
										Func<CallSite, object, string, object> target17 = StaticData.<>o__7.<>p__24.Target;
										CallSite <>p__17 = StaticData.<>o__7.<>p__24;
										if (StaticData.<>o__7.<>p__23 == null)
										{
											StaticData.<>o__7.<>p__23 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "ToString", null, typeof(StaticData), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
										}
										Func<CallSite, object, object> target18 = StaticData.<>o__7.<>p__23.Target;
										CallSite <>p__18 = StaticData.<>o__7.<>p__23;
										if (StaticData.<>o__7.<>p__22 == null)
										{
											StaticData.<>o__7.<>p__22 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetIndex(CSharpBinderFlags.None, typeof(StaticData), new CSharpArgumentInfo[]
											{
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
											}));
										}
										if (target16(<>p__16, target17(<>p__17, target18(<>p__18, StaticData.<>o__7.<>p__22.Target(StaticData.<>o__7.<>p__22, obj, "message")), "connect_discord")))
										{
											Program.GetInstance.LoginWindow.Dispatcher.Invoke(delegate
											{
												Program.GetInstance.LoginWindow.ShowError("Сonnect your Discord account on the website", "yellow");
											});
											return;
										}
										goto IL_9AB;
									}
								}
								Program.GetInstance.LoginWindow.Dispatcher.Invoke(delegate
								{
									Program.GetInstance.LoginWindow.ShowError("Hwid is already used", "red");
								});
								return;
							}
							IL_9AB:
							if (StaticData.<>o__7.<>p__27 == null)
							{
								StaticData.<>o__7.<>p__27 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(bool), typeof(StaticData)));
							}
							Func<CallSite, object, bool> target19 = StaticData.<>o__7.<>p__27.Target;
							CallSite<Func<CallSite, object, bool>> <>p__19 = StaticData.<>o__7.<>p__27;
							if (StaticData.<>o__7.<>p__26 == null)
							{
								StaticData.<>o__7.<>p__26 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetIndex(CSharpBinderFlags.None, typeof(StaticData), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							Program getInstance = Program.GetInstance;
							if (StaticData.<>o__7.<>p__30 == null)
							{
								StaticData.<>o__7.<>p__30 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof(string), typeof(StaticData)));
							}
							Func<CallSite, object, string> target20 = StaticData.<>o__7.<>p__30.Target;
							CallSite <>p__20 = StaticData.<>o__7.<>p__30;
							if (StaticData.<>o__7.<>p__29 == null)
							{
								StaticData.<>o__7.<>p__29 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "ToString", null, typeof(StaticData), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
							}
							Func<CallSite, object, object> target21 = StaticData.<>o__7.<>p__29.Target;
							CallSite <>p__21 = StaticData.<>o__7.<>p__29;
							if (StaticData.<>o__7.<>p__28 == null)
							{
								StaticData.<>o__7.<>p__28 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetIndex(CSharpBinderFlags.None, typeof(StaticData), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							getInstance.TOKEN = target20(<>p__20, target21(<>p__21, StaticData.<>o__7.<>p__28.Target(StaticData.<>o__7.<>p__28, obj, "token")));
							Program getInstance2 = Program.GetInstance;
							if (StaticData.<>o__7.<>p__34 == null)
							{
								StaticData.<>o__7.<>p__34 = CallSite<Func<CallSite, object, int>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof(int), typeof(StaticData)));
							}
							Func<CallSite, object, int> target22 = StaticData.<>o__7.<>p__34.Target;
							CallSite<Func<CallSite, object, int>> <>p__22 = StaticData.<>o__7.<>p__34;
							if (StaticData.<>o__7.<>p__33 == null)
							{
								StaticData.<>o__7.<>p__33 = CallSite<Func<CallSite, Type, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "ToInt32", null, typeof(StaticData), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
								}));
							}
							Func<CallSite, Type, object, object> target23 = StaticData.<>o__7.<>p__33.Target;
							CallSite<Func<CallSite, Type, object, object>> <>p__23 = StaticData.<>o__7.<>p__33;
							typeof(Convert);
							if (StaticData.<>o__7.<>p__32 == null)
							{
								StaticData.<>o__7.<>p__32 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "ToString", null, typeof(StaticData), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
							}
							Func<CallSite, object, object> target24 = StaticData.<>o__7.<>p__32.Target;
							CallSite<Func<CallSite, object, object>> <>p__24 = StaticData.<>o__7.<>p__32;
							if (StaticData.<>o__7.<>p__31 == null)
							{
								StaticData.<>o__7.<>p__31 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetIndex(CSharpBinderFlags.None, typeof(StaticData), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							Application.Current.Dispatcher.Invoke(delegate
							{
								try
								{
									string text = Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
									string text2;
									using (WebClient webClient = new WebClient())
									{
										text2 = webClient.DownloadString("https://panel.tedonstore.com/vb/version.php?key=panel");
									}
									if (new Version(text) < new Version(text2))
									{
										Program.GetInstance.LoginWindow.Hide();
										if (MessageBox.Show("New version of panel is avialable. Update?\n\nIt is recommended to use a newer version of the software.", "VertigoBoostPanel", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
										{
											using (WebClient webClient2 = new WebClient())
											{
												webClient2.DownloadFile("https://panel.tedonstore.com/download/updater.exe", "updater.exe");
											}
											Process.Start(Directory.GetCurrentDirectory() + "\\updater.exe");
											Process.GetCurrentProcess().Kill();
										}
									}
								}
								catch (Exception ex2)
								{
									Console.WriteLine(ex2);
								}
								try
								{
									foreach (string text3 in new string[] { "config.cfg", "remotecache_7.vdf", "remotecache_730.vdf", "serverbrowser_hist.vdf", "sharedconfig.vdf", "video.txt", "videodefaults.txt" })
									{
										if (!File.Exists("cfg/userdata/" + text3))
										{
											try
											{
												using (WebClient webClient3 = new WebClient())
												{
													webClient3.DownloadFile("https://panel.tedonstore.com/download/configs/" + text3, "cfg/userdata/" + text3);
												}
											}
											catch (Exception ex3)
											{
												LogService.GetInstance.LogInformation(ex3.ToString());
											}
										}
									}
								}
								catch (Exception ex4)
								{
									LogService.GetInstance.LogInformation(ex4.ToString());
								}
								try
								{
									File.Copy("lib\\de_dust2.nav", "C:\\de_dust2.nav", true);
								}
								catch
								{
								}
								Program.GetInstance.LoginWindow.Show();
								Program.GetInstance.MainWindow = new MainWindow();
								Program.GetInstance.MainWindow.Show();
								Program.GetInstance.LoginWindow.Close();
							});
						}
						else
						{
							Program.GetInstance.LoginWindow.Dispatcher.Invoke(delegate
							{
								Program.GetInstance.LoginWindow.ShowError("Request expired", "red");
							});
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.ToString(), "VertigoBoostPanel");
					}
				}
			}
		}

		// Token: 0x0400004D RID: 77
		private static volatile StaticData Class;

		// Token: 0x0400004E RID: 78
		private static object SyncObject = new object();

		// Token: 0x0400004F RID: 79
		public List<Player> Players = new List<Player>();

		// Token: 0x04000050 RID: 80
		public List<BoostTask> BoostTasks = new List<BoostTask>();

		// Token: 0x04000051 RID: 81
		public List<string> taskNamesInQueue = new List<string>();

		// Token: 0x04000052 RID: 82
		public string[] LoginCode = new string[]
		{
			"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"
		};
	}
}
