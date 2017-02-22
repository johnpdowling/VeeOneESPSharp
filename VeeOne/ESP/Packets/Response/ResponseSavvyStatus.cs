using System;
using VeeOne.ESP.Constants;
using VeeOne.ESP.Data;

namespace VeeOne.ESP.Packets.Response
{
	public class ResponseSavvyStatus : ESPPacket
    {
		public ResponseSavvyStatus(Devices _destination)
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
            SavvyStatus rc = new SavvyStatus();
            rc.BuildFromBytes(payloadData);

            return rc;
        }
    }
}
