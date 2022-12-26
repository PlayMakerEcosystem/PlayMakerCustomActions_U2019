﻿// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("TextMesh Pro UGUI Basic")]
	[Tooltip("Sets the caret's blink rate of a UI InputField component.")]
	public class TMProUiInputFieldSetCaretBlinkRate : ComponentAction<TMPro.TMP_InputField>
	{
		[RequiredField]
		[CheckForComponent(typeof(TMPro.TMP_InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The caret's blink rate for the UI InputField component.")]
		public FsmInt caretBlinkRate;

		[Tooltip("Deactivate when exiting this state.")]
		public FsmBool resetOnExit;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		private TMPro.TMP_InputField inputField;
	    private float originalValue;

		public override void Reset()
		{
			gameObject = null;
			caretBlinkRate = null;
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

			originalValue = inputField.caretBlinkRate;

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
				inputField.caretBlinkRate = caretBlinkRate.Value;
			}
		}

		public override void OnExit()
		{
			if (inputField == null) return;
			
			if (resetOnExit.Value)
			{
				inputField.caretBlinkRate = originalValue;
			}
		}
	}
}