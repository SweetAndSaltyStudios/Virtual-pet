using System;
using UnityEngine;

public class GameMaster : SingeltonPersistant<GameMaster>
{
    #region VARIABLES

    [Header("Test")]
    public bool SetDate;

    [Header("Date")]
    public int Day = 25;
    public int Month = 6;
    public int Year = 2019;
    [Header("Time")]
    public int Hours = 23;
    public int Minutes = 59;
    public int Seconds = 25;

    #endregion 

    #region PROPERTIES

    public DateTime GetCurrentSessionTime
    {
        get 
        {
            return DateTime.Now;
        }
    }

    public DateTime GetLastSessionTime
    {
        get 
        {
            if(PlayerPrefs.HasKey("LastSession") == false)
            {
                return GetCurrentSessionTime;
            }

            return Convert.ToDateTime(PlayerPrefs.GetString("LastSession"));
        }
    }

    public TimeSpan GetTimeBetweenSessions
    {
        get 
        {
            return GetCurrentSessionTime.Subtract(GetLastSessionTime);
        }
    }

    #endregion PROPERTIES

    #region UNITY_FUNCTIONS

    protected override void Awake()
    {
        base.Awake();

        Physics2D.queriesHitTriggers = false;
    }

    private void OnDestroy()
    {
        Debug.Log(GetCurrentSessionTime.ToString());
        PlayerPrefs.SetString("LastSession", GetCurrentSessionTime.ToString());
    }

    // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    // System.Diagnostics.Process.GetCurrentProcess().Kill();

    #endregion UNITY_FUNCTIONS

    #region CUSTOM_FUNCTIONS



    #endregion CUSTOM_FUNCTIONS
}
