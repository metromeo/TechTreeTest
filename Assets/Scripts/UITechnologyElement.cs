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
    [SerializeField] private Color defaultTech;
    [SerializeField] private Color enabledTech;
    [SerializeField] private Color completedTech;

    private Technology tech;

    public static event Action<TechnologyID> OnUITechnologyClick = delegate { };


    private void Awake() {
        TechnologyTree.OnTechStatusChange += UpdateStatus;
    }

    private void OnDestroy() {
        TechnologyTree.OnTechStatusChange -= UpdateStatus;
    }



    public void OnPointerClick(PointerEventData eventData) {
        OnUITechnologyClick(tech.ID);
        Setup(tech);
    }

    public void Setup(Technology t) {
        tech = t;
        techTitle.text = t.ID.ToString();
        techDescr.text = t.Description;
        SetStatus();
    }

    void UpdateStatus(TechnologyID id, TechnologyStatus status) {
        if (id != tech.ID) return;
        SetStatus();
    }

    void SetStatus() {
        bg.color = tech.Status == TechnologyStatus.Enabled ? enabledTech : (tech.Status == TechnologyStatus.Completed ? completedTech : defaultTech);
    }
         
}
