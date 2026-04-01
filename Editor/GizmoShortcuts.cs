using UnityEditor;
using UnityEngine;
using UnityEditor.ShortcutManagement;
using System.Reflection;

namespace Karianakis.EditTools
{
    public static class GizmoShortcuts
    {


        [Shortcut("Tools/Toggle Scene Game Gizmos gk")]
        static void ToggleSceneGameGizmos()
        {
            //Debug.LogError("ToggleSceneGameGizmosShortcut");
            ToggleSceneGizmos();
            ToggleGameGizmos();
        }

        [Shortcut("Tools/Toggle Scene Gizmos gk")]
        static void ToggleSceneGizmos()
        {
            var sceneViews = Resources.FindObjectsOfTypeAll<SceneView>();

            if (sceneViews.Length < 0) return;

            var sceneView = sceneViews[0];

            sceneView.drawGizmos = !sceneView.drawGizmos;
            sceneView.Repaint();
        }

        // 🔹 Toggle Game View Gizmos
        [Shortcut("Tools/Toggle Game Gizmos gk")]
        static void ToggleGameGizmos()
        {

            if (MyEditorUtils.GetGameView(out var gameView) == false)
            {
                return;
            }
            if (MyEditorUtils.IsWindowViewFocused(gameView) == false)
            {
                return;
            }


            var assembly = typeof(Editor).Assembly;
            var gameViewType = assembly.GetType("UnityEditor.GameView");
            var field = gameViewType.GetField("m_Gizmos",
        BindingFlags.Instance | BindingFlags.NonPublic);

            if (field != null)
            {
                bool current = (bool)field.GetValue(gameView);
                field.SetValue(gameView, !current);

                gameView.Repaint();
                gameView.Focus();
                SceneView.RepaintAll();
            }
            else
            {
                Debug.LogWarning("m_Gizmos field not found.");
            }

        }

        /* METHODS IN GAMEVIEW RELATED TO GIZMOS
        get_drawGizmos
        set_drawGizmos
        get_showGizmos
        set_showGizmos
        IsShowingGizmos
        SetShowGizmos

        PROPERTIES
        drawGizmos
        showGizmos

        FIELDS
        m_Gizmos
        */


        static void Off_ToggleField_m_Gizmos(EditorWindow gameView)
        {
            var assembly = typeof(Editor).Assembly;
            var gameViewType = assembly.GetType("UnityEditor.GameView");
            var field = gameViewType.GetField("m_Gizmos",
        BindingFlags.Instance | BindingFlags.NonPublic);

            if (field != null)
            {

                //Debug.LogError("before " + (bool)field.GetValue(gameView));
                bool current = (bool)field.GetValue(gameView);
                field.SetValue(gameView, !current);

                gameView.Repaint();
                gameView.Focus();
                SceneView.RepaintAll();
                //Debug.LogWarning($"showGizmos field found and toggled {(bool)field.GetValue(gameView)}");
            }
            else
            {
                Debug.LogWarning("m_Gizmos field not found.");
            }
        }

        static void OFF_TogglePropertyAttempt(EditorWindow gameView)
        {
            var assembly = typeof(Editor).Assembly;
            var gameViewType = assembly.GetType("UnityEditor.GameView");

            var prop = gameViewType.GetProperty("showGizmos", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (prop != null)
            {
                bool current = (bool)prop.GetValue(gameView);

                Debug.Log("BEFORE " + current);
                prop.SetValue(gameView, !current);

                gameView.Repaint();
                gameView.Focus();
                SceneView.RepaintAll();
            }
            else
            {
                Debug.LogError("m_ShowGizmos property not found in GameView");
            }
        }

        static void OFF_ToggleGameGizmo(EditorWindow gameView)
        {

            var assembly = typeof(Editor).Assembly;
            var gameViewType = assembly.GetType("UnityEditor.GameView");
            var method = gameViewType.GetMethod("ToggleGizmos",
               BindingFlags.Instance | BindingFlags.NonPublic);

            if (method != null)
            {
                method.Invoke(gameView, null);
                gameView.Repaint();
                Debug.LogError("TOGGLED");
            }
            else
            {
                Debug.LogWarning("ToggleGizmos method not found.");
            }
        }



    }
}