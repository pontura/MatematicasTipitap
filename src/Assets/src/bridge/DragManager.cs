using UnityEngine;
using System.Collections;
public class DragManager : MonoBehaviour
{
    private bool draggingItem = false;
    private GameObject draggedObject;
    private Vector2 touchOffset;
    private Vector3 originalSize;
    private Vector3 originalPosition;
    private BridgeGame game;

    void Start()
    {
        game = GetComponent<BridgeGame>();
    }
    void Update()
    {
        if (game.state != BridgeGame.states.PLAYING) return;
        if (HasInput)
        {
            DragOrPickUp();
        }
        else
        {
            if (draggingItem)
                DropItem();
        }
    }

    Vector2 CurrentTouchPosition
    {
        get
        {
            Vector2 inputPos;
            inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return inputPos;
        }
    }

    private void DragOrPickUp()
    {
        var inputPosition = CurrentTouchPosition;

        if (draggingItem)
        {
            draggedObject.transform.position = inputPosition + touchOffset;            
        }
        else
        {
            RaycastHit2D[] touches = Physics2D.RaycastAll(inputPosition, inputPosition, 0.5f);
            if (touches.Length > 0)
            {
                var hit = touches[0];
                if (hit.transform != null)
                {                    
                    draggingItem = true;
                    draggedObject = hit.transform.gameObject;
                 //   print("DICE: " + draggedObject.GetComponent<BridgeItem>().letter.ToLower());
                    string letra = draggedObject.GetComponent<BridgeItem>().letter.ToLower();
                    if (letra == "ñ")
                        letra = "enie";
                    Events.OnVoiceSay("letras/" + letra);
                    Events.ResetTimeToSayNotPlaying();

                    if (draggedObject.GetComponent<BridgeItem>().slotAttached != null)
                        draggedObject.GetComponent<BridgeItem>().slotAttached.EmptyLetters();

                    draggedObject.GetComponent<BridgeItem>().slotAttached = null;
                    draggedObject.GetComponent<BridgeItem>().SetWrong(false);
                    touchOffset = (Vector2)hit.transform.position - inputPosition;
                    originalSize = draggedObject.transform.localScale;
                    draggedObject.transform.localScale = new Vector3(1f, 1f, 1f);
                    originalPosition = draggedObject.transform.position;
                }
            }
        }
    }

    private bool HasInput
    {
        get
        {
            return Input.GetMouseButton(0);
        }
    }

    void DropItem()
    {
        draggingItem = false;
        BridgeItem item = draggedObject.GetComponent<BridgeItem>();

        draggedObject.transform.localScale = originalSize;

        if ( GetComponent<BridgeGame>().CheckToFill(item) )
            return;

        draggedObject.transform.position = item.originalPosition;
    }
}