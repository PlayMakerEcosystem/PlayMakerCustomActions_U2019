// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("TextMesh Pro UGUI Basic")]
	[Tooltip("Gets the text value of a UI InputField component.")]
	public class TMProUiInputFieldGetText : ComponentAction<TMPro.TMP_InputField>
	{
		[RequiredField]
		[CheckForComponent(typeof(TMPro.TMP_InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The text value of the UI InputField component.")]
		public FsmString text;

        [Tooltip("Repeat every frame.")]
        public bool everyFrame;
		
		private TMPro.TMP_InputField inputField;
		
		public override void Reset()
		{
			text = null;
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
			if (inputField != null)
			{
				text.Value = inputField.text;
			}
		}
		
	}
}