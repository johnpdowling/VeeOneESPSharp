using System;
using VeeOne.ESP.Constants;

namespace VeeOne.ESP.Packets.Request
{
	public class RequestSetSavvyUnmute : ESPPacket
	{
        bool m_mute;

		public RequestSetSavvyUnmute(Devices _valentineType, bool _mute, Devices _destination)
        {
            m_destination = _destination.ToByteValue();
            m_valentineType = _valentineType;
            m_mute = _mute;
            m_timeStamp = Environment.TickCount;
            buildPacket();
        }

        protected override void buildPacket()
        {
            base.buildPacket();
            packetIdentifier = PacketId.reqSetSavvyUnmuteEnable.ToByteValue();

            payloadData = new byte[1];
            if (m_mute)
            {
                payloadData[0] = 1;
            }
            else
            {
                payloadData[0] = 0;
            }
            payloadLength = 1;

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
