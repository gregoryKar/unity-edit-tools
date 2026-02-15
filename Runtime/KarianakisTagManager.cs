
using System;
using System.Collections.Generic;
using UnityEngine;

namespace karianakisEditTools
{
    public class KarianakisTagManager : MonoBehaviour
    {



        static KarianakisTagManager _instForbidden;
        static KarianakisTagManager _inst
        {
            get
            {
                if (_instForbidden == null)
                {
                    var obj = new GameObject("KarianakisTagManager");
                    obj.transform.SetParent(EditSuitFather._inst.transform, false);
                    _instForbidden = obj.AddComponent<KarianakisTagManager>();
                }


                return _instForbidden;

            }
            set { _instForbidden = value; }



        }

        event Action _tagsChanged;


        private void Start()
        {
            CreateTag("testtag" , gameObject);

            EditTerminal.AddCommand("setTaGEnaBleD", SetTagEnabledStatusCommand);

            EditTerminal.AddCommand("printTags", PrintTags);

          

            new DynamicDebugger(
                code: "fakeCode  0",
                content: "test------0",
                color: Color.cyan,
                //tagCode: "testtag",
                tagObjectReference : gameObject
            );
            new DynamicDebugger(
            code: "fakeCode 1",
            content: "test------1",
            color: Color.cyan,
            //tagCode: "testtag"
            tagObjectReference : gameObject
        );
            new DynamicDebugger(
             code: "fakeCode 2",
             content: "test------2",
             color: Color.cyan,
             //tagCode: "testtag"
             tagObjectReference : gameObject
         );







        }



        void SetTagEnabledStatusCommand(string[] variables)
        {
            if (variables == null)
            {
                Debug.LogError("NEED tagcode and status px fakename false");
                return;
            }
            if (variables.Length != 2)
            {
                Debug.LogError("PARAMETERS MUST BE 2: tagcode and status px fakename false");
                return;
            }

            bool tagExisst = CheckTagExists(variables[0]);
            if (tagExisst == false)
            {
                Debug.LogError($"tag ({variables[0]}) doesnt exist");
                return;
            }

            bool status = false;
            if (variables[1] == "true") status = true;
            else if (variables[1] == "false") status = false;
            else return;

            SetTagEnabledStatus(tagCode: variables[0], null, status);

        }


        #region  FOR OUTSIDERS

        public static void SubscribeToTagsChanged(Action action)
        => _inst._tagsChanged += action; public static void UnSubscribeToTagsChanged(Action action)
        => _inst._tagsChanged -= action;

        public static bool GetTagEnabled(string tagCode = "", GameObject tagObjectReference = null)
        {
            var tag = _inst.GetTag(tagCode, tagObjectReference);
            if (tag != null) return tag.enabled;
            return false;

        }




        public static bool CheckTagExists(string tagCode = "", GameObject tagObjectReference = null)
        => _inst.GetTag(tagCode, tagObjectReference) != null;





        public static KarianakisTag GetOrCreateTag(string tagCode, GameObject tagObjectReference = null)
        {
            var tag = _inst.GetTag(tagCode, tagObjectReference); if (tag != null) return tag; CreateTag(tagCode, tagObjectReference); return _inst.GetTag(tagCode, tagObjectReference);
        }


        //! TRIGGERS CHANGE
        public static void CreateTag(string tagCode, GameObject gameObjectReference = null)
        {
            if (tagCode == "" && gameObjectReference == null)
                return;

            if (_inst.GetTag(tagCode, gameObjectReference) != null)
                return;

            var newTag = new KarianakisTag(tagCode, gameObjectReference);
            _inst._tags.Add(newTag);

            _inst._tagsChanged?.Invoke();
        }

        public static void GetTagCode(out string code, out bool found, GameObject gameObjectReference)
        {
            code = "";
            found = false;

            var tag = _inst.GetTag();
            if (tag == null) return;
            {
                code = tag._tag;
                found = true;
            }

        }

        //! TRIGGERS CHANGE
        public static void SetTagEnabledStatus(string tagCode, GameObject tagObjectReference, bool status)
        {
            var tag = _inst.GetTag(tagCode, tagObjectReference);
            if (tag == null) return;

            bool statusBefore = tag.enabled;
            tag._enabled = status;
            Debug.LogError($"TAG :{tag._tag} == {statusBefore} -> {status}");

            _inst._tagsChanged?.Invoke();
        }



        #endregion







        KarianakisTag GetTag(string tagCode = "", GameObject tagObjectReference = null)
        {
            foreach (var item in _inst._tags)
            {
                if (item._tag == tagCode)
                    return item;

                if (item._gameobjectReference == tagObjectReference
                && tagObjectReference != null)
                    return item;

            }
            return null;
        }


        [SerializeField]
        List<KarianakisTag> _tags = new();

        void PrintTags()
        {

            foreach (var item in _tags)
            {
                string objectName = "NULL OBJ";
                if (item._gameobjectReference != null)
                    objectName = item._gameobjectReference.name;

                Debug.Log($"tag: {item._tag} enabled: {item.enabled} gameObjectRef: {objectName}");
            }
        }




        [Serializable]
        public class KarianakisTag
        {

            public bool enabled => _enabled;
            [SerializeField]
            internal bool _enabled = true;


            public string _tag;
            public GameObject _gameobjectReference;
            public KarianakisTag(string tag, GameObject gameObjectReference = null)
            {
                _tag = tag;
                _gameobjectReference = gameObjectReference;
            }


            ~KarianakisTag() { }

        }


    }
}
