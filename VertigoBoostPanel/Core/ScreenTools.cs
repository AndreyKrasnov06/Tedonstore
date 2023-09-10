using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace VertigoBoostPanel.Core
{
	// Token: 0x020000C2 RID: 194
	public static class ScreenTools
	{
		// Token: 0x06000476 RID: 1142
		[DllImport("user32.dll")]
		public static extern int GetDisplayConfigBufferSizes(ScreenTools.QUERY_DEVICE_CONFIG_FLAGS flags, out uint numPathArrayElements, out uint numModeInfoArrayElements);

		// Token: 0x06000477 RID: 1143
		[DllImport("user32.dll")]
		public static extern int QueryDisplayConfig(ScreenTools.QUERY_DEVICE_CONFIG_FLAGS flags, ref uint numPathArrayElements, [Out] ScreenTools.DISPLAYCONFIG_PATH_INFO[] PathInfoArray, ref uint numModeInfoArrayElements, [Out] ScreenTools.DISPLAYCONFIG_MODE_INFO[] ModeInfoArray, IntPtr currentTopologyId);

		// Token: 0x06000478 RID: 1144
		[DllImport("user32.dll")]
		public static extern int DisplayConfigGetDeviceInfo(ref ScreenTools.DISPLAYCONFIG_TARGET_DEVICE_NAME deviceName);

		// Token: 0x06000479 RID: 1145 RVA: 0x0001F33C File Offset: 0x0001D53C
		private static string MonitorFriendlyName(ScreenTools.LUID adapterId, uint targetId)
		{
			ScreenTools.DISPLAYCONFIG_TARGET_DEVICE_NAME displayconfig_TARGET_DEVICE_NAME = default(ScreenTools.DISPLAYCONFIG_TARGET_DEVICE_NAME);
			displayconfig_TARGET_DEVICE_NAME.header.size = (uint)Marshal.SizeOf(typeof(ScreenTools.DISPLAYCONFIG_TARGET_DEVICE_NAME));
			displayconfig_TARGET_DEVICE_NAME.header.adapterId = adapterId;
			displayconfig_TARGET_DEVICE_NAME.header.id = targetId;
			displayconfig_TARGET_DEVICE_NAME.header.type = ScreenTools.DISPLAYCONFIG_DEVICE_INFO_TYPE.DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_NAME;
			ScreenTools.DISPLAYCONFIG_TARGET_DEVICE_NAME displayconfig_TARGET_DEVICE_NAME2 = displayconfig_TARGET_DEVICE_NAME;
			int num = ScreenTools.DisplayConfigGetDeviceInfo(ref displayconfig_TARGET_DEVICE_NAME2);
			if (num != 0)
			{
				throw new Win32Exception(num);
			}
			return displayconfig_TARGET_DEVICE_NAME2.monitorFriendlyDeviceName;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0001F3B4 File Offset: 0x0001D5B4
		public static string DeviceFriendlyName(this Screen screen)
		{
			IEnumerable<string> enumerable = new ScreenTools.<GetAllMonitorsFriendlyNames>d__28(-2);
			for (int i = 0; i < Screen.AllScreens.Length; i++)
			{
				if (object.Equals(screen, Screen.AllScreens[i]))
				{
					return enumerable.ToArray<string>()[i];
				}
			}
			return null;
		}

		// Token: 0x040003D2 RID: 978
		public const int ERROR_SUCCESS = 0;

		// Token: 0x020000C3 RID: 195
		public enum QUERY_DEVICE_CONFIG_FLAGS : uint
		{
			// Token: 0x040003D4 RID: 980
			QDC_ALL_PATHS = 1U,
			// Token: 0x040003D5 RID: 981
			QDC_ONLY_ACTIVE_PATHS,
			// Token: 0x040003D6 RID: 982
			QDC_DATABASE_CURRENT = 4U
		}

		// Token: 0x020000C4 RID: 196
		public enum DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY : uint
		{
			// Token: 0x040003D8 RID: 984
			DISPLAYCONFIG_OUTPUT_TECHNOLOGY_OTHER = 4294967295U,
			// Token: 0x040003D9 RID: 985
			DISPLAYCONFIG_OUTPUT_TECHNOLOGY_HD15 = 0U,
			// Token: 0x040003DA RID: 986
			DISPLAYCONFIG_OUTPUT_TECHNOLOGY_SVIDEO,
			// Token: 0x040003DB RID: 987
			DISPLAYCONFIG_OUTPUT_TECHNOLOGY_COMPOSITE_VIDEO,
			// Token: 0x040003DC RID: 988
			DISPLAYCONFIG_OUTPUT_TECHNOLOGY_COMPONENT_VIDEO,
			// Token: 0x040003DD RID: 989
			DISPLAYCONFIG_OUTPUT_TECHNOLOGY_DVI,
			// Token: 0x040003DE RID: 990
			DISPLAYCONFIG_OUTPUT_TECHNOLOGY_HDMI,
			// Token: 0x040003DF RID: 991
			DISPLAYCONFIG_OUTPUT_TECHNOLOGY_LVDS,
			// Token: 0x040003E0 RID: 992
			DISPLAYCONFIG_OUTPUT_TECHNOLOGY_D_JPN = 8U,
			// Token: 0x040003E1 RID: 993
			DISPLAYCONFIG_OUTPUT_TECHNOLOGY_SDI,
			// Token: 0x040003E2 RID: 994
			DISPLAYCONFIG_OUTPUT_TECHNOLOGY_DISPLAYPORT_EXTERNAL,
			// Token: 0x040003E3 RID: 995
			DISPLAYCONFIG_OUTPUT_TECHNOLOGY_DISPLAYPORT_EMBEDDED,
			// Token: 0x040003E4 RID: 996
			DISPLAYCONFIG_OUTPUT_TECHNOLOGY_UDI_EXTERNAL,
			// Token: 0x040003E5 RID: 997
			DISPLAYCONFIG_OUTPUT_TECHNOLOGY_UDI_EMBEDDED,
			// Token: 0x040003E6 RID: 998
			DISPLAYCONFIG_OUTPUT_TECHNOLOGY_SDTVDONGLE,
			// Token: 0x040003E7 RID: 999
			DISPLAYCONFIG_OUTPUT_TECHNOLOGY_MIRACAST,
			// Token: 0x040003E8 RID: 1000
			DISPLAYCONFIG_OUTPUT_TECHNOLOGY_INTERNAL = 2147483648U,
			// Token: 0x040003E9 RID: 1001
			DISPLAYCONFIG_OUTPUT_TECHNOLOGY_FORCE_UINT32 = 4294967295U
		}

		// Token: 0x020000C5 RID: 197
		public enum DISPLAYCONFIG_SCANLINE_ORDERING : uint
		{
			// Token: 0x040003EB RID: 1003
			DISPLAYCONFIG_SCANLINE_ORDERING_UNSPECIFIED,
			// Token: 0x040003EC RID: 1004
			DISPLAYCONFIG_SCANLINE_ORDERING_PROGRESSIVE,
			// Token: 0x040003ED RID: 1005
			DISPLAYCONFIG_SCANLINE_ORDERING_INTERLACED,
			// Token: 0x040003EE RID: 1006
			DISPLAYCONFIG_SCANLINE_ORDERING_INTERLACED_UPPERFIELDFIRST = 2U,
			// Token: 0x040003EF RID: 1007
			DISPLAYCONFIG_SCANLINE_ORDERING_INTERLACED_LOWERFIELDFIRST,
			// Token: 0x040003F0 RID: 1008
			DISPLAYCONFIG_SCANLINE_ORDERING_FORCE_UINT32 = 4294967295U
		}

		// Token: 0x020000C6 RID: 198
		public enum DISPLAYCONFIG_ROTATION : uint
		{
			// Token: 0x040003F2 RID: 1010
			DISPLAYCONFIG_ROTATION_IDENTITY = 1U,
			// Token: 0x040003F3 RID: 1011
			DISPLAYCONFIG_ROTATION_ROTATE90,
			// Token: 0x040003F4 RID: 1012
			DISPLAYCONFIG_ROTATION_ROTATE180,
			// Token: 0x040003F5 RID: 1013
			DISPLAYCONFIG_ROTATION_ROTATE270,
			// Token: 0x040003F6 RID: 1014
			DISPLAYCONFIG_ROTATION_FORCE_UINT32 = 4294967295U
		}

		// Token: 0x020000C7 RID: 199
		public enum DISPLAYCONFIG_SCALING : uint
		{
			// Token: 0x040003F8 RID: 1016
			DISPLAYCONFIG_SCALING_IDENTITY = 1U,
			// Token: 0x040003F9 RID: 1017
			DISPLAYCONFIG_SCALING_CENTERED,
			// Token: 0x040003FA RID: 1018
			DISPLAYCONFIG_SCALING_STRETCHED,
			// Token: 0x040003FB RID: 1019
			DISPLAYCONFIG_SCALING_ASPECTRATIOCENTEREDMAX,
			// Token: 0x040003FC RID: 1020
			DISPLAYCONFIG_SCALING_CUSTOM,
			// Token: 0x040003FD RID: 1021
			DISPLAYCONFIG_SCALING_PREFERRED = 128U,
			// Token: 0x040003FE RID: 1022
			DISPLAYCONFIG_SCALING_FORCE_UINT32 = 4294967295U
		}

		// Token: 0x020000C8 RID: 200
		public enum DISPLAYCONFIG_PIXELFORMAT : uint
		{
			// Token: 0x04000400 RID: 1024
			DISPLAYCONFIG_PIXELFORMAT_8BPP = 1U,
			// Token: 0x04000401 RID: 1025
			DISPLAYCONFIG_PIXELFORMAT_16BPP,
			// Token: 0x04000402 RID: 1026
			DISPLAYCONFIG_PIXELFORMAT_24BPP,
			// Token: 0x04000403 RID: 1027
			DISPLAYCONFIG_PIXELFORMAT_32BPP,
			// Token: 0x04000404 RID: 1028
			DISPLAYCONFIG_PIXELFORMAT_NONGDI,
			// Token: 0x04000405 RID: 1029
			DISPLAYCONFIG_PIXELFORMAT_FORCE_UINT32 = 4294967295U
		}

		// Token: 0x020000C9 RID: 201
		public enum DISPLAYCONFIG_MODE_INFO_TYPE : uint
		{
			// Token: 0x04000407 RID: 1031
			DISPLAYCONFIG_MODE_INFO_TYPE_SOURCE = 1U,
			// Token: 0x04000408 RID: 1032
			DISPLAYCONFIG_MODE_INFO_TYPE_TARGET,
			// Token: 0x04000409 RID: 1033
			DISPLAYCONFIG_MODE_INFO_TYPE_FORCE_UINT32 = 4294967295U
		}

		// Token: 0x020000CA RID: 202
		public enum DISPLAYCONFIG_DEVICE_INFO_TYPE : uint
		{
			// Token: 0x0400040B RID: 1035
			DISPLAYCONFIG_DEVICE_INFO_GET_SOURCE_NAME = 1U,
			// Token: 0x0400040C RID: 1036
			DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_NAME,
			// Token: 0x0400040D RID: 1037
			DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_PREFERRED_MODE,
			// Token: 0x0400040E RID: 1038
			DISPLAYCONFIG_DEVICE_INFO_GET_ADAPTER_NAME,
			// Token: 0x0400040F RID: 1039
			DISPLAYCONFIG_DEVICE_INFO_SET_TARGET_PERSISTENCE,
			// Token: 0x04000410 RID: 1040
			DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_BASE_TYPE,
			// Token: 0x04000411 RID: 1041
			DISPLAYCONFIG_DEVICE_INFO_FORCE_UINT32 = 4294967295U
		}

		// Token: 0x020000CB RID: 203
		public struct LUID
		{
			// Token: 0x04000412 RID: 1042
			public uint LowPart;

			// Token: 0x04000413 RID: 1043
			public int HighPart;
		}

		// Token: 0x020000CC RID: 204
		public struct DISPLAYCONFIG_PATH_SOURCE_INFO
		{
			// Token: 0x04000414 RID: 1044
			public ScreenTools.LUID adapterId;

			// Token: 0x04000415 RID: 1045
			public uint id;

			// Token: 0x04000416 RID: 1046
			public uint modeInfoIdx;

			// Token: 0x04000417 RID: 1047
			public uint statusFlags;
		}

		// Token: 0x020000CD RID: 205
		public struct DISPLAYCONFIG_PATH_TARGET_INFO
		{
			// Token: 0x04000418 RID: 1048
			public ScreenTools.LUID adapterId;

			// Token: 0x04000419 RID: 1049
			public uint id;

			// Token: 0x0400041A RID: 1050
			public uint modeInfoIdx;

			// Token: 0x0400041B RID: 1051
			private ScreenTools.DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY outputTechnology;

			// Token: 0x0400041C RID: 1052
			private ScreenTools.DISPLAYCONFIG_ROTATION rotation;

			// Token: 0x0400041D RID: 1053
			private ScreenTools.DISPLAYCONFIG_SCALING scaling;

			// Token: 0x0400041E RID: 1054
			private ScreenTools.DISPLAYCONFIG_RATIONAL refreshRate;

			// Token: 0x0400041F RID: 1055
			private ScreenTools.DISPLAYCONFIG_SCANLINE_ORDERING scanLineOrdering;

			// Token: 0x04000420 RID: 1056
			public bool targetAvailable;

			// Token: 0x04000421 RID: 1057
			public uint statusFlags;
		}

		// Token: 0x020000CE RID: 206
		public struct DISPLAYCONFIG_RATIONAL
		{
			// Token: 0x04000422 RID: 1058
			public uint Numerator;

			// Token: 0x04000423 RID: 1059
			public uint Denominator;
		}

		// Token: 0x020000CF RID: 207
		public struct DISPLAYCONFIG_PATH_INFO
		{
			// Token: 0x04000424 RID: 1060
			public ScreenTools.DISPLAYCONFIG_PATH_SOURCE_INFO sourceInfo;

			// Token: 0x04000425 RID: 1061
			public ScreenTools.DISPLAYCONFIG_PATH_TARGET_INFO targetInfo;

			// Token: 0x04000426 RID: 1062
			public uint flags;
		}

		// Token: 0x020000D0 RID: 208
		public struct DISPLAYCONFIG_2DREGION
		{
			// Token: 0x04000427 RID: 1063
			public uint cx;

			// Token: 0x04000428 RID: 1064
			public uint cy;
		}

		// Token: 0x020000D1 RID: 209
		public struct DISPLAYCONFIG_VIDEO_SIGNAL_INFO
		{
			// Token: 0x04000429 RID: 1065
			public ulong pixelRate;

			// Token: 0x0400042A RID: 1066
			public ScreenTools.DISPLAYCONFIG_RATIONAL hSyncFreq;

			// Token: 0x0400042B RID: 1067
			public ScreenTools.DISPLAYCONFIG_RATIONAL vSyncFreq;

			// Token: 0x0400042C RID: 1068
			public ScreenTools.DISPLAYCONFIG_2DREGION activeSize;

			// Token: 0x0400042D RID: 1069
			public ScreenTools.DISPLAYCONFIG_2DREGION totalSize;

			// Token: 0x0400042E RID: 1070
			public uint videoStandard;

			// Token: 0x0400042F RID: 1071
			public ScreenTools.DISPLAYCONFIG_SCANLINE_ORDERING scanLineOrdering;
		}

		// Token: 0x020000D2 RID: 210
		public struct DISPLAYCONFIG_TARGET_MODE
		{
			// Token: 0x04000430 RID: 1072
			public ScreenTools.DISPLAYCONFIG_VIDEO_SIGNAL_INFO targetVideoSignalInfo;
		}

		// Token: 0x020000D3 RID: 211
		public struct POINTL
		{
			// Token: 0x04000431 RID: 1073
			private int x;

			// Token: 0x04000432 RID: 1074
			private int y;
		}

		// Token: 0x020000D4 RID: 212
		public struct DISPLAYCONFIG_SOURCE_MODE
		{
			// Token: 0x04000433 RID: 1075
			public uint width;

			// Token: 0x04000434 RID: 1076
			public uint height;

			// Token: 0x04000435 RID: 1077
			public ScreenTools.DISPLAYCONFIG_PIXELFORMAT pixelFormat;

			// Token: 0x04000436 RID: 1078
			public ScreenTools.POINTL position;
		}

		// Token: 0x020000D5 RID: 213
		[StructLayout(LayoutKind.Explicit)]
		public struct DISPLAYCONFIG_MODE_INFO_UNION
		{
			// Token: 0x04000437 RID: 1079
			[FieldOffset(0)]
			public ScreenTools.DISPLAYCONFIG_TARGET_MODE targetMode;

			// Token: 0x04000438 RID: 1080
			[FieldOffset(0)]
			public ScreenTools.DISPLAYCONFIG_SOURCE_MODE sourceMode;
		}

		// Token: 0x020000D6 RID: 214
		public struct DISPLAYCONFIG_MODE_INFO
		{
			// Token: 0x04000439 RID: 1081
			public ScreenTools.DISPLAYCONFIG_MODE_INFO_TYPE infoType;

			// Token: 0x0400043A RID: 1082
			public uint id;

			// Token: 0x0400043B RID: 1083
			public ScreenTools.LUID adapterId;

			// Token: 0x0400043C RID: 1084
			public ScreenTools.DISPLAYCONFIG_MODE_INFO_UNION modeInfo;
		}

		// Token: 0x020000D7 RID: 215
		public struct DISPLAYCONFIG_TARGET_DEVICE_NAME_FLAGS
		{
			// Token: 0x0400043D RID: 1085
			public uint value;
		}

		// Token: 0x020000D8 RID: 216
		public struct DISPLAYCONFIG_DEVICE_INFO_HEADER
		{
			// Token: 0x0400043E RID: 1086
			public ScreenTools.DISPLAYCONFIG_DEVICE_INFO_TYPE type;

			// Token: 0x0400043F RID: 1087
			public uint size;

			// Token: 0x04000440 RID: 1088
			public ScreenTools.LUID adapterId;

			// Token: 0x04000441 RID: 1089
			public uint id;
		}

		// Token: 0x020000D9 RID: 217
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct DISPLAYCONFIG_TARGET_DEVICE_NAME
		{
			// Token: 0x04000442 RID: 1090
			public ScreenTools.DISPLAYCONFIG_DEVICE_INFO_HEADER header;

			// Token: 0x04000443 RID: 1091
			public ScreenTools.DISPLAYCONFIG_TARGET_DEVICE_NAME_FLAGS flags;

			// Token: 0x04000444 RID: 1092
			public ScreenTools.DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY outputTechnology;

			// Token: 0x04000445 RID: 1093
			public ushort edidManufactureId;

			// Token: 0x04000446 RID: 1094
			public ushort edidProductCodeId;

			// Token: 0x04000447 RID: 1095
			public uint connectorInstance;

			// Token: 0x04000448 RID: 1096
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			public string monitorFriendlyDeviceName;

			// Token: 0x04000449 RID: 1097
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			public string monitorDevicePath;
		}
	}
}
