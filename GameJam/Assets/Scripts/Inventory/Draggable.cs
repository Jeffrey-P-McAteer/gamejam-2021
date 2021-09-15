using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private bool dragging;
    public bool dropFailed;

    private Transform originalParent;
    private Vector2 originalPosition;
    private bool originalRotation;
    private CanvasGroup canvasGroup;

    private GraphicRaycaster raycaster;

    public CanvasScaler canvas;
    public GameObject dragLayer;

    private void Awake() {
        this.rectTransform = this.gameObject.GetComponent<RectTransform>();
        this.canvasGroup = this.gameObject.GetComponent<CanvasGroup>();
        this.raycaster = this.gameObject.GetComponent<GraphicRaycaster>();
    }

    public void OnPointerDown(PointerEventData eventData) {
        //Debug.Log("Pointer Down");
    }

    public void OnPointerUp(PointerEventData eventData) {
        //Debug.Log("Pointer Up");
    }

    public void OnPointerMove(PointerEventData eventData) {
        //Debug.Log("Pointer Move");
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.R) && this.dragging) {
            this.gameObject.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
            this.gameObject.GetComponent<InventoryReference>().item.rotate();
        }
    }

    public void OnBeginDrag(PointerEventData eventData) {
        this.originalRotation = this.gameObject.GetComponent<InventoryReference>().item.isRotated();
        this.originalParent = this.gameObject.transform.parent;
        this.canvasGroup.alpha = 0.6f;
        this.canvasGroup.blocksRaycasts = false;
        this.originalPosition = this.rectTransform.anchoredPosition;
        this.dragging = true;

        this.gameObject.transform.SetParent(UIManager.instance.dragLayer.transform);
    }

    public void OnDrag(PointerEventData eventData) {
        this.rectTransform.anchoredPosition += eventData.delta / UIManager.instance.canvas.GetComponent<CanvasScaler>().scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) {
        this.gameObject.transform.SetParent(this.originalParent);
        this.rectTransform.anchoredPosition = this.originalPosition;
        this.canvasGroup.blocksRaycasts = true;
        this.canvasGroup.alpha = 1.0f;
        this.dragging = false;

        if (this.gameObject.GetComponent<InventoryReference>().item.isRotated() != this.originalRotation && this.dropFailed) {
            this.gameObject.GetComponent<InventoryReference>().item.rotate();
        }
        
        UIManager.instance.UpdateUI();
    }
}