// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("TextMesh Pro UGUI Basic")]
	[Tooltip("Fires an event when editing ended in a UI InputField component. Event string data will contain the text value, and the boolean will be true is it was a cancel action")]
	public class TMProUiInputFieldOnEndEditEvent : ComponentAction<TMPro.TMP_InputField>
	{
		[RequiredField]
		[CheckForComponent(typeof(TMPro.TMP_InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

	    [Tooltip("Where to send the event.")]
	    public FsmEventTarget eventTarget;

		[Tooltip("Send this event when editing ended.")]
		public FsmEvent sendEvent;

		[Tooltip("The content of the InputField when edited ended")]
		[UIHint(UIHint.Variable)]
		public FsmString text;

		[Tooltip("The canceled state of the InputField when edited ended")]
		[UIHint(UIHint.Variable)]
		public FsmBool wasCanceled;

		private TMPro.TMP_InputField inputField;
		
		public override void Reset()
		{
			gameObject = null;
			sendEvent = null;
			text = null;
			wasCanceled = null;
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
			text.Value = value;

		    wasCanceled.Value = inputField.wasCanceled;

		    Fsm.EventData.StringData = value;
			Fsm.EventData.BoolData = inputField.wasCanceled;
			SendEvent(eventTarget, sendEvent);

            Finish();
		}
	}
}