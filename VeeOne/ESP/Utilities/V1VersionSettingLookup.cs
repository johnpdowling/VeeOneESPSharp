using System;
using System.Collections.Generic;
using VeeOne.ESP.Data;

namespace VeeOne.ESP.Utilities
{
	public class V1VersionSettingLookup
	{
		public enum V1_Band
		{
			X,
			Ku,
			K,
			Ka,
			Ka_Lo,
			Ka_Mid,
			Ka_Hi,
			POP,
			No_Band
		};

		// Default to not in demo mode.
		protected static bool isInDemoMode = false;

		// Use version V3.8930 as the default version for this app. 
		protected readonly static double DEFAULT_VERSION = 3.8930;

		// The ability to read the default sweeps from the V1 was added in V3.8950.
		protected readonly static double READ_SWEEP_DEFAULTS_START_VER = 3.8950;

		// The TMF & Junk-K Fighter feature was turned on by default in verison 3.8945
		protected readonly static double START_TMF_ON_BY_DEFAULT = 3.8945;

		protected static double currentVersion = DEFAULT_VERSION;

		/* Demo mode sweep section frequencies */
		private static readonly int V3_8920_DEMO_SWEEP_SECTION_LO = 33383;
		private static readonly int V3_8920_DEMO_SWEEP_SECTION_HI = 36072;

		// V3.8920 is the first production release that supports the ESP specification.
		/* V3.9820 Custom sweep ranges */
		private static readonly Range[] V3_8920_CUSTOM_SWEEPS =
		{
			new Range(33900, 34106),
			new Range(34180, 34475),
			new Range(34563, 34652),
			new Range(35467, 35526),
			new Range(0, 0),
			new Range(0, 0)
		};

		/* V3.9820 maximum number of custom sweep ranges */
		private static readonly int V38920_MAX_SWEEP_INDEX = 5;

		/* V3.9820 Band frequencies */
		private static readonly double V3_8920_BAND_X_LO = 10.477;
		private static readonly double V3_8920_BAND_X_HI = 10.566;
		private static readonly double V3_8920_BAND_X_POLICE_LO = 10.500;
		private static readonly double V3_8920_BAND_X_POLICE_HI = 10.550;
		private static readonly double V3_8920_BAND_KU_LO = 13.394;
		private static readonly double V3_8920_BAND_KU_HI = 13.512;
		private static readonly double V3_8920_BAND_KU_POLICE_LO = 13.400;
		private static readonly double V3_8920_BAND_KU_POLICE_HI = 13.500;
		private static readonly double V3_8920_BAND_K_LO = 24.036;
		private static readonly double V3_8920_BAND_K_HI = 24.272;
		private static readonly double V3_8920_BAND_K_POLICE_LO = 24.050;
		private static readonly double V3_8920_BAND_K_POLICE_HI = 24.250;
		private static readonly double V3_8920_BAND_KA_LO_LO = 33.400;
		private static readonly double V3_8920_BAND_KA_LO_HI = 34.300;
		private static readonly double V3_8920_BAND_KA_LO_POLICE_LO = 33.700;
		private static readonly double V3_8920_BAND_KA_LO_POLICE_HI = 33.900;
		private static readonly double V3_8920_BAND_KA_MID_LO = 34.301;
		private static readonly double V3_8920_BAND_KA_MID_HI = 35.100;
		private static readonly double V3_8920_BAND_KA_MID_POLICE_LO = 34.600;
		private static readonly double V3_8920_BAND_KA_MID_POLICE_HI = 34.800;
		private static readonly double V3_8920_BAND_KA_HI_LO = 35.101;
		private static readonly double V3_8920_BAND_KA_HI_HI = 36.000;
		private static readonly double V3_8920_BAND_KA_HI_POLICE_LO = 35.400;
		private static readonly double V3_8920_BAND_KA_HI_POLICE_HI = 35.600;
		private static readonly double V3_8920_BAND_POP_LO = 33.700;
		private static readonly double V3_8920_BAND_POP_HI = 33.900;

		/* Default sweep section frequencies */
		private static readonly int V3_8920_SWEEP_SECTION_1_LO = 33380;
		private static readonly int V3_8920_SWEEP_SECTION_1_HI = 34770;
		private static readonly int V3_8920_SWEEP_SECTION_2_LO = 34774;
		private static readonly int V3_8920_SWEEP_SECTION_2_HI = 36072;

		public void setV1Version( String _version)
		{
			if (_version.Substring(0, 1).Equals("V"))
			{
				try
				{
					// The version is in the format V0.1234 so we need to start with the second character. 
					currentVersion = Double.Parse(_version.Substring(1));
				}
				catch
				{
					currentVersion = DEFAULT_VERSION;
				}
			}
		}

