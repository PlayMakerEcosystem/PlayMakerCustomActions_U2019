// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;


namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.UnityObject)]
    [Tooltip("Check if  a Component is active. WARNING! COLLIDERS DO NOT WORK WITH THIS ACTION and Some Components Might not work!")]
    public class ComponentIsActive : FsmStateAction
    {
        [RequiredField]
        [Tooltip("Place Component Object here n/WARNING COLLIDERS DO NOT WORK!!")]
        public FsmObject component;

        [RequiredField]
        [Tooltip("Is componenet active n/WARNING COLLIDERS DO NOT WORK!!")]
        [UIHint(UIHint.Variable)]
        public FsmBool isActive;

        public override void Reset()
        {
            component = null;
            isActive = null;
        }

        public override void OnEnter()
        {
            if (component.Value == null)
            {
                LogError("No Component Selected");
                Debug.Log("No Component");
            }
            else
            {
                Behaviour be = component.Value as Behaviour;

                if (be != null)
                {
                    isActive.Value = be.enabled;
                }
                else
                {
                    Renderer rend = component.Value as Renderer;
                    if (rend != null) isActive.Value = rend.enabled;
                }
            }
            Finish();
        }
    }
}