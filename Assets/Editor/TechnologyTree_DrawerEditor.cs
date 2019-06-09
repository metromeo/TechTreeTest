using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(TechnologyTree_Drawer))]
public class TechnologyTree_DrawerEditor : Editor {
    TechnologyTree_Drawer trg;
    SerializedProperty technologiesList;


    private List<bool> showTechInfo;


    private void OnEnable() {
        showTechInfo = new List<bool>();
    }

    void UpdateTechs() {
        technologiesList = serializedObject.FindProperty("technologiesOnCanvas");
        if (technologiesList != null && technologiesList.arraySize > showTechInfo.Count) {
            for (int i = 0; i < technologiesList.arraySize - showTechInfo.Count;) {
                showTechInfo.Add(false);
            }
        }
    }

    public override void OnInspectorGUI() {
        trg = (TechnologyTree_Drawer)target;
        serializedObject.Update();
        UpdateTechs();
        base.OnInspectorGUI();
        DrawControls();


        serializedObject.ApplyModifiedProperties();
    }

    void DrawControls() {
        if (GUILayout.Button("Instantiate/Update techs on canvas")) {
            trg.InstantiateUpdateTechsOnCanvas();
        }
        if (GUILayout.Button("Update techs params")) {
            trg.UpdateTechParams();
        }
        if (GUILayout.Button("Create connections")) {
            trg.InstantiateUpdateConnections();
        }
    }

    void DrawTechs() {
        if (technologiesList == null) return;

        EditorGUILayout.LabelField("List of technologies on canvas", EditorStyles.whiteBoldLabel);
        for (int i = 0; i < technologiesList.arraySize; i++) {
            GUI.color = showTechInfo[i] ? Color.cyan : Color.yellow;
            if (GUILayout.Button(((TechnologyID)technologiesList.GetArrayElementAtIndex(i).FindPropertyRelative("id").enumValueIndex).ToString(),
                                EditorStyles.toolbarButton, new GUILayoutOption[] { GUILayout.Width(150), })) {
                showTechInfo[i] = !showTechInfo[i];
            }
            //if (showTechInfo[i]) {

            //    //properties
            //    GUI.color = Color.white;
            //    BeginHor();
            //    EditorGUILayout.LabelField("Name", EditorStyles.whiteBoldLabel);
            //    SerializedProperty name = technologiesList.GetArrayElementAtIndex(i).FindPropertyRelative("name");
            //    name.stringValue = EditorGUILayout.TextField(name.stringValue);
            //    EndHor();
            //    BeginHor();
            //    EditorGUILayout.LabelField("ID", EditorStyles.whiteBoldLabel);
            //    SerializedProperty id = technologiesList.GetArrayElementAtIndex(i).FindPropertyRelative("id");
            //    id.enumValueIndex = (int)(TechnologyID)(EditorGUILayout.EnumPopup((TechnologyID)id.enumValueIndex));
            //    EndHor();
            //    BeginHor();
            //    EditorGUILayout.LabelField("Status", EditorStyles.whiteBoldLabel);
            //    SerializedProperty status = technologiesList.GetArrayElementAtIndex(i).FindPropertyRelative("status");
            //    status.enumValueIndex = (int)(TechnologyStatus)(EditorGUILayout.EnumPopup((TechnologyStatus)status.enumValueIndex));
            //    EndHor();
            //    EditorGUILayout.LabelField("Description", EditorStyles.whiteBoldLabel);


            //    SerializedProperty descr = technologiesList.GetArrayElementAtIndex(i).FindPropertyRelative("description");
            //    EditorStyles.textArea.wordWrap = true;
            //    descr.stringValue = EditorGUILayout.TextArea(descr.stringValue, EditorStyles.textArea, GUILayout.Height(50));


            //    //childs

            //    SerializedProperty childs = technologiesList.GetArrayElementAtIndex(i).FindPropertyRelative("childs");
            //    if (GUILayout.Button("Add child")) {
            //        childs.arraySize++;
            //    }
            //    for (int c = 0; c < childs.arraySize; c++) {
            //        BeginHor();
            //        childs.GetArrayElementAtIndex(c).enumValueIndex = (int)(TechnologyID)(EditorGUILayout.EnumPopup((TechnologyID)childs.GetArrayElementAtIndex(c).enumValueIndex));
            //        GUI.color = Color.red;
            //        if (GUILayout.Button("Remove child")) {
            //            childs.DeleteArrayElementAtIndex(c);
            //        }
            //        GUI.color = Color.white;
            //        EndHor();
            //    }

            //    //delete
            //    GUI.color = Color.red;
            //    if (GUILayout.Button("Delete technology")) {
            //        technologiesList.DeleteArrayElementAtIndex(i);
            //        return;
            //    }
            //    GUI.color = Color.white;
            //}
        }
    }
}
