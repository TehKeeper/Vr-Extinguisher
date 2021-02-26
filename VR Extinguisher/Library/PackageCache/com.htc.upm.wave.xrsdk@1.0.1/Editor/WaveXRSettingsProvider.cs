using UnityEditor;
using UnityEngine;

namespace Wave.XR
{
    class WaveXRSettingsProvider : SettingsProvider
    {
        public WaveXRSettingsProvider(string path, SettingsScope scope = SettingsScope.Project)
            : base(path, scope)
        {
        }

        public override void OnGUI(string searchContext)
        {
            EditorGUILayout.HelpBox("Recommended project settings for Wave:", MessageType.Info);
            if (GUILayout.Button("PlayerSettings Configure Dialog"))
                WaveXRPlayerSettingsConfigDialog.ShowDialog();
        }

        [SettingsProvider]
        static SettingsProvider Create()
        {
            return new WaveXRSettingsProvider("Project/Wave XR");
        }
    }
}