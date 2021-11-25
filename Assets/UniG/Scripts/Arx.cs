using System.Runtime.InteropServices;
using System;
using UniG.Direct;
using UnityEngine;

namespace UniG {
    /// <summary>
    /// Interface to Arx Control, the remote control feature of G Hub and LGS.
    /// </summary>
    public class Arx {

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

        /// <summary>
        /// Initialize Arx for this app.
        /// </summary>
        /// <param name="identifier">Identifier. Must be in reverse domain format (e.g. com.sample.game).</param>
        /// <param name="friendlyName">Friendly name of this app. Shown to users.</param>
        /// <param name="callback">Callback function, called when an Arx related event happens.</param>
        /// <returns></returns>
        [DllImport("LogitechGArxControlEnginesWrapper", CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiArxInit(String identifier, String friendlyName, ref logiArxCbContext callback);

        /// <summary>
        /// Initialize Arx for this app.<br/>
        /// Uses the application name set in Project Settings as the applet name.<br/>
        /// On macOS uses the identifier set in Project Settings, otherwise uses "app.(company name).(app name)" as an identifier.<br/>
        /// If you want more control over these options use <see cref="LogiArxInit(string, string, ref logiArxCbContext)"/>.
        /// </summary>
        /// <param name="callback">Callback function, called when an Arx related event happens.</param>
        public static bool Init(ref logiArxCbContext callback) {
            var identifier = $"app.{Application.companyName}.{Application.productName}";
            if (Application.identifier != null && Application.identifier.Length != 0) identifier = Application.identifier;
            return LogiArxInit(identifier, Application.productName, ref callback);
        }
        /// <summary>
        /// Initialize Arx for this app with an icon.
        /// </summary>
        /// <param name="identifier">Identifier. Must be in reverse domain format (e.g. com.sample.game).</param>
        /// <param name="friendlyName">Friendly name of this app. Shown to users.</param>
        /// <param name="callback">Callback function, called when an Arx related event happens.</param>
        /// <param name="iconBitmap">The app's icon. Shown to users.</param>
        /// <returns></returns>
        [DllImport("LogitechGArxControlEnginesWrapper", CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl)]

        public static extern bool LogiArxInitWithIcon(String identifier, String friendlyName,
            ref logiArxCbContext callback, byte[] iconBitmap);

        /// <summary>
        /// Upload a file to the connected device.
        /// </summary>
        /// <param name="filePath">The path on the local machine.</param>
        /// <param name="fileName">The remote file name/path.</param>
        /// <param name="mimeType">The file's MIME type.</param>
        /// <returns>Success value.</returns>
        public static bool AddFileAs(string filePath, string fileName, string mimeType = "") => 
            dArx.LogiArxAddFileAs(filePath, fileName, mimeType);

        /// <summary>
        /// Upload a file with the specified content to the connected device.
        /// </summary>
        /// <param name="content">The content of the file.</param>
        /// <param name="fileName">The remote file name/path.</param>
        /// <param name="mimeType">The file's MIME type.</param>
        /// <returns>Success value.</returns>
        public static bool AddContentAs(byte[] content, int size, string fileName, string mimeType = "") => 
            dArx.LogiArxAddContentAs(content, size, fileName, mimeType);

        /// <summary>
        /// Upload a file with the specified content to the connected device.
        /// </summary>
        /// <param name="content">The content of the file.</param>
        /// <param name="fileName">The remote file name/path.</param>
        /// <param name="mimeType">The file's MIME type.</param>
        /// <returns>Success value.</returns>
        public static bool AddStringAs(string content, string fileName, string mimeType = "") =>
            dArx.LogiArxAddUTF8StringAs(content, fileName, mimeType);

        /// <summary>
        /// Upload a bitmap to the connected device.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <param name="width">The width of the bitmap.</param>
        /// <param name="height">The height of the bitmap.</param>
        /// <param name="fileName">The remote file name/path.</param>
        /// <returns>Success value.</returns>
        public static bool AddImageFromBitmap(byte[] bitmap, int width, int height, string fileName) =>
            dArx.LogiArxAddImageFromBitmap(bitmap, width, height, fileName);


        /// <summary>
        /// Upload a StreamingAsset to the connected device.
        /// </summary>
        /// <param name="filePath">The path of the file on the local machine. Starts with /.</param>
        /// <param name="fileName">The remote file name/path.</param>
        /// <param name="mimeType">The file's MIME type.</param>
        /// <returns>Success value.</returns>
        public static bool AddStreamingAssetAs(string filePath, string fileName, string mimeType = "") =>
            dArx.LogiArxAddFileAs(Application.streamingAssetsPath + filePath, fileName, mimeType);

        /// <summary>
        /// Set the index page. This starts the applet on the connected device.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <returns>Success value.</returns>
        public static bool SetIndex(string fileName) => dArx.LogiArxSetIndex(fileName);

        /// <summary>
        /// Find a tag by ID and set a property on it.
        /// </summary>
        /// <param name="id">The ID of the tag.</param>
        /// <param name="prop">The property.</param>
        /// <param name="newValue">The value of the property.</param>
        /// <returns>Success value.</returns>
        public static bool SetTagPropertyById(string id, string prop, string newValue) =>
            dArx.LogiArxSetTagPropertyById(id, prop, newValue);

        /// <summary>
        /// Find a tag (or tags) by class name and set a property on it.
        /// </summary>
        /// <param name="tagClass">The class of the tag(s).</param>
        /// <param name="prop">The property.</param>
        /// <param name="newValue">The value of the property.</param>
        /// <returns>Success value.</returns>
        public static bool SetTagsPropertyByClass(string tagClass, string prop, string newValue) =>
            dArx.LogiArxSetTagsPropertyByClass(tagClass, prop, newValue);

        /// <summary>
        /// Find a tag by ID and sets a set the content of it.
        /// </summary>
        /// <param name="id">The ID of the tag.</param>
        /// <param name="newValue">The new content.</param>
        /// <returns>Success value.</returns>
        public static bool SetTagContentById(string id, string newValue) =>
            dArx.LogiArxSetTagContentById(id, newValue);

        /// <summary>
        /// Find a tag (or tags) by class name and set the content of it.
        /// </summary>
        /// <param name="tagClass">The class of the tag(s).</param>
        /// <param name="newValue">The new content.</param>
        /// <returns>Success value.</returns>
        public static bool SetTagsContentByClass(string tagClass, string newValue) =>
            dArx.LogiArxSetTagsContentByClass(tagClass, newValue);

        /// <summary>
        /// Get the last error.
        /// </summary>
        /// <returns>Error code.</returns>
        public static int GetLastError() => dArx.LogiArxGetLastError();

        /// <summary>
        /// Shut down Arx. This removes the applet.
        /// </summary>
        public static void Shutdown() { dArx.LogiArxShutdown(); }
    }
}