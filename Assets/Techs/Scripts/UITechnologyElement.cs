/* Отрисовывает технологию
 * 
 * 
 * */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
public class UITechnologyElement : MonoBehaviour
{
    [SerializeField] private Image bg;
    [SerializeField] private TextMeshProUGUI techTitle;
    [SerializeField] private TextMeshProUGUI techDescr;
    [SerializeField] private RectTransform[] anchors;


    /// <summary>
    /// Точки входа выхода 
    /// </summary>
    public RectTransform[] Anchors { get { return anchors; } }

    public TechnologyID TechID { get; private set; }


    public void Setup(Technology t) {
        TechID = t.ID;
        techTitle.text = t.Name.ToString();
        techDescr.text = t.Description;
    }


    public void SetStatus(Color color) {
        bg.color = color;
    }

    
         
}
