using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory// : MonoBehaviour
{
    public int height;
    public int width;
    public int itemCount;

    public Cell[] cells;

    public Inventory(int ht, int wd) {
        this.height = ht;
        this.width = wd;
        this.cells = new Cell[this.height * this.width];
        this.itemCount = 0;

        for (int i = 0; i < this.height * this.width; i++) {
            this.cells[i] = new Cell();
        }
    }

    public void addItem(int idx, Item item) {
        for (int i = 0; i < item.width; i++) {
            for (int j = 0; j < item.height; j++) {
                this.cells[idx + i + (j * this.width)].item = item;
            }
        }

        this.itemCount++;
    }

    public void removeItem(Item toRemove) {
        foreach (Cell cell in this.cells)
        {
            if (cell.getItem() == toRemove) {
                cell.setItem(null);
            }
        }

        this.itemCount--;
    }

    public bool isFull() {
        foreach (Cell cell in this.cells)
        {
            if (cell.item == null) {
                return false;
            }
        }
        return true;
    }

    public bool roomAvailable(int idx, Item item) {
        if ((idx % this.width) + (item.width - 1) >= this.width || ((int)(idx / this.width)) + (item.height - 1) >= this.height) {
            return false;
        }

        for (int i = 0; i < item.width; i++) {
            for (int j = 0; j < item.height; j++) {
                if (this.cells[idx + i + (j * this.width)].item != null) {
                    return false;
                }
            }
        }

        return true;
    }

    public bool cellTaken(int idx) {
        return this.cells[idx].item != null;
    }

    public List<Item> getItems() {
        List<Item> items = new List<Item>();

        foreach (Cell cell in this.cells)
        {
            if (cell.item != null && !items.Contains(cell.item)) {
                items.Add(cell.item);
            }
        }

        return items;
    }

    public void clear() {
        foreach (Cell cell in this.cells)
        {
            cell.item = null;
        }

        this.itemCount = 0;
    }

    public void display() {
        string output = "";

        for (int i = 0; i < this.height * this.width; i++) {
            output += "| " + this.cells[i].getItemStr() + " |";
            if (i % this.width == this.width - 1) {
                output += "\n";
            }
        }

        Debug.Log(output);
    }
}
