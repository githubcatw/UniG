using System.Runtime.InteropServices;
using System;
using UniG.Direct;
using UnityEngine;

namespace UniG {
    public class Arx {

        /// <summary>
        /// A device's orientation.
        /// </summary>
        public enum Orientation {
            /// <summary>
            /// A phone/tablet with portrait orientation.
            /// </summary>
            Portrait = 0x01,

            /// <summary>
            /// A phone/tablet with landscape orientation.
            /// </summary>
            Landscape = 0x10
        }

        /// <summary>
        /// An event.
        /// </summary>
        public enum Event {
            /// <summary>
            /// Applet is in focus.
            /// </summary>
            AppletActive = 0x01,

            /// <summary>
            /// Applet isn't in focus.
            /// </summary>
            AppletInactive = 0x02,

            /// <summary>
            /// Someone tapped on a tag.
            /// </summary>
            TagTapped = 0x04,

            /// <summary>
            /// A mobile device was connected.
            /// </summary>
            Arrival = 0x08,

            /// <summary>
            /// A mobile device was disconnected.
            /// </summary>
            Removal = 0x10
        }

        /// <summary>
        /// A device's type.
        /// </summary>
        public enum DeviceType {
            /// <summary>
            /// An iPhone.
            /// </summary>
            iPhone = 0x01,
    
            /// <summary>
            /// An iPad.
            /// </summary>
            iPad = 0x02,
    
            /// <summary>
            /// An Android device with small resolution.
            /// </summary>
            AndroidSmall = 0x03,
    
            /// <summary>
            /// An Android device with normal resolution.
            /// </summary>
            AndroidNormal = 0x04,
    
            /// <summary>
            /// An Android device with large resolution.
            /// </summary>
            AndroidLarge = 0x05,
    
            /// <summary>
            /// An Android device with xlarge resolution.
            /// </summary>
            AndroidXlarge = 0x06,
    
            /// <summary>
            /// Other Android device.
            /// </summary>
            AndroidOther = 0x07
        }
        
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

        public static bool AddFileAs(string filePath, string fileName, string mimeType = "") => 
            dArx.LogiArxAddFileAs(filePath, fileName, mimeType);

        public static bool AddContentAs(byte[] content, int size, string fileName, string mimeType = "") => 
            dArx.LogiArxAddContentAs(content, size, fileName, mimeType);

        public static bool AddUtf8StringAs(string content, string fileName, string mimeType = "") =>
            dArx.LogiArxAddUTF8StringAs(content, fileName, mimeType);

        public static bool AddImageFromBitmap(byte[] bitmap, int width, int height, string fileName) =>
            dArx.LogiArxAddImageFromBitmap(bitmap, width, height, fileName);

        public static bool AddStreamingAssetAs(string filePath, string fileName, string mimeType = "") =>
            dArx.LogiArxAddFileAs(Application.streamingAssetsPath + filePath, fileName, mimeType);

        /// <summary>
        /// Sets the index page.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool SetIndex(string fileName) => dArx.LogiArxSetIndex(fileName);

        public static bool SetTagPropertyById(string id, string prop, string newValue) =>
            dArx.LogiArxSetTagPropertyById(id, prop, newValue);

        public static bool SetTagsPropertyByClass(string tagClass, string prop, string newValue) =>
            dArx.LogiArxSetTagsPropertyByClass(tagClass, prop, newValue);

        public static bool SetTagContentById(string id, string newValue) =>
            dArx.LogiArxSetTagContentById(id, newValue);

        public static bool SetTagsContentByClass(string tagClass, string newValue) =>
            dArx.LogiArxSetTagsContentByClass(tagClass, newValue);

        public static int GetLastError() => dArx.LogiArxGetLastError();

        public static void Shutdown() { dArx.LogiArxShutdown(); }
    }
}