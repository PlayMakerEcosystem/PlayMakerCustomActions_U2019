﻿// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("TextMesh Pro UGUI Basic")]
	[Tooltip("Gets the cancel state of a UI InputField component. This relates to the last onEndEdit Event")]
	public class TMProUiInputFieldGetWasCanceled : ComponentAction<TMPro.TMP_InputField>
	{
		[RequiredField]
		[CheckForComponent(typeof(TMPro.TMP_InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
		[Tooltip("The was canceled flag value of the UI InputField component.")]
		public FsmBool wasCanceled;

		[Tooltip("Event sent if inputField was canceled")]
		public FsmEvent wasCanceledEvent;

		[Tooltip("Event sent if inputField was not canceled")]
		public FsmEvent wasNotCanceledEvent;
		
		private TMPro.TMP_InputField inputField;
		
		public override void Reset()
		{
			wasCanceled = null;
			wasCanceledEvent = null;
			wasNotCanceledEvent = null;
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

		    wasCanceled.Value = inputField.wasCanceled;

		    Fsm.Event(inputField.wasCanceled ? wasCanceledEvent : wasNotCanceledEvent);
		}	
	}
}