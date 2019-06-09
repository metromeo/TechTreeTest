
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[ExecuteInEditMode]
[System.Serializable]
public class TechnologyTree_Data : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]
    private List<Technology> technologies;
    //              child   child
    // parent       0       0
    // parent       0       1
    [SerializeField] private byte[,] parentsChildsBehaviours;

    //[SerializeField] public Dictionary<TechnologyID, List<TechnologyID>> Childs { get; private set;}
    //[SerializeField] public Dictionary<TechnologyID, List<TechnologyID>> Parents { get; private set;}


    //private void Awake() {
    //    if (Childs == null) Childs = new Dictionary<TechnologyID, List<TechnologyID>>();
    //    if (Parents == null) Parents = new Dictionary<TechnologyID, List<TechnologyID>>();
    //}

    //public void SetUpTechs(List<Technology> t) => technologies = t;
    //public void AddTech(Technology t) {
    //    technologies.Add(t);
    //}

    private void Awake() {
        if (technologies == null) technologies = new List<Technology>();
    }

    public Technology GetTechnologyByID(TechnologyID id) => technologies.SingleOrDefault(t => t.ID == id);
    public List<Technology> GetTechnologies() => technologies;

    public void SetUpBehaviours(byte[,] b) => parentsChildsBehaviours = b;
    public byte GetBehaviour(TechnologyID t1, TechnologyID t2) => parentsChildsBehaviours[(int)t1, (int)t2];

    public void OrderByName() {
        technologies = technologies.OrderBy(t => t.Name).ToList();
    }
    public void OrderByID() {
        technologies = technologies.OrderBy(t => t.ID).ToList();
    }

    //public void SetChildsOf(TechnologyID id, List<TechnologyID> _childs) {
    //    if (Childs.ContainsKey(id)) {
    //        // удалить для всех детей запись об этом родителе
    //        List<TechnologyID> c = Childs[id];
    //        foreach (TechnologyID tc in c) {
    //            List<TechnologyID> p = Parents[tc];
    //            foreach (TechnologyID tp in p) {
    //                if (tp == id) p.Remove(id);
    //            }
    //        }
    //        // установить детей для этого родителя
    //        Childs[id] = _childs;
    //    }
    //    else {
    //        Childs.Add(id, _childs);
    //    }
    //    // установить для всех (оставшихся/новых) детей запись об этом родителе
    //    List<TechnologyID> newC = Childs[id];
    //    foreach (TechnologyID tc in newC) {
    //        if (Parents.ContainsKey(tc)) {
    //            List<TechnologyID> p = Parents[tc];
    //            p.Add(id);
    //        }
    //        else {
    //            Parents.Add(tc, new List<TechnologyID> { id });
    //        }
    //    }
    //}
    //public void SetParentsOf(TechnologyID id, List<TechnologyID> _parents) {
    //    if (Parents.ContainsKey(id)) {
    //        Parents[id] = _parents;
    //    }
    //    else {
    //        Parents.Add(id, _parents);
    //    }
    //}
}
