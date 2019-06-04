using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TechnologyTree))]
[ExecuteInEditMode]
public class TechnologyTreeCreator : MonoBehaviour
{
    [SerializeField] private int techCount = 3;
    [SerializeField] private Technology[] techs;
    

    void Start()
    {
        techs = new Technology[techCount];
        CreateTechs();
        GetComponent<TechnologyTree>().AddTechnologies(techs);
    }


    void CreateTechs() {
        techs[0] = new Technology(TechnologyID.Tech1, TechnologyStatus.Enabled, "Base technology");
        techs[1] = new Technology(TechnologyID.Tech2, TechnologyStatus.Disabled, "Second technology");
        techs[2] = new Technology(TechnologyID.Tech3, TechnologyStatus.Disabled, "Third technology");
    }

    


}
