// (c) Copyright HutongGames, LLC 2010-2022. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
// Author Stephen Scott Day
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Array)]
    [Tooltip("Return the maximum value within an array. It can use float, int, vector2 and vector3 ( uses magnitude), rect ( uses surface), gameobject ( using bounding box volume), and string ( use lenght)")]
    public class ArrayGetMinMax : FsmStateAction
    {
        static VariableType[] supportedTypes = new VariableType[] {
            VariableType.Float,
            VariableType.Int,
            VariableType.Rect,
            VariableType.Vector2,
            VariableType.Vector3,
            VariableType.GameObject,
            VariableType.String};

        [RequiredField] 
        [UIHint(UIHint.Variable)] 
        [Tooltip("The Array Variable to use.")] 
        public FsmArray array;

        [RequiredField]
        [MatchElementType("array")]
        [UIHint(UIHint.Variable)]
        [Tooltip("The Maximum Value")]
        public FsmVar maximumValue;

        [UIHint(UIHint.Variable)]
        [Tooltip("The index of the Maximum Value within that arrayList")]
        public FsmInt maximumValueIndex;

        [UIHint(UIHint.Variable)]
        [Tooltip("The Minimum Value")]
        public FsmVar minimumValue;

        [UIHint(UIHint.Variable)]
        [Tooltip("The index of the Maximum Value within that arrayList")]
        public FsmInt minimumValueIndex;

        [Tooltip("Repeat every frame while the state is active.")]
        public bool everyFrame;

        [ActionSection("Events")]

        [Tooltip("The event to trigger if the index is out of range.")] 
        public FsmEvent indexOutOfRange;

        public override void Reset()
        {
            array = null;
            everyFrame = false;
            indexOutOfRange = null;

            maximumValue = null;
            maximumValueIndex = null;

            minimumValue = null;
            minimumValueIndex = null;
        }

        public override void OnEnter()
        {
            DoGetMinMax();
            DoFindMinimumValue();

            if (!everyFrame)
            {
                Finish();
            }

        }

        public override void OnUpdate()
        {
            DoGetMinMax();
            DoFindMinimumValue();

        }
        
        private void DoGetMinMax()
        {
            VariableType _targetType = maximumValue.Type;
            if (!supportedTypes.Contains(maximumValue.Type))
            {
                return;
            }

            float max = float.NegativeInfinity;

            int maxIndex = 0;

            int index = 0;
            foreach (object _obj in array.Values)
            {
                try
                {
                    float _val = PlayMakerUtils.GetFloatFromObject(_obj, _targetType, true);
                    if (max < _val)
                    {
                        max = _val;
                        maxIndex = index;
                    }
                }
                finally { }

                index++;
            }


            maximumValueIndex.Value = maxIndex;
            PlayMakerUtils.ApplyValueToFsmVar(this.Fsm, maximumValue, array.Values[maxIndex]);
        }

        public override string ErrorCheck()
        {
            if (!supportedTypes.Contains(maximumValue.Type))
            {
                return "A " + maximumValue.Type + " can not be processed as a minimum";
            }

            return "";
        }
        private void DoFindMinimumValue()
        {
            VariableType _targetType = minimumValue.Type;
            if (!supportedTypes.Contains(minimumValue.Type))
            {
                return;
            }

            float min = float.PositiveInfinity;

            int minIndex = 0;

            int index = 0;
            foreach (object _obj in array.Values)
            {
                try
                {
                    float _val = PlayMakerUtils.GetFloatFromObject(_obj, _targetType, true);
                    if (min > _val)
                    {
                        min = _val;
                        minIndex = index;
                    }
                }
                finally { }

                index++;
            }

            minimumValueIndex.Value = minIndex;
            PlayMakerUtils.ApplyValueToFsmVar(this.Fsm, minimumValue, array.Values[minIndex]);
        }

    }
}
//Stephen Scott Day was here, y'all. 24-NOV-2021 My first actually useful custom action.