		public void setDemoMode(bool inDemoMode)
		{
			isInDemoMode = inDemoMode;
		}

		public Range[] V1DefaultCustomSweeps
		{
			get
			{
				int maxSweepIndex;
				Range[] customSweeps;

				// Use the sweeps for the initial ESP release
				maxSweepIndex = V38920_MAX_SWEEP_INDEX;
				customSweeps = V3_8920_CUSTOM_SWEEPS;

				// Create a new array that can be modified by the caller without affecting this object.
				// Use '+ 1' because the sweep index is zero based so there are maxSweepIndex + 1 sweeps available.
				Range[] returnRange = new Range[maxSweepIndex + 1];
				for (int i = 0; i <= maxSweepIndex; i++)
				{
					returnRange[i] = new Range(customSweeps[i].LoFreq, customSweeps[i].HiFreq);
				}
				return returnRange;
			}
		}

		public Range DefaultRangeForBand(V1_Band band)
		{
			// All versions use the same band ranges so just use the default values from V3.8920.
			switch (band)
			{
				case V1_Band.Ka:
					return new Range((int)(V3_8920_BAND_KA_LO_LO * 1000), (int)(V3_8920_BAND_KA_HI_HI * 1000));
				case V1_Band.Ka_Hi:
					return new Range((int)(V3_8920_BAND_KA_HI_LO * 1000), (int)(V3_8920_BAND_KA_HI_HI * 1000));
				case V1_Band.Ka_Lo:
					return new Range((int)(V3_8920_BAND_KA_LO_LO * 1000), (int)(V3_8920_BAND_KA_LO_HI * 1000));
				case V1_Band.Ka_Mid:
					return new Range((int)(V3_8920_BAND_KA_MID_LO * 1000), (int)(V3_8920_BAND_KA_MID_HI * 1000));
				case V1_Band.K:
					return new Range((int)(V3_8920_BAND_K_LO * 1000), (int)(V3_8920_BAND_K_HI * 1000));
				case V1_Band.Ku:
					return new Range((int)(V3_8920_BAND_KU_LO * 1000), (int)(V3_8920_BAND_KU_HI * 1000));
				case V1_Band.X:
					return new Range((int)(V3_8920_BAND_X_LO * 1000), (int)(V3_8920_BAND_X_HI * 1000));
				case V1_Band.POP:
					return new Range((int)(V3_8920_BAND_POP_LO * 1000), (int)(V3_8920_BAND_POP_HI * 1000));
				case V1_Band.No_Band:
				default:
					return new Range();

			}
		}

		public Range DefaultRangeForPolice(V1_Band band)
		{
			// All versions use the same box ranges so just use the default values from V3.8920.
			switch (band)
			{
				case V1_Band.K:
					return new Range((int)(V3_8920_BAND_K_POLICE_LO * 1000), (int)(V3_8920_BAND_K_POLICE_HI * 1000));
				case V1_Band.Ka_Hi:
					return new Range((int)(V3_8920_BAND_KA_HI_POLICE_LO * 1000), (int)(V3_8920_BAND_KA_HI_POLICE_HI * 1000));
				case V1_Band.Ka_Mid:
					return new Range((int)(V3_8920_BAND_KA_MID_POLICE_LO * 1000), (int)(V3_8920_BAND_KA_MID_POLICE_HI * 1000));
				case V1_Band.Ka_Lo:
					return new Range((int)(V3_8920_BAND_KA_LO_POLICE_LO * 1000), (int)(V3_8920_BAND_KA_LO_POLICE_HI * 1000));
				case V1_Band.Ku:
					return new Range((int)(V3_8920_BAND_KU_POLICE_LO * 1000), (int)(V3_8920_BAND_KU_POLICE_HI * 1000));
				case V1_Band.X:
					return new Range((int)(V3_8920_BAND_X_POLICE_LO * 1000), (int)(V3_8920_BAND_X_POLICE_HI * 1000));
				case V1_Band.POP:
				case V1_Band.Ka:
				case V1_Band.No_Band:
				default:
					return new Range();
			}
		}

		public double DefaultLowerEdge(V1_Band band)
		{
			// All versions use the same band ranges so just use the default values from V3.8920.
			switch (band)
			{
				case V1_Band.Ka:
					return V3_8920_BAND_KA_LO_LO;
				case V1_Band.Ka_Hi:
					return V3_8920_BAND_KA_HI_LO;
				case V1_Band.Ka_Lo:
					return V3_8920_BAND_KA_LO_LO;
				case V1_Band.Ka_Mid:
					return V3_8920_BAND_KA_MID_LO;
				case V1_Band.K:
					return V3_8920_BAND_K_LO;
				case V1_Band.Ku:
					return V3_8920_BAND_KU_LO;
				case V1_Band.X:
					return V3_8920_BAND_X_LO;
				case V1_Band.POP:
					return V3_8920_BAND_POP_LO;
				case V1_Band.No_Band:
				default:
					return 0.0;
			}
		}

