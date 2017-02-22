using System;
using System.Collections.Generic;
using System.Reflection;

namespace VeeOne.ESP.Constants
{
	public enum PacketId
	{
		[ValueNameAttr(0x01, "reqVersion")]
		reqVersion,
		[ValueNameAttr(0x02, "respVersion")]
		respVersion,
		[ValueNameAttr(0x03, "reqSerialNumber")]
		reqSerialNumber,
		[ValueNameAttr(0x04, "respSerialNumber")]
		respSerialNumber,
		[ValueNameAttr(0x11, "reqUserBytes")]
		reqUserBytes,
		[ValueNameAttr(0x12, "respUserBytes")]
		respUserBytes,
		[ValueNameAttr(0x13, "reqWriteUserBytes")]
		reqWriteUserBytes,
		[ValueNameAttr(0x14, "reqFactoryDefault")]
		reqFactoryDefault,
		[ValueNameAttr(0x15, "reqWriteSweepDefinition")]
		reqWriteSweepDefinition,
		[ValueNameAttr(0x16, "reqAllSweepDefinitions")]
		reqAllSweepDefinitions,
		[ValueNameAttr(0x17, "respSweepDefinition")]
		respSweepDefinition,
		[ValueNameAttr(0x18, "reqSetSweepsToDefault")]
		reqSetSweepsToDefault,
		[ValueNameAttr(0x19, "reqMaxSweepIndex")]
		reqMaxSweepIndex,
		[ValueNameAttr(0x20, "respMaxSweepIndex")]
		respMaxSweepIndex,
		[ValueNameAttr(0x21, "respSweepWriteResult")]
		respSweepWriteResult,
		[ValueNameAttr(0x22, "reqSweepSections")]
		reqSweepSections,
		[ValueNameAttr(0x23, "respSweepSections")]
		respSweepSections,
		[ValueNameAttr(0x24, "reqDefaultSweepDefinitions")]
		reqDefaultSweepDefinitions,
		[ValueNameAttr(0x25, "respDefaultSweepDefinition")]
		respDefaultSweepDefinition,
		[ValueNameAttr(0x31, "infDisplayData")]
		infDisplayData,
		[ValueNameAttr(0x32, "reqTurnOffMainDisplay")]
		reqTurnOffMainDisplay,
		[ValueNameAttr(0x33, "reqTurnOnMainDisplay")]
		reqTurnOnMainDisplay,
		[ValueNameAttr(0x34, "reqMuteOn")]
		reqMuteOn,
		[ValueNameAttr(0x35, "reqMuteOff")]
		reqMuteOff,
		[ValueNameAttr(0x36, "reqChangeMode")]
		reqChangeMode,
		[ValueNameAttr(0x41, "reqStartAlertData")]
		reqStartAlertData,
		[ValueNameAttr(0x42, "reqStopAlertData")]
		reqStopAlertData,
		[ValueNameAttr(0x43, "respAlertData")]
		respAlertData,
		[ValueNameAttr(0x61, "respDataReceived")]
		respDataReceived,
		[ValueNameAttr(0x62, "reqBatteryVoltage")]
		reqBatteryVoltage,
		[ValueNameAttr(0x63, "respBatteryVoltage")]
		respBatteryVoltage,
		[ValueNameAttr(0x64, "respUnsupportedPacket")]
		respUnsupportedPacket,
		[ValueNameAttr(0x65, "respRequestNotProcessed")]
		respRequestNotProcessed,
		[ValueNameAttr(0x66, "infV1Busy")]
		infV1Busy,
		[ValueNameAttr(0x67, "respDataError")]
		respDataError,
		[ValueNameAttr(0x71, "reqSavvyStatus")]
		reqSavvyStatus,
		[ValueNameAttr(0x72, "respSavvyStatus")]
		respSavvyStatus,
		[ValueNameAttr(0x73, "reqVehicleSpeed")]
		reqVehicleSpeed,
		[ValueNameAttr(0x74, "respVehicleSpeed")]
		respVehicleSpeed,
		[ValueNameAttr(0x75, "reqOverrideThumbwheel")]
		reqOverrideThumbwheel,
		[ValueNameAttr(0x76, "reqSetSavvyUnmuteEnable")]
		reqSetSavvyUnmuteEnable,

		[ValueNameAttr(0xFF, "UnknownPacketType")]
		unknownPacketType 
	}

	public static class PacketIdExtensions
	{
		public static byte ToByteValue(this PacketId p)
		{
			ValueNameAttr attr = GetAttr(p);
			return attr.Value;
		}

		public static string ToString(this PacketId p)
		{
			ValueNameAttr attr = GetAttr(p);
			return attr.Name;
		}

		private static ValueNameAttr GetAttr(PacketId p)
		{
			return (ValueNameAttr)Attribute.GetCustomAttribute(ForValue(p), typeof(ValueNameAttr));
		}

		private static MemberInfo ForValue(PacketId p)
		{
			return typeof(PacketId).GetField(Enum.GetName(typeof(PacketId), p));
		}

	}
}
