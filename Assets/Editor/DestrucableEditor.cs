using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Destructable))]
[CanEditMultipleObjects]
public class DestrucableEditor : Editor
{
    //Editor only vars
    bool ShowDestroyObjects = true;

    //Stuff from the original
    SerializedProperty FireballsDestroy;
    SerializedProperty FirebreathsDestroy;
    SerializedProperty BeamDestroy;
    SerializedProperty HealDestroy;
    SerializedProperty AOEDestroy;
    SerializedProperty TargetedAOEDestroy;
    SerializedProperty DestructionType;
    SerializedProperty burnTexture;
    SerializedProperty explodeObj;

    private void OnEnable()
    {
        FireballsDestroy = serializedObject.FindProperty("FireballsDestroy");
        FirebreathsDestroy = serializedObject.FindProperty("FirebreathsDestroy");
        BeamDestroy = serializedObject.FindProperty("BeamDestroy");
        HealDestroy = serializedObject.FindProperty("HealDestroy");
        AOEDestroy = serializedObject.FindProperty("AOEDestroy");
        TargetedAOEDestroy = serializedObject.FindProperty("TargetedAOEDestroy");
        DestructionType = serializedObject.FindProperty("DestructionType");
        burnTexture = serializedObject.FindProperty("burnTexture");
        explodeObj = serializedObject.FindProperty("explodeObj");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}
