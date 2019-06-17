/*
 * Содержит инфо о технологии
 * ID, название, состояние, описание, детей
 * сюда же надо будет добавить стоимость в очках, прогресс исследования
 * 
 * 
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TechnologyID {
    None                    = 0,
    NaturalTax              = 1,
    Agriculture             = 2,
    Logging                 = 3,
    Mining                  = 4,
    Livestock               = 5,
    Hunting                 = 6,
    TraditionsAndRituals    = 7,
    Fishing                 = 8,
    Herbalism               = 9,
    WoodProcessing          = 10,
    BlacksmithCraft         = 11,
    Masonry                 = 12,
    SkinTreatment           = 13,
    MilitaryDoctrine        = 14,
    Bowstring               = 15,
    TrainingRecruits        = 16,
    Wedge                   = 17,
    Phalanx                 = 18,
    TheCalendar             = 19,
    Literacy                = 20,
    Navigation              = 21,
    ForeignPolicy           = 22,
    Mobilization            = 23,
    NaturalExchange         = 24,
    Turnover                = 25,
    Marauding               = 26,
    Mechanics               = 27,
    Siege                   = 28,
    Sedentary               = 29,
    Granary                 = 30,
    Relocation              = 31,
    Infrastructure          = 32,
    SeaLanes                = 33,

    Count       
}

public enum TechnologyStatus {
    Disabled, Enabled, Completed
}

[System.Serializable]
public class Technology {

    [SerializeField] private TechnologyID id;
    /// <summary>
    /// ID технологии
    /// </summary>
    public TechnologyID ID { get { return id; } }


    [SerializeField] private string name;
    /// <summary>
    /// Название технологии
    /// </summary>
    public string Name { get { return name; } }


    [SerializeField] private TechnologyStatus status;
    /// <summary>
    /// Статус технологии
    /// </summary>
    public TechnologyStatus Status { get { return status; } }


    [SerializeField] private string description;
    /// <summary>
    /// Описание технологии
    /// </summary>
    public string Description { get { return description; } }

    [SerializeField] private List<TechnologyID> childs;
    /// <summary>
    /// Зависимые технологии
    /// </summary>
    public List<TechnologyID> Childs { get { return childs; } }


    public Technology() {
        id = TechnologyID.None;
        name = "";
        status = TechnologyStatus.Disabled;
        description = "";
    }
    public Technology(Technology t) {
        id = t.ID;
        name = t.Name;
        status = t.Status;
        description = t.Description;
    }
    public Technology(TechnologyID _id, string _name, TechnologyStatus _status, string _description) {
        id = _id;
        name = _name;
        status = _status;
        description = _description;
    }

    /// <summary>
    /// Установить статус выполнена
    /// </summary>
    public void SetComplete() {
        status = TechnologyStatus.Completed;
    }

    /// <summary>
    /// Установить статус доступна
    /// </summary>
    public void SetEnabled() {
        status = TechnologyStatus.Enabled;
    }
}
