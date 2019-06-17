using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIScrollZoom : MonoBehaviour, IScrollHandler
{
    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;
    [SerializeField] private float speed;
    [SerializeField] private RectTransform target;
        
    public void OnScroll(PointerEventData eventData) {
        float v = Mathf.Clamp(target.localScale.x + eventData.scrollDelta.y * Time.deltaTime * speed, minZoom, maxZoom);
        target.localScale = new Vector3(v, v, v);
    }

}

