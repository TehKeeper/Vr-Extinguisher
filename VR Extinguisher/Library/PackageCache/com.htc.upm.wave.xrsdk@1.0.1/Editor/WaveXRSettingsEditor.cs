using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Wave.XR.Constants;

namespace Wave.XR.Settings
{
    /// <summary>
    /// Simple custom editor used to show how to enable custom UI for XR Management
    /// configuraton data.
    /// </summary>
    [CustomEditor(typeof(WaveXRSettings))]
    public class WaveXRSettingsEditor : UnityEditor.Editor
    {
        static string PropertyName_PreferedStereoRenderingPath = "preferedStereoRenderingPath";
        static GUIContent Label_PreferedStereoRenderingPath = new GUIContent("Render Mode");
        SerializedProperty Property_PreferedStereoRenderingPath;

        static string PropertyName_UseDoubleWidth = "useDoubleWidth";
        static GUIContent Label_UseDoubleWidth = new GUIContent("Use DoubleWidth eye texture");
        SerializedProperty Property_UseDoubleWidth;

        static string PropertyName_UseAdaptiveQuality = "useAdaptiveQuality";
        static GUIContent Label_UseAdaptiveQuality = new GUIContent("Adaptive Quality");
        SerializedProperty Property_UseAdaptiveQuality;

        static string PropertyName_AQSendEvent = "AQ_SendQualityEvent";
        static GUIContent Label_AQSendEvent = new GUIContent("Send Quality Event");
        SerializedProperty Property_AQSendEvent;

        static string PropertyName_AQAutoFoveation = "AQ_AutoFoveation";
        static GUIContent Label_AQAutoFoveation = new GUIContent("Auto Foveation");
        SerializedProperty Property_AQAutoFoveation;

        static string PropertyName_UseRenderMask = "useRenderMask";
        static GUIContent Label_UseRenderMask = new GUIContent("RenderMask");
        SerializedProperty Property_UseRenderMask;

        static string PropertyName_UseAQDR = "useAQDynamicResolution";
        static GUIContent Label_UseAQDR = new GUIContent("Dynamic Resolution");
        SerializedProperty Property_UseAQDR;

        static string PropertyName_DefaultIndex = "DR_DefaultIndex";
        static GUIContent Label_DefaultIndex = new GUIContent("Default Index");
        SerializedProperty Property_DefaultIndex;

        static string PropertyName_TextSize = "DR_TextSize";
        static GUIContent Label_TextSize = new GUIContent("Text Size");
        SerializedProperty Property_TextSize;

        static string PropertyName_ResolutionScaleList = "DR_ResolutionScaleList";
        static GUIContent Label_ResolutionScaleList = new GUIContent("Resolution List");
        SerializedProperty Property_ResolutionScaleList;

        static string PropertyName_FoveationMode = "foveationMode";
        static GUIContent Label_FoveationMode = new GUIContent("Foveation mode");
        SerializedProperty Property_FoveationMode;

        static string PropertyName_LeftClearVisionFOV = "leftClearVisionFOV";
        static GUIContent Label_LeftClearVisionFOV = new GUIContent("LeftClearVisionFOV");
        SerializedProperty Property_LeftClearVisionFOV;

        static string PropertyName_RightClearVisionFOV = "rightClearVisionFOV";
        static GUIContent Label_RightClearVisionFOV = new GUIContent("RightClearVisionFOV");
        SerializedProperty Property_RightClearVisionFOV;

        static string PropertyName_LeftPeripheralQuality = "leftPeripheralQuality";
        static GUIContent Label_LeftPeripheralQuality = new GUIContent("LeftPeripheralQuality");
        SerializedProperty Property_LeftPeripheralQuality;

        static string PropertyName_RightPeripheralQuality = "rightPeripheralQuality";
        static GUIContent Label_RightPeripheralQuality = new GUIContent("RightPeripheralQuality");
        SerializedProperty Property_RightPeripheralQuality;

