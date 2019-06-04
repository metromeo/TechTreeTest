using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechnologyTreeDrawer : MonoBehaviour
{
    [SerializeField] private UITechnologyElement uiTechElementPrefab;
    [SerializeField] private Transform uiTechsRoot;
    //[SerializeField] private TechnologyOnCanvas[] techsOnCanvas;
    

    private List<UITechnologyElement> uiTechs = new List<UITechnologyElement>();
    

    private void Awake() {
        //CreateTechsPositions();
        TechnologyTree.OnTechTreeCreated += DrawTree;
    }

    private void OnDestroy() {
        TechnologyTree.OnTechTreeCreated -= DrawTree;
    }


    void DrawTree(Technology[] techs) {
        foreach (Technology t in techs) {
            UITechnologyElement uit = Instantiate(uiTechElementPrefab, uiTechsRoot);
            uiTechs.Add(uit);
            uit.transform.localPosition = GetPositionFor(t.ID);
            uit.Setup(t);
        }
    }




    //void CreateTechsPositions() {
    //    techsOnCanvas = new TechnologyOnCanvas[(int)TechnologyID.Count];
    //    for (int i = 1; i < techsOnCanvas.Length; i++) {
    //        techsOnCanvas[i] = new TechnologyOnCanvas((TechnologyID)i, GetPositionFor((TechnologyID)i));
    //    }

    //}

    Vector2 GetPositionFor(TechnologyID id) {
        Vector2 res = Vector2.zero;
        switch (id) {
            case TechnologyID.Tech1:
                res = new Vector2(50, 50);
                break;
            case TechnologyID.Tech2:
                res = new Vector2(-150, -150);
                break;
            case TechnologyID.Tech3:
                res = new Vector2(300, -150);
                break;
            default: break;
        }
        return res;
    }

}
