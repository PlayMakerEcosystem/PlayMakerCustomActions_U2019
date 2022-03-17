// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

// Created by : Gokhan Ergin ERYILDIR (ergin3d)
// Url : http://hutonggames.com/playmakerforum/index.php?topic=12941.0

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Pooler")]
    [Tooltip("Disables the stored pooled object for later use.")]
    public class PoolerDestroyStored : FsmStateAction
    {
        [RequiredField]
        [Tooltip("The GameObject to destroy.")]
        public FsmGameObject gameObject;

        public override void Reset()
        {
            gameObject = null;
        }

        public override void OnEnter()
        {
            var go = gameObject.Value;

            if (go != null)
            {
                go.SetActive(false);
            }

            Finish();
        }


    }

}