using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LogsDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI logsDispayText;

    private string logsTextTemplate = "Logs: ";

    private void Start()
    {
        logsDispayText.text = string.Format(logsTextTemplate + "{0}", GameData.Instance.LogCount);
        GameData.Instance.OnLogCountChange += OnLogCountChanged;
    }

    private void OnLogCountChanged()
    {
        logsDispayText.text = string.Format(logsTextTemplate + "{0}",GameData.Instance.LogCount);
    }
}
