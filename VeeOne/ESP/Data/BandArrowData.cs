using System;
namespace VeeOne.Data
{
	public class BandArrowData
	{
		private bool m_laser;
		private bool m_kaBand;
		private bool m_kBand;
		private bool m_xBand;
		private bool m_kuBand;

		private bool m_front;
		private bool m_side;
		private bool m_rear;

		public bool Laser
		{
			get
			{
				return m_laser;
			}
		}

		public bool KaBand
		{
			get
			{
				return m_kaBand;
			}
		}

		public bool KBand
		{
			get
			{
				return m_kBand;
			}
		}

		public bool XBand
		{
			get
			{
				return m_xBand;
			}
		}

		public bool KuBand
		{
			get
			{
				return m_kuBand;
			}
		}

		public bool Front
		{
			get
			{
				return m_front;
			}
		}

		public bool Side
		{
			get
			{
				return m_side;
			}
		}

		public bool Rear
		{
			get
			{
				return m_rear;
			}
		}

		public void BuildFromByte(byte _data)
		{
			/*	
			 Band/Arrow definition
			 07 06 05 04 03 02 01 00
			 |  |  |  |  |  |  |  |
			 |  |  |  |  |  |  |  \-- LASER
			 |  |  |  |  |  |  \----- Ka BAND
			 |  |  |  |  |  \-------- K BAND
			 |  |  |  |  \----------- X BAND
			 |  |  |  \-------------- Ku Band
			 |  |  \----------------- FRONT
			 |  \-------------------- SIDE
			 \----------------------- REAR
			*/

			if ((_data & 1) > 0)
			{
				m_laser = true;
			}
			else
			{
				m_laser = false;
			}

			if ((_data & 2) > 0)
			{
				m_kaBand = true;
			}
			else
			{
				m_kaBand = false;
			}

			if ((_data & 4) > 0)
			{
				m_kBand = true;
			}
			else
			{
				m_kBand = false;
			}

			if ((_data & 8) > 0)
			{
				m_xBand = true;
			}
			else
			{
				m_xBand = false;
			}

			if ((_data & 16) > 0)
			{
				m_kuBand = true;
			}
			else
			{
				m_kuBand = false;
			}

			if ((_data & 32) > 0)
			{
				m_front = true;
			}
			else
			{
				m_front = false;
			}

			if ((_data & 64) > 0)
			{
				m_side = true;
			}
			else
			{
				m_side = false;
			}

			if ((_data & 128) > 0)
			{
				m_rear = true;
			}
			else
			{
				m_rear = false;
			}
		}

		/*
		public BandArrowData()
		{
		}
		*/
	}
}
