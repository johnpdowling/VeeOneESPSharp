using System;
using VeeOne.ESP.Constants;

namespace VeeOne.ESP.Packets.Request
{
    public class RequestStopAlertData : ESPPacket
    {
        public RequestStopAlertData(Devices _destination)
        {
            m_destination = _destination.ToByteValue();
            m_valentineType = _destination;
            m_timeStamp = Environment.TickCount;
            buildPacket();
        }

        protected override void buildPacket()
        {
            base.buildPacket();
            packetIdentifier = PacketId.reqStopAlertData.ToByteValue();
            payloadLength = 0;

            if ((m_valentineType == Devices.VALENTINE1_LEGACY) || (m_valentineType == Devices.VALENTINE1_WITHOUT_CHECKSUM))
            {
                checkSum = 0;
            }
            else
            {
                checkSum = makeMessageChecksum();
            }

            // Sets the packet checksum and packet length values.  
            setPacketInfo();
        }

        public override object getResponseData()
        {
            return null;
        }
    }
}


