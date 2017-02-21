using System;
namespace VeeOne.ESP.Data
{
	public class SweepDefinitionIndex
	{
		private int m_numberOfSectionsAvailable;
		private int m_currentSweepIndex;

		public int NumberOfSectionsAvailable
		{
			get
			{
				return m_numberOfSectionsAvailable;
			}
			set
			{
				m_numberOfSectionsAvailable = value;
			}
		}

		public int CurrentSweepIndex
		{
			get
			{
				return m_currentSweepIndex;
			}
			set
			{
				m_currentSweepIndex = value;
			}
		}

		public void BuildFromByte(byte _data)
		{
			byte temp;

			temp = (byte)(_data & 0x0F);

			m_numberOfSectionsAvailable = temp;

			temp = (byte)(_data & 0xF0);

			m_currentSweepIndex = temp;
		}

		/*
		public SweepDefinitionIndex()
		{
		}
		*/
	}
}
