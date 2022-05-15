// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
// Author: Romi Fauzi
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: Load Save Json Disk load save json disk

using UnityEngine;
using System.IO;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Files")]
    [Tooltip("Save Data to be load later using Json Load, for Array only type of float, integer, string that are currently working, feel free to test other data types")]
    public class JsonSave : FsmStateAction
    {
        [UIHint(UIHint.Variable)]
        [Tooltip("Save your float variable here, can be more than one")]
        public FsmFloat[] floatVar;
        [UIHint(UIHint.Variable)]
        [Tooltip("Save your integer variable here, can be more than one")]
        public FsmInt[] intVar;
        [UIHint(UIHint.Variable)]
        [Tooltip("Save your string variable here, can be more than one")]
        public FsmString[] stringVar;
        [UIHint(UIHint.Variable)]
        [Tooltip("Save your bool variable here, can be more than one")]
        public FsmBool[] boolVar;
        [UIHint(UIHint.Variable)]
        [Tooltip("Save your Vector2 variable here, can be more than one")]
        public FsmVector2[] vector2Var;
        [UIHint(UIHint.Variable)]
        [Tooltip("Save your Vector3 variable here, can be more than one")]
        public FsmVector3[] vector3Var;

        [UIHint(UIHint.Variable)]
        [Tooltip("Save your array variable here, can be more than one and can be a different type (only float, integer and string are currently tested)")]
        public FsmArray[] arrayVar;

        [Tooltip("name of the save file that will be outputted")]
        public FsmString filename;

        [Tooltip("option to encode the Json")]
        public FsmBool encodeJson;

        [Tooltip("string key to encode the Json, use the same string for Json loader"), HideIf("EncodeFieldHide")]
        public FsmString encodeKey;

        private JsonSavedData saveData = new JsonSavedData();

        public override void Reset()
        {
            floatVar = null;
            intVar = null;
            stringVar = null;
            arrayVar = null;
        }

        public override void OnEnter()
        {
            DoSave();

            Finish();
        }

        private void DoSave()
        {
            saveData.savedFloat = floatVar;
            saveData.savedInt = intVar;
            saveData.savedString = stringVar;
            saveData.savedBool = boolVar;
            saveData.savedVector2s = vector2Var;
            saveData.savedVector3s = vector3Var;

            saveData.savedArray = new FsmArray[arrayVar.Length];

            for (int i = 0; i < saveData.savedArray.Length; i++)
            {
                saveData.savedArray[i] = arrayVar[i].Clone() as FsmArray;
                saveData.savedArray[i].Resize(saveData.savedArray[i].Length + 1);
            }

            string jsonString = JsonUtility.ToJson(saveData);
            string jsonToSave = "";

            if (encodeJson.Value)
            {
                for (int i = 0; i < jsonString.Length; i++)
                {
                    jsonToSave += (char)(jsonString[i] ^ encodeKey.Value[i % encodeKey.Value.Length]);
                }
            }
            else
            {
                jsonToSave = jsonString;
            }

            File.WriteAllText(Application.persistentDataPath + "/" + filename.Value, jsonToSave);
        }

        public bool EncodeFieldHide()
        {
            return !encodeJson.Value;
        }
    }

    [System.Serializable]
    public class JsonSavedData
    {
        public FsmFloat[] savedFloat;
        public FsmInt[] savedInt;
        public FsmString[] savedString;
        public FsmBool[] savedBool;
        public FsmVector2[] savedVector2s;
        public FsmVector3[] savedVector3s;
        public FsmArray[] savedArray;
    }
}

