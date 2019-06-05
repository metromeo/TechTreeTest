using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class TechnologyTreeDrawer : MonoBehaviour {
    [SerializeField] private UITechnologyElement uiTechElementPrefab;
    [SerializeField] private UITechnologyConnection uiConnectionPrefab;
    [SerializeField] private Transform uiTechsRoot;
    [SerializeField] private TechnologyTree_Controller techTreeController;


    private List<UITechnologyElement> uiTechs = new List<UITechnologyElement>();
    private List<UITechnologyConnection> uiConnections = new List<UITechnologyConnection>();


    private void Awake() {
        TechnologyTree_Controller.OnTechTreeCreated += DrawTree;
    }

    private void OnDestroy() {
        TechnologyTree_Controller.OnTechTreeCreated -= DrawTree;
    }


    void DrawTree(Technology[] techs) {
        foreach (Technology t in techs) {
            UITechnologyElement uit = Instantiate(uiTechElementPrefab, uiTechsRoot);
            uiTechs.Add(uit);
            uit.transform.localPosition = GetPositionFor(t.ID);
            uit.Setup(t);
        }
        foreach (UITechnologyElement uit in uiTechs) {
            List<TechnologyID> childs = techTreeController.GetChildsOf(uit.Tech.ID);
            foreach (TechnologyID c in childs) {
                UITechnologyConnection line = Instantiate(uiConnectionPrefab, uiTechsRoot);
                uiConnections.Add(line);
                line.Setup(uit, GetTechnologyUIByID(c).GetComponent<RectTransform>());
                line.transform.SetAsFirstSibling();
            }
        }
    }

    UITechnologyElement GetTechnologyUIByID(TechnologyID id) => uiTechs.SingleOrDefault(t => t.Tech.ID == id);

    Vector2 GetPositionFor(TechnologyID id) {
        Vector2 res = Vector2.zero;
        switch (id) {
            case TechnologyID.Tech1:
                res = new Vector2(0, 0);
                break;
            case TechnologyID.Tech2:
                res = new Vector2(-350, -0);
                break;
            case TechnologyID.Tech3:
                res = new Vector2(-400, -180);
                break;
            case TechnologyID.Tech4:
                res = new Vector2(0, -150);
                break;
            case TechnologyID.Tech5:
                res = new Vector2(0, -350);
                break;
            case TechnologyID.Tech6:
                res = new Vector2(300, -400);
                break;
            case TechnologyID.Tech7:
                res = new Vector2(0, -500);
                break;
            case TechnologyID.Tech8:
                res = new Vector2(0, -650);
                break;
            default: break;
        }
        return res;
    }
}
