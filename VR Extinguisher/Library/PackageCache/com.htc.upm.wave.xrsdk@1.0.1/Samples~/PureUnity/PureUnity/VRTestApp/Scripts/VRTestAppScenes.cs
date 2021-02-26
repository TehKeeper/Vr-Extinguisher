using System;
using System.Collections.Generic;
using UnityEngine;

namespace Wave.Generic.Sample
{
	[Serializable]
	public struct SceneData
	{
		public SceneData(string n, string p, bool h = false, bool pc = true)
		{
			name = n;  // if has name, is entry
			path = p;
			isEntry = !string.IsNullOrEmpty(n);
			hasHelp = h;
			onPC = pc;
		}

		public string name;
		public string path;
		public bool isEntry;
		public bool hasHelp;
		public bool onPC;
	}

	[Serializable]
	public class VRTestAppScenes : ScriptableObject
	{
		public static VRTestAppScenes instance = null;
		public static VRTestAppScenes Instance
		{
			get
			{
#if UNITY_EDITOR
				if (instance == null)
				{
					instance = new VRTestAppScenes();
					instance.scenesData = GetPredefinedScenesData();
				}
#endif
				return instance;
			}
		}

		public List<string> pathes = new List<string>();
		public List<SceneData> scenesData = new List<SceneData>();

		// For showing your scene in VRTestApp, you need add your scenes into this function's return list.
		public static List<SceneData> GetPredefinedScenesData()
		{
			/**
			 *  Define title ###
			 *    2XX is for Render Team's test No.XX
			 *    3XX is for Engine Team's test No.XX
			 *    000 is undefined test case.  Maybe the test scene is from sample.
			**/
			return new List<SceneData>() {
				new SceneData("", "VRTestApp"),  // VRTestApp Loader
				new SceneData("301 SeaOfCubes", "SeaOfCubeMain", true),
				new SceneData("", "SeaOfCubeWithTwoHead", true),
				new SceneData("000 RenderMask Test", "RenderMask_Test"),

				//new SceneData("103 InteractionMode Test", "InteractionMode_Test"),
				new SceneData("304 WaveControllerTest", "WaveControllerTest"),
				new SceneData("000 StereoRenderMode", "StereoRenderMode"),

				// Not on PC
				new SceneData("302 CameraTextureTest_DisableSyncPose", "CameraTextureTest_DisableSyncPose", false, false),
				new SceneData("303 PermissionMgrTest", "PermissionMgrTest", false, false),
				new SceneData("207 Foveation Test", "FoveatedTest", false, false),
				new SceneData("209 AQDR_Test", "AQDR_Test", false, false),
				new SceneData("", "AQDR_Loading"),

				new SceneData("305 Gaze Input", "GazeInput"),
				new SceneData("306 Controller Input", "ControllerInput"),
				new SceneData("307 Button Test", "ButtonTest"),

				new SceneData("000 Movie Mode (no exit)", "MovieMode", false, false),
				new SceneData("000 RenderDoc", "RenderDocSample", false, false),
				new SceneData("300 MSAA On/Off", "MSAAOnOffTest", false, false),
			};
		}

#if false
		public static List<string> GetScenePathes(VRTestAppScenes asset, bool isPC = false)
		{
			int N = asset.scenesData.Count;
			List<string> o = new List<string>(N);
			for (int i = 0; i < N; i++)
			{
				// Skip non PC scene
				if (isPC && !asset.scenesData[i].onPC)
					continue;

				string path = asset.scenesData[i].path;
				if (!File.Exists(path))
				{
					Debug.LogWarning("Drop lost scene: " + path);
					continue;
				}
				o.Add(path);

				if (asset.scenesData[i].hasHelp)
				{
					string helpPath = path.Remove(path.Length - 6);
					helpPath += "_Help";
					o.Add(helpPath);
				}
			}
			return o;
		}
#endif

		private void Awake()
		{
			Debug.Log("VRTestAppScenes preload asset loaded");
			instance = this;
		}
	}
}
