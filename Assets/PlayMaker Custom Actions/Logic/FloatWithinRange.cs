// (c) Copyright HutongGames, LLC 2010-2022. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
// Author Stephen Scott Day
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Logic)]
    [Tooltip("Tests if the value of a Float variable changed. Use this to send an event on change, or store a bool that can be used in other operations.")]
    public class FloatWithinRange : FsmStateAction
    {
        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("The Float variable to check if is in range.")]
        public FsmFloat floatVariable;

        [RequiredField]
        [Tooltip("The minimum range.")]
        public FsmFloat minRange;

        [RequiredField]
        [Tooltip("The maximum range.")]
        public FsmFloat maxRange;

        [UIHint(UIHint.Variable)]
        [Tooltip("Set to True if the float variable is in range.")]
        public FsmBool storeResult;

        [Tooltip("Repeat every frame while the state is active.")]
        public bool everyFrame;

        public override void Reset()
        {
            floatVariable = null;
            storeResult = false;
            everyFrame = false;
        }


        public override void OnEnter()
        {
            UpdateResult();

            if (!everyFrame)
            {
                Finish();
            }
        }


        public override void OnUpdate()
        {
            UpdateResult(); 
        }

        private void UpdateResult()
        {
            storeResult.Value = (floatVariable.Value > minRange.Value && floatVariable.Value < maxRange.Value);
        }
    }
}
//ssd y'all