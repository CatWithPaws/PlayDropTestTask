using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance;

    public Constants.VoidDelegate OnLogCountChange;

    private int logCount = 0;

    public int LogCount
    {
        get
        {
            return logCount;
        }

        set
        {
            logCount = value;
            if(OnLogCountChange != null)
            {
                OnLogCountChange();
            }
        }
    }
    
    

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }


}
