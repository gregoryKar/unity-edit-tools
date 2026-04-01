#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Karianakis.EditTools
{
    [ExecuteAlways]
    public class StyledHierarchyItem : MonoBehaviour
    {



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


        public static void HighlightCustom(GameObject obj, Color textColor, Color backColor)
        {
            var item = obj.GetComponent<StyledHierarchyItem>();
            if (item == null)
            {
                item = obj.AddComponent<StyledHierarchyItem>();
            }
            item._backgroundColor = backColor;
            item._textColor = textColor;
        }
        public static void HighlightError(GameObject obj)
            => HighlightCustom(obj, textColor: new Color(1f, 0f, 0f), backColor: new Color(0f, 0f, 0f));
        public static void HighlightGreen(GameObject obj)
            => HighlightCustom(obj, textColor: new Color(0f, 1f, 0f), backColor: new Color(0f, 0f, 0f));


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

    }
}