using System;
namespace VeeOne.ESP.Data
{
	public class AlertIndexAndCount
	{
		int m_count;
		int m_index;

		public int Count
		{
			get
			{
				return m_count;
			}
		}

		public int Index
		{
			get
			{
				return m_index;
			}
		}

		public void BuildFromByte(byte _data)
		{
			/*
			 07 06 05 04 03 02 01 00
			 |  |  |  |  |  |  |  |
			 |  |  |  |  |  |  |  \-- Count B0 (Number of alerts present)
			 |  |  |  |  |  |  \----- Count B1
			 |  |  |  |  |  \-------- Count B2
			 |  |  |  |  \----------- Count B3
			 |  |  |  \-------------- Index B0 (Index of this alert)
			 |  |  \----------------- Index B1
			 |  \-------------------- Index B2
			 \----------------------- Index B3
			*/

			m_count = _data & 0x0F;
			m_index = _data & 0xF0;
			m_index = m_index >> 4;
		}
		/*
		public AlertIndexAndCount()
		{
		}
		*/
	}
}
