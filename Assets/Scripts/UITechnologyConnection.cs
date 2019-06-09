using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

[System.Serializable]
public class UITechnologyConnection : MonoBehaviour
{
    [SerializeField] private UILineConnector line;
    [SerializeField] private TechnologyID sourceID;
    [SerializeField] private TechnologyID targetID;
    RectTransform source;
    RectTransform target;

    private void Awake() {
        TechnologyTree_Controller.OnTechStatusChange += UpdateStatus;
    }

    private void OnDestroy() {
        TechnologyTree_Controller.OnTechStatusChange -= UpdateStatus;
    }

    public void Setup(TechnologyID _sourceID, TechnologyID _targetID, RectTransform source, RectTransform target) {
        transform.position = source.position;
        sourceID = _sourceID;
        targetID = _targetID;
        this.source = source;
        this.target = target;
        line.Setup(source, target);
        
        //UpdateStatus(TechOrigin, origin.TechID.Status);
    }

    public void UpdateLinePositions() {
        line.CheckAndRecalcPositions();
        //transform.position = source.position;
    }

    void UpdateStatus(TechnologyID id, TechnologyStatus status) {
        if (id != sourceID) return;
        line.SetLineColor(status == TechnologyStatus.Enabled ? Color.white : (status == TechnologyStatus.Completed ? Color.green : Color.red));
    }

    public UILineConnector GetLine() => line;
    public TechnologyID GetSource() => sourceID;
    public TechnologyID GetTarget() => targetID;



}
