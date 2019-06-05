using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechnologyTreeCreator : MonoBehaviour
{
    [SerializeField] private int techCount;
    [SerializeField] private Technology[] techs;
    
    public static event Action<Technology[], byte[,]> OnTechnologiesCreate = delegate { };

    void Start()
    {
        techs = new Technology[techCount];
        CreateTechs();
        OnTechnologiesCreate(techs, CreateBehaviours());
    }


    void CreateTechs() {
        techs[0] = new Technology(TechnologyID.Tech1, TechnologyStatus.Enabled, "First technology");
        techs[1] = new Technology(TechnologyID.Tech2, TechnologyStatus.Enabled, "Second technology");
        techs[2] = new Technology(TechnologyID.Tech3, TechnologyStatus.Disabled, "Third technology");
        techs[3] = new Technology(TechnologyID.Tech4, TechnologyStatus.Disabled, "Fourth technology");
        techs[4] = new Technology(TechnologyID.Tech5, TechnologyStatus.Disabled, "Fifth technology");
        techs[5] = new Technology(TechnologyID.Tech6, TechnologyStatus.Disabled, "Sixth technology");
        techs[6] = new Technology(TechnologyID.Tech7, TechnologyStatus.Disabled, "Seventh technology");
        techs[7] = new Technology(TechnologyID.Tech8, TechnologyStatus.Disabled, "Eighth technology");
    }

    byte[,] CreateBehaviours() {
        byte[,] parentsChildsBehaviours = new byte[(int)TechnologyID.Count, (int)TechnologyID.Count];
        parentsChildsBehaviours[(int)TechnologyID.Tech1, (int)TechnologyID.Tech3] = 1;
        parentsChildsBehaviours[(int)TechnologyID.Tech2, (int)TechnologyID.Tech3] = 1;
        parentsChildsBehaviours[(int)TechnologyID.Tech3, (int)TechnologyID.Tech4] = 1;
        parentsChildsBehaviours[(int)TechnologyID.Tech3, (int)TechnologyID.Tech5] = 1;
        parentsChildsBehaviours[(int)TechnologyID.Tech3, (int)TechnologyID.Tech8] = 1;
        parentsChildsBehaviours[(int)TechnologyID.Tech4, (int)TechnologyID.Tech5] = 1;
        parentsChildsBehaviours[(int)TechnologyID.Tech4, (int)TechnologyID.Tech6] = 1;
        parentsChildsBehaviours[(int)TechnologyID.Tech6, (int)TechnologyID.Tech7] = 1;
        parentsChildsBehaviours[(int)TechnologyID.Tech7, (int)TechnologyID.Tech8] = 1;
        return parentsChildsBehaviours;
    }




}
