using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Droppable : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData) {
        //Debug.Log("Dropped");
        /*if (eventData.pointerDrag != null) {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = this.gameObject.GetComponent<RectTransform>().anchoredPosition;
        }*/
        if (eventData.pointerDrag.GetComponent<InventoryReference>() != null) {
            //If Inventory has room...
            if (this.gameObject.GetComponent<InventoryReference>().inventory.roomAvailable(this.gameObject.GetComponent<InventoryReference>().index, eventData.pointerDrag.GetComponent<InventoryReference>().item)) {
                // Remove item from other inventory...
                eventData.pointerDrag.GetComponent<InventoryReference>().inventory.removeItem(eventData.pointerDrag.GetComponent<InventoryReference>().item);
                
                // Add item to new inventory...
                this.gameObject.GetComponent<InventoryReference>().inventory.addItem(this.gameObject.GetComponent<InventoryReference>().index, eventData.pointerDrag.GetComponent<InventoryReference>().item);
                
                // Update item's inventory reference
                eventData.pointerDrag.GetComponent<InventoryReference>().inventory = this.gameObject.GetComponent<InventoryReference>().inventory;
                
                return;
            }
        }

        eventData.pointerDrag.GetComponent<Draggable>().dropFailed = true;
        
        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = this.gameObject.GetComponent<RectTransform>().anchoredPosition;
    }
}
