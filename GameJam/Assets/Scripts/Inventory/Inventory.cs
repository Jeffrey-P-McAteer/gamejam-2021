using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int height = 4;
    public int width = 4;

    private Cell[] cells;

    // Start is called before the first frame update
    void Start()
    {
        this.cells = new Cell[this.height * this.width];
    }

    public void addItem(Item toAdd, int startingCell) {
        // Check for room
        
        // If room exists, add item

        // If room does NOT exist, return item to where it was picked up from
    }

    public void removeItem(Item toRemove) {
        foreach (Cell cell in this.cells)
        {
            if (cell.getItem() == toRemove) {
                cell.setItem(null);
            }
        }
    }
}
