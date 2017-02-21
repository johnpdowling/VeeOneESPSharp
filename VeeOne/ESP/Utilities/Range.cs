using System;
namespace VeeOne.ESP.Utilities
{
	public class Range
	{
		public int LoFreq;
		public int HiFreq;

		public Range(int loFreq, int hiFreq)
		{
			LoFreq = loFreq;
			HiFreq = hiFreq;
		}

		public Range()
		{
			LoFreq = 0;
			HiFreq = 0;
		}

		public double Width
		{
			get
			{
				return HiFreq - LoFreq;
			}
		}

		public Range Clone()
		{
			return new Range(LoFreq, HiFreq);
		}

		public bool IsContained(Range r)
		{
			return IsContained(r.LoFreq, r.HiFreq);
		}

		public bool IsContained(int _loFreq, int _hiFreq)
		{
			if ((_loFreq >= LoFreq) && (_loFreq <= HiFreq) && (_hiFreq >= LoFreq) && (_hiFreq <= HiFreq))
			{
				return true;
			}

			return false;
		}

		public bool IsContained(int frequency)
		{
			if ((frequency >= this.LoFreq) && (frequency <= this.HiFreq))
			{
				return true;
			}
			return false;
		}
	}
}
