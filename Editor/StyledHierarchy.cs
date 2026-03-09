

using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class StyledHierarchy
{

    static Texture2D _defaultIcon;
    static StyledHierarchy()
    {
        _defaultIcon = EditorGUIUtility.FindTexture("Assets/SharedAssets/Textures/Crosshair_T_A.png");
        EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
    }

    private static void OnHierarchyGUI(int instanceID, Rect selectionRect)
    {
        var obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
        if (obj == null) return;

        // Only affect objects with StyledHierarchyItem component
        var item = obj.GetComponent<StyledHierarchyItem>();
        if (item == null)
            return;



        bool iconPresent = item._customIcon != null && item._skipIcon == false;



        const float iconWidth = 16f;
        //const float iconSpaceAfter = 4f;
        const float textStartSpace = 2f;
        // draw rect background
        {

            //float iconOffset = iconPresent ? (iconWidth + iconSpaceAfter) : 0f;
            Rect backroundRect = new Rect(selectionRect.x + iconWidth, selectionRect.y, selectionRect.width - iconWidth, selectionRect.height);

            EditorGUI.DrawRect(backroundRect, item._backgroundColor);
        }

        // draw icon background
        if (iconPresent)
        {
            // Draw background for icon
            Rect itemBackRect = new Rect(selectionRect.x, selectionRect.y,
                iconWidth, selectionRect.height);
            Color iconBgColor = item._customIconBackroundColor ? item._itemBackgroundColor : item._backgroundColor;
            EditorGUI.DrawRect(itemBackRect, iconBgColor);

            // Draw background for space after icon
            // cause the original text can apear in the space 
            //         Color hierarchyBgColor = EditorGUIUtility.isProSkin
            // ? new Color(0.22f, 0.22f, 0.22f, 1f) // Dark theme
            // : new Color(0.76f, 0.76f, 0.76f, 1f); // Light theme

            //         Rect spaceRect = new Rect(selectionRect.x + iconWidth, selectionRect.y,
            //           iconSpaceAfter, selectionRect.height);
            //         EditorGUI.DrawRect(spaceRect, hierarchyBgColor);

            // Draw the custom icon
            Rect iconRect = new Rect(selectionRect.x, selectionRect.y,
                iconWidth, iconWidth);

            Texture2D iconToUse = iconPresent ? item._customIcon : _defaultIcon;
            GUI.DrawTexture(iconRect, iconToUse);

        }


        // draw text
        var style = new GUIStyle(EditorStyles.label)
        {
            fontStyle = FontStyle.Bold,
            normal = { textColor = item._textColor }
        };

        float textOffset = iconWidth + textStartSpace;
        Rect textRect = new Rect(selectionRect.x + textOffset, selectionRect.y, selectionRect.width - textOffset, selectionRect.height);
        EditorGUI.LabelField(textRect, obj.name, style);

    }



}