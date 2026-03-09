
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Karianakis.EditTools
{
    internal class KarianakisTagManager : MonoBehaviour
    {


        /*
        createTag with code
        createTag with object ..

        this null parameters optional is horrible ...
    
        tagg creation triggers event change ???
       

        */



        static KarianakisTagManager _instForbidden;
        static KarianakisTagManager _inst
        {
            get
            {
                if (_instForbidden == null)
                {
                    // var obj = new GameObject("KarianakisTagManager");
                    // obj.transform.SetParent(, false);
                    _instForbidden = EditSuitFather.GetCanvas().gameObject.AddComponent<KarianakisTagManager>();
                }

                return _instForbidden;

            }
            set { _instForbidden = value; }

        }


        event Action _tagsChanged;
        KarianakisTag _defaultTagFrobidden;
        KarianakisTag _defaultTag
        {

            get
            {
                if (_defaultTagFrobidden == null)
                {
                    _defaultTagFrobidden = CreateTag("defaultTag", null);
                }
                return _defaultTagFrobidden;
            }

        }

        [SerializeField] List<KarianakisTag> _tags = new();



        private void Start()
        {

            // EditTerminal.AddCommand("setTaGEnaBleD", SetTagEnabledStatusCommand);

            // EditTerminal.AddCommand("printTags", PrintTags);

        }





        KarianakisTag CreateTag(string tagCode
          , GameObject gameObjectReference)
        {
            if (tagCode == "" &&
                gameObjectReference == null)
            {
                return _defaultTag;
            }
            else
            {
                var newTag = new KarianakisTag
                    (tagCode, gameObjectReference);

                _tags.Add(newTag);
                _tagsChanged?.Invoke();

                return newTag;

            }
        }

        bool TryFindTag(string tagCode, GameObject tagObjectReference,
        out KarianakisTag tagFound)
        {

            tagFound = null;
            foreach (var item in _inst._tags)
            {
                if (item.IsThisYourGameobjectReference(tagObjectReference))
                {
                    tagFound = item;
                    break;
                }

                if (item.GetTag == tagCode)
                {
                    tagFound = item;
                    break;
                }

            }

            return tagFound != null;
        }




        //? EXPOSED FUNCTIONS

        static internal KarianakisTag GetDefaultTag
            => _inst._defaultTag;

        static internal bool EqualsToDefaultTag(KarianakisTag tag)
        {
            if (tag == null) return false;
            else return tag == _inst._defaultTag;
        }

        static internal KarianakisTag GetOrCreateTag(string tagCode, GameObject gameObjectReference)
        {
            bool tagFound = _inst.TryFindTag(tagCode, gameObjectReference
            , out KarianakisTag tag);

            if (tagFound == true)
                return tag;
            else
                return _inst.CreateTag(tagCode, gameObjectReference);

        }
        // for commands that dont want to create a tag if mistaken
        // 
        internal static bool CheckTagExists(string tagCode
          , GameObject tagObjectReference)
              => _inst.TryFindTag(tagCode, tagObjectReference, out var tag);


        internal static void SubscribeToTagsChanged(Action action)
            => _inst._tagsChanged += action;
        internal static void UnSubscribeToTagsChanged(Action action)
            => _inst._tagsChanged -= action;


        // TRIGGERS CHANGE
        internal static void SetTagEnabledStatus(string tagCode, GameObject tagObjectReference, bool status)
        {
            _inst.TryFindTag(tagCode, tagObjectReference, out var tag);
            if (tag == null) return;

            bool statusBefore = tag.GetEnabled;
            tag.SetEnabled(status);

            //todo the debug through the terminal debug not unitys 
            // maybe extra simple log in unity
            // maybe option for that 

            Debug.LogError($"TAG :{tag.GetTag} == {statusBefore} -> {status}");

            _inst._tagsChanged?.Invoke();
        }



        //? WHY ??
        // internal static bool GetTagEnabledStatus(string tagCode
        //     , GameObject tagObjectReference)
        // {
        //     var tag = _inst.TryFindTag(tagCode, tagObjectReference);
        //     if (tag != null) return tag.GetEnabled;
        //     return false;
        // }



        //? WHY 
        // internal static void GetTagCode(out string code
        //     , out bool found, GameObject gameObjectReference)
        // {
        //     code = "";
        //     found = false;

        //     var tag = _inst.GetTag();
        //     if (tag == null) return;
        //     {
        //         code = tag._tag;
        //         found = true;
        //     }

        // }











        void PrintTags()
        {

            Debug.Log($"PRINT TAG COUNT [{_tags.Count}]");

            foreach (var item in _tags)
            {
                bool objectPresent = item.GetGameobjectReference != null;
              
                string content = $"Tag-({item.GetTag}) Enabled-({item.GetEnabled}) ";

                if(objectPresent)
                {
                    content += $" Has GameObject-({objectPresent})";
                }
              
                Debug.Log(content);
            }
        }


        //? WHAT ARE YOU ???



        void SetTagEnabledStatusCommand(string[] variables)
        {
            if (variables == null)
            {
                Debug.LogError("NEED tagcode and status example : fakename false");
                return;
            }
            if (variables.Length != 2)
            {
                Debug.LogError("PARAMETERS MUST BE 2: tagcode and status example : fakename false");
                return;
            }

            bool tagExists = TryFindTag(variables[0], null, out var tag);
            if (tagExists == false)
            {
                Debug.LogError($"tag ({variables[0]}) doesnt exist");
                return;
            }


            if (bool.TryParse(variables[1], out bool status) == false)
            {
                Debug.LogError($"status parameter ({variables[1]}) is not a valid boolean (true/false)");
                return;
            }

            SetTagEnabledStatus(tagCode: variables[0], null, status);

        }




    }
}
