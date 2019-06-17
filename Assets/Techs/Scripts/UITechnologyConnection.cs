/* Отрисовывает соединение
 * 
 * 
 * */

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


    /// <summary>
    /// Устанавливает источник и цель и отрисовывает
    /// </summary>
    /// <param name="_sourceID"></param>
    /// <param name="_targetID"></param>
    /// <param name="source"></param>
    /// <param name="target"></param>
    public void Setup(TechnologyID _sourceID, TechnologyID _targetID, RectTransform source, RectTransform target) {
        transform.position = source.position;
        sourceID = _sourceID;
        targetID = _targetID;
        this.source = source;
        this.target = target;
        line.Setup(source, target);
    }


    /// <summary>
    /// Обновляет позиции линии при перемещении технологии на канвасе
    /// </summary>
    public void UpdateLinePositions() {
        line.CheckAndRecalcPositions();
    }

    public UILineConnector GetLine() => line;
    public TechnologyID GetSource() => sourceID;
    public TechnologyID GetTarget() => targetID;



}
