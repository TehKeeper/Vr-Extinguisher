﻿using System.Runtime.InteropServices;
using UnityEngine;
using Wave.XR.Loader;
using static Wave.XR.Constants;

namespace Wave.XR.Settings
{
    public class SettingsHelper
    {
        internal static void Process(WaveXRLoader loader)
        {
#if UNITY_EDITOR
            if (Application.isEditor)
                return;
#endif
            WaveXRSettings appSettings = WaveXRSettings.GetInstance();

            if (appSettings == null)
            {
                Debug.LogWarning("WaveXRSettings instance is null");
                return;
            }

            #region common
            uint logFlagForNative = appSettings.overrideLogFlagForNative ?
                appSettings.debugLogFlagForNative : (uint)DebugLogFlag.Default;
            SetInt(NameDebugLogFlagForNative, logFlagForNative);
            GetInt(NameDebugLogFlagForUnity, ref appSettings.debugLogFlagForUnity);
            #endregion common

            #region rendering
            loader.displaySubsystem.singlePassRenderingDisabled = appSettings.preferedStereoRenderingPath == WaveXRSettings.StereoRenderingPath.MultiPass;

            SetBool("sRGB", QualitySettings.activeColorSpace == ColorSpace.Linear);
            SetInt("qsMSAA", (uint) QualitySettings.antiAliasing);
            SetBool("useDoubleWidth", appSettings.useDoubleWidth);
            SetBool(NameUseRenderMask, appSettings.useRenderMask);

            SetBool(NameUseAdaptiveQuality, appSettings.useAdaptiveQuality);
            SetBool("AQ_AutoFoveation", appSettings.AQ_AutoFoveation);
            SetBool("AQ_SendQualityEvent", appSettings.AQ_SendQualityEvent);
            SetBool("useAQDynamicResolution", appSettings.useAQDynamicResolution);

            SetInt(NameFoveationMode, (uint)appSettings.foveationMode);
            SetFloat(NameLeftClearVisionFOV, appSettings.leftClearVisionFOV);
            SetFloat(NameRightClearVisionFOV, appSettings.rightClearVisionFOV);
            SetInt(NameLeftPeripheralQuality, (uint)appSettings.leftPeripheralQuality);
            SetInt(NameRightPeripheralQuality, (uint)appSettings.rightPeripheralQuality);

            SetBool(NameOverridePixelDensity, appSettings.overridePixelDensity);
            SetFloat(NamePixelDensity, appSettings.pixelDensity);
            SetFloat(NameResolutionScale, appSettings.resolutionScale);
            #endregion rendering
        }

        public const string NameSRGB = "sRGB";
        public const string NameUseRenderMask = "useRenderMask";
        public const string NameUseAdaptiveQuality = "useAdaptiveQuality";

        public const string NameFoveationMode = "foveationMode";
        public const string NameLeftClearVisionFOV = "leftClearVisionFOV";
        public const string NameRightClearVisionFOV = "rightClearVisionFOV";
        public const string NameLeftPeripheralQuality = "leftPeripheralQuality";
        public const string NameRightPeripheralQuality = "rightPeripheralQuality";

        public const string NameOverridePixelDensity = "overridePixelDensity";
        public const string NamePixelDensity = "pixelDensity";
        public const string NameResolutionScale = "resolutionScale";
               
        public const string NameDebugLogFlagForNative = "debugLogFlagForNative";
        public const string NameDebugLogFlagForUnity = "debugLogFlagForUnity";

        // Set
        [DllImport("wvrunityxr", EntryPoint = "SettingsSetBool")]
        public static extern ErrorCode SetBool(string name, bool value);

        [DllImport("wvrunityxr", EntryPoint = "SettingsSetInt")]
        public static extern ErrorCode SetInt(string name, uint value);

        [DllImport("wvrunityxr", EntryPoint = "SettingsSetFloat")]
        public static extern ErrorCode SetFloat(string name, float value);

        [DllImport("wvrunityxr", EntryPoint = "SettingsSetString")]
        internal static extern ErrorCode SetFloatArray(string name, [In, Out] float[] array, uint length);
        public static ErrorCode SetFloatArray(string name, float[] array)
        {
            return SetFloatArray(name, array, (uint)array.Length);
        }

        [DllImport("wvrunityxr", EntryPoint = "SettingsSetString")]
        internal static extern ErrorCode SetString(string name, System.IntPtr value, uint length);
        public static ErrorCode SetString(string name, string value) {
            System.IntPtr ptrValue = Marshal.StringToHGlobalAnsi(value);
            ErrorCode ret = SetString(name, ptrValue, (uint)value.Length);
            Marshal.FreeHGlobal(ptrValue);
            return ret;
        }

        // Get
        [DllImport("wvrunityxr", EntryPoint = "SettingsGetBool")]
        public static extern ErrorCode GetBool(string name, ref bool value);

        [DllImport("wvrunityxr", EntryPoint = "SettingsGetInt")]
        public static extern ErrorCode GetInt(string name, ref uint value);

        [DllImport("wvrunityxr", EntryPoint = "SettingsGetFloat")]
        public static extern ErrorCode GetFloat(string name, ref float value);

        [DllImport("wvrunityxr", EntryPoint = "SettingsGetFloatArray")]
        internal static extern ErrorCode GetFloatArray(string name, ref float [] array, uint bufferSize);
        public static ErrorCode GetFloatArray(string name, ref float[] array)
        {
            uint length = (uint) array.Length;
            if (length > 0xFFFF)
                return ErrorCode.OutOfRange;
            return GetFloatArray(name, ref array, length);  // Native can handle length validation
        }

        [DllImport("wvrunityxr", EntryPoint = "SettingsSetString")]
        public static extern ErrorCode GetString(string name, System.IntPtr value, uint bufferSize);
        public static ErrorCode GetString(string name, ref string value, uint bufferSize)
        {
            if (bufferSize > 0xFFFF)
                return ErrorCode.OutOfRange;
            System.IntPtr ptrValue = Marshal.AllocHGlobal((int)bufferSize);
            ErrorCode ret = GetString(name, ptrValue, (uint)value.Length);
            value = Marshal.PtrToStringAnsi(ptrValue);
            Marshal.FreeHGlobal(ptrValue);
            return ret;
        }
    }
}
