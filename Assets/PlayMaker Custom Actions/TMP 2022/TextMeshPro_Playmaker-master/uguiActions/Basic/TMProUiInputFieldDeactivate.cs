// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("TextMesh Pro UGUI Basic")]
	[Tooltip("Deactivate a UI InputField to stop the processing of Events and send OnSubmit if not canceled. Optionally Activate on state exit")]
	public class TMProUiInputFieldDeactivate : ComponentAction<TMPro.TMP_InputField>
	{
		[RequiredField]
		[CheckForComponent(typeof(TMPro.TMP_InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Activate when exiting this state.")]
		public FsmBool activateOnExit;

		private TMPro.TMP_InputField inputField;

		public override void Reset()
		{
			gameObject = null;
			activateOnExit = null;
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
				inputField.DeactivateInputField();
			}
		}

		public override void OnExit()
		{
			if (inputField == null) return;
			
			if (activateOnExit.Value)
			{
				inputField.ActivateInputField();
			}
		}

	}
}