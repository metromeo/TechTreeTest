using UnityEngine;
using UnityEngine.EventSystems;

public class OnDragExample : MonoBehaviour {
    void Start() {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Drag;
        entry.callback.AddListener((data) => { OnDragDelegate((PointerEventData)data); });
        trigger.triggers.Add(entry);
    }

    public void OnDragDelegate(PointerEventData data) {
        Debug.Log("Dragging. " + data.delta);
        transform.localPosition += new Vector3(data.delta.x, data.delta.y, 0);
    }
}