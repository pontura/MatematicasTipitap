using UnityEngine;
using System.Collections;
using System;

public class AvatarStyles : MonoBehaviour {

    [Serializable]
    public class Styles
    {
        public int hats;
        public int body;
        public int glasses;
    }
    public Styles nene;
    public Styles nena;

    void Start()
    {
        nene.hats = PlayerPrefs.GetInt("nene_hats", 0);
        nene.body = PlayerPrefs.GetInt("nene_body", 0);
        nene.glasses = PlayerPrefs.GetInt("nene_glasses", 0);

        nena.hats = PlayerPrefs.GetInt("nena_hats", 0);
        nena.body = PlayerPrefs.GetInt("nena_body", 0);
        nena.glasses = PlayerPrefs.GetInt("nena_glasses", 0);

        Events.OnAvatarChangeCloth += OnAvatarChangeCloth;
        Events.ResetApp += ResetApp;
    }
    void OnDestroy()
    {
        Events.OnAvatarChangeCloth -= OnAvatarChangeCloth;
        Events.ResetApp -= ResetApp;
    }
    void OnAvatarChangeCloth(AvatarCustomizator.partsType type, string _sexo, int id)
    {
        Styles sexo;
        if (_sexo == "nene")
            sexo = nene;
        else sexo = nena;

        switch (type)
        {
            case AvatarCustomizator.partsType.BODY:
                PlayerPrefs.SetInt(_sexo + "_body", id);
                sexo.body = id; 
                break;
            case AvatarCustomizator.partsType.GLASSES:
                PlayerPrefs.SetInt(_sexo + "_glasses", id);
                sexo.glasses = id; 
                break;
            default: sexo.hats = id;
                PlayerPrefs.SetInt(_sexo + "_hats", id);
                break;
        }
    }
    void ResetApp()
    {
        nene.body = 0;
        nene.hats = 0;
        nene.glasses = 0;

        nena.body = 0;
        nena.hats = 0;
        nena.glasses = 0;

    }
}
