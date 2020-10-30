using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class GameData {

    public types type;
    public bool perfect;

    public enum types
    {
        MONKEY,
        BRIDGE,
        FROGGER,
        DOLPHIN
    }
    public int wordsQty;

}
