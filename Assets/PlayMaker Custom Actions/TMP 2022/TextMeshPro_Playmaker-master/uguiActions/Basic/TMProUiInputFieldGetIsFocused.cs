﻿// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("TextMesh Pro UGUI Basic")]
	[Tooltip("Gets the focused state of a UI InputField component.")]
	public class TMProUiInputFieldGetIsFocused : ComponentAction<TMPro.TMP_InputField>
	{
		[RequiredField]
		[CheckForComponent(typeof(TMPro.TMP_InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the is focused flag value of the UI InputField component.")]
		public FsmBool isFocused;

		[Tooltip("Event sent if inputField is focused")]
		public FsmEvent isfocusedEvent;

		[Tooltip("Event sent if nputField is not focused")]
		public FsmEvent isNotFocusedEvent;
		
		private TMPro.TMP_InputField inputField;
		
		public override void Reset()
		{
			isFocused = null;
			isfocusedEvent = null;
			isNotFocusedEvent = null;
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

		    isFocused.Value = inputField.isFocused;

		    Fsm.Event(inputField.isFocused ? isfocusedEvent : isNotFocusedEvent);
		}
		
	}
}