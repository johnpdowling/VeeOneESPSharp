using System;
using System.Collections.Generic;
using VeeOne.ESP.Constants;

namespace VeeOne.ESP.Packets
{
	public class InfV1Busy : ESPPacket
	{
		public InfV1Busy(Devices _destination)
		{
            m_destination = _destination.ToByteValue();
            m_timeStamp = Environment.TickCount;
		}

        protected override void buildPacket()
        {
            // ¯\_(ツ)_ /¯
        }

        public override object getResponseData()
        {
            List<Int32> rc = new List<Int32>();

            for (int i = 0; i < payloadLength; i++)
            {
                rc.Add((int)payloadData[i]);
            }

            return rc;
        }
    }
}
