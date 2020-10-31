using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
       // throw new System.NotImplementedException();
        Debug.Log("OnBeginDrag");
        canvasGroup.blocksRaycasts = false;
    }
     public void OnDrag(PointerEventData eventData)
    {
       // throw new System.NotImplementedException();
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta/canvas.scaleFactor;
    }
     public void OnEndDrag(PointerEventData eventData)
    {
       // throw new System.NotImplementedException();
        Debug.Log("OnEndDrag");
        canvasGroup.blocksRaycasts = true;
    }
     public void OnPointerDown(PointerEventData eventData)
    {
       // throw new System.NotImplementedException();
        Debug.Log("OnPointerDown");
        
    }
    // Start is called before the first frame update
    public void OnDrop(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
