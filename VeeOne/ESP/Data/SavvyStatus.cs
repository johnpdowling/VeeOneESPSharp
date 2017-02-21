using System;
namespace VeeOne.ESP.Data
{
	public class SavvyStatus
	{
		private int m_speedThreshold;
		private bool m_thresholdOverriddenByUser;
		private bool m_unmuteEnabled;

		public int SpeedThreshold
		{
			get
			{
				return m_speedThreshold;
			}
		}

		public bool ThresholdOverriddenByUser
		{
			get
			{
				return m_thresholdOverriddenByUser;
			}
		}

		public bool UnmuteEnabled
		{
			get
			{
				return m_unmuteEnabled;
			}
		}

		public void BuildFromBytes(byte[] _data)
		{
			m_speedThreshold = (int)_data[0] & 0xff;

			if ((_data[1] & 1) > 0)
			{
				m_thresholdOverriddenByUser = true;
			}
			else
			{
				m_thresholdOverriddenByUser = false;

			}

			if ((_data[1] & 2) > 0)
			{
				m_unmuteEnabled = true;
			}
			else
			{
				m_unmuteEnabled = false;

			}
		}

		/*
		public SavvyStatus()
		{
		}
		*/
	}
}
