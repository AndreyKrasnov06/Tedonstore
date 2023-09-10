using System;
using System.Collections.Generic;
using System.Numerics;
using VertigoBoostPanel.Core.ErrorHandler;

namespace VertigoBoostPanel.Core.WalkBot
{
	// Token: 0x02000133 RID: 307
	public static class Maps
	{
		// Token: 0x0600066F RID: 1647 RVA: 0x0003001C File Offset: 0x0002E21C
		public static List<Vector3> getPathByMapName(string mapName)
		{
			if (mapName.ToLower().Contains("vertigo"))
			{
				return Maps.coords_vertigo;
			}
			if (mapName.ToLower().Contains("dust"))
			{
				return Maps.coords_dust2;
			}
			if (mapName.ToLower().Contains("overpass"))
			{
				return Maps.coords_overpass;
			}
			if (mapName.ToLower().Contains("inferno"))
			{
				return Maps.coords_inferno;
			}
			if (mapName.ToLower().Contains("nuke"))
			{
				return Maps.coords_nuke;
			}
			if (mapName.ToLower().Contains("ancient"))
			{
				return Maps.coords_ancient;
			}
			if (mapName.ToLower().Contains("mirage"))
			{
				return Maps.coords_mirage;
			}
			CreateError.NewError("Panel does not support this map", false);
			return new List<Vector3>();
		}

		// Token: 0x040005FF RID: 1535
		private static List<Vector3> coords_vertigo = new List<Vector3>
		{
			new Vector3(-1690.094f, -1258.348f, 11488.03f),
			new Vector3(-2156.525f, -1244.738f, 11616.03f),
			new Vector3(-2139.712f, -740.9659f, 11776.03f),
			new Vector3(-1929.229f, -468.1042f, 11776.03f),
			new Vector3(-1460.846f, -421.5618f, 11776.03f),
			new Vector3(-1499.841f, -180.0467f, 11776.03f),
			new Vector3(-1888.566f, -102.8077f, 11776.03f),
			new Vector3(-1852.563f, 282.6699f, 11776.03f),
			new Vector3(-1100.474f, 272.3763f, 11776.03f),
			new Vector3(-1161.838f, 571.0498f, 11777.03f)
		};

		// Token: 0x04000600 RID: 1536
		private static List<Vector3> coords_dust2 = new List<Vector3>
		{
			new Vector3(-495.2485f, -986.3611f, 121.6906f),
			new Vector3(178.3509f, -738.8535f, -8.065741f),
			new Vector3(287.8546f, -290.4438f, 0.2084709f),
			new Vector3(568.5607f, 198.6083f, 0.9212723f),
			new Vector3(633.0673f, 294.9809f, 0.6716421f),
			new Vector3(702.1058f, 389.0921f, 2.326099f),
			new Vector3(579.5931f, 594.7777f, 2.791279f),
			new Vector3(606.8956f, 727.0117f, 0.6434283f),
			new Vector3(1115.727f, 1113.858f, -0.5460548f),
			new Vector3(1415.47f, 1165.145f, -10.01733f),
			new Vector3(1391.919f, 2146.59f, -5.737211f),
			new Vector3(405.383f, 2162.648f, -129.557f)
		};

		// Token: 0x04000601 RID: 1537
		private static List<Vector3> coords_overpass = new List<Vector3>
		{
			new Vector3(-785.3538f, -2540.191f, 151.7881f),
			new Vector3(-787.6808f, -2151.401f, 240.0313f),
			new Vector3(-1360.716f, -2153.847f, 336.0313f),
			new Vector3(-1392.804f, -2372.324f, 336.0313f),
			new Vector3(-1922.564f, -2371.371f, 434.7001f),
			new Vector3(-2027.322f, -2104.313f, 466.136f),
			new Vector3(-2616.02f, -1770.272f, 472.0313f),
			new Vector3(-2757.955f, -1506.077f, 434.5468f),
			new Vector3(-2164.042f, -845.022f, 386.6012f),
			new Vector3(-2296.859f, -49.11845f, 406.9271f),
			new Vector3(-2619.819f, 95.91656f, 431.5348f),
			new Vector3(-2492.675f, 480.2283f, 480.0313f),
			new Vector3(-2412.329f, 490.0773f, 480.0313f)
		};

