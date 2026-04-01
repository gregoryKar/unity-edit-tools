


using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Karianakis.EditTools
{
    [Serializable]
    public class TerminalLogs
    {


        public TerminalLogs(TMPro.TextMeshProUGUI logsAboveText)
        {
            _logsAboveText = logsAboveText;
        }

        [SerializeField] List<LogItem> _logs = new();
        int _logBrowsingIndex = -1;
        TMPro.TextMeshProUGUI _logsAboveText;


        //? INTERNAL
        void RefreshLogs()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < _logs.Count; i++)
            {
                string lineText = $"[{i}]" + _logs[i].GetDisplayContent();
                sb.AppendLine(lineText);
            }
            string result = sb.ToString();
            _logsAboveText.text = result;
        }
        void RefreshLogsHighlight()
        {
            for (int i = 0; i < _logs.Count; i++)
            {
                if (i == _logBrowsingIndex)
                {
                    _logs[i].SetHighlighted(true);
                }
                else
                {
                    _logs[i].SetHighlighted(false);
                }
            }

        }


        //? EXPOSED
        internal void ClearLogs()
             => _logs.Clear();

        internal string GetNextFromLogs(int direction)
        {
            if (_logs.Count <= 0) return "emptyLogs";

            _logBrowsingIndex += direction;

            if (_logBrowsingIndex >= _logs.Count)
            {
                _logBrowsingIndex = 0;
            }
            else if (_logBrowsingIndex < 0)
            {
                _logBrowsingIndex = _logs.Count - 1;
            }

            RefreshLogsHighlight();
            RefreshLogs();
            return GetLogLine(_logBrowsingIndex);
        }


        internal void AddLogLine(string line, string prefix, Color color = default)
        {

            var log = new LogItem(line, prefix, color);
            _logs.Add(log);
            if (_logs.Count > EditToolsSettings.GetTerminalMaxLogs)
                _logs.RemoveAt(0);

            RefreshLogs();
        }
        internal string GetLogLine(int index)
        {
            if (index < 0 || index >= _logs.Count)
                return "problematic log index : " + index;
            return _logs[index].GetContent; // to remove the prefix from the log line
        }

        internal void ClearLogBrowsing()
        {
            _logBrowsingIndex = -1;
            RefreshLogsHighlight();
            RefreshLogs();
        }

        internal void EditPrintAllLogs()
        {
            Debug.LogError("EditPrintAllLogs: "+ _logs.Count);
            for (int i = 0; i < _logs.Count; i++)
            {
                int index = i;
                string content =
                _logs[i].GetContent;

                Debug.Log(index + " " + content);
                Debug.Log("[ " + _logs[i].GetDisplayContent() + " ]");
            }
        }

    }
}