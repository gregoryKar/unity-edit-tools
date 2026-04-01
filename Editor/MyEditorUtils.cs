using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Karianakis.EditTools
{
    internal static class MyEditorUtils
    {

        public static bool GetGameView(out EditorWindow gameView)
        {
            gameView = null;
            var gameViewType = typeof(Editor).Assembly.GetType("UnityEditor.GameView");
            if (gameViewType == null)
            {
                Debug.LogError("UnityEditor.GameView type not found in editor Assembly");
                return false;
            }

            var allViews = Resources.FindObjectsOfTypeAll<EditorWindow>();
            for (int i = 0; i < allViews.Length; i++)
            {
                if (allViews[i].GetType() == gameViewType)
                {
                    gameView = allViews[i];
                    break;
                }
            }
            return gameView != null;
        }


        public static bool IsWindowViewFocused(EditorWindow window)
        {
            if (window == null)
            {
                Debug.LogError("Provided window is null");
                return false;
            }

            return window == EditorWindow.focusedWindow;
        }




        //? SEARCH FOR FIELDS, PROPERTIES, METHODS IN A TYPE
        //? i used to look inside GameView to find how gizmos are toggled on and off in the game view, but you can use this to look for anything in any type. Just change the typeName and searchText variables.

        const string searchText = "gizmo";
        const string typeName = "UnityEditor.GameView";
        const bool searchForText = true;

        [MenuItem("Tools/Debug/GameView Members")]
        static void ListMembers()
        {
            var assembly = typeof(Editor).Assembly;
            var type = assembly.GetType(typeName);

            Debug.Log("=== METHODS ===");
            foreach (var m in type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (ContainsText(m.Name, searchText)
                    || searchForText == false)
                {
                    Debug.Log(m.Name);
                }
            }

            Debug.LogWarning("=== PROPERTIES ===");
            foreach (var p in type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (ContainsText(p.Name, searchText)
                    || searchForText == false)
                {
                    Debug.LogWarning(p.Name);
                }
            }

            Debug.LogError("=== FIELDS ===");
            foreach (var f in type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (ContainsText(f.Name, searchText)
                    || searchForText == false)
                {
                    Debug.LogError(f.Name);
                }
            }
        }

        public static bool ContainsText(string text, string lookFOr)
        {
            return text.IndexOf(lookFOr, System.StringComparison.OrdinalIgnoreCase) >= 0;
        }


    }
}