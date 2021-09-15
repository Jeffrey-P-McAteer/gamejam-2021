using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager instance;

    void Awake() {
        //this.instance = this;
        instance = this;
    }
    #endregion

    [SerializeField]
    private GameObject startObject;

    [SerializeField]
    private GameObject startSlots;

    [SerializeField]
    private GameObject startItemLayer;

    [SerializeField]
    private GameObject puzzleObject;

    [SerializeField]
    private GameObject puzzleSlots;

    [SerializeField]
    private GameObject puzzleItemLayer;

    [SerializeField]
    private GameObject cellPrefab;

    [SerializeField]
    private GameObject itemPrefab;

    public GameObject canvas;
    public GameObject dragLayer;

    // Start is called before the first frame update
    void Start()
    {
        PuzzleGenerator.instance.GeneratePuzzle();

        this.SpawnUI();

        this.UpdateUI();
    }

    // Update is called once per frame
    void Update() {
        //UpdateUI();
    }

    void SpawnUI() {
        int count = 0;
        int x_offset = 30;
        int y_offset = -30;

        foreach (Cell cell in PuzzleGenerator.instance.start.cells)
        {
            count++;
            
            GameObject currCell = Instantiate(this.cellPrefab, this.startSlots.transform);
            currCell.GetComponent<ItemHolder>().item = cell.item;

            currCell.GetComponent<Image>().rectTransform.anchoredPosition = new Vector3(x_offset, y_offset, 0);
            
            x_offset += (50);// + 2);
            
            if (count % PuzzleGenerator.instance.start.width == 0) {
                y_offset -= (50);// + 2);
                x_offset = 30;
            }

            currCell.GetComponent<InventoryReference>().inventory = PuzzleGenerator.instance.start;
            currCell.GetComponent<InventoryReference>().index = count - 1;
        }

        this.startObject.GetComponent<RectTransform>().sizeDelta += new Vector2(PuzzleGenerator.instance.start.width * 50, PuzzleGenerator.instance.start.height * 50);

        count = 0;
        x_offset = 30;
        y_offset = -30;

        foreach (Cell cell in PuzzleGenerator.instance.puzzle.cells)
        {
            count++;

            GameObject currCell = Instantiate(this.cellPrefab, this.puzzleSlots.transform);
            currCell.GetComponent<ItemHolder>().item = cell.item;

            currCell.GetComponent<Image>().rectTransform.anchoredPosition = new Vector3(x_offset, y_offset, 0);
            
            x_offset += (50);// + 2);
            
            if (count % PuzzleGenerator.instance.puzzle.width == 0) {
                y_offset -= (50);// + 2);
                x_offset = 30;
            }

            currCell.GetComponent<InventoryReference>().inventory = PuzzleGenerator.instance.puzzle;
            currCell.GetComponent<InventoryReference>().index = count - 1;
        }

        this.puzzleObject.GetComponent<RectTransform>().sizeDelta += new Vector2(PuzzleGenerator.instance.puzzle.width * 50, PuzzleGenerator.instance.puzzle.height * 50);
    }

    public void UpdateUI() {
        /*foreach (Transform child in this.startItemLayer.transform)
        {
            DestroyImmediate(child.gameObject);
            Debug.Log(this.startItemLayer.transform.childCount);
        }

        foreach (Transform child in this.puzzleItemLayer.transform)
        {
            DestroyImmediate(child.gameObject);
        }*/

        int startItemSize = this.startItemLayer.transform.childCount;
        int puzzleItemSize = this.puzzleItemLayer.transform.childCount;

        for (int i = 0; i < startItemSize; i++) {
            DestroyImmediate(this.startItemLayer.transform.GetChild(0).gameObject);
        }

        for (int i = 0; i < puzzleItemSize; i++) {
            DestroyImmediate(this.puzzleItemLayer.transform.GetChild(0).gameObject);
        }

        List<Item> displayedStart = new List<Item>();

        for (int i = 0; i < PuzzleGenerator.instance.start.cells.Length; i++) {
            if (PuzzleGenerator.instance.start.cells[i].item != null && !displayedStart.Contains(PuzzleGenerator.instance.start.cells[i].item)) {
                GameObject child = Instantiate(this.itemPrefab, this.startItemLayer.transform);

                int imgHeight = (PuzzleGenerator.instance.start.cells[i].item.height * 50);
                int imgWidth = (PuzzleGenerator.instance.start.cells[i].item.width * 50);
                child.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(imgWidth, imgHeight);

                Vector3 offset = new Vector3(0, 0, 0);
                if (PuzzleGenerator.instance.start.cells[i].item.height > 1) {
                    offset -= new Vector3(0, (((imgHeight / 2) - 25) * this.canvas.GetComponent<CanvasScaler>().scaleFactor), 0);
                }
                if (PuzzleGenerator.instance.start.cells[i].item.width > 1) {
                    offset += new Vector3((((imgWidth / 2) - 25) * this.canvas.GetComponent<CanvasScaler>().scaleFactor), 0, 0);
                }
                child.gameObject.transform.position = this.startSlots.transform.GetChild(i).transform.position + offset;

                displayedStart.Add(PuzzleGenerator.instance.start.cells[i].item);

                child.GetComponent<InventoryReference>().item = PuzzleGenerator.instance.start.cells[i].item;
                child.GetComponent<InventoryReference>().inventory = PuzzleGenerator.instance.start;
            }
        }

        List<Item> displayedPuzzle = new List<Item>();

        for (int i = 0; i < PuzzleGenerator.instance.puzzle.cells.Length; i++) {
            if (PuzzleGenerator.instance.puzzle.cells[i].item != null && !displayedPuzzle.Contains(PuzzleGenerator.instance.puzzle.cells[i].item)) {
                GameObject child = Instantiate(this.itemPrefab, this.puzzleItemLayer.transform);

                int imgHeight = (PuzzleGenerator.instance.puzzle.cells[i].item.height * 50);
                int imgWidth = (PuzzleGenerator.instance.puzzle.cells[i].item.width * 50);
                child.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(imgWidth, imgHeight);

                Vector3 offset = new Vector3(0, 0, 0);
                if (PuzzleGenerator.instance.puzzle.cells[i].item.height > 1) {
                    offset -= new Vector3(0, (imgHeight / 2) - 25, 0);
                }
                if (PuzzleGenerator.instance.puzzle.cells[i].item.width > 1) {
                    offset += new Vector3((imgWidth / 2) - 25, 0, 0);
                }

                child.gameObject.transform.position = this.puzzleSlots.transform.GetChild(i).transform.position + offset;

                displayedPuzzle.Add(PuzzleGenerator.instance.puzzle.cells[i].item);

                child.GetComponent<InventoryReference>().item = PuzzleGenerator.instance.puzzle.cells[i].item;
                child.GetComponent<InventoryReference>().inventory = PuzzleGenerator.instance.puzzle;
            }
        }
    }
}
