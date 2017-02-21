using System;
namespace VeeOne.ESP.Data
{
	public class BogeyCounterData
	{
		private byte m_rawData;
		private byte m_rawDataWithOutDp;
		private bool m_segA;
		private bool m_segB;
		private bool m_segC;
		private bool m_segD;
		private bool m_segE;
		private bool m_segF;
		private bool m_segG;
		private bool m_dp;

		public BogeyCounterData()
		{
			// ¯\_(ツ)_/¯
		}

		public BogeyCounterData(BogeyCounterData src)
		{
			m_rawData = src.m_rawData;
			m_rawDataWithOutDp = src.m_rawDataWithOutDp;

			m_segA = src.m_segA;
			m_segB = src.m_segB;
			m_segC = src.m_segC;
			m_segD = src.m_segD;
			m_segE = src.m_segE;
			m_segF = src.m_segF;
			m_segG = src.m_segG;
			m_dp = src.m_dp;
		}

		public bool IsEqual(BogeyCounterData src)
		{
			if (m_rawData != src.m_rawData)
			{
				return false;
			}
			if (m_rawDataWithOutDp != src.m_rawDataWithOutDp)
			{
				return false;
			}
			if (m_segA != src.m_segA)
			{
				return false;
			}
			if (m_segB != src.m_segB)
			{
				return false;
			}
			if (m_segC != src.m_segC)
			{
				return false;
			}
			if (m_segD != src.m_segD)
			{
				return false;
			}
			if (m_segE != src.m_segE)
			{
				return false;
			}
			if (m_segF != src.m_segF)
			{
				return false;
			}
			if (m_segG != src.m_segG)
			{
				return false;
			}
			if (m_dp != src.m_dp)
			{
				return false;
			}

			return true;
		}

		public void Clear()
		{
			SetFromByte((byte)0x00);
		}

		public bool SegA
		{
			get
			{
				return m_segA;
			}
		}

		public bool SegB
		{
			get
			{
				return m_segB;
			}
		}
		public bool SegC
		{
			get
			{
				return m_segC;
			}
		}

		public bool SegD
		{
			get
			{
				return m_segD;
			}
		}

		public bool SegE
		{
			get
			{
				return m_segE;
			}
		}

		public bool SegF
		{
			get
			{
				return m_segF;
			}
		}

		public bool SegG
		{
			get
			{
				return m_segG;
			}
		}

		public bool Dp
		{
			get
			{
				return m_dp;
			}
		}

		public byte RawData
		{
			get
			{
				return m_rawData;
			}
		}

		public byte RawDataWithOutDecimalPoint
		{
			get
			{
				return m_rawDataWithOutDp;
			}
		}

		public void SetFromByte(byte _data)
		{
			/*
			07 06 05 04 03 02 01 00
			|  |  |  |  |  |  |  |
			|  |  |  |  |  |  |  \-- Seg a
			|  |  |  |  |  |  \----- Seg b
			|  |  |  |  |  \-------- Seg c
			|  |  |  |  \----------- Seg d
			|  |  |  \-------------- Seg e
			|  |  \----------------- Seg f
			|  \-------------------- Seg g
			\----------------------- dp
			*/

			m_rawData = _data;
			m_rawDataWithOutDp = (byte)(_data & 0x7f);

			if ((_data & 1) > 0)
			{
				m_segA = true;
			}
			else
			{
				m_segA = false;
			}

			if ((_data & 2) > 0)
			{
				m_segB = true;
			}
			else
			{
				m_segB = false;
			}

			if ((_data & 4) > 0)
			{
				m_segC = true;
			}
			else
			{
				m_segC = false;
			}

			if ((_data & 8) > 0)
			{
				m_segD = true;
			}
			else
			{
				m_segD = false;
			}

			if ((_data & 16) > 0)
			{
				m_segE = true;
			}
			else
			{
				m_segE = false;
			}

			if ((_data & 32) > 0)
			{
				m_segF = true;
			}
			else
			{
				m_segF = false;
			}

			if ((_data & 64) > 0)
			{
				m_segG = true;
			}
			else
			{
				m_segG = false;
			}

			if ((_data & 128) > 0)
			{
				m_dp = true;
			}
			else
			{
				m_dp = false;
			}
		}

		public String ConvertToLetter()
		{
			String rc = "?";

			if (SegA && SegB && SegC && SegD && SegE && SegF && !SegG)
			{
				rc = "0";
			}
			else if (!SegA && SegB && SegC && !SegD && !SegE && !SegF && !SegG)
			{
				rc = "1";
			}
			else if (SegA && SegB && !SegC && SegD && SegE && !SegF && SegG)
			{
				rc = "2";
			}
			else if (SegA && SegB && SegC && SegD && !SegE && !SegF && SegG)
			{
				rc = "3";
			}
			else if (!SegA && SegB && SegC && !SegD && !SegE && SegF && SegG)
			{
				rc = "4";
			}
			else if (SegA && !SegB && SegC && SegD && !SegE && SegF && SegG)
			{
				rc = "5";
			}
			else if (SegA && !SegB && SegC && SegD && SegE && SegF && SegG)
			{
				rc = "6";
			}
			else if (SegA && SegB && SegC && !SegD && !SegE && !SegF && !SegG)
			{
				rc = "7";
			}
			else if (SegA && SegB && SegC && SegD && SegE && SegF && SegG)
			{
				rc = "8";
			}
			else if (SegA && SegB && SegC && SegD && !SegE && SegF && SegG)
			{
				rc = "9";
			}
			else if (SegA && SegB && SegC && !SegD && SegE && SegF && SegG)
			{
				rc = "A";
			}
			else if (!SegA && !SegB && SegC && SegD && SegE && SegF && SegG)
			{
				rc = "b";
			}
			else if (SegA && !SegB && !SegC && SegD && SegE && SegF && !SegG)
			{
				rc = "C";
			}
			else if (!SegA && SegB && SegC && SegD && SegE && !SegF && SegG)
			{
				rc = "d";
			}
			else if (SegA && !SegB && !SegC && SegD && SegE && SegF && SegG)
			{
				rc = "E";
			}
			else if (SegA && !SegB && !SegC && !SegD && SegE && SegF && SegG)
			{
				rc = "F";
			}

			else if (SegA && !SegB && !SegC && SegD && !SegE && !SegF && SegG)
			{
				rc = "#";
			}
			else if (!SegA && !SegB && !SegC && SegD && SegE && !SegF && !SegG)
			{
				rc = "&";
			}
			else if (!SegA && !SegB && !SegC && SegD && SegE && SegF && !SegG)
			{
				rc = "L";
			}
			else if (!SegA && SegB && SegC && SegD && SegE && !SegF && !SegG)
			{
				rc = "J";
			}

			else if (SegA && !SegB && !SegC && SegD && SegE && SegF && !SegG)
			{
				rc = "C";
			}
			else if (!SegA && !SegB && !SegC && SegD && SegE && !SegF && SegG)
			{
				rc = "c";
			}
			else if (!SegA && SegB && SegC && SegD && SegE && SegF && !SegG)
			{
				rc = "U";
			}
			else if (!SegA && !SegB && SegC && SegD && SegE && !SegF && !SegG)
			{
				rc = "u";
			}

			else if (!SegA && !SegB && !SegC && !SegD && !SegE && !SegF && !SegG)
			{
				rc = " ";
			}

			if (Dp)
			{
				rc += ".";
			}

			return rc;
		}

	}
}
