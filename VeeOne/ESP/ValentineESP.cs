using System;
using System.Collections.Generic;
using VeeOne.ESP.Constants;
using VeeOne.ESP.Demo;

namespace VeeOne
{
	public class ValentineESP
	{
		private static readonly String     LOG_TAG = "ValentineESP LOG";
	
		private Dictionary<PacketId, List<CallbackData>> m_callbackData = new Dictionary<PacketId, List<CallbackData>>();

		private Object m_stopObject;
		private String m_stopFunction;

		private bool m_notified;
		private bool m_inDemoMode;

		private DemoData m_demoData;

		// Callback object and function references.
		private object m_noDataObject;
		private string m_noDataFunction;
		private object m_unsupportedObject;
		private string m_unsupportedFunction;


		private Context mContext;
		private ProcessingThread m_processingThread;
		private VR_BluetoothWrapper mVrBluetoothWrapper;
		private ProcessDemoFileThread m_demoFileThread;
		private BluetoothDevice mBluetoothDevice;

		private Object m_ConnectionCallbackObject;
		private String m_ConnectionCallbackName;

		private Object m_DisconnectionCallbackObject;
		private String m_DisconnectionCallbackName;


		public ValentineESP()
		{
		}

		public class CallbackData
		{
			public Object callBackOwner;
			public String method;
		}

		public class DeregCallbackInfo
		{
			public Object callBackOwner;
			public PacketId type;
			public String method;
		}
	}
}
