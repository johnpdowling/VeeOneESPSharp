using System;
using System.Collections.Generic;

namespace VeeOne.ESP.Constants
{
	public class PacketId
	{
		public static readonly PacketId reqVersion = new PacketId(0x01, "reqVersion");
		public static readonly PacketId respVersion = new PacketId(0x02, "respVersion");
		public static readonly PacketId reqSerialNumber = new PacketId(0x03, "reqSerialNumber");
		public static readonly PacketId respSerialNumber = new PacketId(0x04, "respSerialNumber");
		public static readonly PacketId reqUserBytes = new PacketId(0x11, "reqUserBytes");
		public static readonly PacketId respUserBytes = new PacketId(0x12, "respUserBytes");
		public static readonly PacketId reqWriteUserBytes = new PacketId(0x13, "reqWriteUserBytes");
		public static readonly PacketId reqFactoryDefault = new PacketId(0x14, "reqFactoryDefault");
		public static readonly PacketId reqWriteSweepDefinition = new PacketId(0x15, "reqWriteSweepDefinition");
		public static readonly PacketId reqAllSweepDefinitions = new PacketId(0x16, "reqAllSweepDefinitions");
		public static readonly PacketId respSweepDefinition = new PacketId(0x17, "respSweepDefinition");
		public static readonly PacketId reqSetSweepsToDefault = new PacketId(0x18, "reqSetSweepsToDefault");
		public static readonly PacketId reqMaxSweepIndex = new PacketId(0x19, "reqMaxSweepIndex");
		public static readonly PacketId respMaxSweepIndex = new PacketId(0x20, "respMaxSweepIndex");
		public static readonly PacketId respSweepWriteResult = new PacketId(0x21, "respSweepWriteResult");
		public static readonly PacketId reqSweepSections = new PacketId(0x22, "reqSweepSections");
		public static readonly PacketId respSweepSections = new PacketId(0x23, "respSweepSections");
		public static readonly PacketId reqDefaultSweepDefinitions = new PacketId(0x24, "reqDefaultSweepDefinitions");
		public static readonly PacketId respDefaultSweepDefinition = new PacketId(0x25, "respDefaultSweepDefinition");
		public static readonly PacketId infDisplayData = new PacketId(0x31, "infDisplayData");
		public static readonly PacketId reqTurnOffMainDisplay = new PacketId(0x32, "reqTurnOffMainDisplay");
		public static readonly PacketId reqTurnOnMainDisplay = new PacketId(0x33, "reqTurnOnMainDisplay");
		public static readonly PacketId reqMuteOn = new PacketId(0x34, "reqMuteOn");
		public static readonly PacketId reqMuteOff = new PacketId(0x35, "reqMuteOff");
		public static readonly PacketId reqChangeMode = new PacketId(0x36, "reqChangeMode");
		public static readonly PacketId reqStartAlertData = new PacketId(0x41, "reqStartAlertData");
		public static readonly PacketId reqStopAlertData = new PacketId(0x42, "reqStopAlertData");
		public static readonly PacketId respAlertData = new PacketId(0x43, "respAlertData");
		public static readonly PacketId respDataReceived = new PacketId(0x61, "respDataReceived");
		public static readonly PacketId reqBatteryVoltage = new PacketId(0x62, "reqBatteryVoltage");
		public static readonly PacketId respBatteryVoltage = new PacketId(0x63, "respBatteryVoltage");
		public static readonly PacketId respUnsupportedPacket = new PacketId(0x64, "respUnsupportedPacket");
		public static readonly PacketId respRequestNotProcessed = new PacketId(0x65, "respRequestNotProcessed");
		public static readonly PacketId infV1Busy = new PacketId(0x66, "infV1Busy");
		public static readonly PacketId respDataError = new PacketId(0x67, "respDataError");
		public static readonly PacketId reqSavvyStatus = new PacketId(0x71, "reqSavvyStatus");
		public static readonly PacketId respSavvyStatus = new PacketId(0x72, "respSavvyStatus");
		public static readonly PacketId reqVehicleSpeed = new PacketId(0x73, "reqVehicleSpeed");
		public static readonly PacketId respVehicleSpeed = new PacketId(0x74, "respVehicleSpeed");
		public static readonly PacketId reqOverrideThumbwheel = new PacketId(0x75, "reqOverrideThumbwheel");
		public static readonly PacketId reqSetSavvyUnmuteEnable = new PacketId(0x76, "reqSetSavvyUnmuteEnable");

		public static readonly PacketId unknownPacketType = new PacketId(0xFF, "UnknownPacketType");


		public static IEnumerable<PacketId> Values
		{
			get
			{
				yield return reqVersion;
				yield return respVersion;
				yield return reqSerialNumber;
				yield return respSerialNumber;
				yield return reqUserBytes;
				yield return respUserBytes;
				yield return reqWriteUserBytes;
				yield return reqFactoryDefault;
				yield return reqWriteSweepDefinition;
				yield return reqAllSweepDefinitions;
				yield return respSweepDefinition;
				yield return reqSetSweepsToDefault;
				yield return reqMaxSweepIndex;
				yield return respMaxSweepIndex;
				yield return respSweepWriteResult;
				yield return reqSweepSections;
				yield return respSweepSections;
				yield return reqDefaultSweepDefinitions;
				yield return respDefaultSweepDefinition;
				yield return infDisplayData;
				yield return reqTurnOffMainDisplay;
				yield return reqTurnOnMainDisplay;
				yield return reqMuteOn;
				yield return reqMuteOff;
				yield return reqChangeMode;
				yield return reqStartAlertData;
				yield return reqStopAlertData;
				yield return respAlertData;
				yield return respDataReceived;
				yield return reqBatteryVoltage;
				yield return respBatteryVoltage;
				yield return respUnsupportedPacket;
				yield return respRequestNotProcessed;
				yield return infV1Busy;
				yield return respDataError;
				yield return reqSavvyStatus;
				yield return respSavvyStatus;
				yield return reqVehicleSpeed;
				yield return respVehicleSpeed;
				yield return reqOverrideThumbwheel;
				yield return reqSetSavvyUnmuteEnable;

				yield return unknownPacketType;
			}
		}


		byte m_value;
		String m_name;

		PacketId(byte _value, String _name)
		{
			m_value = _value;
			m_name = _name;
		}

		public byte ToByteValue()
		{
			return m_value;
		}

		public override String ToString()
		{
			return m_name;
		}
	}
}
