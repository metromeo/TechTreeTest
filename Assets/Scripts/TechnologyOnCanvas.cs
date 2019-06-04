using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechnologyOnCanvas 
{
    [SerializeField] private TechnologyID id;
    [SerializeField] private Vector2 position;

    public TechnologyOnCanvas(TechnologyOnCanvas techOnCanvas) {
        id = techOnCanvas.GetID();
        position = techOnCanvas.GetPosition();
    }

    public TechnologyOnCanvas(TechnologyID ID, Vector2 position) {
        id = ID;
        this.position = position;
    }

    public TechnologyID GetID() => id;
    public Vector2 GetPosition() => position;

}
