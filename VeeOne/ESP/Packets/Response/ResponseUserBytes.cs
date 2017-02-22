using System;
using VeeOne.ESP.Constants;
using VeeOne.ESP.Data;

namespace VeeOne.ESP.Packets.Response
{
	public class ResponseUserBytes : ESPPacket
    {
		public ResponseUserBytes(Devices _destination)
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
            UserSettings rc = new UserSettings();
            rc.BuildFromBytes(payloadData);
            return rc;
        }
    }
}