        static string PropertyName_OverridePixelDensity = "overridePixelDensity";
        static GUIContent Label_OverridePixelDensity = new GUIContent("Override system PixelDensity");
        SerializedProperty Property_OverridePixelDensity;

        static string PropertyName_PixelDensity = "pixelDensity";
        static GUIContent Label_PixelDensity = new GUIContent("PixelDensity");
        SerializedProperty Property_PixelDensity;

        static string PropertyName_ResolutionScale = "resolutionScale";
        static GUIContent Label_ResolutionScale = new GUIContent("ResolutionScale");
        SerializedProperty Property_ResolutionScale;

        static string PropertyName_LogFlagForNative = "debugLogFlagForNative";
        static GUIContent Label_LogFlagForNative = new GUIContent("LogFlagForNative");
        SerializedProperty Property_LogFlagForNative;

        static string PropertyName_OverrideLogFlag = "overrideLogFlagForNative";
        static GUIContent Label_OverrideLogFlag = new GUIContent("Override LogFlag");
        SerializedProperty Property_OverrideLogFlag;

        enum Platform
        {
            Standalone,
            Android
        }

        /// <summary>Override of Editor callback.</summary>
        public override void OnInspectorGUI()
        {
            if (serializedObject == null || serializedObject.targetObject == null)
                return;

            BuildTargetGroup selectedBuildTargetGroup = EditorGUILayout.BeginBuildTargetSelectionGrouping();
            if (selectedBuildTargetGroup == BuildTargetGroup.Android)
            {
				AndroidSettings();
            }

            if (selectedBuildTargetGroup == BuildTargetGroup.Standalone)
            {
                EditorGUILayout.LabelField("Standalone specific things");
            }

            EditorGUILayout.EndBuildTargetSelectionGrouping();

            if (GUILayout.Button("Remove Settings"))
                RemoveSettings();
        }

        bool foldoutRendering = true;
        bool foldoutCommon = true;

