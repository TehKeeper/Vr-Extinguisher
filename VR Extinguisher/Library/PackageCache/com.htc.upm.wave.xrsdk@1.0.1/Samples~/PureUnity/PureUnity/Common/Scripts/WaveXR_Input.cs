using UnityEngine;
using UnityEngine.EventSystems;

namespace Wave.XR.Sample
{
	public class WaveXR_Input : BaseInput
	{
		public override bool mousePresent
		{
			get { return true; }
		}

		protected override void Awake()
		{
			StandaloneInputModule standaloneInputModule = FindObjectOfType<StandaloneInputModule>();
			if (standaloneInputModule) standaloneInputModule.inputOverride = this;
		}

		public override Vector2 mouseScrollDelta
		{
			get { return Vector2.zero; }
		}

		public override Vector2 mousePosition
		{
			get { return new Vector2(UnityEngine.XR.XRSettings.eyeTextureWidth / 2f, UnityEngine.XR.XRSettings.eyeTextureHeight / 2f); }
		}

		public override bool GetMouseButton(int button)
		{
			if (button != 0)
				return Input.GetMouseButton(button);

			return Input.GetMouseButton(button) || Input.GetButton("Button0") || Input.GetButton("Button2") || Input.GetButton("Button8") || Input.GetButton("Button9");
		}

		public override bool GetMouseButtonDown(int button)
		{
			if (button != 0)
				return Input.GetMouseButtonDown(button);

			return Input.GetMouseButtonDown(button) || Input.GetButtonDown("Button0") || Input.GetButtonDown("Button2") || Input.GetButtonDown("Button8") || Input.GetButtonDown("Button9");
		}

		public override bool GetMouseButtonUp(int button)
		{
			if (button != 0)
				return Input.GetMouseButtonDown(button);

			return Input.GetMouseButtonDown(button) || Input.GetButtonUp("Button0") || Input.GetButtonUp("Button2") || Input.GetButtonUp("Button8") || Input.GetButtonUp("Button9");
		}

		public override bool GetButtonDown(string buttonName)
		{
			//if (buttonName == "Submit")
			//	return Input.GetButtonDown("Button0") || Input.GetButtonDown("Button2") || Input.GetButtonDown("Button8") || Input.GetButtonDown("Button9");
			//if (buttonName == "Cancel")
			//	return Input.GetButtonDown("Button1") || Input.GetButtonDown("Button3") || Input.GetButtonDown("Button14") || Input.GetButtonDown("Button15");
			return false;
		}

		public override bool touchSupported
		{
			get { return false; }
		}
	}
}
