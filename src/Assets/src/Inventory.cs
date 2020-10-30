using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour, IHasChanged
{
    [SerializeField]
    Transform slots;
    [SerializeField]
    Text inventoryText;

    // Use this for initialization
    void Start()
    {
        HasChanged();
    }

    #region IHasChanged implementation
    public void HasChanged()
    {
    }
    #endregion
}


namespace UnityEngine.EventSystems
{
    public interface IHasChanged : IEventSystemHandler
    {
        void HasChanged();
    }
}