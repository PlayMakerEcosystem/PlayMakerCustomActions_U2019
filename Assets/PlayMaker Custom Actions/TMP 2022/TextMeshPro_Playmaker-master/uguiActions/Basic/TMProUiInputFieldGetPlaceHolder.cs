// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("TextMesh Pro UGUI Basic")]
	[Tooltip("Gets the placeHolder GameObject of a UI InputField component.")]
	public class TMProUiInputFieldGetPlaceHolder : ComponentAction<TMPro.TMP_InputField>
	{
		[RequiredField]
		[CheckForComponent(typeof(TMPro.TMP_InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the placeholder for the UI InputField component.")]
		public FsmGameObject placeHolder;

		[Tooltip("true if placeholder is found")]
		public FsmBool placeHolderDefined;

		[Tooltip("Event sent if no placeholder is defined")]
		public FsmEvent foundEvent;

		[Tooltip("Event sent if a placeholder is defined")]
		public FsmEvent notFoundEvent;
		
		private TMPro.TMP_InputField inputField;
		
		public override void Reset()
		{
			placeHolder = null;
			placeHolderDefined = null;
			foundEvent = null;
			notFoundEvent = null;
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

		    var _placeholder = inputField.placeholder;

		    placeHolderDefined.Value = _placeholder!=null;
		    if (_placeholder!=null)
		    {
		        placeHolder.Value = _placeholder.gameObject;
		        Fsm.Event(foundEvent);
		    }else{
		        Fsm.Event(notFoundEvent);
		    }
		}
		
	}
}