// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("TextMesh Pro UGUI Basic")]
	[Tooltip("Activate a UI InputField component to begin processing Events. Optionally Deactivate on state exit")]
	public class TMProUiInputFieldActivate : ComponentAction<TMPro.TMP_InputField>
	{
		[RequiredField]
		[CheckForComponent(typeof(TMPro.TMP_InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool deactivateOnExit;

		private TMPro.TMP_InputField inputField;

		public override void Reset()
		{
			gameObject = null;
			deactivateOnExit = null;
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
				inputField.ActivateInputField();
			}
		}

		public override void OnExit()
		{
		    if (inputField == null) return;
			
			if (deactivateOnExit.Value)
			{
				inputField.DeactivateInputField();
			}
		}

	}
}