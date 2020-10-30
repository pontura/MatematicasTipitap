using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragSlot : MonoBehaviour, IDropHandler, IPointerDownHandler {

    public string sexo;

    public void OnPointerDown(PointerEventData eventData)
    {
        Events.OnAvatarChangeCloth(AvatarCustomizator.partsType.BODY, sexo, 0);
        Events.OnAvatarChangeCloth(AvatarCustomizator.partsType.GLASSES, sexo, 0);
        Events.OnAvatarChangeCloth(AvatarCustomizator.partsType.HATS, sexo, 0);
    }
	#region IDropHandler implementation
	public void OnDrop (PointerEventData eventData)
	{
		DragItems.itemBeingDragged.IsWear();
		//DragItems.itemBeingDragged.transform.SetParent (transform);
		ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject,null,(x,y) => x.HasChanged ());
        Events.OnAvatarChangeCloth(DragItems.itemBeingDragged.type, sexo, DragItems.itemBeingDragged.id);
	}
	#endregion
}