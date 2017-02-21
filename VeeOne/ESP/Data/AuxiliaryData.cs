using System;
namespace VeeOne.ESP.Data
{
	public class AuxiliaryData
	{
		private bool m_soft;
		private bool m_TSHoldOff;
		private bool m_sysStatus;
		private bool m_displayOn;
		private bool m_euroMode;
		private bool m_customSweep;
		private bool m_legacy;
		private bool m_reserved2;

		public AuxiliaryData()
		{
			// ¯\_(ツ)_/¯
		}

		public AuxiliaryData(AuxiliaryData src)
		{
			m_soft = src.m_soft;
			m_TSHoldOff = src.m_TSHoldOff;
			m_sysStatus = src.m_sysStatus;
			m_displayOn = src.m_displayOn;
			m_euroMode = src.m_euroMode;
			m_customSweep = src.m_customSweep;
			m_legacy = src.m_legacy;
			m_reserved2 = src.m_reserved2;
		}

		public void Clear()
		{
			SetFromByte((byte)0x00);
		}

		public bool IsEqual(AuxiliaryData src)
		{
			if (m_soft != src.m_soft) { return false; }
			if (m_TSHoldOff != src.m_TSHoldOff) { return false; }
			if (m_sysStatus != src.m_sysStatus) { return false; }
			if (m_displayOn != src.m_displayOn) { return false; }
			if (m_euroMode != src.m_euroMode) { return false; }
			if (m_customSweep != src.m_customSweep) { return false; }
			if (m_legacy != src.m_legacy) { return false; }
			if (m_reserved2 != src.m_reserved2) { return false; }

			return true;
		}

		public bool Soft
		{
			get
			{
				return m_soft;
			}
		}

		public bool TSHoldOff
		{
			get
			{
				return m_TSHoldOff;
			}
		}

		public bool SysStatus
		{
			get
			{
				return m_sysStatus;
			}
		}

		public bool DisplayOn
		{
			get
			{
				return m_displayOn;
			}
		}

		public bool EuroMode
		{
			get
			{
				return m_euroMode;
			}
		}

		public bool CustomSweep
		{
			get
			{
				return m_customSweep;
			}
		}

		public bool Legacy
		{
			get
			{
				return m_legacy;
			}
		}

		public bool Reserved2
		{
			get
			{
				return m_reserved2;
			}
		}

		public void SetFromByte(byte _data)
		{
			/*
			07 06 05 04 03 02 01 00
			|  |  |  |  |  |  |  |
			|  |  |  |  |  |  |  \-- Soft
			|  |  |  |  |  |  \----- TS Holdoff
			|  |  |  |  |  \-------- Sys. Status
			|  |  |  |  \----------- Display On
			|  |  |  \-------------- Euro Mode
			|  |  \----------------- Custom Sweep
			|  \-------------------- ESP/Legacy
			\----------------------- Reserved
			*/

			if ((_data & 1) > 0)
			{
				m_soft = true;
			}
			else
			{
				m_soft = false;
			}

			if ((_data & 2) > 0)
			{
				m_TSHoldOff = true;
			}
			else
			{
				m_TSHoldOff = false;
			}

			if ((_data & 4) > 0)
			{
				m_sysStatus = true;
			}
			else
			{
				m_sysStatus = false;
			}

			if ((_data & 8) > 0)
			{
				m_displayOn = true;
			}
			else
			{
				m_displayOn = false;
			}

			if ((_data & 16) > 0)
			{
				m_euroMode = true;
			}
			else
			{
				m_euroMode = false;
			}

			if ((_data & 32) > 0)
			{
				m_customSweep = true;
			}
			else
			{
				m_customSweep = false;
			}

			if ((_data & 64) > 0)
			{
				m_legacy = true;
			}
			else
			{
				m_legacy = false;
			}

			if ((_data & 128) > 0)
			{
				m_reserved2 = true;
			}
			else
			{
				m_reserved2 = false;
			}
		}

	}
}
