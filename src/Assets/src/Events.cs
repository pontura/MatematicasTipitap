using UnityEngine;
using System.Collections;

public static class Events {

    public static System.Action DebugWinLevel = delegate { };
    public static System.Action ResetApp = delegate { }; 
    public static System.Action OnSceneLoaded = delegate { };
    public static System.Action StartGame = delegate { };
    public static System.Action<bool> OnGamePaused = delegate { };
    public static System.Action OnStartCountDown = delegate { }; 
    public static System.Action<int> OnRefreshScore = delegate { };

    public static System.Action UnlockNextRoute = delegate { };
    public static System.Action AvatarsIdle = delegate { };
    public static System.Action MonkeysShoot = delegate { };
    public static System.Action<bool> MonkeysAiming = delegate { };
    public static System.Action<string> OnSoundFX = delegate { };
    public static System.Action<string> OnMusic = delegate { };
    public static System.Action<string> OnVoiceSay = delegate { };
    public static System.Action OnTutorialReady = delegate { };
    public static System.Action OnSayCorrectWord = delegate { };
    public static System.Action OnSayCorrectWordReal = delegate { };
    
    public static System.Action OnSayCorrectWord_with_beep = delegate { };
    public static System.Action<string, float> OnVoiceSayFromList = delegate { };
    public static System.Action ResetTimeToSayNotPlaying = delegate { };
    public static System.Action<AvatarCustomizator.partsType, string, int> OnAvatarChangeCloth = delegate { };

    public static System.Action<bool> OnShowMap = delegate { };
    
    public static System.Action<string> OnSoundFXSecondary = delegate { };
    public static System.Action<GameData.types, bool> OnLevelComplete = delegate { };

    public static System.Action WinDiploma = delegate { };
    public static System.Action OnPerfect = delegate { };
    public static System.Action OnGood = delegate { };
    public static System.Action<int> RutaSelected = delegate { };

    public static System.Action<GameData.types, int> OnAddWordToList = delegate { };
    public static System.Action<GameData.types, string> OnAddFinalWordToList = delegate { };
    public static System.Action<string> OnAddWrongWord = delegate { };
    

    public static System.Action OnChangeLane = delegate { };
    public static System.Action OnGameReady = delegate { };
    public static System.Action OnChangeLaneComplete = delegate { }; 

    public static System.Action OnDolphinCrash = delegate { };
    public static System.Action OnDolphinJump = delegate { };

    public static System.Action OnJump = delegate { };
    
    public static System.Action<SwipeDetector.directions> OnSwipe = delegate { };

    public static System.Action OnPoolAllItemsInScene = delegate { };

    public static System.Action<string> OnGotWord = delegate { };
    public static System.Action<GameData.types> OnOkWord = delegate { };

    public static System.Action<int> WonItem = delegate { };

    public static System.Action OnButtonClick = delegate { };

}
