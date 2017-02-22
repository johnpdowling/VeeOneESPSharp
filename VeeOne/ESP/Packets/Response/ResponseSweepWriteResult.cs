using System;
using VeeOne.ESP.Constants;
using VeeOne.ESP.Data;

namespace VeeOne.ESP.Packets.Response
{
	public class ResponseSweepWriteResult : ESPPacket
    {
		public ResponseSweepWriteResult(Devices _destination)
        {
            m_destination = _destination.ToByteValue();
            m_timeStamp = Environment.TickCount;
            buildPacket();
        }

        protected override void buildPacket()
        {

        }

        public override object getResponseData()
        {
            SweepWriteResult rc = new SweepWriteResult();

            rc.BuildFromByte(payloadData[0]);

            return rc;
        }
    }
}
