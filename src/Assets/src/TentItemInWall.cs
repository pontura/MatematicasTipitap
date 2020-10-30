using UnityEngine;
using System.Collections;

public class TentItemInWall : MonoBehaviour {

    public GameObject locked;
    public GameObject available;
    public GameObject wearing;
    public Vector3 originalPosition;

    public states state;
    public string sexo = "nene";
    public enum states
    {
        LOCKED,
        AVAILABLE,
        WEARING
    }

    public AvatarCustomizator.partsType type;
    public int id;

	void Start () {
        SetState();
        Events.OnAvatarChangeCloth += OnAvatarChangeCloth;
    }
    void OnDestroy()
    {
        Events.OnAvatarChangeCloth -= OnAvatarChangeCloth;
    }
    void OnAvatarChangeCloth(AvatarCustomizator.partsType type, string _sexo, int id)
    {
        this.sexo = _sexo;
        Invoke("SetState", 0.1f);
    }
    void SetState()
    {
        originalPosition = transform.position;

        Items.Data data = Data.Instance.GetComponent<Items>().GetItemData(type, id);
        AvatarStyles avatarStyles = Data.Instance.GetComponent<AvatarStyles>();
        if (data.locked)
        {
            state = states.LOCKED;
            locked.SetActive(true);
            available.SetActive(false);
            wearing.SetActive(false);
            GetComponent<CanvasGroup>().interactable = false;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        else if (
            ((avatarStyles.nena.body == data.id || avatarStyles.nene.body == data.id) && type == AvatarCustomizator.partsType.BODY)
            ||
            ((avatarStyles.nena.glasses == data.id || avatarStyles.nene.glasses == data.id) && type == AvatarCustomizator.partsType.GLASSES)
            ||
            ((avatarStyles.nena.hats == data.id || avatarStyles.nene.hats == data.id) && type == AvatarCustomizator.partsType.HATS)            
            )
        {
            IsWear();
        }
        else
        {
            SetAvailable();
        }
	}
    public void IsWear()
    {
        state = states.WEARING;
        locked.SetActive(false);
        available.SetActive(false);
        wearing.SetActive(true);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
    public void SetAvailable()
    {
        state = states.AVAILABLE;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        locked.SetActive(false);
        available.SetActive(true);
        wearing.SetActive(false);
    }
}
