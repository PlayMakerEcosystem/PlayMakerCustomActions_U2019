// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.
// Created by : Gokhan Ergin ERYILDIR (ergin3d)
// Url : http://hutonggames.com/playmakerforum/index.php?topic=12941.0

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Pooler")]
    [Tooltip("Destroys current pool")]
    public class PoolerDestroyPool : FsmStateAction
    {
        [RequiredField]
        [Tooltip("Unique Name of the Pool")]
        public FsmString NameOfThePool;

        public override void Reset()
        {
            NameOfThePool = null;
        }

        public override void OnEnter()
        {
            PoolerPool.DestroyPool(NameOfThePool.Value);
            Finish();
        }
    }
}