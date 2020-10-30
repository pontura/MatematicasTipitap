using UnityEngine;
using System.Collections;
//using GameAnalyticsSDK;

public class StatsManager : MonoBehaviour {
    
	void Start () {
        Events.OnLevelComplete += OnLevelComplete;
    }
    void OnLevelComplete(GameData.types types, bool commitError)
    {
      //  TrackLevelComplete(GAProgressionStatus.Complete, types.ToString(), Data.Instance.routes.gameID.ToString(), commitError.ToString());
    }



    //public void TrackLevelComplete(GAProgressionStatus status, string progression1, string progression2, string progression3)
    //{
    //    GameAnalytics.NewProgressionEvent(status, progression1, progression2, progression3);
    //}
    public void TrackEvent(string eventName, float eventValue = 0)
    {
    //    if(eventValue>0)
    //        GameAnalytics.NewDesignEvent(eventName, eventValue);
    //    else
    //        GameAnalytics.NewDesignEvent(eventName);
    }
   // public void TrackError(GAErrorSeverity severity, string message)
    //{
    //    GameAnalytics.NewErrorEvent(severity, message);
   // }


    ///  setea un tipo de usuario (ponerlo en los settings)
    ///  yo puse:
    ///  easy
    ///  medium
    ///  hard
    void SetCustomDimension01(string customDimension)
    {
       // GameAnalytics.SetCustomDimension01(customDimension);
    }
    /////  setea un tipo de usuario (ponerlo en los settings)
    //void SetCustomDimension02(string customDimension)
    //{
    //    GameAnalytics.SetCustomDimension02(customDimension);
    //}
    /////  setea un tipo de usuario (ponerlo en los settings)
    //void SetCustomDimension03(string customDimension)
    //{
    //   GameAnalytics.SetCustomDimension03(customDimension);
    //}
}
