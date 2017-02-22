using System;
using VeeOne.ESP.Constants;
using VeeOne.ESP.Data;

namespace VeeOne.ESP.Packets.Response
{
	public class ResponseSweepSections : ESPPacket
    {
		public ResponseSweepSections(Devices _destination)
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
            SweepSection[] rc;

            SweepSection temp = new SweepSection();

            // Get the number of sections in the data coming from the V1.
            temp.BuildFromBytes(payloadData, 0);
            int v1Sections = temp.SweepDefinitionIndex.NumberOfSectionsAvailable;

            // Determine how many of the sweeps are valid
            int numValidSections = 0;

            for (int i = 0; i < v1Sections; i++)
            {
                temp.BuildFromBytes(payloadData, 5 * i);
                if (temp.LowerFrequencyEdgeInteger != 0 && temp.UpperFrequencyEdgeInteger!= 0)
                {
                    numValidSections++;
                }
            }

            // Allocate the array to hold only the valid sections		
            rc = new SweepSection[numValidSections];

            // Add the sections to the array
            int curIdx = 1;
            for (int i = 0; i < v1Sections; i++)
            {
                temp = new SweepSection();
                temp.BuildFromBytes(payloadData, 5 * i);
                if (temp.LowerFrequencyEdgeInteger != 0 && temp.UpperFrequencyEdgeInteger != 0)
                {
                    // Only use the sweep section if it is not zero.
                    // Use '<< 4' because the index is stored in the upper nibble of the index byte.
                    temp.SetSweepDefinition(curIdx << 4, numValidSections);
                    curIdx++;

                    rc[i] = temp;
                }
            }

            return rc;
        }
    }
}
