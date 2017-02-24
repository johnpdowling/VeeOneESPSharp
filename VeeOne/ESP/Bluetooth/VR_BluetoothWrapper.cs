using System;
using System.Collections.Generic;
using System.Threading;
using VeeOne.ESP.Constants;
using VeeOne.ESP.Packets;

namespace VeeOne.ESP.Bluetooth
{
	public abstract class VR_BluetoothWrapper : IVR_BluetoothWrapper
	{
		protected static readonly int CONNECTED = 200;
		protected static readonly int DISCONNECTED = CONNECTED + 1;
		protected static readonly int CONNECTIONLOSS = DISCONNECTED + 1;
		protected static readonly int CONNECT_FAILURE = CONNECTIONLOSS + 1;
		protected static readonly int CHECK_CONNECTION_STATE = CONNECT_FAILURE + 1;

		protected static readonly int ATTEMPT_CONNECTION = 100;
		protected static readonly int ATTEMPT_DISCONNECTION = ATTEMPT_CONNECTION + 1;
		protected static readonly int STOP_SCAN = ATTEMPT_DISCONNECTION + 1;


		private static readonly String                     LOG_TAG = "VR_BluetoothWrapper LOG";
		private static readonly int EMPTY_READ_SLEEP_TIME = 100;
		private int MAX_EMPTY_READS;
		private static bool m_protectLegacyMode = false;

		protected ConnectionType mConnectedType;
		protected ValentineESP mValentineESP;
		private int m_emptyReadCount;
		private bool m_notifiedNoData;
		protected bool mShouldNotify = false;

		/**
		 * Lock to control read & write access to the isConnected flag.
		 */
		protected object mConnectedLock = new object();//ReentrantLock();
		/** 
		 * Lock to control read & write access to the canWrite flag.
		 */
		private object mWritelock = new object(); //ReentrantLock();
		  /** 
		   * Lock to control read & write access to the mIsESPRunning flag.
		   */
		private object mESPRunningLock = new object(); //ReentrantLock();

		private bool mIsESPRunning = false;
		private bool mIsConnected = false;

		protected DataReaderThread mReaderThread;
		protected DataWriterThread mWriterThread;

		protected BluetoothDevice mBluetoothDevice;
		protected bool m_retryOnConnectFailure;
		protected int mSecondsToWait;
		protected Context mContext;
		protected List<Byte> m_readByteBuffer;

		private bool mCanWrite = true;

		protected VRScanCallback mScanCallback = null;

		protected int mSecondsToScan = -1;

		protected object echoLock = new object();
		protected List<KeyValuePair<long, ESPPacket>> expectedEchoPackets = new List<KeyValuePair<long, ESPPacket>>();

		protected Devices mlastKnownV1Type = Devices.UNKNOWN;

		private Thread mConnThread = null;

		protected bool mConnectionLost = false;
		protected volatile bool mConnectOnResult = false;
		private readonly Object                            mLock = new Object();


		public bool isConnected()
		{
			throw new NotImplementedException();
		}

		public void sendPacket(ESPPacket packet)
		{
			throw new NotImplementedException();
		}

		public int startAsync()
		{
			throw new NotImplementedException();
		}

		public bool startSync()
		{
			throw new NotImplementedException();
		}

		public void stopAsync()
		{
			throw new NotImplementedException();
		}

		public bool stopSync()
		{
			throw new NotImplementedException();
		}
	}
}
