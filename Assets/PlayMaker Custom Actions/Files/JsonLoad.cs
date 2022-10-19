// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
// Author: Romi Fauzi
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: Load Save Json Disk load save json disk

using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Files")]
    [Tooltip("Load Data saved using Binary Save, for Array only type of float, integer, string that are currently working, feel free to test other data types")]
    public class JsonLoad : FsmStateAction
    {
        [UIHint(UIHint.Variable)]
        [Tooltip("load your float variable here if there are any saved, can be more than one")]
        public FsmFloat[] floatVar;
        [UIHint(UIHint.Variable)]
        [Tooltip("load your int variable here if there are any saved, can be more than one")]
        public FsmInt[] intVar;
        [UIHint(UIHint.Variable)]
        [Tooltip("load your string variable here if there are any saved, can be more than one")]
        public FsmString[] stringVar;
        [UIHint(UIHint.Variable)]
        [Tooltip("load your bool variable here if there are any saved, can be more than one")]
        public FsmBool[] boolVar;
        [Tooltip("Load your Vector2 variable here, can be more than one")]
        public FsmVector2[] vector2Var;
        [UIHint(UIHint.Variable)]
        [Tooltip("Load your Vector3 variable here, can be more than one")]
        public FsmVector3[] vector3Var;

        [UIHint(UIHint.Variable)]
        [Tooltip("load your array variable here, can be more than one and can be a different type (only float, integer and string are currently tested)")]
        public FsmArray[] arrayVar;

        [Tooltip("name of the save file that will be loaded, make sure the name its the same with the one on Binary Save")]
        public FsmString filename;

        [Tooltip("option to decode the encoded Json (only when saving with encoded option")]
        public FsmBool decodeJson;

        [Tooltip("string key to decode the Json, use the same string from Json saver"), HideIf("EncodeFieldHide")]
        public FsmString decodeKey;

        public FsmEvent notFoundEvent;

        private JsonSavedData loadData = new JsonSavedData();

        public override void Reset()
        {
            floatVar = null;
            intVar = null;
            stringVar = null;
            arrayVar = null;
        }

        public override void OnEnter()
        {
            DoLoad();

            Finish();
        }

        private void DoLoad()
        {
            if (File.Exists(Application.persistentDataPath + "/" + filename.Value))
            {
                string jsonString = File.OpenText(Application.persistentDataPath + "/" + filename.Value).ReadToEnd();
                string jsonToLoad = "";

                if (decodeJson.Value)
                {
                    for (int i = 0; i < jsonString.Length; i++)
                    {
                        jsonToLoad += (char)(jsonString[i] ^ decodeKey.Value[i % decodeKey.Value.Length]);
                    }
                }
                else
                {
                    jsonToLoad = jsonString;
                }

                loadData = JsonUtility.FromJson<JsonSavedData>(jsonToLoad);


                if (loadData.savedFloat.Length > 0)
                {
                    for (int i = 0; i < loadData.savedFloat.Length; i++)
                    {
                        floatVar[i].Value = loadData.savedFloat[i].Value;
                    }
                }

                if (loadData.savedInt.Length > 0)
                {
                    for (int i = 0; i < loadData.savedInt.Length; i++)
                    {
                        intVar[i].Value = loadData.savedInt[i].Value;
                    }
                }

                if (loadData.savedString.Length > 0)
                {
                    for (int i = 0; i < loadData.savedString.Length; i++)
                    {
                        stringVar[i].Value = loadData.savedString[i].Value;
                    }
                }

                if (loadData.savedBool.Length > 0)
                {
                    for (int i = 0; i < loadData.savedBool.Length; i++)
                    {
                        boolVar[i].Value = loadData.savedBool[i].Value;
                    }
                }

                if (loadData.savedVector2s.Length > 0)
                {
                    for (int i = 0; i < loadData.savedVector2s.Length; i++)
                    {
                        vector2Var[i].Value = loadData.savedVector2s[i].Value;
                    }
                }

                if (loadData.savedVector3s.Length > 0)
                {
                    for (int i = 0; i < loadData.savedVector3s.Length; i++)
                    {
                        vector3Var[i].Value = loadData.savedVector3s[i].Value;
                    }
                }

                if (loadData.savedArray.Length > 0)
                {
                    for (int i = 0; i < loadData.savedArray.Length; i++)
                    {
                        arrayVar[i].Values = loadData.savedArray[i].Values;
                    }
                }
            }
            else
                Fsm.Event(notFoundEvent);
        }

        public bool EncodeFieldHide()
        {
            return !decodeJson.Value;
        }
    }
}

