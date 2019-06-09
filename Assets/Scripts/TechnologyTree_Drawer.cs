using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class TechnologyTree_Drawer : MonoBehaviour {
    [HideInInspector]
    [SerializeField]
    private TechnologyTree_Data ttData;
    public TechnologyTree_Data TTData { get { return ttData; } }
    //[HideInInspector]
    [SerializeField]
    private List<TechnologyOnCanvas> technologiesOnCanvas;
    [SerializeField]
    private List<UITechnologyConnection> uiConnections = new List<UITechnologyConnection>();

    [SerializeField] private GameObject uiElementPrefab;
    [SerializeField] private UITechnologyConnection uiConnectionPrefab;
    [SerializeField] private Transform uiElementsRoot;
    [SerializeField] private Color colorDisabled;
    [SerializeField] private Color colorEnabled;
    [SerializeField] private Color colorCompleted;




    private void Awake() {
        if (technologiesOnCanvas == null) technologiesOnCanvas = new List<TechnologyOnCanvas>();
        if (uiConnections == null) uiConnections = new List<UITechnologyConnection>();
        if (Application.isPlaying) {
            UpdateTechParams();
            this.enabled = false;
        }
    }

    private void Update() {
        // проверять были ли сдвинуты технологии и если да, двигать линии
        foreach (UITechnologyConnection c in uiConnections) {
            if (c.GetLine() == null) continue;
            RectTransform[] anchors = GetClosestAnchorsBTWTechs(GetTechOfCanvasByID(c.GetSource()), GetTechOfCanvasByID(c.GetTarget()));
            if (c.GetLine().transformsFromTo[0] != anchors[0] || c.GetLine().transformsFromTo[1] != anchors[1]) {
                c.Setup(c.GetSource(), c.GetTarget(), anchors[0], anchors[1]);
            }
            c.UpdateLinePositions();
        }
    }

    public void InstantiateUpdateTechsOnCanvas() {
        technologiesOnCanvas.ForEach(t => DestroyImmediate(t.GetUIElement().gameObject));
        technologiesOnCanvas.Clear();

        foreach (Technology t in ttData.GetTechnologies()) {
            GameObject tmp;
            tmp = PrefabUtility.InstantiatePrefab(uiElementPrefab, uiElementsRoot) as GameObject;
            TechnologyOnCanvas toc = new TechnologyOnCanvas(t.ID, tmp, tmp.GetComponent<UITechnologyElement>());
            technologiesOnCanvas.Add(toc);
        }
        UpdateTechParams();
    }

    public void UpdateTechParams() {
        foreach (Technology t in ttData.GetTechnologies()) {
            UITechnologyElement uit = GetTechOfCanvasByID(t.ID).GetUIElement();
            if (uit == null) {
                Debug.LogError("Tech " + t.ID + " not instantiated, instantiate first");
            }
            else {
                uit.name = t.ID.ToString();
                uit.Setup(t);
                uit.SetStatus(GetColorByStatus(t.Status));
            }
        }
    }

    //bool IsTechAlreadyOnCanvas(TechnologyID id) {
    //    bool res = false;
    //    foreach (TechnologyOnCanvas t in technologiesOnCanvas) {
    //        if (t.GetID() == id) res = true;
    //    }
    //    return res;
    //}

    TechnologyOnCanvas GetTechOfCanvasByID(TechnologyID id) {
        TechnologyOnCanvas res = null;
        foreach (TechnologyOnCanvas t in technologiesOnCanvas) {
            if (t.GetID() == id) res = t;
        }
        return res;
    }


    Color GetColorByStatus(TechnologyStatus s) {
        return s == TechnologyStatus.Disabled ? colorDisabled : (s == TechnologyStatus.Enabled ? colorEnabled : colorCompleted);
    }

    public void InstantiateUpdateConnections() {
        uiConnections.ForEach( t => DestroyImmediate(t.gameObject));
        uiConnections.Clear();

        foreach (TechnologyOnCanvas toc in technologiesOnCanvas) {
            Technology t = ttData.GetTechnologyByID(toc.GetID());
            foreach (TechnologyID child in t.Childs) {
                CreateConnection(toc, GetTechOfCanvasByID(child));
            }
        }
    }

    void CreateConnection(TechnologyOnCanvas source, TechnologyOnCanvas target) {
        //сначала найти ближайший к центру source якорь на target
        RectTransform[] anchors = GetClosestAnchorsBTWTechs(source, target);

        //float minDist = float.MaxValue;
        //int anchorOnTargetID = -1;
        //for(int i = 0; i < target.GetUIElement().Anchors.Length; i++){
        //    float dist = Vector2.Distance(source.GetUIElement().transform.position, target.GetUIElement().Anchors[i].position);
        //    if (dist < minDist) {
        //        minDist = dist;
        //        anchorOnTargetID = i;
        //    }
        //}
        //Transform anchorTarget = target.GetUIElement().Anchors[anchorOnTargetID];
        // тоже самое, но от найденного якоря на target ко всем якорям в source
        //minDist = float.MaxValue;
        //int anchorOnSourceID = -1;
        //for (int i = 0; i < source.GetUIElement().Anchors.Length; i++) {
        //    float dist = Vector2.Distance(anchorTarget.position, source.GetUIElement().Anchors[i].position);
        //    if (dist < minDist) {
        //        minDist = dist;
        //        anchorOnSourceID = i;
        //    }
        //}
        //Transform anchorsource = source.GetUIElement().Anchors[anchorOnSourceID];

        // построить линию между двумя точками
        UITechnologyConnection connection = Instantiate(uiConnectionPrefab, uiElementsRoot);
        uiConnections.Add(connection);
        connection.name = "_" + source.GetID().ToString() + "_to_" + target.GetID().ToString();
        connection.Setup(source.GetID(), target.GetID(), anchors[0], anchors[1]);
        connection.transform.SetAsFirstSibling();
        

    }

    RectTransform[] GetClosestAnchorsBTWTechs(TechnologyOnCanvas source, TechnologyOnCanvas target) {
        RectTransform[] res = new RectTransform[2];
        float minDist = float.MaxValue;
        int anchorOnSourceID = -1;
        int anchorOnTargetID = -1;

        for (int i = 0; i < source.GetUIElement().Anchors.Length; i++) {
            float distFromSourceCenterToAnchor = Vector2.Distance(source.GetUIElement().transform.position, source.GetUIElement().Anchors[i].position);
            for (int j = 0; j < target.GetUIElement().Anchors.Length; j++) {
                float distFromTargetCenterToAnchor = Vector2.Distance(target.GetUIElement().transform.position, target.GetUIElement().Anchors[j].position);
                float distFromAnchorToAnchor = Vector2.Distance(source.GetUIElement().Anchors[i].position, target.GetUIElement().Anchors[j].position);
                if (distFromSourceCenterToAnchor + distFromTargetCenterToAnchor + distFromAnchorToAnchor < minDist) {
                    minDist = distFromSourceCenterToAnchor + distFromTargetCenterToAnchor + distFromAnchorToAnchor;
                    anchorOnSourceID = i;
                    anchorOnTargetID = j;
                }
            }
        }
        res[0] = source.GetUIElement().Anchors[anchorOnSourceID];
        res[1] = target.GetUIElement().Anchors[anchorOnTargetID];


        return res;
    }



    //[SerializeField] private UITechnologyElement uiTechElementPrefab;
    //[SerializeField] private UITechnologyConnection uiConnectionPrefab;
    //[SerializeField] private Transform uiTechsRoot;
    //[SerializeField] private TechnologyTree_Controller techTreeController;


    //private List<UITechnologyElement> uiTechs = new List<UITechnologyElement>();
    //private List<UITechnologyConnection> uiConnections = new List<UITechnologyConnection>();


    //private void Awake() {
    //    TechnologyTree_Controller.OnTechTreeCreated += DrawTree;
    //}

    //private void OnDestroy() {
    //    TechnologyTree_Controller.OnTechTreeCreated -= DrawTree;
    //}


    //void DrawTree(Technology[] techs) {
    //    foreach (Technology t in techs) {
    //        UITechnologyElement uit = Instantiate(uiTechElementPrefab, uiTechsRoot);
    //        uiTechs.Add(uit);
    //        //uit.transform.localPosition = GetPositionFor(t.Name);
    //        uit.Setup(t);
    //    }
    //    foreach (UITechnologyElement uit in uiTechs) {
    //        List<TechnologyID> childs = techTreeController.GetChildsOf(uit.Tech.ID);
    //        foreach (TechnologyID c in childs) {
    //            UITechnologyConnection line = Instantiate(uiConnectionPrefab, uiTechsRoot);
    //            uiConnections.Add(line);
    //            line.Setup(uit, GetTechnologyUIByID(c).GetComponent<RectTransform>());
    //            line.transform.SetAsFirstSibling();
    //        }
    //    }
    //}

    //UITechnologyElement GetTechnologyUIByID(TechnologyID id) => uiTechs.SingleOrDefault(t => t.Tech.ID == id);

}
