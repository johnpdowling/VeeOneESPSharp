using System;
using System.Collections.Generic;
using System.Linq;

namespace VeeOne.Data
{
	public class AlertData
	{
		public class SignalDirection
		{
			public static readonly SignalDirection ORIENTATION_FRONT = new SignalDirection(0);
			public static readonly SignalDirection ORIENTATION_SIDE = new SignalDirection(1);
			public static readonly SignalDirection ORIENTATION_REAR = new SignalDirection(2);
			public static readonly SignalDirection ORIENTATION_INVALID = new SignalDirection(3);

			public static IEnumerable<SignalDirection> Values
			{
				get
				{
					yield return ORIENTATION_FRONT;
					yield return ORIENTATION_SIDE;
					yield return ORIENTATION_REAR;
					yield return ORIENTATION_INVALID;
				}
			}

			int index;

			SignalDirection(int index)
			{
				this.index = index;
			}

			public int Value
			{
				get
				{
					return index;
				}
			}

			public static SignalDirection EnumForPos(int pos)
			{
				return Values.ElementAt(pos);
			}
		}

		private AlertIndexAndCount m_alertIndexAndCount;
		private byte m_frequencyMSB;
		private byte m_frequencyLSB;
		private int m_frequency;
		private int m_frontSignalStrength;
		private int m_rearSignalStrength;
		private byte m_frontSignalStrengthByte;
		private byte m_rearSignalStrengthByte;
		private BandArrowData m_bandArrowData;
		private bool m_priorityAlert;

		public AlertIndexAndCount AlertIndexAndCount
		{
			get
			{
				return m_alertIndexAndCount;
			}
		}

		public int Frequency
		{
			get
			{
				return m_frequency;
			}
		}

		public int FrontSignalStrength
		{
			get
			{
				return m_frontSignalStrength;
			}
		}

		public int RearSignalStrength
		{
			get
			{
				return m_rearSignalStrength;
			}
		}

		public BandArrowData BandArrowData
		{
			get
			{
				return m_bandArrowData;
			}
		}

		public bool PriorityAlert
		{
			get
			{
				return m_priorityAlert;
			}
			set
			{
				m_priorityAlert = value;
			}
		}

		public int FrontSignalNumberOfLEDs
		{
			get
			{
				return NumberOfLEDs(m_frontSignalStrengthByte);
			}
		}

		public int RearSignalNumberOfLEDs
		{
			get
			{
				return NumberOfLEDs(m_rearSignalStrengthByte);
			}
		}

		private int NumberOfLEDs(byte _data)
		{
			int unsigned = _data & 0xff;
			if (unsigned == 0x00)
			{
				return 0;
			}

			if (m_bandArrowData.XBand)
			{
				if (unsigned >= 0xd0)
				{
					return 8;
				}
				else if (unsigned >= 0xc5)
				{
					return 7;
				}
				else if (unsigned >= 0xbd)
				{
					return 6;
				}
				else if (unsigned >= 0xb4)
				{
					return 5;
				}
				else if (unsigned >= 0xaa)
				{
					return 4;
				}
				else if (unsigned >= 0xa0)
				{
					return 3;
				}
				else if (unsigned >= 0x96)
				{
					return 2;
				}
				else if (unsigned >= 0x01)
				{
					return 1;
				}
			}
			else if ((m_bandArrowData.KuBand) || (m_bandArrowData.KBand))
			{
				if (unsigned >= 0xc2)
				{
					return 8;
				}
				else if (unsigned >= 0xb8)
				{
					return 7;
				}
				else if (unsigned >= 0xae)
				{
					return 6;
				}
				else if (unsigned >= 0xa4)
				{
					return 5;
				}
				else if (unsigned >= 0x9a)
				{
					return 4;
				}
				else if (unsigned >= 0x90)
				{
					return 3;
				}
				else if (unsigned >= 0x88)
				{
					return 2;
				}
				else if (unsigned >= 0x01)
				{
					return 1;
				}
			}
			else if (m_bandArrowData.KaBand)
			{
				if (unsigned >= 0xba)
				{
					return 8;
				}
				else if (unsigned >= 0xb3)
				{
					return 7;
				}
				else if (unsigned >= 0xac)
				{
					return 6;
				}
				else if (unsigned >= 0xa5)
				{
					return 5;
				}
				else if (unsigned >= 0x9e)
				{
					return 4;
				}
				else if (unsigned >= 0x97)
				{
					return 3;
				}
				else if (unsigned >= 0x90)
				{
					return 2;
				}
				else if (unsigned >= 0x01)
				{
					return 1;
				}
			}

			return 0;
		}

		public void BuildFromData(byte[] _bytes)
		{
			m_alertIndexAndCount = new AlertIndexAndCount();
			m_bandArrowData = new BandArrowData();

			m_alertIndexAndCount.BuildFromByte(_bytes[0]);
			m_frequencyMSB = _bytes[1];
			m_frequencyLSB = _bytes[2];

			byte[] tempBytes = new byte[4];
			/*ByteBuffer b = ByteBuffer.allocate(4);
			b.put((byte)0);
			b.put((byte)0);
			b.put(m_frequencyMSB);
			b.put(m_frequencyLSB);
			int temp = b.getInt(0);*/
			tempBytes[0] = 0;
			tempBytes[1] = 0;
			tempBytes[2] = m_frequencyMSB;
			tempBytes[3] = m_frequencyLSB;
			int temp = Convert.ToInt32(tempBytes);
			m_frequency = temp;

			m_frontSignalStrength = (int)(_bytes[3] & 0xff);
			m_rearSignalStrength = (int)(_bytes[4] & 0xff);

			m_frontSignalStrengthByte = _bytes[3];
			m_rearSignalStrengthByte = _bytes[4];

			m_bandArrowData.BuildFromByte(_bytes[5]);

			if ((_bytes[6] & 128) > 0)
			{
				m_priorityAlert = true;
			}
			else
			{
				m_priorityAlert = false;
			}
		}

		public SignalDirection Orientation
		{
			get
			{
				if (m_bandArrowData.Front)
				{
					return SignalDirection.ORIENTATION_FRONT;
				}
				else if (m_bandArrowData.Side)
				{
					return SignalDirection.ORIENTATION_SIDE;
				}
				else if (m_bandArrowData.Rear)
				{
					return SignalDirection.ORIENTATION_REAR;
				}
				return SignalDirection.ORIENTATION_INVALID;
			}
		}

		public int NumBarGraphLED(SignalDirection orientation)
		{
			if (orientation == SignalDirection.ORIENTATION_FRONT)
			{
				return FrontSignalNumberOfLEDs;
			}
			else if (orientation == SignalDirection.ORIENTATION_SIDE)
			{
				if (FrontSignalNumberOfLEDs > RearSignalNumberOfLEDs)
					return FrontSignalNumberOfLEDs;
				else
					return RearSignalNumberOfLEDs;
			}
			else if (orientation == SignalDirection.ORIENTATION_REAR)
			{
				return RearSignalNumberOfLEDs;
			}
			return 4;
		}

		/*
		public AlertData()
		{


		}
		*/
	}
}
