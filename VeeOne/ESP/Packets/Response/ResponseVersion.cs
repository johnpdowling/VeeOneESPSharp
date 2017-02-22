using System;
using VeeOne.ESP.Constants;
using VeeOne.ESP.Data;

namespace VeeOne.ESP.Packets.Response
{
	public class ResponseVersion : ESPPacket
    {
		public ResponseVersion(Devices _destination)
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
            if (PacketIdLookup.getConstant(packetIdentifier) != PacketId.respVersion)
            {
                return null;
            }

            /*
             0 The version identification letter for the responding device
                ‘V’ for Valentine One
                ‘C’ for Concealed Display
                ‘R’ for Remote Audio
                ‘S’ for Savvy
             1 ASCII value of the major version number.
             2 Decimal point (‘.’)
             3 ASCII value of the minor version number.
             4 ASCII value of the first digit of the revision number.
             5 ASCII value of the second digit of the revision number.
             6 ASCII value of the Engineering Control Number.
            */

            String rc = "";

            int length = payloadLength;

            for (int i = 0; i < length; i++)
            {
                char temp = (char)payloadData[i];
                rc = rc + temp;
            }

            return rc;
        }
    }
}
