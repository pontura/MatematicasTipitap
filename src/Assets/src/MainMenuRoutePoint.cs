using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuRoutePoint : MonoBehaviour {

    public GameObject doneImage;
    public Sprite imageNotPerfectSymbol;
    public Sprite imagePerfectSymbol;
    public GameObject imagePlayed;

    bool isPerfect;
    private bool played;

    void Start()
    {
        if (imagePlayed == null) return;
        imagePlayed.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);

        if (!played)
            imagePlayed.SetActive(false);
        else
            imagePlayed.SetActive(true);

    }
	public void Init(bool isPerfect) {

        this.isPerfect = isPerfect;
        if (isPerfect)
        {
            doneImage.SetActive(true);
            doneImage.GetComponent<Image>().sprite = imagePerfectSymbol;
            GetComponent<Animator>().enabled = false;
        }
        else
        {
            doneImage.GetComponent<Image>().sprite = imageNotPerfectSymbol;
            SetOff();
        }        
        
    }
    public void ToBePlayed()
    {
        GetComponent<Animator>().enabled = true;
        GetComponent<Animator>().Play("point_win");
    }
    public void SetPlayed()
    {
        played = true;
        imagePlayed.SetActive(true);
    }
    public void SetUnPlayed()
    {
        played = false;
        if (imagePlayed != null)
            imagePlayed.SetActive(false);
    }
    public void SetOn()
    {
        GetComponent<Animator>().enabled = true;
        GetComponent<Animator>().Play("point_on");
    }
    public void SetOff()
    {
        if (isPerfect) return;
        GetComponent<Animator>().Play("point_off");
    }
}