		public void AndroidSettings()
        {
            if (Property_PreferedStereoRenderingPath == null) Property_PreferedStereoRenderingPath = serializedObject.FindProperty(PropertyName_PreferedStereoRenderingPath);
            if (Property_UseDoubleWidth == null) Property_UseDoubleWidth = serializedObject.FindProperty(PropertyName_UseDoubleWidth);
            if (Property_UseAdaptiveQuality == null) Property_UseAdaptiveQuality = serializedObject.FindProperty(PropertyName_UseAdaptiveQuality);
            if (Property_AQSendEvent == null) Property_AQSendEvent = serializedObject.FindProperty(PropertyName_AQSendEvent);
            if (Property_AQAutoFoveation == null) Property_AQAutoFoveation = serializedObject.FindProperty(PropertyName_AQAutoFoveation);
            if (Property_UseRenderMask == null) Property_UseRenderMask = serializedObject.FindProperty(PropertyName_UseRenderMask);
            if (Property_UseAQDR == null) Property_UseAQDR = serializedObject.FindProperty(PropertyName_UseAQDR);
            if (Property_ResolutionScaleList == null) Property_ResolutionScaleList = serializedObject.FindProperty(PropertyName_ResolutionScaleList);
            if (Property_TextSize == null) Property_TextSize = serializedObject.FindProperty(PropertyName_TextSize);
            if (Property_DefaultIndex == null) Property_DefaultIndex = serializedObject.FindProperty(PropertyName_DefaultIndex);

            if (Property_FoveationMode == null) Property_FoveationMode = serializedObject.FindProperty(PropertyName_FoveationMode);
            if (Property_LeftClearVisionFOV == null) Property_LeftClearVisionFOV = serializedObject.FindProperty(PropertyName_LeftClearVisionFOV);
            if (Property_RightClearVisionFOV == null) Property_RightClearVisionFOV = serializedObject.FindProperty(PropertyName_RightClearVisionFOV);
            if (Property_LeftPeripheralQuality == null) Property_LeftPeripheralQuality = serializedObject.FindProperty(PropertyName_LeftPeripheralQuality);
            if (Property_RightPeripheralQuality == null) Property_RightPeripheralQuality = serializedObject.FindProperty(PropertyName_RightPeripheralQuality);

            if (Property_OverridePixelDensity == null) Property_OverridePixelDensity = serializedObject.FindProperty(PropertyName_OverridePixelDensity);
            if (Property_PixelDensity == null) Property_PixelDensity = serializedObject.FindProperty(PropertyName_PixelDensity);
            if (Property_ResolutionScale == null) Property_ResolutionScale = serializedObject.FindProperty(PropertyName_ResolutionScale);

            if (Property_LogFlagForNative == null) Property_LogFlagForNative = serializedObject.FindProperty(PropertyName_LogFlagForNative);
            if (Property_OverrideLogFlag == null) Property_OverrideLogFlag = serializedObject.FindProperty(PropertyName_OverrideLogFlag);
            
            bool guiEnableLastCond = false;

            foldoutRendering = EditorGUILayout.Foldout(foldoutRendering, "Rendering");
            if (foldoutRendering)
            {
                EditorGUI.indentLevel++;

                EditorGUILayout.PropertyField(Property_PreferedStereoRenderingPath, Label_PreferedStereoRenderingPath);

#if false
                // Double Width is an experimental feature.
                if (((WaveXRSettings.StereoRenderingPath)Property_PreferedStereoRenderingPath.intValue) == WaveXRSettings.StereoRenderingPath.MultiPass)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(Property_UseDoubleWidth, Label_UseDoubleWidth);
                    EditorGUI.indentLevel--;
                }
#endif

                AdaptiveQualityGUI();

                EditorGUILayout.PropertyField(Property_UseRenderMask, Label_UseRenderMask);

                EditorGUILayout.PropertyField(Property_FoveationMode, Label_FoveationMode);
                guiEnableLastCond = GUI.enabled;
                GUI.enabled = Property_FoveationMode.enumValueIndex == (int)WaveXRSettings.FoveationMode.Enable && guiEnableLastCond;
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(Property_LeftClearVisionFOV, Label_LeftClearVisionFOV);
                EditorGUILayout.PropertyField(Property_RightClearVisionFOV, Label_RightClearVisionFOV);
                EditorGUILayout.PropertyField(Property_LeftPeripheralQuality, Label_LeftPeripheralQuality);
                EditorGUILayout.PropertyField(Property_RightPeripheralQuality, Label_RightPeripheralQuality);
                EditorGUI.indentLevel--;
                GUI.enabled = true;

                EditorGUILayout.PropertyField(Property_OverridePixelDensity, Label_OverridePixelDensity);
                GUI.enabled = Property_OverridePixelDensity.boolValue;
                EditorGUILayout.PropertyField(Property_PixelDensity, Label_PixelDensity);
                GUI.enabled = true;
                EditorGUILayout.PropertyField(Property_ResolutionScale, Label_ResolutionScale);

                EditorGUI.indentLevel--;
            }

            EditorGUILayout.Space();

            foldoutCommon = EditorGUILayout.Foldout(foldoutCommon, "Common");
            if (foldoutCommon)
            {
                EditorGUI.indentLevel++;
                LogFlagGUI();
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.Space();

            serializedObject.ApplyModifiedProperties();
        }

