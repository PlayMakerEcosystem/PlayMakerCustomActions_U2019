// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

// Created by : Gokhan Ergin ERYILDIR (ergin3d)
// Url : http://hutonggames.com/playmakerforum/index.php?topic=12941.0

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{   
    [ActionCategory("Pooler")]
    [Tooltip("Spawns pre-instanciated objects instead of instantiating them on the runtime for better cpu performance.")]
    public class PoolerSpawn : FsmStateAction
    {
        [RequiredField]
        [Tooltip("Unique Name of the Pool")]
        public FsmString NameOfThePool;
        [Tooltip("Optional Spawn Point.")]
        public FsmGameObject spawnPoint;

        [Tooltip("Position. If a Spawn Point is defined, this is used as a local offset from the Spawn Point position.")]
        public FsmVector3 position;

        [Tooltip("Rotation. NOTE: Overrides the rotation of the Spawn Point.")]
        public FsmVector3 rotation;

        [UIHint(UIHint.Variable)]
        [Tooltip("Optionally store the created object.")]
        public FsmGameObject storeObject;

        string CurrentPool;

        public override void Reset()
        {
            NameOfThePool = null;
            spawnPoint = null;
            position = new FsmVector3 { UseVariable = true };
            rotation = new FsmVector3 { UseVariable = true };
        }

        public override void OnEnter()
        {
            CurrentPool = NameOfThePool.Value;
            GameObject pooledObject = PoolerPool.current.GetPooledObject(CurrentPool);

            if (pooledObject != null)
            {
                var spawnPosition = Vector3.zero;
                var spawnRotation = Vector3.zero;

                if (spawnPoint.Value != null)
                {
                    spawnPosition = spawnPoint.Value.transform.position;

                    if (!position.IsNone)
                    {
                        spawnPosition += position.Value;
                    }

                    spawnRotation = !rotation.IsNone ? rotation.Value : spawnPoint.Value.transform.eulerAngles;
                }
                else
                {
                    if (!position.IsNone)
                    {
                        spawnPosition = position.Value;
                    }

                    if (!rotation.IsNone)
                    {
                        spawnRotation = rotation.Value;
                    }
                }

                pooledObject.transform.position = spawnPosition;
                pooledObject.transform.rotation = Quaternion.Euler(spawnRotation);
                pooledObject.SetActive(true);
            }

            storeObject.Value = pooledObject;
            Finish();
        }
    }

}