using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PuzzleGenerator : MonoBehaviour
{

    #region Singleton
    public static PuzzleGenerator instance;

    void Awake() {
        //this.instance = this;
        instance = this;
    }
    #endregion

    public Inventory start;
    public Inventory puzzle;

    public Sprite itemSprite;

    public void GeneratePuzzle() {
        this.puzzle = new Inventory(Random.Range(1, 16), Random.Range(1, 16));

        this.Populate();
        
        List<Item> items = this.puzzle.getItems().OrderBy(x => Random.value).ToList();

        int totalHt = 0;
        int totalWd = 0;

        foreach (Item item in items)
        {
            if (Random.Range(0, 100) >= 50) {
                item.rotate();
            }
            
            totalHt += item.height;

            if (item.width > totalWd) {
                totalWd = item.width;
            }
        }

        this.puzzle.clear();

        this.start = new Inventory(totalHt, totalWd);

        int addAtIdx = 0;

        foreach (Item item in items)
        {
            this.start.addItem(addAtIdx, item);

            addAtIdx += item.height * this.start.width;
        }
    }

    void Populate() {
        int itemID = 0;

        int maxRetries = 1000;
        int currRetries = 0;

        while (!this.puzzle.isFull()) {
            int idx = Random.Range(0, (this.puzzle.height * this.puzzle.width));

            if (this.puzzle.cellTaken(idx)) {
                continue;
            } else {
                Item item = new Item(itemID, Random.Range(1, 4), Random.Range(1, 6), this.itemSprite);

                if (Random.Range(0, 100) >= 50) {
                    item.rotate();
                }

                if (this.puzzle.roomAvailable(idx, item)) {
                    itemID++;

                    this.puzzle.addItem(idx, item);
                } 
            }

            currRetries++;

            if (currRetries >= maxRetries) {
                break;
            }
        }
    }
}
