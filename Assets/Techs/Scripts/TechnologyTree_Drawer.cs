/* Отрисовывает дерево технологий, в Edit mode
 * 
 * 
 * */

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
    [HideInInspector]
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

        // на старте обновить параметры технологий
        //if (Application.isPlaying) {
            UpdateTechParams();
            //this.enabled = false;
        //}
    }

    private void Update() {
        if (Application.isPlaying) {
            return;
        }

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

    /// <summary>
    /// Отрисовать не отрисованные технологии, отрисованным обновить параметры
    /// </summary>
    public void InstantiateUpdateTechsOnCanvas() {

        foreach (Technology t in ttData.GetTechnologies()) {
            //отрисовать технологию, если такая не отрисована (будет создана в центре, позицию надо выставить вручную
            if (GetTechOfCanvasByID(t.ID) == null) {
                GameObject tmp = PrefabUtility.InstantiatePrefab(uiElementPrefab, uiElementsRoot) as GameObject;
                TechnologyOnCanvas toc = new TechnologyOnCanvas(t.ID, tmp, tmp.GetComponent<UITechnologyElement>());
                technologiesOnCanvas.Add(toc);
            }
        }
        UpdateTechParams();
    }

    /// <summary>
    /// Обновить параметры технологии на канвасе
    /// </summary>
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

    /// <summary>
    /// Найти технологию на канвасе по ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    TechnologyOnCanvas GetTechOfCanvasByID(TechnologyID id) {
        TechnologyOnCanvas res = null;
        foreach (TechnologyOnCanvas t in technologiesOnCanvas) {
            if (t.GetID() == id) res = t;
        }
        return res;
    }


    /// <summary>
    /// Возвращает цвет бэка технологии по статусу
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    Color GetColorByStatus(TechnologyStatus s) {
        return s == TechnologyStatus.Disabled ? colorDisabled : (s == TechnologyStatus.Enabled ? colorEnabled : colorCompleted);
    }


    /// <summary>
    /// Перестроить соединения
    /// </summary>
    public void InstantiateUpdateConnections() {

        foreach (TechnologyOnCanvas toc in technologiesOnCanvas) {
            Technology t = ttData.GetTechnologyByID(toc.GetID());
            foreach (TechnologyID child in t.Childs) {
                CreateConnection(toc, GetTechOfCanvasByID(child));
            }
        }
    }


    /// <summary>
    /// Создать соединение между двумя технологиями, если такое не существует
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    void CreateConnection(TechnologyOnCanvas source, TechnologyOnCanvas target) {
        //Проверить, есть ли соединение, если да, ничего не делать
        foreach (UITechnologyConnection u in uiConnections) {
            if (u.GetSource() == source.GetID() && u.GetTarget() == target.GetID()) {
                return;
            }
        }

        //сначала найти ближайший к центру source якорь на target
        RectTransform[] anchors = GetClosestAnchorsBTWTechs(source, target);

        // построить линию между двумя точками
        UITechnologyConnection connection = Instantiate(uiConnectionPrefab, uiElementsRoot);
        uiConnections.Add(connection);
        connection.name = "_" + source.GetID().ToString() + "_to_" + target.GetID().ToString();
        connection.Setup(source.GetID(), target.GetID(), anchors[0], anchors[1]);
        connection.transform.SetAsFirstSibling();
    }


    /// <summary>
    /// Найти ближайшие точки между технологиями (Top, Right, Bottom, Left)
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <returns></returns>
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
}
