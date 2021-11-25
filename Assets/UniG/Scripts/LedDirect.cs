using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace UniG.Direct {
    

    /// <summary>
    /// Control the LEDs of supported Logitech keyboards.
    /// </summary>
    public class dLed {
        private const int LOGI_DEVICETYPE_MONOCHROME_ORD = 0;
        private const int LOGI_DEVICETYPE_RGB_ORD = 1;
        private const int LOGI_DEVICETYPE_PERKEY_RGB_ORD = 2;

        /// <summary>
        /// A peripheral that supports monochrome lighting (ex. G710+).
        /// </summary>
        public const int LOGI_DEVICETYPE_MONOCHROME = (1 << LOGI_DEVICETYPE_MONOCHROME_ORD);
        /// <summary>
        /// A peripheral that supports monochrome lighting (ex. G213, G633).
        /// </summary>
        public const int LOGI_DEVICETYPE_RGB = (1 << LOGI_DEVICETYPE_RGB_ORD);
        /// <summary>
        /// An RGB keyboard that supports per key lighting (ex. G910).
        /// </summary>
        public const int LOGI_DEVICETYPE_PERKEY_RGB = (1 << LOGI_DEVICETYPE_PERKEY_RGB_ORD);
        /// <summary>
        /// Any peripheral.
        /// </summary>
        public const int LOGI_DEVICETYPE_ALL = (LOGI_DEVICETYPE_MONOCHROME | LOGI_DEVICETYPE_RGB | LOGI_DEVICETYPE_PERKEY_RGB);

        public const int LOGI_LED_BITMAP_WIDTH = 21;
        public const int LOGI_LED_BITMAP_HEIGHT = 6;
        public const int LOGI_LED_BITMAP_BYTES_PER_KEY = 4;

        public const int LOGI_LED_BITMAP_SIZE = LOGI_LED_BITMAP_WIDTH * LOGI_LED_BITMAP_HEIGHT * LOGI_LED_BITMAP_BYTES_PER_KEY;
        public const int LOGI_LED_DURATION_INFINITE = 0;

        /// <summary>
        /// Makes sure there isn’t already another instance running and then makes necessary initializations.
        /// </summary>
        /// <returns>If the function succeeds, it returns true. Otherwise false.</returns>
        [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedInit();

        /// <summary>
        /// Makes sure there isn’t already another instance running and then makes necessary initializations.
        /// </summary>
        /// <param name="name">The preferred name for this integration to show up as.</param>
        /// <returns></returns>
        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedInitWithName(String name);

        //Config option functions

        /// <summary>
        /// Allows the developer to query for a number set by the user and use that value to customize the interaction with the SDK.
        /// </summary>
        /// <param name="configPath">This identfes the option uniquely. This can be just a string (e.g."Terrorist") or it can be a two level tree ("Colors/Terrorist").</param>
        /// <param name="defaultNumber">The number set by the user.</param>
        /// <returns></returns>
        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedGetConfigOptionNumber([MarshalAs(UnmanagedType.LPWStr)] String configPath, ref double defaultNumber);

        /// <summary>
        /// Allows the developer to query for a boolean set by the user and use it to customize the interaction with the SDK.
        /// </summary>
        /// <param name="configPath">This identfes the option uniquely. This can be just a string (e.g."Terrorist") or it can be a two level tree ("Colors/Terrorist").</param>
        /// <param name="defaultRed">The boolean set by the user.</param>
        /// <returns></returns>
        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedGetConfigOptionBool([MarshalAs(UnmanagedType.LPWStr)] String configPath, ref bool defaultRed);

        /// <summary>
        /// Allows the developer to query for a color set by the user and use it to customize the interaction with the SDK.
        /// </summary>
        /// <param name="configPath">This identfes the option uniquely. This can be just a string (e.g."Terrorist") or it can be a two level tree ("Colors/Terrorist").</param>
        /// <param name="defaultRed">The red value of the color set by the user.</param>
        /// <param name="defaultBlue">The green value of the color set by the user.</param>
        /// <param name="defaultGreen">The green value of the color set by the user.</param>
        /// <returns></returns>
        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedGetConfigOptionColor([MarshalAs(UnmanagedType.LPWStr)] String configPath, ref int defaultRed, ref int defaultGreen, ref int defaultBlue);

        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedGetConfigOptionKeyInput([MarshalAs(UnmanagedType.LPWStr)] String configPath, StringBuilder buffer, int bufsize);
        /////////////////////

        /// <summary>
        /// Sets the target device type for future calls.
        /// The default target device is LOGI_DEVICETYPE_ALL, therefore, if no call is made to LogiLedSetTargetDevice the SDK will apply any function to all the connected devices.
        /// </summary>
        /// <returns></returns>
        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedSetTargetDevice(int targetDevice);

        /// <summary>
        /// Retrieves the version of the SDK version installed on the user’s system.
        /// </summary>
        /// <param name="majorNum">Will be set to the major build number.</param>
        /// <param name="minorNum">Will be set to the minor build number.</param>
        /// <param name="buildNum">Will be set to the patch build number.</param>
        /// <returns>False if there is no SDK installed on the system, or the version could not be retrieved. Otherwise true.</returns>
        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedGetSdkVersion(ref int majorNum, ref int minorNum, ref int buildNum);

        /// <summary>
        /// Saves the current lighting so that it can be restored after a temporary effect is finished.
        /// </summary>
        /// <returns></returns>
        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedSaveCurrentLighting();

        /// <summary>
        /// Sets the lighting on connected and supported devices.
        /// </summary>
        /// <param name="redPercentage">Percentage of red, range is 0-100</param>
        /// <param name="greenPercentage">Percentage of green, range is 0-100</param>
        /// <param name="bluePercentage">Percentage of blue, range is 0-100</param>
        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedSetLighting(int redPercentage, int greenPercentage, int bluePercentage);

        /// <summary>
        /// Restores the last saved lighting. It should be called after a temporary effect is finished.
        /// </summary>
        /// <returns></returns>
        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedRestoreLighting();

        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedFlashLighting(int redPercentage, int greenPercentage, int bluePercentage, int milliSecondsDuration, int milliSecondsInterval);

        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedPulseLighting(int redPercentage, int greenPercentage, int bluePercentage, int milliSecondsDuration, int milliSecondsInterval);

        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedStopEffects();

        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedExcludeKeysFromBitmap(KeyCode[] keyList, int listCount);

        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedSetLightingFromBitmap(byte[] bitmap);

        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedSetLightingForKeyWithScanCode(int keyCode, int redPercentage, int greenPercentage, int bluePercentage);

        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedSetLightingForKeyWithHidCode(int keyCode, int redPercentage, int greenPercentage, int bluePercentage);

        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedSetLightingForKeyWithQuartzCode(int keyCode, int redPercentage, int greenPercentage, int bluePercentage);

        /// <summary>
        /// Sets the key identified by the code passed as parameter to the desired color. This function only affects per-key backlighting featured connected devices.
        /// </summary>
        /// <param name="keyCode"></param>
        /// <param name="redPercentage"></param>
        /// <param name="greenPercentage"></param>
        /// <param name="bluePercentage"></param>
        /// <returns></returns>
        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedSetLightingForKeyWithKeyName(KeyCode keyCode, int redPercentage, int greenPercentage, int bluePercentage);

        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedSaveLightingForKey(KeyCode keyName);

        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedRestoreLightingForKey(KeyCode keyName);

        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedFlashSingleKey(KeyCode keyName, int redPercentage, int greenPercentage, int bluePercentage, int msDuration, int msInterval);

        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedPulseSingleKey(KeyCode keyName, int startRedPercentage, int startGreenPercentage, int startBluePercentage, int finishRedPercentage, int finishGreenPercentage, int finishBluePercentage, int msDuration, bool isInfinite);

        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedStopEffectsOnKey(KeyCode keyName);

        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedSetLightingForTargetZone(DeviceType deviceType, int zone, int redPercentage, int greenPercentage, int bluePercentage);

        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
        public static extern void LogiLedShutdown();
    }
}