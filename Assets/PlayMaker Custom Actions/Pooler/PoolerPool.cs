// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

// Created by : Gokhan Ergin ERYILDIR (ergin3d)
// Url : http://hutonggames.com/playmakerforum/index.php?topic=12941.0

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Pooler")]
    [Tooltip("Use this action at Start state to create objects when game loads to save cpu time by loading objects into memory when the game starts. Use in conjunction with Pooler Spawn, and Pooler Destroy.")]
    public class PoolerPool : FsmStateAction
    {
        [RequiredField]
        [Tooltip("Object or Prefab to Spawn.")]
        public FsmGameObject PrefabToPool;
        [RequiredField]
        [Tooltip("Unique Name of the Pool")]
        public FsmString NameOfThePool;
        [Tooltip("This number of objects is loded into memory when the event is first triggered")]
        public FsmInt InitiallyPooledObjects;
        [Tooltip("If set to True, new objects will be pooled when neccesary, should not be used unless neccesary")]
        public FsmBool CanGrow;
        [Tooltip("Total number of pooled instances can't exceed this number. 0 for no limit.")]
        public FsmInt MaxPooledObjects;
        [Tooltip("Recycles oldest.")]
        [HideIf ("checkCanGrow")]
        public FsmBool recycle;

        public bool checkCanGrow()
        {
            return CanGrow.Value;
        }

        public static PoolerPool current;

        GameObject pooledObject;
        string poolername;
        int pooledAmount;
        int MaxNoOfPooledObjects;
        bool samepoolexists;
        bool allowGrow;

        public class Pooler
        {
            public GameObject theinstance { get; set; }
            public List<GameObject> thePool { get; set; }
            public bool Growth { get; set; }
            public int MaxGroth { get; set; }
        }

        public static Dictionary<string, Pooler> ObjectPool;
        List<GameObject> PoolList;

        public override void Awake()
        {
            ObjectPool = new Dictionary<string, Pooler>();
            PoolList = new List<GameObject>();
        }

        public override void Reset()
        {
            PrefabToPool = null;
            NameOfThePool = null;
            InitiallyPooledObjects = 20;
            CanGrow = false;
            MaxPooledObjects = 0;
        }

        public override void OnEnter()
        {
            current = this;

            PoolObjects();

            Finish();
        }

        public GameObject GetPooledObject(string PoolName)
        {

            if (ObjectPool != null)
            {
                Pooler getpool = new Pooler();

                if (ObjectPool.TryGetValue(PoolName, out getpool))
                {
                    for (int i = 0; i < getpool.thePool.Count; i++)
                    {
                        if (!getpool.thePool[i].activeInHierarchy)
                        {
                            return getpool.thePool[i];
                        }
                    }

                    allowGrow = getpool.Growth;
                    MaxNoOfPooledObjects = getpool.MaxGroth;

                    if (allowGrow)
                    {
                        if (MaxNoOfPooledObjects == 0 || MaxNoOfPooledObjects >= getpool.thePool.Count)
                        {
                            GameObject instance = (GameObject)UnityEngine.Object.Instantiate(getpool.theinstance);
                            getpool.thePool.Add(instance);
                            return instance;
                        }

                    }
                    else if (recycle.Value)
                    {
                        if (MaxNoOfPooledObjects == 0 || MaxNoOfPooledObjects >= getpool.thePool.Count)
                        {
                            GameObject oldest = getpool.thePool[0];
                            getpool.thePool.RemoveAt(0);
                            getpool.thePool.Add(oldest);
                            return oldest;
                        }
                    }

                }
                else
                {
                    Debug.Log("Pooler: No Such Pool, try using variables for pools names to avoid mistyping.");
                }
            }
            else
            {
                Debug.Log("Pooler: Pool is empty");
            }

            Debug.Log("Pooler: Can't Spawn, max. instances reached?");
            return null;
        }

        void PoolObjects()
        {
            pooledObject = PrefabToPool.Value;
            poolername = NameOfThePool.Value;

            if (pooledObject == null || poolername == null)
            {
                Debug.Log("Pooler: Prefab to Pool or Name of the Pool is empty.");
                return;
            }

            pooledAmount = InitiallyPooledObjects.Value;
            Pooler testpool = new Pooler();

            samepoolexists = false;

            if (ObjectPool == null)
            {
                ObjectPool = new Dictionary<string, Pooler>();
                PoolList = new List<GameObject>();

            }
            else
            {
                if (ObjectPool.TryGetValue(poolername, out testpool))
                {
                    samepoolexists = true;
                }
                else
                {
                    PoolList = new List<GameObject>();
                }

            }

            if (!samepoolexists)
            {
                for (int i = 0; i < pooledAmount; i++)
                {
                    GameObject instance = (GameObject)UnityEngine.Object.Instantiate(pooledObject);
                    instance.SetActive(false);
                    PoolList.Add(instance);
                }

                Pooler adddata = new Pooler();
                adddata.theinstance = PrefabToPool.Value;
                adddata.thePool = PoolList;
                adddata.Growth = CanGrow.Value;
                adddata.MaxGroth = MaxPooledObjects.Value;
                ObjectPool.Add(poolername, adddata);

            }
            else
            {
                Debug.Log("Pooler: Same pool exists, choose unique names for pools");
            }
        }

        public static void DestroyPool(string PoolName)
        {
            PoolerPool poolerPool = new PoolerPool();
            poolerPool.DestroyThePool(PoolName);
        }


        void DestroyThePool(string PoolName)
        {
            if (ObjectPool != null)
            {
                Pooler getpool = new Pooler();

                if (ObjectPool.TryGetValue(PoolName, out getpool))
                {
                    foreach (var o in getpool.thePool)
                    {
                        UnityEngine.Object.Destroy(o);
                    }
                    ObjectPool.Remove(PoolName);
                }
                else
                {
                    Debug.Log("Pooler: No Such Pool, try using variables for pools names to avoid mistyping.");
                }
            }
        }
    }
}