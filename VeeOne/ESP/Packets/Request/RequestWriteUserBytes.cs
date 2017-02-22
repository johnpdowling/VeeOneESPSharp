using System;
using VeeOne.ESP.Constants;
using VeeOne.ESP.Data;

namespace VeeOne.ESP.Packets.Request
{
	public class RequestWriteUserBytes : ESPPacket
	{
        UserSettings m_settings;

		public RequestWriteUserBytes(UserSettings _settings, Devices _destination)
        {
            m_destination = _destination.ToByteValue();
            m_valentineType = _destination;
            m_settings = _settings;
            m_timeStamp = Environment.TickCount;
            buildPacket();
        }

        protected override void buildPacket()
        {
            base.buildPacket();
            packetIdentifier = PacketId.reqWriteUserBytes.ToByteValue();

            if (m_settings != null)
            {
                payloadData = m_settings.buildBytes();
            }
            else
            {
                payloadData = new byte[6];
            }
            payloadLength = 6;

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
