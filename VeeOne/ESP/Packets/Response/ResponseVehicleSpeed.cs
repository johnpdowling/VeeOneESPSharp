﻿using System;
using VeeOne.ESP.Constants;
using VeeOne.ESP.Data;

namespace VeeOne.ESP.Packets.Response
{
	public class ResponseVehicleSpeed : ESPPacket
    {
		public ResponseVehicleSpeed(Devices _destination)
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
            Int32 rc;

            rc = (int)payloadData[0];

            return rc;
        }
    }
}
