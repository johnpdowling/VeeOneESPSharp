using System;
using VeeOne.ESP.Constants;
using VeeOne.ESP.Data;

namespace VeeOne.ESP.Packets.Request
{
	public class RequestWriteSweepDefinition : ESPPacket
	{
        SweepDefinition m_sweep;

		public RequestWriteSweepDefinition(SweepDefinition _sweep, Devices _destination)
        {
            m_destination = _destination.ToByteValue();
            m_valentineType = _destination;
            m_sweep = _sweep;
            m_timeStamp = Environment.TickCount;
            buildPacket();
        }

        protected override void buildPacket()
        {
            base.buildPacket();
            packetIdentifier = PacketId.reqWriteSweepDefinition.ToByteValue();

            if (m_sweep != null)
            {
                payloadData = m_sweep.buildBytes();
            }
            else
            {
                payloadData = new byte[5];
            }
            payloadLength = 5;

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
