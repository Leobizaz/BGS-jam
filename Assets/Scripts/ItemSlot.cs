using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ItemSlot : MonoBehaviour, IDropHandler
{

    public bool isMetal;
    public bool isMateria;
    public bool isMinerio;
    public AnimationCurve curve;
    RectTransform UI_Element;
    public GameObject position;
    public int index;
    AudioSource audioS;
    void Awake()
    {
        audioS = GetComponent<AudioSource>();
        UI_Element = this.GetComponent<RectTransform>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Metal")
        {
            if (isMetal)
                Collect(collision.gameObject);
            else CollectWrong(collision.gameObject);

            return;
        }

        if (collision.gameObject.tag == "Materia")
        {
            if (isMateria)
                Collect(collision.gameObject);
            else CollectWrong(collision.gameObject);

            return;
        }


        if (collision.gameObject.tag == "Minerio")
        {
            if (isMinerio)
                Collect(collision.gameObject);
            else CollectWrong(collision.gameObject);

            return;
        }

    }

    public void Collect(GameObject obj)
    {
        audioS.Play();
        GameEvents.current.collectTreco(index);
        Debug.Log(obj + " coletado");
        obj.GetComponent<BoxCollider2D>().enabled = false;
        obj.GetComponent<DragDrop>().enabled = false;
        obj.transform.DOScale(0, 0.6f).SetEase(curve);
        obj.transform.DOMove(position.transform.position, 0.6f).SetEase(curve);
        Destroy(obj, 1);
    }

    public void CollectWrong(GameObject obj)
    {
        //audioS.Play();
        GameEvents.current.collectTreco(index);
        GameEvents.current.collectWrong(index);
        Debug.Log(obj + " coletado");
        obj.GetComponent<BoxCollider2D>().enabled = false;
        obj.GetComponent<DragDrop>().enabled = false;
        obj.transform.DOScale(0, 0.6f).SetEase(curve);
        obj.transform.DOMove(position.transform.position, 0.6f).SetEase(curve);
        Destroy(obj, 1);
    }


}
