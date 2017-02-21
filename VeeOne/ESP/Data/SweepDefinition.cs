using System;
using System.IO;

namespace VeeOne.ESP.Data
{
	public class SweepDefinition
	{
		private int m_index;
		private bool m_commit;

		private byte m_msbUpperFrequencyEdge;
		private byte m_lsbUpperFrequencyEdge;

		private Int32 m_upperFrequencyEdge;

		private byte m_msbLowerFrequencyEdge;
		private byte m_lsbLowerFrequencyEdge;

		private Int32 m_lowerFrequencyEdge;

		public int Index
		{
			get
			{
				return m_index;
			}
			set
			{
				m_index = value;
			}
		}

		public bool Commit
		{
			get
			{
				return m_commit;
			}
			set
			{
				m_commit = value;
			}
		}

		public Int32 UpperFrequencyEdge
		{
			get
			{
				return m_upperFrequencyEdge;
			}
			set
			{
				m_upperFrequencyEdge = value;
			}
		}

		public Int32 LowerFrequencyEdge
		{
			get
			{
				return m_lowerFrequencyEdge;
			}
			set
			{
				m_lowerFrequencyEdge = value;
			}
		}

		public void BuildFromBytes(byte[] _data)
		{
			/*
			Sweep Index byte definition
			07 06 05 04 03 02 01 00
			|  |  |  |  |  |  |  |
			|  |  |  |  |  |  |  \-- Index bit 0
			|  |  |  |  |  |  \----- Index bit 1
			|  |  |  |  |  \-------- Index bit 2
			|  |  |  |  \----------- Index bit 3
			|  |  |  \-------------- Index bit 4
			|  |  \----------------- Index bit 5
			|  \-------------------- Commit Changes (see Custom Sweeps)
			\----------------------- Reserved
			*/

			m_index = _data[0] & 0x3f;
			if ((_data[0] & 64) > 1)
			{
				m_commit = true;
			}
			else
			{
				m_commit = false;
			}

			m_msbUpperFrequencyEdge = _data[1];
			m_lsbUpperFrequencyEdge = _data[2];

			byte[] tempBytes = new byte[4];
			/*MemoryStream b = new MemoryStream(4);
			b.put((byte)0);
			b.put((byte)0);
			b.put(m_msbUpperFrequencyEdge);
			b.put(m_lsbUpperFrequencyEdge);
			*/
			tempBytes[0] = 0;
			tempBytes[1] = 0;
			tempBytes[2] = m_msbUpperFrequencyEdge;
			tempBytes[3] = m_lsbUpperFrequencyEdge;
			int temp = Convert.ToInt32(tempBytes); //b.getInt(0);

			/*
			NumberFormat nf = DecimalFormat.getInstance();

			nf.setMaximumIntegerDigits(2);
			nf.setMaximumFractionDigits(3);
			*/
			m_upperFrequencyEdge = temp;

			m_msbLowerFrequencyEdge = _data[3];
			m_lsbLowerFrequencyEdge = _data[4];

			/*b = ByteBuffer.allocate(4);
			b.put((byte)0);
			b.put((byte)0);
			b.put(m_msbLowerFrequencyEdge);
			b.put(m_lsbLowerFrequencyEdge);
			temp = b.getInt(0);
*/
			tempBytes[0] = 0;
			tempBytes[1] = 0;
			tempBytes[2] = m_msbLowerFrequencyEdge;
			tempBytes[3] = m_lsbLowerFrequencyEdge;
			temp = Convert.ToInt32(tempBytes); //b.getInt(0);

			m_lowerFrequencyEdge = temp;
		}

		public byte[] buildBytes()
		{
			/*
			Sweep Index byte definition
			07 06 05 04 03 02 01 00
			|  |  |  |  |  |  |  |
			|  |  |  |  |  |  |  \-- Index bit 0
			|  |  |  |  |  |  \----- Index bit 1
			|  |  |  |  |  \-------- Index bit 2
			|  |  |  |  \----------- Index bit 3
			|  |  |  \-------------- Index bit 4
			|  |  \----------------- Index bit 5
			|  \-------------------- Commit Changes (see Custom Sweeps)
			\----------------------- Reserved
			*/

			byte[] data = new byte[5];

			data[0] = (byte)(m_index & 0x3f);

			if (m_commit)
			{
				data[0] = (byte)(data[0] | 64);
			}

			data[0] = (byte)(data[0] | 0x80);

			/*ByteBuffer b = ByteBuffer.allocate(4);
			int temp = m_upperFrequencyEdge;
			b.putInt(temp);

			byte[] result = b.array();
			*/
			data[1] = (byte)((m_upperFrequencyEdge & 0xFF00) >> 8);// result[2];
			data[2] = (byte)(m_upperFrequencyEdge & 0x00FF);// result[3];
			/*
			b = ByteBuffer.allocate(4);
			temp = m_lowerFrequencyEdge;
			b.putInt(temp);

			result = b.array();
			*/

			data[3] = (byte)((m_lowerFrequencyEdge & 0xFF00) >> 8); //result[2];
			data[4] = (byte)(m_lowerFrequencyEdge & 0x00FF); // result[3];

			return data;
		}

		/*
		public SweepDefinition()
		{
		}
		*/
	}
}
