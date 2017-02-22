using System;
using VeeOne.ESP.Constants;
using VeeOne.ESP.Packets;
using VeeOne.ESP.Packets.Request;
using VeeOne.ESP.Packets.Response;

namespace VeeOne.ESP.Factories
{
	public class PacketFactory
	{
		public static ESPPacket getPacket(PacketId _id)
		{
			ESPPacket rc = null;
			if (_id != null)
			{
				switch (_id)
				{
					//basic data
					case PacketId.reqVersion:
						rc = new RequestVersion(Devices.UNKNOWN, Devices.UNKNOWN);
						break;
					case PacketId.respVersion:
						rc = new ResponseVersion(Devices.UNKNOWN);
						break;
					case PacketId.reqSerialNumber:
						rc = new RequestSerialNumber(Devices.UNKNOWN, Devices.UNKNOWN);
						break;
					case PacketId.respSerialNumber:
						rc = new ResponseSerialNumber(Devices.UNKNOWN);
						break;
					case PacketId.reqUserBytes:
						rc = new RequestUserBytes(Devices.UNKNOWN);
						break;
					case PacketId.respUserBytes:
						rc = new ResponseUserBytes(Devices.UNKNOWN);
						break;
					case PacketId.reqWriteUserBytes:
						rc = new RequestWriteUserBytes(null, Devices.UNKNOWN);
						break;
					case PacketId.reqFactoryDefault:
						rc = new RequestFactoryDefault(Devices.UNKNOWN, Devices.UNKNOWN);
						break;
					case PacketId.reqDefaultSweepDefinitions:
						rc = new RequestDefaultSweepDefinitions(Devices.UNKNOWN);
						break;
					case PacketId.respDefaultSweepDefinition:
						rc = new ResponseDefaultSweepDefinitions(Devices.UNKNOWN);
						break;
					//custom sweep data
					case PacketId.reqWriteSweepDefinition:
						rc = new RequestWriteSweepDefinition(null, Devices.UNKNOWN);
						break;
					case PacketId.reqAllSweepDefinitions:
						rc = new RequestAllSweepDefinitions(Devices.UNKNOWN);
						break;
					case PacketId.respSweepDefinition:
						rc = new ResponseSweepDefinitions(Devices.UNKNOWN);
						break;
					case PacketId.reqSetSweepsToDefault:
						rc = new RequestSetSweepsToDefault(Devices.UNKNOWN);
						break;
					case PacketId.reqMaxSweepIndex:
						rc = new RequestMaxSweepIndex(Devices.UNKNOWN);
						break;
					case PacketId.respMaxSweepIndex:
						rc = new ResponseMaxSweepIndex(Devices.UNKNOWN);
						break;
					case PacketId.respSweepWriteResult:
						rc = new ResponseSweepWriteResult(Devices.UNKNOWN);
						break;
					case PacketId.reqSweepSections:
						rc = new RequestSweepSections(Devices.UNKNOWN);
						break;
					case PacketId.respSweepSections:
						rc = new ResponseSweepSections(Devices.UNKNOWN);
						break;

					//informational packets
					case PacketId.infDisplayData:
						rc = new InfDisplayData(Devices.UNKNOWN);
						break;
					case PacketId.reqTurnOffMainDisplay:
						rc = new RequestTurnOffMainDisplay(Devices.UNKNOWN);
						break;
					case PacketId.reqTurnOnMainDisplay:
						rc = new RequestTurnOnMainDisplay(Devices.UNKNOWN);
						break;
					case PacketId.reqMuteOn:
						rc = new RequestMuteOn(Devices.UNKNOWN);
						break;
					case PacketId.reqMuteOff:
						rc = new RequestMuteOff(Devices.UNKNOWN);
						break;
					case PacketId.reqChangeMode:
						rc = new RequestChangeMode((byte)0, Devices.UNKNOWN);
						break;
					case PacketId.reqStartAlertData:
						rc = new RequestStartAlertData(Devices.UNKNOWN);
						break;
					case PacketId.reqStopAlertData:
						rc = new RequestStopAlertData(Devices.UNKNOWN);
						break;
					case PacketId.respAlertData:
						rc = new ResponseAlertData(Devices.UNKNOWN);
						break;
					case PacketId.respDataReceived:
						rc = new ResponseDataReceived(Devices.UNKNOWN);
						break;
					case PacketId.reqBatteryVoltage:
						rc = new RequestBatteryVoltage(Devices.UNKNOWN);
						break;
					case PacketId.respBatteryVoltage:
						rc = new ResponseBatteryVoltage(Devices.UNKNOWN);
						break;

					//unspported and error
					case PacketId.respUnsupportedPacket:
						rc = new ResponseUnsupported(Devices.UNKNOWN);
						break;
					case PacketId.respRequestNotProcessed:
						rc = new ResponseRequestNotProcessed(Devices.UNKNOWN);
						break;
					case PacketId.infV1Busy:
						rc = new InfV1Busy(Devices.UNKNOWN);
						break;
					case PacketId.respDataError:
						rc = new ResponseDataError(Devices.UNKNOWN);
						break;

					//Savvy
					case PacketId.reqSavvyStatus:
						rc = new RequestSavvyStatus(Devices.UNKNOWN, Devices.UNKNOWN);
						break;
					case PacketId.respSavvyStatus:
						rc = new ResponseSavvyStatus(Devices.UNKNOWN);
						break;
					case PacketId.reqVehicleSpeed:
						rc = new RequestVehicleSpeed(Devices.UNKNOWN, Devices.UNKNOWN);
						break;
					case PacketId.respVehicleSpeed:
						rc = new ResponseVehicleSpeed(Devices.UNKNOWN);
						break;
					case PacketId.reqOverrideThumbwheel:
						rc = new RequestOverrideThumbwheel(Devices.UNKNOWN, (byte)0, Devices.UNKNOWN);
						break;
					case PacketId.reqSetSavvyUnmuteEnable:
						rc = new RequestSetSavvyUnmute(Devices.UNKNOWN, false, Devices.UNKNOWN);
						break;
					default:
						rc = new UnknownPacket(Devices.UNKNOWN);
						break;
				}
			}
			return rc;
		}
	}
}
