using System;
using System.Collections.Generic;
using System.Linq;

namespace VeeOne.Constants
{
	public class DevicesLookup
	{
		static Dictionary<Byte, Devices> m_values = new Dictionary<byte, Devices>();

		public static Devices getConstant(byte _value)
		{
			if (m_values.Count == 0)
			{
				setUpValues();
			}

			// Trap special cases
			if (_value == Devices.VALENTINE1_LEGACY.ToByteValue())
			{
				return Devices.VALENTINE1_LEGACY;
			}
			if (_value == Devices.UNKNOWN.ToByteValue())
			{
				return Devices.UNKNOWN;
			}

			// Cast to char so it isn't treated as a signed value.
			if ((char)_value > (char)0x0F)
			{
				_value &= (byte)0x0F;
			}

			Devices retDev = m_values[_value];

			if (retDev == null)
			{
				retDev = Devices.UNKNOWN;
			}

			return retDev;
		}

		/**
	 	* Sets up a map containing the all the Devices values using the Devices byte value as the key.
	 	*/
		private static void setUpValues()
		{
			Devices[] array = Devices.Values.Cast<Devices>().ToArray();

			for (int i = 0; i < array.Length; i++)
			{
				m_values.Add(array[i].ToByteValue(), array[i]);
			}
		}
	}
}
