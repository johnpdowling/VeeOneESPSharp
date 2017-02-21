using System;
namespace VeeOne.Data
{
	public class ModeData
	{
		private bool m_usaMode;
		private bool m_euroMode;
		private bool m_allBogeysMode;
		private bool m_logicMode;
		private bool m_advLogicMode;
		private bool m_customSweeps;

		public ModeData()
		{
			m_usaMode = false;
			m_euroMode = false;
			m_allBogeysMode = false;
			m_logicMode = false;
			m_advLogicMode = false;
			m_customSweeps = false;
		}

		public bool UsaMode
		{
			get
			{
				return m_usaMode;
			}
			set
			{
				m_usaMode = value;
			}
		}

		public bool EuroMode
		{
			get
			{
				return m_euroMode;
			}
			set
			{
				m_euroMode = value;
			}
		}

		public bool AllBogeysMode
		{
			get
			{
				return m_allBogeysMode;
			}
			set
			{
				m_allBogeysMode = value;
			}
		}

		public bool LogicMode
		{
			get
			{
				return m_logicMode;
			}
			set
			{
				m_logicMode = value;
			}
		}

		public bool AdvLogicMode
		{
			get
			{
				return m_advLogicMode;
			}
			set
			{
				m_advLogicMode = value;
			}
		}

		public bool CustomSweeps
		{
			get
			{
				return m_customSweeps;
			}
			set
			{
				m_customSweeps = value;
			}
		}
	}
}
