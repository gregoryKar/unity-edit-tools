
//PanelEditorNavigationShortcuts

using UnityEditor;
using UnityEngine;
using System;
using System.Reflection;
using System.Collections;
using UnityEditor.ShortcutManagement;

static class DockTabSwitcherShortcuts
{



    // %#h = Ctrl + Shift + H (Windows)
    // % = Ctrl
    // # = Shift
    // & = Alt

    // [Shortcut("Tools/Collapse Everything Kar", KeyCode.F, ShortcutModifiers.Shift)]

    // other way to add shortcuts[MenuItem("Tools/Next Tab %#]")] // Ctrl + PageDown
    [Shortcut("Tools/Next Tab Karianakis", KeyCode.A)] // Ctrl + PageDown
    static void NextTab()
    {
        SwitchTab(1);
    }

    [Shortcut("Tools/Previous Tab Karianakis", KeyCode.A)] 
    static void PreviousTab()
    {
        SwitchTab(-1);
    }

    static void SwitchTab(int direction)
    {
        var window = EditorWindow.focusedWindow;
        if (window == null) return;

        var parentField = typeof(EditorWindow)
            .GetField("m_Parent", BindingFlags.NonPublic | BindingFlags.Instance);

        var dockArea = parentField.GetValue(window);
        if (dockArea == null) return;

        var panesField = dockArea.GetType()
            .GetField("m_Panes", BindingFlags.NonPublic | BindingFlags.Instance);

        var panes = panesField.GetValue(dockArea) as IList;
        if (panes == null || panes.Count <= 1) return;

        int currentIndex = panes.IndexOf(window);
        int nextIndex = (currentIndex + direction + panes.Count) % panes.Count;

        var nextWindow = panes[nextIndex] as EditorWindow;
        nextWindow?.Focus();
    }
}