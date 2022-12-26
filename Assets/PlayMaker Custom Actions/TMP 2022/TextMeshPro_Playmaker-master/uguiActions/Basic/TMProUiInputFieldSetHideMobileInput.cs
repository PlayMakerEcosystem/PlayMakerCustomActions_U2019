﻿// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("TextMesh Pro UGUI Basic")]
	[Tooltip("Sets the Hide Mobile Input property of a UI InputField component.")]
	public class TMProUiInputFieldSetHideMobileInput : ComponentAction<TMPro.TMP_InputField>
	{
		[RequiredField]
		[CheckForComponent(typeof(TMPro.TMP_InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.TextArea)]
		[Tooltip("The Hide Mobile Input flag value of the UI InputField component.")]
		public FsmBool hideMobileInput;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		private TMPro.TMP_InputField inputField;

	    private bool originalValue;

		public override void Reset()
		{
			gameObject = null;
			hideMobileInput = null;
			resetOnExit = null;
		}
		
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(go))
			{
				inputField = cachedComponent;
			}

			originalValue = inputField.shouldHideMobileInput;

			DoSetValue();

			Finish();
		}

	    private void DoSetValue()
		{
			if (inputField != null)
			{
				inputField.shouldHideMobileInput = hideMobileInput.Value;
			}
		}

		public override void OnExit()
		{
			if (inputField == null) return;
			
			if (resetOnExit.Value)
			{
				inputField.shouldHideMobileInput = originalValue;
			}
		}
	}
}