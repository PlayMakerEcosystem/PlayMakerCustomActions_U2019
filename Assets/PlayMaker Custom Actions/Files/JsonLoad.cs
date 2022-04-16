// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
// Author: Romi Fauzi
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: Load Save Json Disk

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
                    floatVar = loadData.savedFloat;
                }

                if (loadData.savedInt.Length > 0)
                {
                    intVar = loadData.savedInt;
                }

                if (loadData.savedString.Length > 0)
                {
                    stringVar = loadData.savedString;
                }

                if (loadData.savedBool.Length > 0)
                {
                    boolVar = loadData.savedBool;
                }

                if (loadData.savedVector2s.Length > 0)
                {
                    vector2Var = loadData.savedVector2s;
                }

                if (loadData.savedVector3s.Length > 0)
                {
                    vector3Var = loadData.savedVector3s;
                }

                if (loadData.savedArray.Length > 0)
                {
                    for (int i = 0; i < loadData.savedArray.Length; i++)
                    {
                        arrayVar[i].Values = loadData.savedArray[i].Values.Clone() as object[];
                        arrayVar[i].Resize(arrayVar[i].Length - 1);
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

