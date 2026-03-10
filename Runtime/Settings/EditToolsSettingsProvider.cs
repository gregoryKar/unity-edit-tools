


using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditorInternal;

namespace Karianakis.EditTools
{
    internal static class EditToolsSettingsProvider
    {



        /// <summary>
        /// Opens the Edit Tools Settings panel in Project Settings.
        /// </summary>
        public static void OpenSettingsPanel()
        {
            SettingsService.OpenProjectSettings(_path);
        }
        const string _path = "ProjectEditTools";
        const string _label = "myLabel";
        const string _showcasePath = "EDIT TOOLS SHOWCASE";

        [SettingsProvider]
        public static SettingsProvider CreateProvider()
        {





            var provider = new SettingsProvider(
                _path + "/" + _label,
                SettingsScope.Project)
            {
                label = _label,
                keywords = new HashSet<string>(new[]
                    { "EditTools",
             "Karianakis",
             "EDIT" ,
             "edit" ,
             "tools" ,
             "Tool" ,}),

                guiHandler = (searchContext) =>
                {
                    var settings = EditToolsSettings.GetOrCreateSettings();

                    EditorGUI.BeginChangeCheck();

                    EditorGUILayout.HelpBox(
                   "hallo my noob friend",
                   MessageType.Info);


                    EditorGUILayout.HelpBox(
                   "Dynamic Debug Settings",
                   MessageType.Info);

                    settings._dynamicDebugPageDisplayCount =
                 EditorGUILayout.IntField("DegugPageCount", settings._dynamicDebugPageDisplayCount);

                    settings._dunamicDebugShowMonobhaviourNames =
              EditorGUILayout.Toggle("Debug Items Show MonoBehaviour Names", settings._dunamicDebugShowMonobhaviourNames);


                    EditorGUILayout.HelpBox(
                   "Terminal Settings",
                   MessageType.Info);

                    settings._terminalMaxLogs =
                EditorGUILayout.IntField("TerminalMaxLogs", settings._terminalMaxLogs);




                    if (EditorGUI.EndChangeCheck())
                    {

                        //? CHECKS
                        // settings.rand = Mathf.Clamp(settings.rand, 0.2f, 1f);
                        // MyPackageSettings.SaveSettings();

                        settings._dynamicDebugPageDisplayCount = EditToolsSettings.ClampDynamicDebugPageDisplayCount(settings._dynamicDebugPageDisplayCount);

                        settings._terminalMaxLogs = EditToolsSettings.ClampTerminalMaxLogs(settings._terminalMaxLogs);


                        DynamicDebugManager.RefreshPage();

                        EditToolsSettings.SaveSettings();
                    }


                    EditorGUILayout.Space();
                    EditorGUILayout.Space();

                    //! both restore works
                    if (GUILayout.Button("Restore Defaults"))
                    {

                        if (EditorUtility.DisplayDialog(
          "Restore Defaults",
          "Are you sure you want to reset settings?",
          "Yes",
          "Cancel"))
                        {
                            settings.RestoreToDefault();
                            EditToolsSettings.SaveSettings();

                            GUI.FocusControl(null);          // Remove focus from active field
                            UnityEditorInternal.InternalEditorUtility.RepaintAllViews();

                        }

                    }
                    if (GUILayout.Button("Spawn Showcase Gameobject"))
                    {
                        EditorUtility.DisplayDialog("Hi", "i will spawn a gameObject "
                       + "go check out monobehaviour attached", "ok", "sure");

                        var obj = GameObject.Instantiate(Resources.Load<GameObject>(_showcasePath));
                        obj.name = "EDIT TOOLS SHOWCASE";

                    }


                },




            };

            return provider;
        }

        /*

          if (GUILayout.Button("Restore Defaults 1"))
                    {

                        if (EditorUtility.DisplayDialog(
          "Restore Defaults",
          "Are you sure you want to reset settings?",
          "Yes",
          "Cancel"))
                        {
                            settings.RestoreToDefault();
                            EditToolsSettings.SaveSettings();

                            GUI.FocusControl(null);          // Remove focus from active field

                            SettingsService.RepaintAllSettingsWindow();
                        }

                    }





        */






    }
}