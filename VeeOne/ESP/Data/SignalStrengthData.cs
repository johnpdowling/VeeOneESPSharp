using System;
namespace VeeOne.Data
{
	public class SignalStrengthData
	{
		private bool m_b0;
		private bool m_b1;
		private bool m_b2;
		private bool m_b3;
		private bool m_b4;
		private bool m_b5;
		private bool m_b6;
		private bool m_b7;

		private byte m_rawData;

		public SignalStrengthData()
		{
			// ¯\_(ツ)_/¯
		}

		public SignalStrengthData(SignalStrengthData src)
		{
			m_b0 = src.m_b0;
			m_b1 = src.m_b1;
			m_b2 = src.m_b2;
			m_b3 = src.m_b3;
			m_b4 = src.m_b4;
			m_b5 = src.m_b5;
			m_b6 = src.m_b6;
			m_b7 = src.m_b7;

			m_rawData = src.m_rawData;
		}

		public bool IsEqual(SignalStrengthData src)
		{
			if (m_rawData != src.m_rawData)
			{
				return false;
			}
			if (m_b0 != src.m_b0) { return false; }
			if (m_b1 != src.m_b1) { return false; }
			if (m_b2 != src.m_b2) { return false; }
			if (m_b3 != src.m_b3) { return false; }
			if (m_b4 != src.m_b4) { return false; }
			if (m_b5 != src.m_b5) { return false; }
			if (m_b6 != src.m_b6) { return false; }
			if (m_b7 != src.m_b7) { return false; }

			return true;
		}

		public void Clear()
		{
			SetFromByte((byte)0x00);
		}

		public bool B0
		{
			get
			{
				return m_b0;
			}
		}

		public bool B1
		{
			get
			{
				return m_b1;
			}
		}

		public bool B2
		{
			get
			{
				return m_b2;
			}
		}

		public bool B3
		{
			get
			{
				return m_b3;
			}
		}

		public bool B4
		{
			get
			{
				return m_b4;
			}
		}

		public bool B5
		{
			get
			{
				return m_b5;
			}
		}

		public bool B6
		{
			get
			{
				return m_b6;
			}
		}

		public bool B7
		{
			get
			{
				return m_b7;
			}
		}

		public byte RawData
		{
			get
			{
				return m_rawData;
			}
		}

		public int NumberOfLEDs
		{
			get
			{
				if (m_rawData == 0x00)
				{
					return 0;
				}
				else if (m_rawData == 0x01)
				{
					return 1;
				}
				else if (m_rawData == 0x03)
				{
					return 2;
				}
				else if (m_rawData == 0x07)
				{
					return 3;
				}
				else if (m_rawData == 0x0F)
				{
					return 4;
				}
				else if (m_rawData == 0x1f)
				{
					return 5;
				}
				else if (m_rawData == 0x3f)
				{
					return 6;
				}
				else if (m_rawData == 0x7f)
				{
					return 7;
				}
				else
				{
					return 8;
				}
			}
		}

		public void SetFromByte(byte _data)
		{
			/*
			07 06 05 04 03 02 01 00
			|  |  |  |  |  |  |  |
			|  |  |  |  |  |  |  \-- b0 (left)
			|  |  |  |  |  |  \----- b1
			|  |  |  |  |  \-------- b2
			|  |  |  |  \----------- b3
			|  |  |  \-------------- b4
			|  |  \----------------- b5
			|  \-------------------- b6
			\----------------------- b7 (right)
			*/

			m_rawData = _data;

			if ((_data & 1) > 0)
			{
				m_b0 = true;
			}
			else
			{
				m_b0 = false;
			}

			if ((_data & 2) > 0)
			{
				m_b1 = true;
			}
			else
			{
				m_b1 = false;
			}

			if ((_data & 4) > 0)
			{
				m_b2 = true;
			}
			else
			{
				m_b2 = false;
			}

			if ((_data & 8) > 0)
			{
				m_b3 = true;
			}
			else
			{
				m_b3 = false;
			}

			if ((_data & 16) > 0)
			{
				m_b4 = true;
			}
			else
			{
				m_b4 = false;
			}

			if ((_data & 32) > 0)
			{
				m_b5 = true;
			}
			else
			{
				m_b5 = false;
			}

			if ((_data & 64) > 0)
			{
				m_b6 = true;
			}
			else
			{
				m_b6 = false;
			}

			if ((_data & 128) > 0)
			{
				m_b7 = true;
			}
			else
			{
				m_b7 = false;
			}
		}
	}
}
