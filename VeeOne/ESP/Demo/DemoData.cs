using System;
using System.Collections.Generic;
using VeeOne.ESP.Constants;
using VeeOne.ESP.Data;
using VeeOne.ESP.Packets;
using VeeOne.ESP.Packets.Response;
using VeeOne.ESP.Utilities;

namespace VeeOne.ESP.Demo
{
    public class DemoData
    {
        private Dictionary<Devices, ResponseVersion> m_versionPackets;

        private Dictionary<Devices, ResponseSerialNumber> m_V1SerialPackets;

        private ResponseSavvyStatus m_SavvyConfiguration;
        private ResponseUserBytes m_V1Configuration;
        private List<ResponseSweepSections> m_CustomSweepSections;
        private List<ResponseSweepDefinitions> m_ResponseSweepDefinitions;
        private ResponseMaxSweepIndex m_MaximumCustomSweepIndex;
        private ResponseBatteryVoltage m_BatteryVoltage;
        private ResponseVehicleSpeed m_VehicleSpeed;

        private object m_V1ConfigObject;
        private string m_V1ConfigFunction;

        public DemoData()
        {
            m_versionPackets = new Dictionary<Devices, ResponseVersion>();
            m_V1SerialPackets = new Dictionary<Devices, ResponseSerialNumber>();
            m_ResponseSweepDefinitions = new List<ResponseSweepDefinitions>();
            m_CustomSweepSections = new List<ResponseSweepSections>();
        }

        public void setConfigCallbackData(object _owner, string _function)
        {
            m_V1ConfigObject = _owner;
            m_V1ConfigFunction = _function;
        }

        public void handleDemoPacket(ESPPacket _packet)
        {
            switch (_packet.PacketIdentifier)
            {
                case PacketId.respVersion:
                    m_versionPackets.Add(_packet.Origin, (ResponseVersion)_packet);
                    break;
                case PacketId.respSerialNumber:
                    m_V1SerialPackets.Add(_packet.Origin, (ResponseSerialNumber)_packet);
                    break;
                case PacketId.respSavvyStatus:
                    m_SavvyConfiguration = (ResponseSavvyStatus)_packet;
                    break;
                case PacketId.respUserBytes:
                    m_V1Configuration = (ResponseUserBytes)_packet;
                    if ((m_V1ConfigObject != null) && (m_V1ConfigFunction != null))
                    {
                        Utilities.Utilities.doCallback(m_V1ConfigObject, m_V1ConfigFunction, UserSettings.GetType(), (UserSettings) m_V1Configuration.getResponseData());
                    }
                    break;
                case PacketId.respSweepSections:
                    m_CustomSweepSections.Add((ResponseSweepSections)_packet);
                    break;
                case PacketId.respSweepDefinition:
                    m_ResponseSweepDefinitions.Add((ResponseSweepDefinitions)_packet);
                    break;
                case PacketId.respMaxSweepIndex:
                    m_MaximumCustomSweepIndex = (ResponseMaxSweepIndex)_packet;
                    break;
                case PacketId.respBatteryVoltage:
                    m_BatteryVoltage = (ResponseBatteryVoltage)_packet;
                    break;
                case PacketId.respVehicleSpeed:
                    m_VehicleSpeed = (ResponseVehicleSpeed)_packet;
                    break;
                case PacketId.reqVersion:
                    PacketQueue.pushInputPacketOntoQueue(m_versionPackets[_packet.Destination]);
                    break;
                case PacketId.reqSerialNumber:
                    PacketQueue.pushInputPacketOntoQueue(m_V1SerialPackets[_packet.Destination]);
                    break;
                case PacketId.reqUserBytes:
                    PacketQueue.pushInputPacketOntoQueue(m_V1Configuration);
                    break;
                case PacketId.reqAllSweepDefinitions:
                case PacketId.reqSetSweepsToDefault:
                    for (int i = 0; i < m_ResponseSweepDefinitions.Count; i++)
                    {
                        PacketQueue.pushInputPacketOntoQueue(m_ResponseSweepDefinitions[i]);
                    }
                    break;
                case PacketId.reqDefaultSweepDefinitions:
                case PacketId.respDefaultSweepDefinition:
                    // Ignore these packets in the demo mode file.
                    break;
                case PacketId.reqMaxSweepIndex:
                    PacketQueue.pushInputPacketOntoQueue(m_MaximumCustomSweepIndex);
                    break;
                case PacketId.reqSweepSections:
                    for (int i = 0; i < m_CustomSweepSections.Count; i++)
                    {
                        PacketQueue.pushInputPacketOntoQueue(m_CustomSweepSections[i]);
                    }
                    break;
                case PacketId.reqBatteryVoltage:
                    PacketQueue.pushInputPacketOntoQueue(m_BatteryVoltage);
                    break;
                case PacketId.reqSavvyStatus:
                    PacketQueue.pushInputPacketOntoQueue(m_SavvyConfiguration);
                    break;
                case PacketId.reqVehicleSpeed:
                    PacketQueue.pushInputPacketOntoQueue(m_VehicleSpeed);
                    break;



                case PacketId.reqTurnOffMainDisplay:
                case PacketId.reqTurnOnMainDisplay:
                case PacketId.respDataReceived:
                case PacketId.reqStartAlertData:
                case PacketId.reqStopAlertData:
                case PacketId.reqChangeMode:
                case PacketId.reqMuteOn:
                case PacketId.reqMuteOff:
                case PacketId.reqFactoryDefault:
                case PacketId.reqWriteUserBytes:
                case PacketId.reqWriteSweepDefinition:
                case PacketId.reqOverrideThumbwheel:
                case PacketId.reqSetSavvyUnmuteEnable:
                case PacketId.respDataError:
                case PacketId.respUnsupportedPacket:
                case PacketId.respRequestNotProcessed:
                case PacketId.infV1Busy:
                case PacketId.respSweepWriteResult:
                    break;

                case PacketId.respAlertData:
                case PacketId.infDisplayData:
                    PacketQueue.pushInputPacketOntoQueue(_packet);
                    break;

                case PacketId.unknownPacketType:
                    // There is nothing to do with an unknown packet type in demo mode
                    break;
            }
        }

        public UserSettings UserSettings
        {
            get
            {
                return (UserSettings)m_V1Configuration.getResponseData();
            }
        }
	}
}
