using System;
using VeeOne.ESP.Constants;
using VeeOne.ESP.Data;

namespace VeeOne.ESP.Packets.Response
{
	public class ResponseBatteryVoltage : ESPPacket
	{
		public ResponseBatteryVoltage(Devices _destination)
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
            /*
		     0 Integer portion of the battery voltage
		     1 Payload Bytes Decimal portion of the battery voltage
		    */

            Byte integerPart = payloadData[0];
            Byte decimalPart = payloadData[1];
            System.Globalization.CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentCulture;
            
            String temp = integerPart.ToString() + ci.NumberFormat.NumberDecimalSeparator;

            if (decimalPart < 10)
            {
                temp += "0";
            }

            temp += decimalPart.ToString();

            return float.Parse(temp);
        }
    }
}
