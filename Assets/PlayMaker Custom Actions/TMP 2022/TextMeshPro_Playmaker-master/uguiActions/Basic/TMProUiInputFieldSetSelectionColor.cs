﻿// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("TextMesh Pro UGUI Basic")]
	[Tooltip("Sets the selection color of a UI InputField component. This is the color of the highlighter to show what characters are selected.")]
	public class TMProUiInputFieldSetSelectionColor : ComponentAction<TMPro.TMP_InputField>
	{
		[RequiredField]
		[CheckForComponent(typeof(TMPro.TMP_InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The color of the highlighter to show what characters are selected for the UI InputField component.")]
		public FsmColor selectionColor;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		private TMPro.TMP_InputField inputField;
	    private Color originalValue;

		public override void Reset()
		{
			gameObject = null;
			selectionColor = null;
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

		    originalValue = inputField.selectionColor;

			DoSetValue();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoSetValue();
		}

	    private void DoSetValue()
		{
			if (inputField != null)
			{
				inputField.selectionColor = selectionColor.Value;
			}
		}

		public override void OnExit()
		{
			if (inputField == null) return;
			
			if (resetOnExit.Value)
			{
				inputField.selectionColor = originalValue;
			}
		}
	}
}