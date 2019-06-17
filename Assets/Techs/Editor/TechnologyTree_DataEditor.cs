/* Редактор технологий 
 * рисует кнопки для добавления/удаления технологий
 * для обновления параметров, добавления/удаления детей, сортировки
 * 
 * 
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;


[CustomEditor(typeof(TechnologyTree_Data))]
public class TechnologyTree_DataEditor : Editor
{
    TechnologyTree_Data trg;
    SerializedProperty technologiesList;
    SerializedProperty childs;

    private List<bool> showTechInfo;

    private bool confirmClear;



    private void OnEnable() {
        showTechInfo = new List<bool>();
    }

    void UpdateTechs() {
        technologiesList = serializedObject.FindProperty("technologies");
        if (technologiesList != null && technologiesList.arraySize > showTechInfo.Count) {
            for (int i = 0; i < technologiesList.arraySize - showTechInfo.Count;) {
                showTechInfo.Add(false);
            }
        }
    }

    public override void OnInspectorGUI() {
        trg = (TechnologyTree_Data)target;
        serializedObject.Update();
        UpdateTechs();
        base.OnInspectorGUI();
        DrawSomeButtons();
        
        DrawTechnologies();
        EditorGUILayout.Separator();
        Adding();

        serializedObject.ApplyModifiedProperties();
    }

    void DrawSomeButtons() {
        GUI.color = Color.red;
        if (GUILayout.Button("Clear all")) {
            confirmClear = !confirmClear;
            
        }
        if (confirmClear) {
            BeginHor();
            if (GUILayout.Button("Yes")) {
                if (technologiesList != null) {
                    technologiesList.ClearArray();
                    showTechInfo.Clear();
                }
                confirmClear = false;
            }
            GUI.color = Color.white;
            if (GUILayout.Button("No")) {
                confirmClear = false;
            }
            EndHor();
        }
        GUI.color = Color.white;

        EditorGUILayout.LabelField("Sorting by:");
        BeginHor();
        if (GUILayout.Button("ID", GUILayout.Width(100))) {
            trg.OrderByID();
        }
        if (GUILayout.Button("Name", GUILayout.Width(100))) {
            trg.OrderByName();
        }

        EndHor();
    }

    void DrawTechnologies() {
        if (technologiesList == null) return;

        EditorGUILayout.LabelField("List of technologies", EditorStyles.whiteBoldLabel);
        for (int i = 0; i < technologiesList.arraySize; i++) {
            GUI.color = showTechInfo[i] ? Color.cyan : Color.yellow;
            if (GUILayout.Button(technologiesList.GetArrayElementAtIndex(i).FindPropertyRelative("name").stringValue,
                                EditorStyles.toolbarButton, new GUILayoutOption[] { GUILayout.Width(150), })) {
                showTechInfo[i] = !showTechInfo[i];
                GUI.FocusControl("");
            }
            if (showTechInfo[i]) {
                
                //properties
                GUI.color = Color.white;
                BeginHor();
                EditorGUILayout.LabelField("Name", EditorStyles.whiteBoldLabel);
                SerializedProperty name = technologiesList.GetArrayElementAtIndex(i).FindPropertyRelative("name");
                name.stringValue = EditorGUILayout.TextField(name.stringValue);
                EndHor();
                BeginHor();
                EditorGUILayout.LabelField("ID", EditorStyles.whiteBoldLabel);
                SerializedProperty id = technologiesList.GetArrayElementAtIndex(i).FindPropertyRelative("id");
                id.enumValueIndex = (int)(TechnologyID)(EditorGUILayout.EnumPopup((TechnologyID)id.enumValueIndex));
                EndHor();
                BeginHor();
                EditorGUILayout.LabelField("Status", EditorStyles.whiteBoldLabel);
                SerializedProperty status = technologiesList.GetArrayElementAtIndex(i).FindPropertyRelative("status");
                status.enumValueIndex = (int)(TechnologyStatus)(EditorGUILayout.EnumPopup((TechnologyStatus)status.enumValueIndex));
                EndHor();
                EditorGUILayout.LabelField("Description", EditorStyles.whiteBoldLabel);


                SerializedProperty descr = technologiesList.GetArrayElementAtIndex(i).FindPropertyRelative("description");
                EditorStyles.textArea.wordWrap = true;
                descr.stringValue = EditorGUILayout.TextArea(descr.stringValue, EditorStyles.textArea, GUILayout.Height(50));


                //childs
                
                SerializedProperty childs = technologiesList.GetArrayElementAtIndex(i).FindPropertyRelative("childs");
                if (GUILayout.Button("Add child")) {
                    childs.arraySize++;
                }
                for (int c = 0; c < childs.arraySize; c++) {
                    BeginHor();
                    childs.GetArrayElementAtIndex(c).enumValueIndex = (int)(TechnologyID)(EditorGUILayout.EnumPopup((TechnologyID)childs.GetArrayElementAtIndex(c).enumValueIndex));
                    GUI.color = Color.red;
                    if (GUILayout.Button("Remove child")) {
                        childs.DeleteArrayElementAtIndex(c);
                    }
                    GUI.color = Color.white;
                    EndHor();
                }

                //delete
                GUI.color = Color.red;
                if (GUILayout.Button("Delete technology")) {
                    technologiesList.DeleteArrayElementAtIndex(i);
                    return;
                }
                GUI.color = Color.white;
            }
        }
    }

    void Adding() {
        EditorGUILayout.LabelField("Add technology");
        GUI.color = Color.green;
        if (GUILayout.Button("ADD")) {
            technologiesList.arraySize ++;
            CopyPropertyValues(technologiesList.GetArrayElementAtIndex(technologiesList.arraySize - 1), 
                               technologiesList.GetArrayElementAtIndex(technologiesList.arraySize - 2));
            showTechInfo[showTechInfo.Count - 1] = false;
            showTechInfo.Add(true);
        }
        GUI.color = Color.white;
    }

    void CopyPropertyValues(SerializedProperty source, SerializedProperty dest) {
        source.FindPropertyRelative("name").stringValue = dest.FindPropertyRelative("name").stringValue;
        source.FindPropertyRelative("status").enumValueIndex = dest.FindPropertyRelative("status").enumValueIndex;
        source.FindPropertyRelative("description").stringValue = dest.FindPropertyRelative("description").stringValue;
    }

    void BeginHor() {
        EditorGUILayout.BeginHorizontal();
    }

    void EndHor() {
        EditorGUILayout.EndHorizontal();
    }

    void BeginVer() {
        EditorGUILayout.BeginVertical();
    }

    void EndVer() {
        EditorGUILayout.EndVertical();
    }

}
