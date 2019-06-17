/* Управляет состоянием технологий, пока не используется
 * 
 * 
 * */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TechnologyTree_Data))]
public class TechnologyTree_Controller : MonoBehaviour
{
    private TechnologyTree_Data data;



    void EnableTech(Technology t) {
        t.SetEnabled();
        //тут надо сообщить технологии на канвасе об изменении статуса и отрисовать
    }
    void CompleteTech(Technology t) {
        t.SetComplete();
        //тут надо сообщить технологии на канвасе об изменении статуса и отрисовать
    }

}
