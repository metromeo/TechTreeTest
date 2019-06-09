using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TechnologyOnCanvas 
{
    [SerializeField] private TechnologyID id;
    //[SerializeField] private Vector2 position;
    [SerializeField] private GameObject uiElementObject;
    [SerializeField] private UITechnologyElement uiElement;



    public TechnologyOnCanvas(TechnologyID ID, GameObject uiObject, UITechnologyElement uiElement) {
        id = ID;
        uiElementObject = uiObject;
        this.uiElement = uiElement;
    }

    public TechnologyID GetID() => id;
    public UITechnologyElement GetUIElement() => uiElement;

    //public Vector2 GetPosition() => position;

    //public void CheckPosition() {
    //    if (uiElement.transform.localPosition != position) {
    //        position = uiElement
    //    }
    //}

}