		// Token: 0x04000602 RID: 1538
		private static List<Vector3> coords_inferno = new List<Vector3>
		{
			new Vector3(-1479.142f, 187.746f, -63.96875f),
			new Vector3(-1243.134f, 197.256f, -55.96875f),
			new Vector3(-1245.414f, 512.1519f, -55.96875f),
			new Vector3(-755.076f, 531.14f, -33.89419f),
			new Vector3(-650.2424f, 747.8565f, -33.18771f),
			new Vector3(-50.98327f, 861.6093f, 52.73531f),
			new Vector3(134.3046f, 696.0452f, 77.21309f),
			new Vector3(197.0575f, 563.1827f, 77.41042f),
			new Vector3(1397.015f, 508.8708f, 132.8323f),
			new Vector3(1454.156f, 1121.962f, 157.7225f),
			new Vector3(2326.362f, 1097.182f, 157.7677f),
			new Vector3(2435.575f, 1121.666f, 160.5837f),
			new Vector3(2602.346f, 1127.377f, 160.0313f),
			new Vector3(2531.69f, 1450.253f, 160.0313f),
			new Vector3(2327.233f, 1471.581f, 160.0313f),
			new Vector3(2352.905f, 1735.685f, 141.5807f)
		};

		// Token: 0x04000603 RID: 1539
		private static List<Vector3> coords_nuke = new List<Vector3>
		{
			new Vector3(-1776.157f, -1082.062f, -415.9416f),
			new Vector3(-1630.403f, -1161.23f, -417.1808f),
			new Vector3(-904.7786f, -1058.214f, -417.2479f),
			new Vector3(-648.6877f, -1197.475f, -416.6062f),
			new Vector3(-540.318f, -1480.686f, -415.9688f),
			new Vector3(-81.18461f, -2086.579f, -415.9688f),
			new Vector3(332.7275f, -2210.806f, -415.9688f),
			new Vector3(600.9422f, -1902.826f, -407.9688f),
			new Vector3(1121.075f, -1859.642f, -415.969f),
			new Vector3(1201.647f, -1423.603f, -415.9691f),
			new Vector3(1474.217f, -865.3776f, -416.1538f),
			new Vector3(1482.963f, -504.7802f, -417.1014f),
			new Vector3(1809.54f, -498.2699f, -353.0026f),
			new Vector3(1927.3f, -398.5411f, -352.5937f)
		};

		// Token: 0x04000604 RID: 1540
		private static List<Vector3> coords_ancient = new List<Vector3>
		{
			new Vector3(-671.4639f, -2213.168f, -166.3559f),
			new Vector3(-707.0906f, -1739.68f, -135.0659f),
			new Vector3(-1230.005f, -1512.544f, -23.25575f),
			new Vector3(-1141.646f, -835.9147f, 36.03125f),
			new Vector3(-575.2972f, -727.4749f, 32.03117f),
			new Vector3(-508.5625f, 371.0576f, 160.0313f),
			new Vector3(-538.2132f, 586.3229f, 162.0313f),
			new Vector3(-515.7653f, 801.8593f, 103.5392f),
			new Vector3(-240.0491f, 805.5728f, 77.66425f),
			new Vector3(-245.8232f, 1292.571f, 29.50545f)
		};

		// Token: 0x04000605 RID: 1541
		private static List<Vector3> coords_mirage = new List<Vector3>
		{
			new Vector3(1251.563f, -418.4739f, -163.9688f),
			new Vector3(1156.958f, -664.6707f, -222.9572f),
			new Vector3(1166.334f, -1108.155f, -262.9948f),
			new Vector3(742.0814f, -1111.909f, -255.6965f),
			new Vector3(705.8729f, -1216.51f, -261.2435f),
			new Vector3(590.4526f, -1637.252f, -263.9688f),
			new Vector3(196.7668f, -1513.741f, -175.9688f),
			new Vector3(-435.918f, -1928.13f, -179.9671f),
			new Vector3(-423.3526f, -2207.656f, -175.7106f),
			new Vector3(-914.7095f, -2359.121f, -167.9688f),
			new Vector3(-1430.003f, -2308.916f, -232.7253f),
			new Vector3(-1536.485f, -2072.321f, -261.2769f)
		};
	}
}
