using System;
using VeeOne.ESP.Constants;

namespace VeeOne.ESP.Packets.Request
{
    public class RequestOverrideThumbwheel : ESPPacket
    {
        byte m_speed;

        public RequestOverrideThumbwheel(Devices _valentineType, byte _speed, Devices _destination)
        {
            m_destination = _destination.ToByteValue();
            m_valentineType = _valentineType;
            m_speed = _speed;
            m_timeStamp = Environment.TickCount;
            buildPacket();
        }

        protected override void buildPacket()
        {
            base.buildPacket();
            packetIdentifier = PacketId.reqOverrideThumbwheel.ToByteValue();

            payloadData = new byte[1];
            payloadData[0] = m_speed;

            if ((m_valentineType == Devices.VALENTINE1_LEGACY) || (m_valentineType == Devices.VALENTINE1_WITHOUT_CHECKSUM))
            {
                payloadLength = 1;
                checkSum = 0;
            }
            else
            {
                payloadLength = 1;
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
