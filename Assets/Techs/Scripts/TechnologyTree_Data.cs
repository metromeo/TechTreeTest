/* Содержит в себе все созданные технологии
 * 
 * 
 * */

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

    private void Awake() {
        if (technologies == null) technologies = new List<Technology>();
    }

    /// <summary>
    /// Получить технологию по ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Technology GetTechnologyByID(TechnologyID id) => technologies.SingleOrDefault(t => t.ID == id);

    /// <summary>
    /// Получить список всех технологий
    /// </summary>
    /// <returns></returns>
    public List<Technology> GetTechnologies() => technologies;


    /// <summary>
    /// Отсортировать по имени (для редактора)
    /// </summary>
    public void OrderByName() {
        technologies = technologies.OrderBy(t => t.Name).ToList();
    }

    /// <summary>
    /// Отсортировать по ID int (для редактора)
    /// </summary>
    public void OrderByID() {
        technologies = technologies.OrderBy(t => t.ID).ToList();
    }
}
