using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;

namespace VertigoBoostPanel.Services.Steam
{
	// Token: 0x02000087 RID: 135
	public static class CaseInfo
	{
		// Token: 0x06000388 RID: 904 RVA: 0x00018178 File Offset: 0x00016378
		public static ValueTuple<string, string, string> GetCaseInfo(string Id)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(Id);
			if (num <= 3195792475U)
			{
				if (num <= 1201880104U)
				{
					if (num <= 346490218U)
					{
						if (num <= 258519163U)
						{
							if (num != 216630596U)
							{
								if (num == 258519163U)
								{
									if (Id == "4598")
									{
										return new ValueTuple<string, string, string>("Prisma Case", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_community_22.f223dd42c8d4c3ecf5a03b27416cb523f493d8af.png", CaseInfo.GetItemPriceByMarketname("Prisma Case"));
									}
								}
							}
							else if (Id == "4403")
							{
								return new ValueTuple<string, string, string>("Spectrum 2 Case", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_community_18.4255d9e03d5dad034bbe868622733deeb81434c1.png", CaseInfo.GetItemPriceByMarketname("Spectrum 2 Case"));
							}
						}
						else if (num != 291235758U)
						{
							if (num != 329712599U)
							{
								if (num == 346490218U)
								{
									if (Id == "4351")
									{
										return new ValueTuple<string, string, string>("Spectrum Case", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_community_16.a2ec6a235e52612fa82e5858af3751b6e77f4ec2.png", CaseInfo.GetItemPriceByMarketname("Spectrum Case"));
									}
								}
							}
							else if (Id == "4352")
							{
								return new ValueTuple<string, string, string>("Operation Hydra Case", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_community_17.8d4528eca229d65d0c19929ae2078aab38df1369.png", CaseInfo.GetItemPriceByMarketname("Operation Hydra Case"));
							}
						}
						else if (Id == "4548")
						{
							return new ValueTuple<string, string, string>("Danger Zone Case", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_community_21.0c6cec0c247809e291dea2fe514d46ea21607201.png", CaseInfo.GetItemPriceByMarketname("Danger Zone Case"));
						}
					}
					else if (num <= 842496704U)
					{
						if (num != 779079864U)
						{
							if (num == 842496704U)
							{
								if (Id == "4717")
								{
									return new ValueTuple<string, string, string>("Operation Broken Fang Case", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_community_27.410809461956f26e23d1268152af7bdea7875fb0.png", CaseInfo.GetItemPriceByMarketname("Operation Broken Fang Case"));
								}
							}
						}
						else if (Id == "4818")
						{
							return new ValueTuple<string, string, string>("Dreams & Nightmares Case", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_community_30.c471a3f74a254c80754f7deec19adaf0df266e23.png", CaseInfo.GetItemPriceByMarketname("Dreams & Nightmares Case"));
						}
					}
					else if (num != 1015393553U)
					{
						if (num != 1065834771U)
						{
							if (num == 1201880104U)
							{
								if (Id == "4029")
								{
									return new ValueTuple<string, string, string>("Operation Vanguard Weapon Case", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_community_5.49a38d2d3afff918ae3b5c5bc2ba2f99b02888f3.png", CaseInfo.GetItemPriceByMarketname("Operation Vanguard Weapon Case"));
								}
							}
						}
						else if (Id == "4091")
						{
							return new ValueTuple<string, string, string>("Falchion Case", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_community_8.3cd07b46c7bcb7577453816c5d2f8afdbee98234.png", CaseInfo.GetItemPriceByMarketname("Falchion Case"));
						}
					}
					else if (Id == "4880")
					{
						return new ValueTuple<string, string, string>("Revolution Case", "https://steamcommunity-a.akamaihd.net/economy/image/-9a81dlWLwJ2UUGcVs_nsVtzdOEdtWwKGZZLQHTxDZ7I56KU0Zwwo4NUX4oFJZEHLbXU5A1PIYQNqhpOSV-fRPasw8rsUFJ5KBFZv668FFQynaHMJT9B74-ywtjYxfOmMe_Vx28AucQj3brAoYrz3Fay_kY4MG_wdYeLMlhpLMaM-1U/256fx198f", CaseInfo.GetItemPriceByMarketname("Revolution Case"));
					}
				}
				else if (num <= 1801967570U)
				{
					if (num <= 1746552788U)
					{
						if (num != 1227102013U)
						{
							if (num == 1746552788U)
							{
								if (Id == "4186")
								{
									return new ValueTuple<string, string, string>("Revolver Case", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_community_10.a7a2e0b4f6ee7a99b25c531b2d3bdef5147200f7.png", CaseInfo.GetItemPriceByMarketname("Revolver Case"));
								}
							}
						}
						else if (Id == "4790")
						{
							return new ValueTuple<string, string, string>("Operation Riptide Case", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_community_29.9b830067732b4de110c01fb24cd067a3061b43a8.png", CaseInfo.GetItemPriceByMarketname("Operation Riptide Case"));
						}
					}
					else if (num != 1751634713U)
					{
						if (num != 1763330407U)
						{
							if (num == 1801967570U)
							{
								if (Id == "4236")
								{
									return new ValueTuple<string, string, string>("Gamma Case", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_community_13.9a7d2f757ddbdc915aa005def74ac186a457346a.png", CaseInfo.GetItemPriceByMarketname("Gamma Case"));
								}
							}
						}
						else if (Id == "4187")
						{
							return new ValueTuple<string, string, string>("Operation Wildfire Case", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_community_11.4839d78c3416c2036da2ed42111df77177828399.png", CaseInfo.GetItemPriceByMarketname("Operation Wildfire Case"));
						}
					}
					else if (Id == "4233")
					{
						return new ValueTuple<string, string, string>("Chroma 3 Case", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_community_12.7555fc0b45c4d1e0ff1c117af393463f29f20f66.png", CaseInfo.GetItemPriceByMarketname("Chroma 3 Case"));
					}
				}
				else if (num <= 2767453198U)
				{
					if (num != 2128734709U)
					{
						if (num != 2717120341U)
						{
							if (num == 2767453198U)
							{
								if (Id == "4698")
								{
									return new ValueTuple<string, string, string>("Fracture Case", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_community_26.b25098af5a4285004052786e261be43dec5b89cf.png", CaseInfo.GetItemPriceByMarketname("Fracture Case"));
								}
							}
						}
						else if (Id == "4695")
						{
							return new ValueTuple<string, string, string>("Prisma 2 Case", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_community_25.ef3b4b9eeb6cd8c29eb79e23ceb8205d40df4b55.png", CaseInfo.GetItemPriceByMarketname("Prisma 2 Case"));
						}
					}
					else if (Id == "4471")
					{
						return new ValueTuple<string, string, string>("Clutch Case", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_community_19.982c3a44362ee65b192b359c12d3d3af9ecb56b2.png", CaseInfo.GetItemPriceByMarketname("Clutch Case"));
					}
				}
				else if (num != 2784672102U)
				{
					if (num != 2802141269U)
					{
						if (num == 3195792475U)
						{
							if (Id == "4846")
							{
								return new ValueTuple<string, string, string>("Recoil Case", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_community_31.440b43f4ad3fcfe344ce63b5d8cf59cd8034ba1a.png", CaseInfo.GetItemPriceByMarketname("Recoil Case"));
							}
						}
					}
					else if (Id == "4620")
					{
						return new ValueTuple<string, string, string>("Shattered Web Case", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_community_23.3248a20393f5e73c0ee99448b994e89381565353.png", CaseInfo.GetItemPriceByMarketname("Shattered Web Case"));
					}
				}
				else if (Id == "4669")
				{
					return new ValueTuple<string, string, string>("CS20 Case", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_community_24.2b3bab86cb5b63a196342af58c0e4c82986ab54d.png", CaseInfo.GetItemPriceByMarketname("CS20 Case"));
				}
			}
			else if (num <= 3533571787U)
			{
				if (num <= 3416128454U)
				{
					if (num <= 3332093264U)
					{
						if (num != 3257635197U)
						{
							if (num == 3332093264U)
							{
								if (Id == "4018")
								{
									return new ValueTuple<string, string, string>("Operation Breakout Weapon Case", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_community_4.f0d23848527b7be0f1fc9556b1f3ecfb1193ee40.png", CaseInfo.GetItemPriceByMarketname("Operation Breakout Weapon Case"));
								}
							}
						}
						else if (Id == "4747")
						{
							return new ValueTuple<string, string, string>("Snakebite Case", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_community_28.1f6e656d8fc297c9f2b65f2c05b8552d1cc63082.png", CaseInfo.GetItemPriceByMarketname("Snakebite Case"));
						}
					}
					else if (num != 3348870883U)
					{
						if (num != 3414848526U)
						{
							if (num == 3416128454U)
							{
								if (Id == "4009")
								{
									return new ValueTuple<string, string, string>("Winter Offensive Weapon Case", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_community_1.b176b8ca60249d0a2e7c6d72ec7d2d1a9632bd06.png", CaseInfo.GetItemPriceByMarketname("Winter Offensive Weapon Case"));
								}
							}
						}
						else if (Id == "4089")
						{
							return new ValueTuple<string, string, string>("Chroma 2 Case", "http://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_community_7.57286f710260d1a8eb4e93c4795ae7ca980ea981.png", CaseInfo.GetItemPriceByMarketname("Chroma 2 Case"));
						}
					}
					else if (Id == "4019")
					{
						return new ValueTuple<string, string, string>("eSports 2014 Summer Case", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_esports_2014_summer.e579e3f7ca004fd9b51aa0f597590c936bb9f67c.png", CaseInfo.GetItemPriceByMarketname("eSports 2014 Summer Case"));
					}
				}
				else if (num <= 3483091835U)
				{
					if (num != 3466314216U)
					{
						if (num == 3483091835U)
						{
							if (Id == "4011")
							{
								return new ValueTuple<string, string, string>("Operation Phoenix Weapon Case", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_community_2.49174abddcccb6519b83d27d0cff476e1c44cc57.png", CaseInfo.GetItemPriceByMarketname("Operation Phoenix Weapon Case"));
							}
						}
					}
					else if (Id == "4010")
					{
						return new ValueTuple<string, string, string>("CS:GO Weapon Case 3", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_valve_3.b49dc22a06991946e849c7b364b7d5876534ef61.png", CaseInfo.GetItemPriceByMarketname("CS:GO Weapon Case 3"));
					}
				}
				else if (num != 3499869454U)
				{
					if (num != 3516794168U)
					{
						if (num == 3533571787U)
						{
							if (Id == "4002")
							{
								return new ValueTuple<string, string, string>("eSports 2013 Case", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_esports_2013.c4fd3c71742688383914a7ef7127652764f4567c.png", CaseInfo.GetItemPriceByMarketname("eSports 2013 Case"));
							}
						}
					}
					else if (Id == "4003")
					{
						return new ValueTuple<string, string, string>("Operation Bravo Case", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_operation_ii.5e5104a6291741c5693a1e78bd6ecc9560b51f0a.png", CaseInfo.GetItemPriceByMarketname("Operation Bravo Case"));
					}
				}
				else if (Id == "4012")
				{
					return new ValueTuple<string, string, string>("Sticker Capsule 2", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_sticker_pack02.49c83ded1d25de8cc486de43797b260361bdeff7.png", CaseInfo.GetItemPriceByMarketname("Sticker Capsule 2"));
				}
			}
			else if (num <= 3617459882U)
			{
				if (num <= 3566979930U)
				{
					if (num != 3550349406U)
					{
						if (num == 3566979930U)
						{
							if (Id == "4016")
							{
								return new ValueTuple<string, string, string>("Community Sticker Capsule 1", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_sticker_pack_community01.86463c48a690c9df0853510aeb9f43275baa2d49.png", CaseInfo.GetItemPriceByMarketname("Community Sticker Capsule 1"));
							}
						}
					}
					else if (Id == "4001")
					{
						return new ValueTuple<string, string, string>("CS:GO Weapon Case", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_valve_1.23c783d005b446a1004c97057cfb5ac2d8dae186.png", CaseInfo.GetItemPriceByMarketname("CS:GO Weapon Case"));
					}
				}
				else if (num != 3583757549U)
				{
					if (num != 3583904644U)
					{
						if (num == 3617459882U)
						{
							if (Id == "4005")
							{
								return new ValueTuple<string, string, string>("eSports 2013 Winter Case", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_esports_2013_14.a83d1976bb20db8b4a64e7acad93aac87127ddd5.png", CaseInfo.GetItemPriceByMarketname("eSports 2013 Winter Case"));
							}
						}
					}
					else if (Id == "4007")
					{
						return new ValueTuple<string, string, string>("Sticker Capsule", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_sticker_pack01.050d07ce442dc326f33bbb10ade6df941136b479.png", CaseInfo.GetItemPriceByMarketname("Sticker Capsule"));
					}
				}
				else if (Id == "4017")
				{
					return new ValueTuple<string, string, string>("Huntsman Weapon Case", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_community_3.ce832a92f9fc329dc87d7a802374b918b07cdb84.png", CaseInfo.GetItemPriceByMarketname("Huntsman Weapon Case"));
				}
			}
			else if (num <= 3867335920U)
			{
				if (num != 3618445620U)
				{
					if (num != 3634237501U)
					{
						if (num == 3867335920U)
						{
							if (Id == "4281")
							{
								return new ValueTuple<string, string, string>("Gamma 2 Case", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_gamma_2.ab916b78e7093039642cc7538466bf87cf314363.png", CaseInfo.GetItemPriceByMarketname("Gamma 2 Case"));
							}
						}
					}
					else if (Id == "4004")
					{
						return new ValueTuple<string, string, string>("CS:GO Weapon Case 2", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_valve_2.912031176f2320d2d39f449ab3e27c41f5ec7faa.png", CaseInfo.GetItemPriceByMarketname("CS:GO Weapon Case 2"));
					}
				}
				else if (Id == "4061")
				{
					return new ValueTuple<string, string, string>("Chroma Case", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_community_6.4a84ff42a0e0149973c8580dd23f8ba6e7c68142.png", CaseInfo.GetItemPriceByMarketname("Chroma Case"));
				}
			}
			else if (num != 3892557829U)
			{
				if (num != 4018334491U)
				{
					if (num == 4261213535U)
					{
						if (Id == "4482")
						{
							return new ValueTuple<string, string, string>("Horizon Case", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_community_20.efee8198ed2e0b6ab4b5bb1e99be04a1308b43fe.png", CaseInfo.GetItemPriceByMarketname("Horizon Case"));
						}
					}
				}
				else if (Id == "4288")
				{
					return new ValueTuple<string, string, string>("Glove Case", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_community_15.7dfa18f8f7ce3bc4e55aac0c566fe068e741bddf.png", CaseInfo.GetItemPriceByMarketname("Glove Case"));
				}
			}
			else if (Id == "4138")
			{
				return new ValueTuple<string, string, string>("Shadow Case", "https://steamcdn-a.akamaihd.net/apps/730/icons/econ/weapon_cases/crate_community_9.e8303075e1a0969497a4502140ea47ecc65b4c50.png", CaseInfo.GetItemPriceByMarketname("Shadow Case"));
			}
			return new ValueTuple<string, string, string>("Unknown Case", string.Empty, "$0");
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00018AAC File Offset: 0x00016CAC
		private static string GetItemPriceByMarketname(string name)
		{
			if (CaseInfo._prices.ContainsKey(name))
			{
				return CaseInfo._prices[name];
			}
			string text = string.Empty;
			using (WebClient webClient = new WebClient())
			{
				text = webClient.DownloadString("https://steamcommunity.com/market/priceoverview/?country=DE&currency=1&format=json&appid=730&market_hash_name=" + name.Replace("&", "%26"));
			}
			if (string.IsNullOrEmpty(text))
			{
				return "Price: UNK";
			}
			string text2 = JObject.Parse(text)["median_price"].ToString().Replace(" USD", string.Empty);
			CaseInfo._prices[name] = text2;
			return text2;
		}

		// Token: 0x040002FE RID: 766
		private static Dictionary<string, string> _prices = new Dictionary<string, string>();
	}
}
