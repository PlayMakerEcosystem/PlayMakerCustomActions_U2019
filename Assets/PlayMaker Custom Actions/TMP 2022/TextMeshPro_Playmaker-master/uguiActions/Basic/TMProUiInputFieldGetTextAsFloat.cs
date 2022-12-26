// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("TextMesh Pro UGUI Basic")]
	[Tooltip("Gets the text value of a UI InputField component as a float.")]
	public class TMProUiInputFieldGetTextAsFloat : ComponentAction<TMPro.TMP_InputField>
	{
		[RequiredField]
		[CheckForComponent(typeof(TMPro.TMP_InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The text value as a float of the UI InputField component.")]
		public FsmFloat value;

		[UIHint(UIHint.Variable)]
		[Tooltip("true if text resolves to a float")]
		public FsmBool isFloat;

		[Tooltip("true if text resolves to a float")]
		public FsmEvent isFloatEvent;

		[Tooltip("Event sent if text does not resolves to a float")]
		public FsmEvent isNotFloatEvent;

        [Tooltip("Repeat every frame.")]
        public bool everyFrame;
		
		private TMPro.TMP_InputField inputField;

	    private float _value;
	    private bool _success;

		public override void Reset()
		{
			value = null;
			isFloat = null;
			isFloatEvent = null;
			isNotFloatEvent = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
		    var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(go))
			{
				inputField = cachedComponent;
			}
			
			DoGetTextValue();
			
			if (!everyFrame)
				Finish();
		}
		
		public override void OnUpdate()
		{
			DoGetTextValue();
		}

	    private void DoGetTextValue()
		{
		    if (inputField == null) return;

		    _success = float.TryParse(inputField.text, out _value);
		    value.Value = _value;
		    isFloat.Value = _success;

		    Fsm.Event(_success ? isFloatEvent : isNotFloatEvent);
		}
		
	}
}