        void AdaptiveQualityGUI()
        {
            bool guiEnableLastCond = false;

            // AQ
            EditorGUILayout.PropertyField(Property_UseAdaptiveQuality, Label_UseAdaptiveQuality);
            GUI.enabled = Property_UseAdaptiveQuality.boolValue;
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(Property_AQSendEvent, Label_AQSendEvent);
            EditorGUILayout.PropertyField(Property_AQAutoFoveation, Label_AQAutoFoveation);
            EditorGUI.indentLevel--;

            // DR
            guiEnableLastCond = GUI.enabled;
#if WAVE_ESSENCE
            GUI.enabled = Property_UseAdaptiveQuality.boolValue && Property_AQSendEvent.boolValue && guiEnableLastCond;
#else
            GUI.enabled = false;
            Property_UseAQDR.boolValue = false;
#endif
            EditorGUILayout.PropertyField(Property_UseAQDR, Label_UseAQDR);
            if (Property_UseAQDR.boolValue)
            {
				EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(Property_TextSize, Label_TextSize);

				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField(Property_ResolutionScaleList, Label_ResolutionScaleList);
				//Validate values of resolution scale list
				if (EditorGUI.EndChangeCheck())
				{
					if (Property_ResolutionScaleList.arraySize < 2)
					{
						Property_ResolutionScaleList.arraySize = 2;
						Property_ResolutionScaleList.GetArrayElementAtIndex(0).floatValue = 1f;
						Property_ResolutionScaleList.GetArrayElementAtIndex(1).floatValue = 0.1f;
					}
				}

				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField(Property_DefaultIndex, Label_DefaultIndex);
				//Validate values of default index
				if (EditorGUI.EndChangeCheck())
				{
					if (Property_DefaultIndex.intValue >= Property_ResolutionScaleList.arraySize)
					{
						Property_DefaultIndex.intValue = Property_ResolutionScaleList.arraySize - 1;
					}

					if (Property_DefaultIndex.intValue < 0)
					{
						Property_DefaultIndex.intValue = 0;
					}
				}

				EditorGUI.indentLevel--;
            }
            GUI.enabled = true;
        }

        static string[] LogTitleList =
        {
            "Basic", "Debug", "Lifecyle", "Render", "Input",
        };
        static bool foldoutLog = false;

        void LogFlagGUI()
        {
            EditorGUILayout.PropertyField(Property_OverrideLogFlag, Label_OverrideLogFlag);
            foldoutLog = EditorGUILayout.Foldout(foldoutLog, "LogFlag");
            if (foldoutLog)
            {
                EditorGUI.indentLevel++;
                GUI.enabled = Property_OverrideLogFlag.boolValue;
                int flags = Property_LogFlagForNative.intValue;

                for (int i = 0; i < 5; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(LogTitleList[i], GUILayout.Width(80));
                    for (int j = 0; j < 4; j++)
                    {
                        int bit = i * 4 + j;
                        int mask = 1 << bit;
                        bool flag = (flags & mask) > 0;
                        bool ret = GUILayout.Button(flag ? "X" : " ", GUILayout.Width(20));
                        if (ret)
                            flags = !flag ? (flags | mask) : (flags & ~mask);
                    }
                    EditorGUILayout.EndHorizontal();
                }
                Property_LogFlagForNative.intValue = flags;
                EditorGUILayout.PropertyField(Property_LogFlagForNative, Label_LogFlagForNative);
                GUI.enabled = true;
                EditorGUI.indentLevel--;
            }
        }

        public static void RemoveSettings()
        {
            // I don't know why the system will remember the old asset in memory.  Use this method to clean them.
            AssetDatabase.DeleteAsset("Assets/XR/Settings/Wave XR Settings.asset");
            EditorBuildSettings.RemoveConfigObject(k_SettingsKey);
        }

#if false
        [MenuItem("WaveVR/XRSDK/Create WaveXRSettings Assets")]
        public static void CreateWaveXRSettingsAssets()
        {
            string path = EditorUtility.SaveFilePanel("Create WaveXRSettings Asset", "Assets/", "WaveXRSettings", "asset");
            if (string.IsNullOrEmpty(path)) return;

            path = FileUtil.GetProjectRelativePath(path);
            WaveXRSettings instance = CreateInstance<WaveXRSettings>();

            AssetDatabase.CreateAsset(instance, path);
            AssetDatabase.SaveAssets();
        }
#endif
    }

}
