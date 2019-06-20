/* Добавляет кнопки для перерисовки дерева технологий
 * 
 * 
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(TechnologyTree_Drawer))]
public class TechnologyTree_DrawerEditor : Editor {
    TechnologyTree_Drawer trg;
    SerializedProperty technologiesList;



    void UpdateTechs() {
        technologiesList = serializedObject.FindProperty("technologiesOnCanvas");
    }

    public override void OnInspectorGUI() {
        trg = (TechnologyTree_Drawer)target;
        serializedObject.Update();
        UpdateTechs();
        base.OnInspectorGUI();
        DrawControls();
        DrawTechs();

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
            if (GUILayout.Button(((TechnologyID)technologiesList.GetArrayElementAtIndex(i).FindPropertyRelative("id").enumValueIndex).ToString(),
                                EditorStyles.toolbarButton, new GUILayoutOption[] { GUILayout.Width(150), })) {
                Selection.activeGameObject = technologiesList.GetArrayElementAtIndex(i).FindPropertyRelative("uiElementObject").objectReferenceValue as GameObject;
                SceneView.FrameLastActiveSceneView();

            }
        }
    }
}
