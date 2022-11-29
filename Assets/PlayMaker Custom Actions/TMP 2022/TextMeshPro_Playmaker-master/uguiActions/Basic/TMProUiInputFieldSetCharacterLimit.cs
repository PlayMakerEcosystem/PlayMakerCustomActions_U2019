// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("TextMesh Pro UGUI Basic")]
	[Tooltip("Sets the maximum number of characters that the user can type into a UI InputField component. Optionally reset on exit")]
	public class TMProUiInputFieldSetCharacterLimit : ComponentAction<TMPro.TMP_InputField>
	{
		[RequiredField]
		[CheckForComponent(typeof(TMPro.TMP_InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The maximum number of characters that the user can type into the UI InputField component. 0 = infinite")]
		public FsmInt characterLimit;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		private TMPro.TMP_InputField inputField;
	    private int originalValue;

		public override void Reset()
		{
			gameObject = null;
			characterLimit = null;
			resetOnExit = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(go))
			{
				inputField = cachedComponent;
			}

			originalValue = inputField.characterLimit;

			DoSetValue();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoSetValue();
		}

	    private void DoSetValue()
		{
			if (inputField != null)
			{
				inputField.characterLimit = characterLimit.Value;
			}
		}

		public override void OnExit()
		{
			if (inputField == null) return;
			
			if (resetOnExit.Value)
			{
				inputField.characterLimit = originalValue;
			}
		}
	}
}