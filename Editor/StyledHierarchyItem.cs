


using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class StyledHierarchyItem : MonoBehaviour
{
#if UNITY_EDITOR
    public bool _skipIcon = false;
    public bool _customIconBackroundColor = false;
    public Texture2D _customIcon;
    public Color _itemBackgroundColor = new Color(0.1f, 0.1f, 0.1f, 1f);
    public Color _backgroundColor = new Color(0.1f, 0.1f, 0.1f, 1f);
    public Color _textColor = new Color(.9f, .9f, .9f, 1f);

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