using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
public class UITechnologyElement : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image bg;
    [SerializeField] private TextMeshProUGUI techTitle;
    [SerializeField] private TextMeshProUGUI techDescr;
    [SerializeField] private RectTransform[] anchors;
    public RectTransform[] Anchors { get { return anchors; } }

    //public Technology Tech { get; private set; }
    public TechnologyID TechID { get; private set; }

    public static event Action<TechnologyID> OnUITechnologyClick = delegate { };


    //private void Awake() {
    //    TechnologyTree_Controller.OnTechStatusChange += UpdateStatus;
    //}

    //private void OnDestroy() {
    //    TechnologyTree_Controller.OnTechStatusChange -= UpdateStatus;
    //}



    public void OnPointerClick(PointerEventData eventData) {
        OnUITechnologyClick(TechID);
        //SetStatus(t.Status);
    }

    //public void Setup(Technology t) {
    //    Tech = t;
    //    techTitle.text = t.Name.ToString();
    //    techDescr.text = t.Description;
    //    SetStatus();
    //}

    public void Setup(Technology t) {
        TechID = t.ID;
        techTitle.text = t.Name.ToString();
        techDescr.text = t.Description;
        //SetStatus(t.Status);
    }

    //void UpdateStatus(TechnologyID id, Color color) {
    //    if (id != TechID) return;
    //    SetStatus(status);
    //}

    public void SetStatus(Color color) {
        //bg.color = status == TechnologyStatus.Enabled ? Color.white : (status == TechnologyStatus.Completed ? Color.green : Color.red);
        bg.color = color;
        Debug.Log("Color");
    }

    
         
}
