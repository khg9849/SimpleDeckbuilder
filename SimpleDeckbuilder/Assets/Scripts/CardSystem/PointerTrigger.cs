using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerTrigger : MonoBehaviour, IPointerEnterHandler
{
    protected CardUI _cardUI;
    private void Awake()
    {
        _cardUI = GetComponentInParent<CardUI>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(_cardUI != null)
        {
            _cardUI.OnSelected();
        }
    }
}
