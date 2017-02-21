using System;
namespace VeeOne.Data
{
	public class SweepSection
	{
		private SweepDefinitionIndex m_sweepDefinition;

		private int m_upperFrequencyEdgeInt;
		private int m_lowerFrequencyEdgeInt;

		public SweepSection()
		{
			//
		}

		public SweepSection(int lowerFrequencyEdgeInt, int upperFrequencyEdgeInt)
		{
			m_upperFrequencyEdgeInt = upperFrequencyEdgeInt;
			m_lowerFrequencyEdgeInt = lowerFrequencyEdgeInt;
		}

		public SweepDefinitionIndex SweepDefinitionIndex
		{
			get
			{
				return m_sweepDefinition;
			}
		}

		public int UpperFrequencyEdgeInt
		{
			get
			{
				return m_upperFrequencyEdgeInt;
			}
			set
			{
				m_upperFrequencyEdgeInt = value;
			}
		}

		public int LowerFrequencyEdgeInt
		{
			get
			{
				return m_lowerFrequencyEdgeInt;
			}
			set
			{
				m_upperFrequencyEdgeInt = value;
			}
		}

		public void SetSweepDefinition(int index, int numSections)
		{
			m_sweepDefinition.CurrentSweepIndex = index;
			m_sweepDefinition.NumberOfSectionsAvailable = numSections;
		}

		public void BuildFromBytes(byte[] _data, int _startIndex)
		{
			m_sweepDefinition = new SweepDefinitionIndex();
			m_sweepDefinition.BuildFromByte(_data[_startIndex]);

			byte msbUpperFrequencyEdge = _data[_startIndex + 1];
			byte lsbUpperFrequencyEdge = _data[_startIndex + 2];

			byte[] tempBytes = new byte[4];
			/*
			ByteBuffer b = ByteBuffer.allocate(4);
			b.put((byte)0);
			b.put((byte)0);
			b.put(msbUpperFrequencyEdge);
			b.put(lsbUpperFrequencyEdge);
			int temp = b.getInt(0);
			*/
			tempBytes[0] = 0;
			tempBytes[1] = 0;
			tempBytes[2] = msbUpperFrequencyEdge;
			tempBytes[3] = lsbUpperFrequencyEdge;
			int temp = Convert.ToInt32(tempBytes);
			//m_upperFrequencyEdge = temp / 1000.0f;

			m_upperFrequencyEdgeInt = temp;

			byte msbLowerFrequencyEdge = _data[_startIndex + 3];
			byte lsbLowerFrequencyEdge = _data[_startIndex + 4];

			/*b = ByteBuffer.allocate(4);
			b.put((byte)0);
			b.put((byte)0);
			b.put(msbLowerFrequencyEdge);
			b.put(lsbLowerFrequencyEdge);
			temp = b.getInt(0);*/
			tempBytes[0] = 0;
			tempBytes[1] = 0;
			tempBytes[2] = msbLowerFrequencyEdge;
			tempBytes[3] = lsbLowerFrequencyEdge;
			temp = Convert.ToInt32(tempBytes);

			//m_lowerFrequencyEdge = temp / 1000.0f;

			m_lowerFrequencyEdgeInt = temp;
		}
	}
}
