using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    Vector3 screenBounds;

    private void Awake()
    {
        {
            canvas = transform.parent.GetComponent<Canvas>();
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
        }
    }

    private void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -screenBounds.x, screenBounds.x), Mathf.Clamp(transform.position.y, -screenBounds.y, screenBounds.y), 0);
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
