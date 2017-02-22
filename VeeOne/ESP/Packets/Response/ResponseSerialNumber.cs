using System;
using VeeOne.ESP.Constants;
using VeeOne.ESP.Data;

namespace VeeOne.ESP.Packets.Response
{
	public class ResponseSerialNumber : ESPPacket
    {
		public ResponseSerialNumber(Devices _destination)
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
            if (PacketIdLookup.getConstant(packetIdentifier) != PacketId.respSerialNumber)
            {
                return null;
            }

            /*
            0 The first character of the serial number string, in ASCII.
            1 The second character of the serial number string, in ASCII
            2 The third character of the serial number string, in ASCII
            3 The fourth character of the serial number string, in ASCII
            4 The fifth character of the serial number string, in ASCII
            5 The sixth character of the serial number string, in ASCII
            6 The seventh character of the serial number string, in ASCII
            7 The eighth character of the serial number string, in ASCII
            8 The ninth character of the serial number string, in ASCII
            9 The tenth character of the serial number string, in ASCII
            */

            String rc = "";
            // This is the serial number length per ESP Specification version 3.002
            int snLength = 10;

            if (snLength > payloadLength)
            {
                // This must be an different format so just use the payload data
                snLength = payloadLength;
            }

            for (int i = 0; i < snLength; i++)
            {
                char temp = (char)payloadData[i];
                if (temp == 0)
                {
                    break;
                }
                rc = rc + temp;

            }

            return rc;
        }
    }
}
