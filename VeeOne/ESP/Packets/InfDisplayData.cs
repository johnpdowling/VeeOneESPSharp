using System;
using VeeOne.ESP.Constants;
using VeeOne.ESP.Data;

namespace VeeOne.ESP.Packets
{
	public class InfDisplayData : ESPPacket
	{
        public InfDisplayData(Devices _destination)
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
            InfDisplayInfoData rc = new InfDisplayInfoData();
            BogeyCounterData bogey1 = new BogeyCounterData();
            BogeyCounterData bogey2 = new BogeyCounterData();
            SignalStrengthData signal = new SignalStrengthData();
            BandAndArrowIndicatorData bandAndArrow1 = new BandAndArrowIndicatorData();
            BandAndArrowIndicatorData bandAndArrow2 = new BandAndArrowIndicatorData();
            AuxiliaryData auxData = new AuxiliaryData();

            bogey1.SetFromByte(payloadData[0]);
            bogey2.SetFromByte(payloadData[1]);
            signal.SetFromByte(payloadData[2]);
            bandAndArrow1.SetFromByte(payloadData[3]);
            bandAndArrow2.SetFromByte(payloadData[4]);
            auxData.SetFromByte(payloadData[5]);

            rc.Aux1Data = payloadData[6];
            rc.Aux2Data = payloadData[7];

            rc.BogeyCounterData1 = bogey1;
            rc.BogeyCounterData2 = bogey2;
            rc.SignalStrengthData = signal;
            rc.BandAndArrowIndicator1 = bandAndArrow1;
            rc.BandAndArrowIndicator2 = bandAndArrow2;
            rc.AuxData = auxData;

            return rc;
        }
    }
}
