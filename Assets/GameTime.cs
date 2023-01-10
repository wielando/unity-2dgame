using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTime : MonoBehaviour
{

    private static bool _isPaused = false;
    public static float _deltaTime { get { return _isPaused ? 0 : Time.deltaTime; } }

    
    public static bool SetIsPaused(bool status)
    {
        return _isPaused = status;
    }

    public static bool GetIsPaused()
    {
        return _isPaused;
    }

}
