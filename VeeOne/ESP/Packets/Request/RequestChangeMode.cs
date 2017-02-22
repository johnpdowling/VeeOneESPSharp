using System;
using VeeOne.ESP.Constants;

namespace VeeOne.ESP.Packets.Request
{
	public class RequestChangeMode : ESPPacket
	{
        byte m_mode;

        public static readonly byte ALL_BOGIES = 1;
        public static readonly byte LOGIC_MODE = 2;
        public static readonly byte ADVANCED_LOGIC_MODE = 3;
        
        public RequestChangeMode(byte _mode, Devices _destination)
        {
            m_mode = _mode;
            m_destination = _destination.ToByteValue();
            m_valentineType = _destination;
            m_timeStamp = Environment.TickCount;
            buildPacket();
        }

        protected override void buildPacket()
        {
            base.buildPacket();
            packetIdentifier = PacketId.reqChangeMode.ToByteValue();

            payloadData = new byte[1];
            payloadData[0] = m_mode;

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
