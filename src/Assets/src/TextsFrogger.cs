using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System;

public class TextsFrogger : Texts {

    public List<Vuelta> vueltas1;
    public List<Vuelta> vueltas2;
    public List<Vuelta> vueltas3;

    [Serializable]
    public class Vuelta
    {
        public int id;
        public string ok;
        public List<string> wrong;
        public string terminacion;
    }
	void Start () {
        Load("frogger");
	}
    public Vuelta GetVueltaByLevel()
    {
         List<Vuelta> activeRoute = vueltas1;
         switch (Data.Instance.routes.routeID)
         {
             case 2:
            case 5:
            case 8:
                activeRoute = vueltas2;
                 break;
             case 3:
            case 6:
            case 9:
                activeRoute = vueltas3;
                 break;
         }       

        if (activeRoute.Count == 0) return null;

        if (Data.Instance.levelsManager.frogger > activeRoute.Count - 1)
            Data.Instance.levelsManager.frogger = 0;

        return activeRoute[Data.Instance.levelsManager.frogger];
    }
    public List<Vuelta> GetVuelta()
    {
        List<Vuelta> activeRoute = vueltas1;
        switch (Data.Instance.routes.routeID)
        {
            case 2:
                activeRoute = vueltas2;
                break;
            case 5:
                activeRoute = vueltas2;
                break;
            case 8:
                activeRoute = vueltas2;
                break;
            case 3:
                activeRoute = vueltas3;
                break;
            case 6:
                activeRoute = vueltas3;
                break;
            case 9:
                activeRoute = vueltas3;
                break;
        }

        if (Data.Instance.levelsManager.frogger > activeRoute.Count - 1)
            Data.Instance.levelsManager.frogger = 0;

        return activeRoute;
    }
    public override void LoadDataMinigames(string json_data)
    {        
        JSONNode Json = SimpleJSON.JSON.Parse(json_data);

        string texts = "texts";

        for (int a = 0; a < Json[texts].Count; a++)
        {
            Vuelta vuelta = new Vuelta();
            vuelta.id = a;
            vuelta.ok = Json[texts][a]["ok"];
            vuelta.wrong = new List<string>();
            for (int b = 0; b < Json[texts][a]["wrong"].Count; b++ )
            {
                string word = Json[texts][a]["wrong"][b];
                vuelta.wrong.Add(word);
            }
            Utils.ShuffleListTexts(vuelta.wrong);

            string lastTwoChars = vuelta.ok.Substring(vuelta.ok.Length - 2);
            vuelta.terminacion = lastTwoChars.ToUpper();

            if (vuelta.terminacion == "AR")
                vueltas1.Add(vuelta);
            else if (vuelta.terminacion == "ER")
                vueltas2.Add(vuelta);
            else
                vueltas3.Add(vuelta);
        }
        
    }
}
