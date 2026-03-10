


using UnityEditor;
using UnityEngine;



[ExecuteAlways]
internal class StyledHierarchyItem : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] bool _skipIcon = false;
    [SerializeField] bool _enableCustomIconBackroundColor = false;
    [SerializeField] Texture2D _customIcon;
    [SerializeField] Color _itemBackgroundColor = new Color(0.1f, 0.1f, 0.1f, 1f);
    [SerializeField] Color _backgroundColor = new Color(0.1f, 0.1f, 0.1f, 1f);
    [SerializeField] Color _textColor = new Color(.9f, .9f, .9f, 1f);



    public bool GetEnableCustomIconBackroundColor => _enableCustomIconBackroundColor;

    public Texture2D GetCustomIcon => _customIcon;
    public Color GetItemBackgroundColor => _itemBackgroundColor;
    public Color GetBackgroundColor => _backgroundColor;
    public Color GetTextColor => _textColor;

    
    public bool HasCustomIcon 
        => _customIcon != null && _skipIcon == false;
   


    private void OnValidate()
    {
        if (_customIcon != null)
        {
            EditorGUIUtility.SetIconForObject(gameObject, _customIcon);
        }

#if UNITY_EDITOR
        EditorApplication.RepaintHierarchyWindow();
#endif
    }

#endif
}