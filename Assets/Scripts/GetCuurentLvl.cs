using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GetCuurentLvl
{
    public static int GetLVLIndex()
    {
        return PlayerPrefs.GetInt("LvlIndex", 1);
    }

    public static void ChangeLVLIndex(int newIndex)
    {
        PlayerPrefs.SetInt("LvlIndex", newIndex);
    }
}
