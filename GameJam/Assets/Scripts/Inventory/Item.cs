using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int height = 1;
    public int width = 1;

    public UnityEngine.UI.Image image;

    private bool rotated = false;

    bool getRotated() {
        return this.rotated;
    }
}
