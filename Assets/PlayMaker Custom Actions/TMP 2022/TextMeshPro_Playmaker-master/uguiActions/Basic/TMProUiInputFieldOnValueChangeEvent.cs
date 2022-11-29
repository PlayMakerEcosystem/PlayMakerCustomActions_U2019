// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("TextMesh Pro UGUI Basic")]
	[Tooltip("Catches UI InputField onValueChanged event. Store the new value and/or send events. Event string data also contains the new value.")]
	public class TMProUiInputFieldOnValueChangeEvent : ComponentAction<TMPro.TMP_InputField>
	{
		[RequiredField]
		[CheckForComponent(typeof(TMPro.TMP_InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

	    [Tooltip("Where to send the event.")]
	    public FsmEventTarget eventTarget;

		[Tooltip("Send this event when value changed.")]
		public FsmEvent sendEvent;

	    [Tooltip("Store new value in string variable.")]
	    [UIHint(UIHint.Variable)]
	    public FsmString text;

		private TMPro.TMP_InputField inputField;
		
		public override void Reset()
		{
			gameObject = null;
		    text = null;
            eventTarget = null;
			sendEvent = null;
		}
		
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(go))
			{
				inputField = cachedComponent;
				if (inputField != null)
				{
					inputField.onValueChanged.AddListener(DoOnValueChange);
				}
			}
		}

		public override void OnExit()
		{
			if (inputField != null)
			{
		        inputField.onValueChanged.RemoveListener(DoOnValueChange);
			}
		}

		public void DoOnValueChange(string value)
		{
		    text.Value = value;

			Fsm.EventData.StringData = value;
			SendEvent(eventTarget, sendEvent);
		}
	}
}