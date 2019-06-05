using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UITechnologyElement : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image bg;
    [SerializeField] private TextMeshProUGUI techTitle;
    [SerializeField] private TextMeshProUGUI techDescr;

    public Technology Tech { get; private set; }

    public static event Action<TechnologyID> OnUITechnologyClick = delegate { };


    private void Awake() {
        TechnologyTree_Controller.OnTechStatusChange += UpdateStatus;
    }

    private void OnDestroy() {
        TechnologyTree_Controller.OnTechStatusChange -= UpdateStatus;
    }



    public void OnPointerClick(PointerEventData eventData) {
        OnUITechnologyClick(Tech.ID);
        Setup(Tech);
    }

    public void Setup(Technology t) {
        Tech = t;
        techTitle.text = t.ID.ToString();
        techDescr.text = t.Description;
        SetStatus();
    }

    void UpdateStatus(TechnologyID id, TechnologyStatus status) {
        if (id != Tech.ID) return;
        SetStatus();
    }

    void SetStatus() {
        bg.color = Tech.Status == TechnologyStatus.Enabled ? Color.white : (Tech.Status == TechnologyStatus.Completed ? Color.green : Color.red);
    }
         
}
