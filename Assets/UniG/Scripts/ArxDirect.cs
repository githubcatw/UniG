using System.Runtime.InteropServices;
using System;

namespace UniG.Direct {

    public class dArx {

        /// <summary>
        /// A phone/tablet with portrait orientation.
        /// </summary>
        public const int LOGI_ARX_ORIENTATION_PORTRAIT = 0x01;
        /// <summary>
        /// A phone/tablet with landscape orientation.
        /// </summary>
        public const int LOGI_ARX_ORIENTATION_LANDSCAPE = 0x10;

        /// <summary>
        /// Applet is in focus.
        /// </summary>
        public const int LOGI_ARX_EVENT_FOCUS_ACTIVE = 0x01;
        /// <summary>
        /// Applet isn't in focus.
        /// </summary>
        public const int LOGI_ARX_EVENT_FOCUS_INACTIVE = 0x02;
        /// <summary>
        /// Applet's tag was tapped.
        /// </summary>
        public const int LOGI_ARX_EVENT_TAP_ON_TAG = 0x04;
        /// <summary>
        /// A mobile device was connected.
        /// </summary>
        public const int LOGI_ARX_EVENT_MOBILEDEVICE_ARRIVAL = 0x08;
        /// <summary>
        /// A mobile device was disconnected.
        /// </summary>
        public const int LOGI_ARX_EVENT_MOBILEDEVICE_REMOVAL = 0x10;

        /// <summary>
        /// An iPhone.
        /// </summary>
        public const int LOGI_ARX_DEVICETYPE_IPHONE = 0x01;
        /// <summary>
        /// An iPad.
        /// </summary>
        public const int LOGI_ARX_DEVICETYPE_IPAD = 0x02;

        /// <summary>
        /// An Android device with small resolution.
        /// </summary>
        public const int LOGI_ARX_DEVICETYPE_ANDROID_SMALL = 0x03;
        /// <summary>
        /// An Android device with normal resolution.
        /// </summary>
        public const int LOGI_ARX_DEVICETYPE_ANDROID_NORMAL = 0x04;
        /// <summary>
        /// An Android device with large resolution.
        /// </summary>
        public const int LOGI_ARX_DEVICETYPE_ANDROID_LARGE = 0x05;
        /// <summary>
        /// An Android device with xlarge resolution.
        /// </summary>
        public const int LOGI_ARX_DEVICETYPE_ANDROID_XLARGE = 0x06;
        /// <summary>
        /// Other Android device.
        /// </summary>
        public const int LOGI_ARX_DEVICETYPE_ANDROID_OTHER = 0x07;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void logiArxCB(int eventType, int eventValue, [MarshalAs(UnmanagedType.LPWStr)] String eventArg,
            IntPtr context);

        public struct logiArxCbContext {
            public logiArxCB arxCallBack;
            public IntPtr arxContext;
        }

        [DllImport("LogitechGArxControlEnginesWrapper", CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiArxInit(String identifier, String friendlyName, ref logiArxCbContext callback);

        [DllImport("LogitechGArxControlEnginesWrapper", CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiArxInitWithIcon(String identifier, String friendlyName,
            ref logiArxCbContext callback, byte[] iconBitmap);

        [DllImport("LogitechGArxControlEnginesWrapper", CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiArxAddFileAs(String filePath, String fileName, String mimeType = "");

        [DllImport("LogitechGArxControlEnginesWrapper", CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiArxAddContentAs(byte[] content, int size, String fileName, String mimeType = "");

        [DllImport("LogitechGArxControlEnginesWrapper", CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiArxAddUTF8StringAs(String stringContent, String fileName, String mimeType = "");

        [DllImport("LogitechGArxControlEnginesWrapper", CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiArxAddImageFromBitmap(byte[] bitmap, int width, int height, String fileName);

        /// <summary>
        /// Sets the index page.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [DllImport("LogitechGArxControlEnginesWrapper", CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiArxSetIndex(String fileName);

        [DllImport("LogitechGArxControlEnginesWrapper", CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiArxSetTagPropertyById(String tagId, String prop, String newValue);

        [DllImport("LogitechGArxControlEnginesWrapper", CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiArxSetTagsPropertyByClass(String tagsClass, String prop, String newValue);

        [DllImport("LogitechGArxControlEnginesWrapper", CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiArxSetTagContentById(String tagId, String newContent);

        [DllImport("LogitechGArxControlEnginesWrapper", CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiArxSetTagsContentByClass(String tagsClass, String newContent);

        [DllImport("LogitechGArxControlEnginesWrapper", CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern int LogiArxGetLastError();

        [DllImport("LogitechGArxControlEnginesWrapper", CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern void LogiArxShutdown();
    }
}
