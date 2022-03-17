// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

// Created by : Gokhan Ergin ERYILDIR (ergin3d)
// Url : http://hutonggames.com/playmakerforum/index.php?topic=12941.0

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Pooler")]
    [Tooltip("Disables the pooled object so that it can be spawned again when neccesary. ")]
    public class PoolerDestroySelf : FsmStateAction
    {
        public override void OnEnter()
        {

            if (Owner != null)
            {
                Owner.SetActive(false);
            }

            Finish();
        }
    }

}