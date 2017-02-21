using System;
namespace VeeOne.ESP.Data
{
	public class SweepWriteResult
	{
		private bool m_success;
		private int m_errorIndex;

		public bool Success
		{
			get
			{
				return m_success;
			}
		}

		public int ErrorIndex
		{
			get
			{
				return m_errorIndex;
			}
		}

		public void BuildFromByte(byte _data)
		{
			if (_data == 0)
			{
				m_success = true;
				m_errorIndex = -1;
			}
			else
			{
				m_success = false;
				m_errorIndex = _data;
			}
		}

		/*
		public SweepWriteResult()
		{
		}
		*/
	}
}
