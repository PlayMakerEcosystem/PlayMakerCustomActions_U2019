﻿// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("TextMesh Pro UGUI Basic")]
	[Tooltip("Gets the Hide Mobile Input value of a UI InputField component.")]
	public class TMProUiInputFieldGetHideMobileInput : ComponentAction<TMPro.TMP_InputField>
	{
		[RequiredField]
		[CheckForComponent(typeof(TMPro.TMP_InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the Hide Mobile flag value of the UI InputField component.")]
		public FsmBool hideMobileInput;

		[Tooltip("Event sent if hide mobile input property is true")]
		public FsmEvent mobileInputHiddenEvent;

		[Tooltip("Event sent if hide mobile input property is false")]
		public FsmEvent mobileInputShownEvent;
		
		private TMPro.TMP_InputField inputField;
		
		public override void Reset()
		{
			hideMobileInput = null;
			mobileInputHiddenEvent = null;
			mobileInputShownEvent = null;
		}
		
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(go))
			{
				inputField = cachedComponent;
			}
			
			DoGetValue();
			
			Finish();
		}

	    private void DoGetValue()
		{
		    if (inputField == null) return;

		    hideMobileInput.Value = inputField.shouldHideMobileInput;

		    Fsm.Event(inputField.shouldHideMobileInput ? 
		        mobileInputHiddenEvent : mobileInputShownEvent);
		}
		
	}
}