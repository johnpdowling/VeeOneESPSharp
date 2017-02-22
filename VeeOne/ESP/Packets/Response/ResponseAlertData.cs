using System;
using VeeOne.ESP.Constants;
using VeeOne.ESP.Data;

namespace VeeOne.ESP.Packets.Response
{
	public class ResponseAlertData : ESPPacket
	{
		public ResponseAlertData(Devices _destination)
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
            AlertData rc = new AlertData();
            rc.BuildFromData(payloadData);

            return rc;
        }
    }
}
