using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell// : MonoBehaviour
{
    public Item item;

    public void setItem(Item toSet) {
        this.item = toSet;
    }

    public Item getItem() {
        return this.item;
    }

    public string getItemStr() {
        if (this.item == null) {
            return " ";
        } else {
            return this.item.id.ToString();
        }
    }
}
