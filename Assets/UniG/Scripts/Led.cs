using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UniG.Direct;
using UnityEngine;

namespace UniG {
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
