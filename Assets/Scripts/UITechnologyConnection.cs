using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class UITechnologyConnection : MonoBehaviour
{
    [SerializeField] private UILineConnector line;
    public TechnologyID TechOrigin { get; private set; }


    private void Awake() {
        TechnologyTree_Controller.OnTechStatusChange += UpdateStatus;
    }

    private void OnDestroy() {
        TechnologyTree_Controller.OnTechStatusChange -= UpdateStatus;
    }

    public void Setup(UITechnologyElement origin, RectTransform destination) {
        TechOrigin = origin.Tech.ID;
        line.Setup(origin.GetComponent<RectTransform>(), destination);
        UpdateStatus(TechOrigin, origin.Tech.Status);
    }

    void UpdateStatus(TechnologyID id, TechnologyStatus status) {
        if (id != TechOrigin) return;
        line.SetLineColor(status == TechnologyStatus.Enabled ? Color.white : (status == TechnologyStatus.Completed ? Color.green : Color.red));
    }

    
}
