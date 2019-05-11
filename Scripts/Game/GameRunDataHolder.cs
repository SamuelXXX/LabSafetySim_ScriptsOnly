using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameRunDataHolder
{
    static Dictionary<string, bool> flagData = new Dictionary<string, bool>();
    static Dictionary<string, int> typeData = new Dictionary<string, int>();

    public static bool SetFlag(string flagName, bool value)
    {
        if (string.IsNullOrEmpty(flagName))
        {
            return false;
        }
        if (flagData.ContainsKey(flagName))
        {
            flagData[flagName] = value;
            return false;
        }
        else
        {
            flagData.Add(flagName, value);
            return true;
        }
    }

    public static bool GetFlag(string flagName)
    {
        if (string.IsNullOrEmpty(flagName))
        {
            return false;
        }

        if (flagData.ContainsKey(flagName))
        {
            return flagData[flagName];
        }
        else
        {
            flagData.Add(flagName, false);
            return false;
        }
    }

    public static bool SetType(string typeName, int value)
    {
        if (string.IsNullOrEmpty(typeName))
        {
            return false;
        }
        if (typeData.ContainsKey(typeName))
        {
            typeData[typeName] = value;
            return false;
        }
        else
        {
            typeData.Add(typeName, value);
            return true;
        }
    }

    public static int GetType(string typeName)
    {
        if (string.IsNullOrEmpty(typeName))
        {
            return 0;
        }

        if (typeData.ContainsKey(typeName))
        {
            return typeData[typeName];
        }
        else
        {
            typeData.Add(typeName, 0);
            return 0;
        }
    }

}
