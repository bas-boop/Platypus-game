using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PickupSystem))]
public sealed class PickupSystemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var pickupSystem = target as PickupSystem;

        GUILayout.Space(10);

        EditorGUILayout.LabelField("Inventory", EditorStyles.boldLabel);

        foreach (var kvp in pickupSystem.Inventory())
        {
            EditorGUILayout.LabelField(kvp.Key + ": " + kvp.Value);
        }
    }
}
