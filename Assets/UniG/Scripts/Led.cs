using System;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace UniG {
    /// <summary>
    /// Key scan codes.
    /// </summary>
    public enum KeyCode {
        ESC = 0x01,
        F1 = 0x3b,
        F2 = 0x3c,
        F3 = 0x3d,
        F4 = 0x3e,
        F5 = 0x3f,
        F6 = 0x40,
        F7 = 0x41,
        F8 = 0x42,
        F9 = 0x43,
        F10 = 0x44,
        F11 = 0x57,
        F12 = 0x58,
        PRINT_SCREEN = 0x137,
        SCROLL_LOCK = 0x46,
        PAUSE_BREAK = 0x145,
        TILDE = 0x29,
        ONE = 0x02,
        TWO = 0x03,
        THREE = 0x04,
        FOUR = 0x05,
        FIVE = 0x06,
        SIX = 0x07,
        SEVEN = 0x08,
        EIGHT = 0x09,
        NINE = 0x0A,
        ZERO = 0x0B,
        MINUS = 0x0C,
        EQUALS = 0x0D,
        BACKSPACE = 0x0E,
        INSERT = 0x152,
        HOME = 0x147,
        PAGE_UP = 0x149,
        NUM_LOCK = 0x45,
        NUM_SLASH = 0x135,
        NUM_ASTERISK = 0x37,
        NUM_MINUS = 0x4A,
        TAB = 0x0F,
        Q = 0x10,
        W = 0x11,
        E = 0x12,
        R = 0x13,
        T = 0x14,
        Y = 0x15,
        U = 0x16,
        I = 0x17,
        O = 0x18,
        P = 0x19,
        OPEN_BRACKET = 0x1A,
        CLOSE_BRACKET = 0x1B,
        BACKSLASH = 0x2B,
        KEYBOARD_DELETE = 0x153,
        END = 0x14F,
        PAGE_DOWN = 0x151,
        NUM_SEVEN = 0x47,
        NUM_EIGHT = 0x48,
        NUM_NINE = 0x49,
        NUM_PLUS = 0x4E,
        CAPS_LOCK = 0x3A,
        A = 0x1E,
        S = 0x1F,
        D = 0x20,
        F = 0x21,
        G = 0x22,
        H = 0x23,
        J = 0x24,
        K = 0x25,
        L = 0x26,
        SEMICOLON = 0x27,
        APOSTROPHE = 0x28,
        ENTER = 0x1C,
        NUM_FOUR = 0x4B,
        NUM_FIVE = 0x4C,
        NUM_SIX = 0x4D,
        LEFT_SHIFT = 0x2A,
        Z = 0x2C,
        X = 0x2D,
        C = 0x2E,
        V = 0x2F,
        B = 0x30,
        N = 0x31,
        M = 0x32,
        COMMA = 0x33,
        PERIOD = 0x34,
        FORWARD_SLASH = 0x35,
        RIGHT_SHIFT = 0x36,
        ARROW_UP = 0x148,
        NUM_ONE = 0x4F,
        NUM_TWO = 0x50,
        NUM_THREE = 0x51,
        NUM_ENTER = 0x11C,
        LEFT_CONTROL = 0x1D,
        LEFT_WINDOWS = 0x15B,
        LEFT_ALT = 0x38,
        SPACE = 0x39,
        RIGHT_ALT = 0x138,
        RIGHT_WINDOWS = 0x15C,
        APPLICATION_SELECT = 0x15D,
        RIGHT_CONTROL = 0x11D,
        ARROW_LEFT = 0x14B,
        ARROW_DOWN = 0x150,
        ARROW_RIGHT = 0x14D,
        NUM_ZERO = 0x52,
        NUM_PERIOD = 0x53,
        G_1 = 0xFFF1,
        G_2 = 0xFFF2,
        G_3 = 0xFFF3,
        G_4 = 0xFFF4,
        G_5 = 0xFFF5,
        G_6 = 0xFFF6,
        G_7 = 0xFFF7,
        G_8 = 0xFFF8,
        G_9 = 0xFFF9,
        G_LOGO = 0xFFFF1,
        G_BADGE = 0xFFFF2
    };

    /// <summary>
    /// Device type.
    /// </summary>
    public enum DeviceType {
        Keyboard = 0x0,
        Mouse = 0x3,
        Mousemat = 0x4,
        Headset = 0x8,
        Speaker = 0xe
    }

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
        public static extern bool LogiLedGetConfigOptionNumber([MarshalAs(UnmanagedType.LPWStr)]String configPath, ref double defaultNumber);

        /// <summary>
        /// Allows the developer to query for a boolean set by the user and use it to customize the interaction with the SDK.
        /// </summary>
        /// <param name="configPath">This identfes the option uniquely. This can be just a string (e.g."Terrorist") or it can be a two level tree ("Colors/Terrorist").</param>
        /// <param name="defaultRed">The boolean set by the user.</param>
        /// <returns></returns>
        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedGetConfigOptionBool([MarshalAs(UnmanagedType.LPWStr)]String configPath, ref bool defaultRed);

        /// <summary>
        /// Allows the developer to query for a color set by the user and use it to customize the interaction with the SDK.
        /// </summary>
        /// <param name="configPath">This identfes the option uniquely. This can be just a string (e.g."Terrorist") or it can be a two level tree ("Colors/Terrorist").</param>
        /// <param name="defaultRed">The red value of the color set by the user.</param>
        /// <param name="defaultBlue">The green value of the color set by the user.</param>
        /// <param name="defaultGreen">The green value of the color set by the user.</param>
        /// <returns></returns>
        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedGetConfigOptionColor([MarshalAs(UnmanagedType.LPWStr)]String configPath, ref int defaultRed, ref int defaultGreen, ref int defaultBlue);

        [DllImport("LogitechLedEnginesWrapper ", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LogiLedGetConfigOptionKeyInput([MarshalAs(UnmanagedType.LPWStr)]String configPath, StringBuilder buffer, int bufsize);
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

    /// <summary>
    /// Control the LEDs of supported Logitech G hardware.
    /// </summary>
    public class Led {
        private const int LOGI_DEVICETYPE_MONOCHROME_ORD = 0;
        private const int LOGI_DEVICETYPE_RGB_ORD = 1;
        private const int LOGI_DEVICETYPE_PERKEY_RGB_ORD = 2;

        /// <summary>
        /// Converts a Unity color to an R/G/B percentage for use with this library.
        /// </summary>
        public static Color UnityToPercentage(Color unityColor) {
            return new Color(unityColor.r * 100, unityColor.g * 100, unityColor.b * 100);
        }

        /// <summary>
        /// Converts an R/G/B percentage to a Unity color.
        /// </summary>
        public static Color PercentageToUnity(Color percColor) {
            return new Color(percColor.r / 100, percColor.g / 100, percColor.b / 100);
        }

        public enum DeviceLightingType {
            /// <summary>
            /// A peripheral that supports monochrome lighting (ex. G710+).
            /// </summary>
            Monochrome = (1 << LOGI_DEVICETYPE_MONOCHROME_ORD),
            /// <summary>
            /// A peripheral that supports monochrome lighting (ex. G213, G633).
            /// </summary>
            RGB = (1 << LOGI_DEVICETYPE_RGB_ORD),
            /// <summary>
            /// An RGB keyboard that supports per key lighting (ex. G910).
            /// </summary>
            PerKeyRGB = (1 << LOGI_DEVICETYPE_PERKEY_RGB_ORD),
            /// <summary>
            /// Any peripheral.
            /// </summary>
            All = (Monochrome | RGB | PerKeyRGB)
        }

        public const int BitmapWidth = 21;
        public const int BitmapHeight = 6;
        public const int BitmapBytesPerKey = 4;

        public const int BitmapSize = BitmapWidth * BitmapHeight * BitmapBytesPerKey;
        public const int DurationInfinite = 0;

        /// <summary>
        /// Makes sure there isn’t already another instance running and then makes necessary initializations.
        /// </summary>
        public static bool Init() => dLed.LogiLedInit();

        /// <summary>
        /// Makes sure there isn’t already another instance running and then makes necessary initializations.
        /// </summary>
        /// <param name="name">The preferred name for this integration to show up as.</param>
        public static bool Init(string name) => dLed.LogiLedInitWithName(name);

        //Config option functions

        /// <summary>
        /// Allows the developer to query for a number set by the user and use that value to customize the interaction with the SDK.
        /// </summary>
        /// <param name="configPath">This identfes the option uniquely. This can be just a string (e.g."Terrorist") or it can be a two level tree ("Colors/Terrorist").</param>
        /// <param name="defaultNumber">The number set by the user.</param>
        public static bool GetConfigOption(string configPath, ref double defaultNumber) => dLed.LogiLedGetConfigOptionNumber(configPath, ref defaultNumber);

        /// <summary>
        /// Allows the developer to query for a boolean set by the user and use it to customize the interaction with the SDK.
        /// </summary>
        /// <param name="configPath">This identfes the option uniquely. This can be just a string (e.g."Terrorist") or it can be a two level tree ("Colors/Terrorist").</param>
        /// <param name="defaultBool">The boolean set by the user.</param>
        public static bool GetConfigOption(string configPath, ref bool defaultBool) => dLed.LogiLedGetConfigOptionBool(configPath, ref defaultBool);

        /// <summary>
        /// Allows the developer to query for a color set by the user and use it to customize the interaction with the SDK.
        /// </summary>
        /// <param name="configPath">This identfes the option uniquely. This can be just a string (e.g."Terrorist") or it can be a two level tree ("Colors/Terrorist").</param>
        /// <param name="defaultRed">The red value of the color set by the user.</param>
        /// <param name="defaultBlue">The green value of the color set by the user.</param>
        /// <param name="defaultGreen">The green value of the color set by the user.</param>
        public static bool GetConfigOption(string configPath, ref int defaultRed, ref int defaultGreen, ref int defaultBlue) =>
            dLed.LogiLedGetConfigOptionColor(configPath, ref defaultRed, ref defaultBlue, ref defaultGreen);

        public static bool GetConfigOption(string configPath, StringBuilder buffer, int bufsize) =>
            dLed.LogiLedGetConfigOptionKeyInput(configPath, buffer, bufsize);
        /////////////////////

        /// <summary>
        /// Sets the target device type for future calls.
        /// The default target device is LOGI_DEVICETYPE_ALL, therefore, if no call is made to LogiLedSetTargetDevice the SDK will apply any function to all the connected devices.
        /// </summary>
        public static bool SetTargetDevice(int targetDevice) => dLed.LogiLedSetTargetDevice(targetDevice);

        /// <summary>
        /// Retrieves the version of the SDK version installed on the user’s system.
        /// </summary>
        /// <param name="majorNum">Will be set to the major build number.</param>
        /// <param name="minorNum">Will be set to the minor build number.</param>
        /// <param name="buildNum">Will be set to the patch build number.</param>
        /// <returns>False if there is no SDK installed on the system, or the version could not be retrieved. Otherwise true.</returns>
        public static bool GetSdkVersion(ref int majorNum, ref int minorNum, ref int buildNum) =>
            dLed.LogiLedGetSdkVersion(ref majorNum, ref minorNum, ref buildNum);

        /// <summary>
        /// Saves the current lighting so that it can be restored after a temporary effect is finished.
        /// </summary>
        public static bool SaveCurrentLighting() => dLed.LogiLedSaveCurrentLighting();

        /// <summary>
        /// Sets the lighting on connected and supported devices.
        /// </summary>
        /// <param name="redPercentage">Percentage of red, range is 0-100</param>
        /// <param name="greenPercentage">Percentage of green, range is 0-100</param>
        /// <param name="bluePercentage">Percentage of blue, range is 0-100</param>
        public static bool SetLighting(int redPercentage, int greenPercentage, int bluePercentage) =>
            dLed.LogiLedSetLighting(redPercentage, greenPercentage, bluePercentage);

        public static bool SetLighting(int brightness) {
            int bri = brightness - 1;
            if (bri < 0) bri = brightness;
            return dLed.LogiLedSetLighting(bri, brightness, bri);
        }

        /// <summary>
        /// Restores the last saved lighting. It should be called after a temporary effect is finished.
        /// </summary>
        public static bool RestoreLighting() => dLed.LogiLedRestoreLighting();

        public static bool FlashLighting(int redPercentage, int greenPercentage, int bluePercentage, int milliSecondsDuration, int milliSecondsInterval) =>
            dLed.LogiLedFlashLighting(redPercentage, greenPercentage, bluePercentage, milliSecondsDuration, milliSecondsInterval);

        public static bool PulseLighting(int redPercentage, int greenPercentage, int bluePercentage, int milliSecondsDuration, int milliSecondsInterval) =>
            dLed.LogiLedPulseLighting(redPercentage, greenPercentage, bluePercentage, milliSecondsDuration, milliSecondsInterval);

        public static bool StopEffects() => dLed.LogiLedStopEffects();

        public static bool ExcludeKeysFromBitmap(KeyCode[] keyList, int listCount) =>
            dLed.LogiLedExcludeKeysFromBitmap(keyList, listCount);

        public static bool SetLightingFromBitmap(byte[] bitmap) =>
            dLed.LogiLedSetLightingFromBitmap(bitmap);

        public static bool SetLightingForKeyWithScanCode(int keyCode, int redPercentage, int greenPercentage, int bluePercentage) =>
            dLed.LogiLedSetLightingForKeyWithScanCode(keyCode, redPercentage, greenPercentage, bluePercentage);

        public static bool SetLightingForKeyWithHidCode(int keyCode, int redPercentage, int greenPercentage, int bluePercentage) =>
            dLed.LogiLedSetLightingForKeyWithHidCode(keyCode, redPercentage, greenPercentage, bluePercentage);

        public static bool SetLightingForKeyWithQuartzCode(int keyCode, int redPercentage, int greenPercentage, int bluePercentage) =>
            dLed.LogiLedSetLightingForKeyWithQuartzCode(keyCode, redPercentage, greenPercentage, bluePercentage);

        /// <summary>
        /// Sets the key identified by the code passed as parameter to the desired color. This function only affects per-key backlighting featured connected devices.
        /// </summary>
        /// <param name="keyCode"></param>
        /// <param name="redPercentage"></param>
        /// <param name="greenPercentage"></param>
        /// <param name="bluePercentage"></param>
        public static bool SetLightingForKeyWithKeyName(KeyCode keyCode, int redPercentage, int greenPercentage, int bluePercentage) =>
            dLed.LogiLedSetLightingForKeyWithKeyName(keyCode, redPercentage, greenPercentage, bluePercentage);

        public static bool SaveLightingForKey(KeyCode keyName) => dLed.LogiLedSaveLightingForKey(keyName);

        public static bool RestoreLightingForKey(KeyCode keyName) => dLed.LogiLedRestoreLightingForKey(keyName);

        public static bool FlashSingleKey(KeyCode keyName, int redPercentage, int greenPercentage, int bluePercentage, int msDuration, int msInterval) =>
            dLed.LogiLedFlashSingleKey(keyName, redPercentage, greenPercentage, bluePercentage, msDuration, msInterval);

        public static bool PulseSingleKey(KeyCode keyName, int startRedPercentage, int startGreenPercentage, int startBluePercentage, int finishRedPercentage,
            int finishGreenPercentage, int finishBluePercentage, int msDuration, bool isInfinite) =>
        dLed.LogiLedPulseSingleKey(keyName, startRedPercentage, startGreenPercentage, startBluePercentage,
                                   finishRedPercentage, finishGreenPercentage, finishBluePercentage, msDuration, isInfinite);

        public static bool StopEffectsOnKey(KeyCode keyName) => dLed.LogiLedStopEffectsOnKey(keyName);

        public static bool SetLightingForTargetZone(DeviceType deviceType, int zone, int redPercentage, int greenPercentage, int bluePercentage) =>
            dLed.LogiLedSetLightingForTargetZone(deviceType, zone, redPercentage, greenPercentage, bluePercentage);

        public static void Shutdown() { dLed.LogiLedShutdown(); }

        /// <summary>
        /// Sets the lighting on connected and supported devices, simulating behavior of monochrome keyboards.
        /// </summary>
        /// <param name="redPercentage">Percentage of red, range is 0-100</param>
        /// <param name="greenPercentage">Percentage of green, range is 0-100</param>
        /// <param name="bluePercentage">Percentage of blue, range is 0-100</param>
        public static bool SetMonochromeLighting(int redPercentage, int greenPercentage, int bluePercentage) {
            int highest = 0;
            if (redPercentage > greenPercentage && redPercentage > bluePercentage) highest = redPercentage;
            else if (greenPercentage > redPercentage && greenPercentage > bluePercentage) highest = greenPercentage;
            else if (bluePercentage > greenPercentage && bluePercentage > redPercentage) highest = bluePercentage;
            else if (bluePercentage == greenPercentage && bluePercentage == redPercentage) highest = bluePercentage;

            return dLed.LogiLedSetLighting(highest, highest, highest);
        }
    }

}
