// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("TextMesh Pro UGUI Basic")]
	[Tooltip("Move Caret to text start in a UI InputField component. Optionally select from the current caret position")]
	public class TMProUiInputFieldMoveCaretToTextStart: ComponentAction<TMPro.TMP_InputField>
	{
		[RequiredField]
		[CheckForComponent(typeof(TMPro.TMP_InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Define if we select or not from the current caret position. Default is true = no selection")]
		public FsmBool shift;

		private TMPro.TMP_InputField inputField;

		public override void Reset()
		{
			gameObject = null;
			shift = true;
		}
		
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(go))
			{
				inputField = cachedComponent;
			}

			DoAction();
			
			Finish();
		}

	    private void DoAction()
		{
			if (inputField != null)
			{
				inputField.MoveTextStart(shift.Value);
			}
		}

	}
}