using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class StatsPage : MonoBehaviour
{
    public static StatsPage Instance;

    [SerializeField] private GameObject screen;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI totalKills;
    [SerializeField] private TextMeshProUGUI totalDamage;
    [SerializeField] private TextMeshProUGUI totalSpent;
    [SerializeField] private TextMeshProUGUI totalElapsedTime;
    [SerializeField] private TextMeshProUGUI totalWins;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        if(screen != null)
        {
            screen.SetActive(false);
        }
        else
        {
            Debugger.Log(Debugger.AlertType.Warning, "No screen object set for stats page");
        }
    }

    public void ToggleScreen()
    {
        if(screen != null)
        {
            totalKills.text = DataHandler.GetFlagInt(DataHandler.Flag.totalKills).ToString();
            totalDamage.text = DataHandler.GetFlagInt(DataHandler.Flag.totalDamage).ToString();
            totalSpent.text = DataHandler.GetFlagInt(DataHandler.Flag.totalSpent).ToString();
            totalElapsedTime.text = FormatTime(DataHandler.GetFlagInt(DataHandler.Flag.totalTime));
            totalWins.text = DataHandler.GetFlagInt(DataHandler.Flag.totalWins).ToString();

            screen.SetActive(!screen.activeSelf);
        }
    }

    public static string FormatTime(float timeInSeconds)
    {
        if (timeInSeconds < 0)
        {
            throw new ArgumentOutOfRangeException("Time cannot be negative.");
        }

        // Convert seconds to hours, minutes, and remaining seconds
        int hours = (int)Mathf.Floor(timeInSeconds / 3600f); // Floor ensures whole numbers
        int minutes = (int)Mathf.Floor((timeInSeconds % 3600f) / 60f);
        int seconds = (int)(timeInSeconds % 60f);

        // Format the time string with leading zeros for hours and minutes if necessary
        string formattedTime = $"{hours:00}:{minutes:00}:{seconds:00}";

        return formattedTime;
    }

}
