using System;
namespace VeeOne.Data
{
	public class InfDisplayInfoData
	{
		private BogeyCounterData m_bogeyCounterData1;
		private BogeyCounterData m_bogeyCounterData2;
		private SignalStrengthData m_signalStrengthData;
		private BandAndArrowIndicatorData m_bandAndArrowIndicatorData1;
		private BandAndArrowIndicatorData m_bandAndArrowIndicatorData2;
		private AuxiliaryData m_auxData;
		private byte m_auxData1;
		private byte m_auxData2;

		public InfDisplayInfoData()
		{
			// ¯\_(ツ)_/¯
		}

		public InfDisplayInfoData(InfDisplayInfoData src)
		{
			m_bogeyCounterData1 = new BogeyCounterData(src.m_bogeyCounterData1);
			m_bogeyCounterData2 = new BogeyCounterData(src.m_bogeyCounterData2);
			m_signalStrengthData = new SignalStrengthData(src.m_signalStrengthData);
			m_bandAndArrowIndicatorData1 = new BandAndArrowIndicatorData(src.m_bandAndArrowIndicatorData1);
			m_bandAndArrowIndicatorData2 = new BandAndArrowIndicatorData(src.m_bandAndArrowIndicatorData2);
			m_auxData = new AuxiliaryData(src.m_auxData);
			m_auxData1 = src.m_auxData1;
			m_auxData2 = src.m_auxData2;
		}

		public void Clear()
		{
			if (m_bogeyCounterData1 == null)
			{
				// Create the objects
				NoDataInit();
			}

			m_bogeyCounterData1.Clear();
			m_bogeyCounterData2.Clear();
			m_signalStrengthData.Clear();
			m_bandAndArrowIndicatorData1.Clear();
			m_bandAndArrowIndicatorData2.Clear();
			m_auxData.Clear();
			m_auxData1 = 0x00;
			m_auxData2 = 0x00;
		}

		public BogeyCounterData BogeyCounterData1
		{
			get
			{
				return m_bogeyCounterData1;
			}
			set
			{
				m_bogeyCounterData1 = value;
			}
		}

		public BogeyCounterData BogeyCounterData2
		{
			get
			{
				return m_bogeyCounterData2;
			}
			set
			{
				m_bogeyCounterData2 = value;
			}
		}

		public SignalStrengthData SignalStrengthData
		{
			get
			{
				return m_signalStrengthData;
			}
			set
			{
				m_signalStrengthData = value;
			}
		}

		public BandAndArrowIndicatorData BandAndArrowIndicator1
		{
			get
			{
				return m_bandAndArrowIndicatorData1;
			}
			set
			{
				m_bandAndArrowIndicatorData1 = value;
			}
		}

		public BandAndArrowIndicatorData BandAndArrowIndicator2
		{
			get
			{
				return m_bandAndArrowIndicatorData2;
			}
			set
			{
				m_bandAndArrowIndicatorData2 = value;
			}
		}

		public AuxiliaryData AuxData
		{
			get
			{
				return m_auxData;
			}
			set
			{
				m_auxData = value;
			}
		}

		public byte Aux1Data
		{
			get
			{
				return m_auxData1;
			}
			set
			{
				m_auxData1 = value;
			}
		}

		public byte Aux2Data
		{
			get
			{
				return m_auxData2;
			}
			set
			{
				m_auxData2 = value;
			}
		}

		public bool IsActiveAlerts()
		{
			return BandAndArrowIndicator1.Front || BandAndArrowIndicator1.Side || BandAndArrowIndicator1.Rear;
		}

		public ModeData getMode(ModeData _previousModeData)
		{
			ModeData rc = new ModeData();

			byte data = m_bogeyCounterData1.RawDataWithOutDecimalPoint;

			switch (data)
			{
				case (byte)0x77:
					rc.UsaMode = true;
					rc.AllBogeysMode = true;
					break;
				case 0x18:
					rc.UsaMode = true;
					rc.LogicMode = true;
					break;
				case 0x38:
					rc.UsaMode = true;
					rc.AdvLogicMode = true;
					break;
				case 0x39:
				case 0x3E:
					rc.EuroMode = true;
					rc.AllBogeysMode = true;
					break;
				case 0x1C:
				case 0x58:
					rc.EuroMode = true;
					rc.LogicMode = true;
					break;
				default:
					rc.EuroMode = m_auxData.EuroMode;
					rc.CustomSweeps = m_auxData.CustomSweep;
					break;
			}

			if (rc.EuroMode)
			{
				rc.CustomSweeps = m_auxData.CustomSweep;
			}

			return rc;
		}

		public void NoDataInit()
		{
			m_bogeyCounterData1 = new BogeyCounterData();
			m_bogeyCounterData2 = new BogeyCounterData();
			m_signalStrengthData = new SignalStrengthData();
			m_bandAndArrowIndicatorData1 = new BandAndArrowIndicatorData();
			m_bandAndArrowIndicatorData2 = new BandAndArrowIndicatorData();
			m_auxData = new AuxiliaryData();
		}

		public bool IsEqual(InfDisplayInfoData src)
		{

			if (m_auxData1 != src.m_auxData1)
			{
				return false;
			}

			if (m_auxData2 != src.m_auxData2)
			{
				return false;
			}

			if (!m_bogeyCounterData1.IsEqual(src.m_bogeyCounterData1))
			{
				return false;
			}

			if (!m_bogeyCounterData2.IsEqual(src.m_bogeyCounterData2))
			{
				return false;
			}

			if (!m_signalStrengthData.IsEqual(src.m_signalStrengthData))
			{
				return false;
			}

			if (!m_bandAndArrowIndicatorData1.IsEqual(src.m_bandAndArrowIndicatorData1))
			{
				return false;
			}

			if (!m_bandAndArrowIndicatorData2.IsEqual(src.m_bandAndArrowIndicatorData2))
			{
				return false;
			}

			if (!m_auxData.IsEqual(src.m_auxData))
			{
				return false;
			}

			return true;
		}

	}
}
