using UnityEditor;
using UnityEngine;
using UnityEditor.ShortcutManagement;



static class CollapseEditorShortcuts
{
    // %#h = Ctrl + Shift + H (Windows)
    // % = Ctrl
    // # = Shift
    // & = Alt

    [Shortcut("Tools/Collapse Everything gk")]
    private static void CollapseAllShortcut()
    {
        CollapseExpandAllHierarchyItems(false, false);
        CollapseExpandAllInspectorComponents(false);
    }
    [Shortcut("Tools/Expand Everything gk")]
    private static void ExpandAllShortcut()
    {
        CollapseExpandAllHierarchyItems(true, false);
        CollapseExpandAllInspectorComponents(true);
    }

    [Shortcut("Tools/Collapse Selected gk")]
    private static void CollapseSelectedShortcut()
    {
        CollapseExpandAllHierarchyItems(false, true);
        CollapseExpandAllInspectorComponents(false);
    }
    [Shortcut("Tools/Expand Selected gk")]
    private static void ExpandSelectedShortcut()
    {
        CollapseExpandAllHierarchyItems(true, true);
        CollapseExpandAllInspectorComponents(true);
    }


    // Collapses all components in the currently focused HIERARCHY window.
    public static void CollapseExpandAllHierarchyItems
        (bool value, bool selectedOnly)
    {
        var hierarchy = EditorWindow.focusedWindow;
        if (hierarchy == null || hierarchy.titleContent.text != "Hierarchy")
        {
            Debug.LogWarning("Focus the Hierarchy window first.");
            return;
        }

        var type = typeof(EditorWindow).Assembly.GetType("UnityEditor.SceneHierarchyWindow");
        var setExpandedRecursive = type.GetMethod("SetExpandedRecursive",
            System.Reflection.BindingFlags.Instance |
            System.Reflection.BindingFlags.Public |
            System.Reflection.BindingFlags.NonPublic);

        if (setExpandedRecursive == null)
        {
            Debug.LogError("Could not find SetExpandedRecursive method via reflection.");
            return;
        }

        if (selectedOnly)
        {
            foreach (var obj in Selection.gameObjects)
            {
                setExpandedRecursive.Invoke(hierarchy, new object[] { obj.GetInstanceID(), value });
            }
        }
        else
        {
            foreach (var go in UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects())
            {
                setExpandedRecursive.Invoke(hierarchy, new object[] { go.GetInstanceID(), value });
            }
        }

    }



    // Collapses all components in the currently focused Inspector window.
    public static void CollapseExpandAllInspectorComponents(bool value)
    {
        var inspector = EditorWindow.focusedWindow;
        if (inspector == null)
        {
            //Debug.LogWarning("Focus the Inspector window first.");
            return;
        }
        var inspectorType = typeof(Editor).Assembly.GetType("UnityEditor.InspectorWindow");
        if (inspectorType == null || !inspectorType.IsInstanceOfType(inspector))
        {
            //Debug.LogWarning("Focused window is not an Inspector window.");
            return;
        }

        void CollapseAll()
        {

            var collapseMethod = inspectorType.GetMethod("CollapseAllComponents", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);

            if (collapseMethod == null)
            {
                Debug.LogError("Could not find CollapseAllComponents method via reflection.");
                return;
            }
            collapseMethod.Invoke(inspector, null);

        }

        void ExpandAll()
        {

            var expandMethod = inspectorType.GetMethod("ExpandAllComponents", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);

            if (expandMethod == null)
            {
                Debug.LogError("Could not find ExpandAllComponents method via reflection.");
                return;
            }
            expandMethod.Invoke(inspector, null);

        }

        if (value)
            ExpandAll();
        else
            CollapseAll();

    }
}
