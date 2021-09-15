using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item// : MonoBehaviour
{
    public int id;

    public int height = 1;
    public int width = 1;

    private bool rotated = false;

    public Sprite sprite;

    public Item(int id, int ht, int wd, Sprite itemSprite) {
        this.id = id;
        this.height = ht;
        this.width = wd;
        this.rotated = false;
        this.sprite = itemSprite;
    }

    public bool isRotated() {
        return this.rotated;
    }

    public void rotate() {
        int swap = this.height;
        this.height = this.width;
        this.width = swap;

        this.rotated = !this.rotated;
    }
}
