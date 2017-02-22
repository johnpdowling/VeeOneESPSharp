using System;
using VeeOne.ESP.Constants;

namespace VeeOne.ESP.Packets
{
	public class UnknownPacket : ESPPacket
	{
        public UnknownPacket(Devices _destination)
        {
            m_destination = _destination.ToByteValue();
            m_timeStamp = Environment.TickCount;// Environment.TickCount;
        }

        protected override void buildPacket()
        {
            // ¯\_(ツ)_ /¯
        }

        public override object getResponseData()
        {
            return null;
        }
    }
}
