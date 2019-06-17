/* Содержит в себе позицию технологии на канвасе и сам UI объект
 * 
 * 
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TechnologyOnCanvas 
{
    [SerializeField] private TechnologyID id;
    [SerializeField] private GameObject uiElementObject;
    [SerializeField] private UITechnologyElement uiElement;



    public TechnologyOnCanvas(TechnologyID ID, GameObject uiObject, UITechnologyElement uiElement) {
        id = ID;
        uiElementObject = uiObject;
        this.uiElement = uiElement;
    }

    public TechnologyID GetID() => id;
    public UITechnologyElement GetUIElement() => uiElement;

}
