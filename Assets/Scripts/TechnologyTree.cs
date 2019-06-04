using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TechnologyTree : MonoBehaviour
{
    [SerializeField] private Technology[] technologies;
    [SerializeField] private byte[,] parentsChildsBehaviours;



    public static event Action<Technology[]> OnTechTreeCreated = delegate { };
    public static event Action<TechnologyID, TechnologyStatus> OnTechStatusChange = delegate { };


    private void Awake() {
        UITechnologyElement.OnUITechnologyClick += ChangeTechStatus;
    }

    private void OnDestroy() {
        UITechnologyElement.OnUITechnologyClick -= ChangeTechStatus;
    }


    public void AddTechnologies(Technology[] techs) {
        parentsChildsBehaviours = new byte[(int)TechnologyID.Count, (int)TechnologyID.Count];
        parentsChildsBehaviours[(int)TechnologyID.Tech1, (int)TechnologyID.Tech2] = 1;
        parentsChildsBehaviours[(int)TechnologyID.Tech1, (int)TechnologyID.Tech3] = 1;

        
        for (int i = 1; i < (int)TechnologyID.Count; i++) {
            string s = "";
            for (int j = 1; j < (int)TechnologyID.Count; j++) {
                if (parentsChildsBehaviours[i,j] == 1) Debug.Log((TechnologyID)i + " is parent of " + (TechnologyID)j);
            }
        }
        Debug.Log("----------");
        for (int i = 1; i < (int)TechnologyID.Count; i++) {
            string s = "";
            for (int j = 1; j < (int)TechnologyID.Count; j++) {
                if (parentsChildsBehaviours[j, i] == 1) Debug.Log((TechnologyID)i + " is child of " + (TechnologyID)j);
            }
        }



        technologies = techs;
        OnTechTreeCreated(technologies);
    }

    void ChangeTechStatus(TechnologyID id) {
        Technology t = GetTechnologyByID(id);
        //if (!t.IsEnabled) EnableTech(t);
        //else 
        if (t.Status != TechnologyStatus.Completed) {
            CompleteTech(t);
            for (int i = 0; i < (int)TechnologyID.Count; i++) {
                if (parentsChildsBehaviours[(int)t.ID, i] == 1) {
                    bool allParentsEnabled = true;
                    for (int j = 0; j < (int)TechnologyID.Count; j++) {
                        if (parentsChildsBehaviours[i, j] == 1 &&
                            GetTechnologyByID((TechnologyID)j).Status != TechnologyStatus.Completed) {
                            allParentsEnabled = false;
                            break;
                        }
                    }
                    if (allParentsEnabled) {
                        GetTechnologyByID((TechnologyID)i).SetEnabled();
                        OnTechStatusChange((TechnologyID)i, GetTechnologyByID((TechnologyID)i).Status);
                    }
                }
            }

            //foreach (TechnologyID tcID in t.Childs) {
            //    Technology techChild = GetTechnologyByID(tcID);
            //    techChild.SetEnabled();
            //    OnTechStatusChange(techChild.ID, techChild.Status);
            //}
        }
    }

    Technology GetTechnologyByID(TechnologyID id) => technologies.SingleOrDefault(t => t.ID == id);

    void EnableTech(Technology t) => t.SetEnabled();
    void CompleteTech(Technology t) => t.SetComplete();
}
