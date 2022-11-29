﻿// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("TextMesh Pro UGUI Basic")]
	[Tooltip("Fires an event when user submits from a UI InputField component. \n" +
		"This only fires if the user press Enter, not when field looses focus or user escaped the field.\n" +
		"Event string data will contain the text value.")]
	public class TMProUiInputFieldOnSubmitEvent : ComponentAction<TMPro.TMP_InputField>
	{
		[RequiredField]
		[CheckForComponent(typeof(TMPro.TMP_InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

	    [Tooltip("Where to send the event.")]
	    public FsmEventTarget eventTarget;

		[Tooltip("Send this event when editing ended.")]
		public FsmEvent sendEvent;

		[Tooltip("The content of the InputField when submitting")]
		[UIHint(UIHint.Variable)]
		public FsmString text;

		private TMPro.TMP_InputField inputField;
		
		public override void Reset()
		{
			gameObject = null;
		    eventTarget = null;
			sendEvent = null;
			text = null;
		}
		
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(go))
			{
				inputField = cachedComponent;
				if (inputField != null)
				{
					inputField.onEndEdit.AddListener(DoOnEndEdit);
				}
			}
		}

		public override void OnExit()
		{
			if (inputField != null)
			{
				inputField.onEndEdit.RemoveListener(DoOnEndEdit);
			}
		}

		public void DoOnEndEdit(string value)
		{
		    if (inputField.wasCanceled) return;

		    text.Value = value;
		    Fsm.EventData.StringData = value;
		    SendEvent(eventTarget, sendEvent);

		    Finish();
		}
	}
}