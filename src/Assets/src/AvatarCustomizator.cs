using UnityEngine;
using System.Collections;

public class AvatarCustomizator : MonoBehaviour {

    public partsType parts;
    public enum partsType
    {
        HATS,
        GLASSES,
        BODY,
        SPECIAL
    }

    public string hats;
    public string glasses;
    public string body;

    public SpriteRenderer hats_container;
    public SpriteRenderer glasses_container;
    public SpriteRenderer body1_container;
    public SpriteRenderer body2_container;

	void Start () {
        EmptyItems();
	}
    public void AddItem(partsType parts, int _item)
    {
        SpriteRenderer container = body1_container;
        SpriteRenderer container2 = body2_container;
        string item = "";
        string item2 = "";
        switch (parts)
        {
            case partsType.BODY: 
                container = body1_container;
                item = "items2_body_" + _item + "_a";
                item2 = "items2_body_" + _item + "_b";
                break;
            case partsType.GLASSES:     
                container = glasses_container;
                item = "items2_glasses_" + _item; 
                break;
            case partsType.HATS:        
                container = hats_container;
                item = "items2_hat_" + _item; 
                break;
        }
        container.sprite = Resources.Load<Sprite>("items/" + item);
        container.enabled = true;
        if (item2 != "")
        {
            container2.sprite = Resources.Load<Sprite>("items/" + item2);
            container2.enabled = true;
        }
    }
    void EmptyItems()
    {
        hats_container.enabled = false;
        glasses_container.enabled = false;
        body1_container.enabled = false;
        body2_container.enabled = false;
    }
}
