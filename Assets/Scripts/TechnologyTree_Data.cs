
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TechnologyTree_Data : MonoBehaviour
{
    [SerializeField] private Technology[] technologies;
    //              child   child
    // parent       0       0
    // parent       0       1
    [SerializeField] private byte[,] parentsChildsBehaviours; 


    public void SetUpTechs(Technology[] t) => technologies = t;
    public Technology GetTechnologyByID(TechnologyID id) => technologies.SingleOrDefault(t => t.ID == id);

    public void SetUpBehaviours(byte[,] b) => parentsChildsBehaviours = b;
    public byte GetBehaviour(TechnologyID t1, TechnologyID t2) => parentsChildsBehaviours[(int)t1, (int)t2];


    
}
