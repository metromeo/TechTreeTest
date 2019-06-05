using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TechnologyTree_Data))]
public class TechnologyTree_Controller : MonoBehaviour
{

    private TechnologyTree_Data data;

    public static event Action<Technology[]> OnTechTreeCreated = delegate { };
    public static event Action<TechnologyID, TechnologyStatus> OnTechStatusChange = delegate { };

    private void Awake() {
        data = GetComponent<TechnologyTree_Data>();
        UITechnologyElement.OnUITechnologyClick += ChangeTechStatus;
        TechnologyTreeCreator.OnTechnologiesCreate += AddTechnologies;
    }

    private void OnDestroy() {
        UITechnologyElement.OnUITechnologyClick -= ChangeTechStatus;
        TechnologyTreeCreator.OnTechnologiesCreate -= AddTechnologies;
    }

    public void AddTechnologies(Technology[] techs, byte[,] behs) {
        data.SetUpTechs(techs);
        data.SetUpBehaviours(behs);
        OnTechTreeCreated(techs);
    }

    

    void ChangeTechStatus(TechnologyID id) {
        Technology t = data.GetTechnologyByID(id);
        if (t.Status != TechnologyStatus.Completed) {
            CompleteTech(t);
            List<TechnologyID> childs = GetChildsOf(id);
            foreach (TechnologyID c in childs) {
                List<TechnologyID> parents = GetParentsOf(c);
                bool allParentsEnabled = true;
                foreach (TechnologyID p in parents) {
                    if (data.GetTechnologyByID(p).Status != TechnologyStatus.Completed) {
                        allParentsEnabled = false;
                        break;
                    }
                }
                if (allParentsEnabled) {
                    Technology ct = data.GetTechnologyByID(c);
                    EnableTech(ct);
                }
            }
        }
    }

    public List<TechnologyID> GetChildsOf(TechnologyID parent) {
        List<TechnologyID> res = new List<TechnologyID>();
        for (int i = 0; i < (int)TechnologyID.Count; i++) {
            if (data.GetBehaviour(parent, (TechnologyID)i) == 1) {
                res.Add((TechnologyID)i);
            }
        }
        return res;
    }

    public List<TechnologyID> GetParentsOf(TechnologyID child) {
        List<TechnologyID> res = new List<TechnologyID>();
        for (int i = 0; i < (int)TechnologyID.Count; i++) {
            if (data.GetBehaviour((TechnologyID)i, child) == 1) {
                res.Add((TechnologyID)i);
            }
        }
        return res;
    }


    void EnableTech(Technology t) {
        t.SetEnabled();
        OnTechStatusChange(t.ID, t.Status);
    }
    void CompleteTech(Technology t) {
        t.SetComplete();
        OnTechStatusChange(t.ID, t.Status);
    }

}
