using System;
using UnityEngine;



[Serializable]
internal class KarianakisTag
{

    internal KarianakisTag(string tag, GameObject gameObjectReference = null)
    {
        _tag = tag;
        _gameObjectReference = gameObjectReference;
        if (gameObjectReference != null)
            _tag = gameObjectReference.name;
    }

    [SerializeField] private bool _enabled = true;
    [SerializeField] private string _tag;
    [SerializeField] private GameObject _gameObjectReference;


    internal string GetTag => _tag;
    internal bool GetEnabled => _enabled;
    internal GameObject GetGameobjectReference 
        => _gameObjectReference;


    internal void SetEnabled(bool value)
        => _enabled = value;


    internal bool IsThisYourGameobjectReference(GameObject reference)
    {
        if (_gameObjectReference == null)
            return false;
        else
            return _gameObjectReference == reference;
    }








}