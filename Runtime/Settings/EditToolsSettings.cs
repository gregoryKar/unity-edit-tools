



using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEditorInternal;


namespace Karianakis.EditTools
{
    internal class EditToolsSettings : ScriptableObject
    {

        private const string k_SettingsPath = "ProjectSettings/EditToolsSettings.asset";

        private static EditToolsSettings _inst;



        //?  FIELDS
        public bool _dunamicDebugShowMonobhaviourNames;
        const bool _dunamicDebugShowMonobhaviourNamesDefault = true;
        public int _dynamicDebugPageDisplayCount
            = _dynamicDebugPageDisplayCountDefault;
        const int _dynamicDebugPageDisplayCountDefault = 5;
        public float _dynamicDebugSize = _dynamicDebugSizeDefault;
        const float _dynamicDebugSizeDefault = 1f;


        public float _terminalSize = _terminalSizeDefault;
        const float _terminalSizeDefault = 1f;
        public int _terminalMaxLogs = _terminalMaxLogsDefault;
        const int _terminalMaxLogsDefault = 10;





        //? GETTERS
        internal static int GetDynamicDebugPageDisplayCount
            => GetOrCreateSettings()._dynamicDebugPageDisplayCount;
        internal static float GetDynamicDebugSize
            => GetOrCreateSettings()._dynamicDebugSize;
        internal static bool GetDynamicDebugShowMonobehaviourNames
            => GetOrCreateSettings()._dunamicDebugShowMonobhaviourNames;

        internal static float GetTerminalSize
            => GetOrCreateSettings()._terminalSize;
        internal static int GetTerminalMaxLogs
            => GetOrCreateSettings()._terminalMaxLogs;




        //? SET VALUES 

        internal static void SetDynamicDebugSize(float value)
        {
            var settings = GetOrCreateSettings();
            settings._dynamicDebugSize = value;
            SaveSettings();
        }

        internal static void SetTerminalSize(float value)
        {
            var settings = GetOrCreateSettings();
            settings._terminalSize = value;
            SaveSettings();
        }
        internal static void SetTerminalMaxLogs(int value)
        {
            var settings = GetOrCreateSettings();
            settings._terminalMaxLogs = value;
            SaveSettings();
        }


        //? CLAMP VALUES
        const float _panelSizeMin = 0.5f;
        const float _panelSizeMax = 2f;
        internal static float ClampDynamicDebugSize(float value)
        {
            return Mathf.Clamp(value, _panelSizeMin, _panelSizeMax);
        }
        internal static float ClampTerminalSize(float value)
        {
            return Mathf.Clamp(value, _panelSizeMin, _panelSizeMax);
        }

        internal static int ClampDynamicDebugPageDisplayCount(int value)
        {
            return Mathf.Clamp(value, 3, 20);
        }

        internal static int ClampTerminalMaxLogs(int value)
        {
            return Mathf.Clamp(value, 3, 12);
        }



        //? FUNCTIONS
        public void RestoreToDefault()
        {

            _dynamicDebugSize = _dynamicDebugSizeDefault;
            _dynamicDebugPageDisplayCount
               = _dynamicDebugPageDisplayCountDefault;
            _dunamicDebugShowMonobhaviourNames
               = _dunamicDebugShowMonobhaviourNamesDefault;

            _terminalSize = _terminalSizeDefault;
            _terminalMaxLogs = _terminalMaxLogsDefault;


        }


        internal static EditToolsSettings GetOrCreateSettings()
        {
            if (_inst != null)
                return _inst;

            Object[] loaded = InternalEditorUtility.LoadSerializedFileAndForget(k_SettingsPath);

            if (loaded != null && loaded.Length > 0)
            {
                _inst = loaded[0] as EditToolsSettings;
            }
            else
            {
                _inst = CreateInstance<EditToolsSettings>();
                SaveSettings();
            }

            return _inst;
        }

        internal static void SaveSettings()
        {
            if (_inst == null)
                return;

            InternalEditorUtility.SaveToSerializedFileAndForget(
                new Object[] { _inst },
                k_SettingsPath,
                true);
        }



    }
}