		public double DefaultUpperEdge(V1_Band band)
		{
			// All versions use the same band ranges so just use the default values from V3.8920.
			switch (band)
			{
				case V1_Band.Ka:
					return V3_8920_BAND_KA_HI_HI;
				case V1_Band.Ka_Hi:
					return V3_8920_BAND_KA_HI_HI;
				case V1_Band.Ka_Mid:
					return V3_8920_BAND_KA_MID_HI;
				case V1_Band.Ka_Lo:
					return V3_8920_BAND_KA_LO_HI;
				case V1_Band.K:
					return V3_8920_BAND_K_HI;
				case V1_Band.Ku:
					return V3_8920_BAND_KU_HI;
				case V1_Band.X:
					return V3_8920_BAND_X_HI;
				case V1_Band.POP:
					return V3_8920_BAND_POP_HI;
				case V1_Band.No_Band:
				default:
					return 0.0;
			}
		}

		public double DefaultPoliceLowerEdge(V1_Band band)
		{
			// All versions use the same box ranges so just use the default values from V3.8920.
			switch (band)
			{
				case V1_Band.K:
					return V3_8920_BAND_K_POLICE_LO;
				case V1_Band.Ka_Hi:
					return V3_8920_BAND_KA_HI_POLICE_LO;
				case V1_Band.Ka_Mid:
					return V3_8920_BAND_KA_MID_POLICE_LO;
				case V1_Band.Ka_Lo:
					return V3_8920_BAND_KA_LO_POLICE_LO;
				case V1_Band.Ku:
					return V3_8920_BAND_KU_POLICE_LO;
				case V1_Band.X:
					return V3_8920_BAND_X_POLICE_LO;
				case V1_Band.Ka:
				case V1_Band.POP:
				case V1_Band.No_Band:
				default:
					return 0.0;
			}
		}

		public double DefaultPoliceUpperEdge(V1_Band band)
		{
			// All versions use the same box ranges so just use the default values from V3.8920.
			switch (band)
			{
				case V1_Band.K:
					return V3_8920_BAND_K_POLICE_HI;
				case V1_Band.Ka_Hi:
					return V3_8920_BAND_KA_HI_POLICE_HI;
				case V1_Band.Ka_Mid:
					return V3_8920_BAND_KA_MID_POLICE_HI;
				case V1_Band.Ka_Lo:
					return V3_8920_BAND_KA_LO_POLICE_HI;
				case V1_Band.Ku:
					return V3_8920_BAND_KU_POLICE_HI;
				case V1_Band.X:
					return V3_8920_BAND_X_POLICE_HI;
				case V1_Band.Ka:
				case V1_Band.POP:
				case V1_Band.No_Band:
				default:
					return 0.0;
			}
		}

		public List<SweepSection> getV1DefaultSweepSections()
		{
			List<SweepSection> retObj = new List<SweepSection>();

			if (isInDemoMode)
			{
				// Use a full sweep as the only section when in demo mode
				retObj.Add(new SweepSection(V3_8920_DEMO_SWEEP_SECTION_LO, V3_8920_DEMO_SWEEP_SECTION_HI));
			}
			else 
			{
				// Use the V3.8920 defaults when not in demo mode
				retObj.Add(new SweepSection(V3_8920_SWEEP_SECTION_1_LO, V3_8920_SWEEP_SECTION_1_HI));
				retObj.Add(new SweepSection(V3_8920_SWEEP_SECTION_2_LO, V3_8920_SWEEP_SECTION_2_HI));
			}

			return retObj;
		}

		public int V1DefaultMaxSweepIndex
		{
			get
			{
				// Use the index for the initial ESP release
				return V38920_MAX_SWEEP_INDEX;
			}
		}

		public Range EmptyRange
		{
			get
			{
				return new Range();
			}
		}

		public bool AllowDefaulSweepDefRead
		{
			get
			{
				return (currentVersion >= READ_SWEEP_DEFAULTS_START_VER);
			}
		}

		public static bool DefaultTMFState
		{
			get
			{
				if (isInDemoMode)
				{
					// Turn on TMF & Junk-K Fighter by default when in demo mode.
					return true;
				}

				if (currentVersion >= START_TMF_ON_BY_DEFAULT)
				{
					// This version has TMF on by default as the unit is shipped from the factory.
					return true;
				}

				// This version does not have TMF on by default
				return false;
			}
		}

		/*
		public V1VersionSettingLookup()
		{
		}
		*/
	}
}
