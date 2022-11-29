// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("TextMesh Pro UGUI Basic")]
	[Tooltip("Sets the text value of a UI InputField component.")]
	public class TMProUiInputFieldSetText : ComponentAction<TMPro.TMP_InputField>
	{
		[RequiredField]
		[CheckForComponent(typeof(TMPro.TMP_InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.TextArea)]
		[Tooltip("The text of the UI InputField component.")]
		public FsmString text;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		private TMPro.TMP_InputField inputField;
	    private string originalString;

		public override void Reset()
		{
			gameObject = null;
			text = null;
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

			originalString = inputField.text;

			DoSetTextValue();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoSetTextValue();
		}

	    private void DoSetTextValue()
		{
			if (inputField != null)
			{
				inputField.text = text.Value;
			}
		}

		public override void OnExit()
		{
			if (inputField == null) return;
			
			if (resetOnExit.Value)
			{
				inputField.text = originalString;
			}
		}
	}
}