using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Player))]
public class PlayerEditor : Editor
{

	private SerializedProperty isAlwaysShooting;
	private SerializedProperty firePos;

	private void OnEnable()
	{
		isAlwaysShooting = serializedObject.FindProperty("isAlwaysShooting");
		firePos = serializedObject.FindProperty("firePos");
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		serializedObject.UpdateIfRequiredOrScript();
		if (firePos.objectReferenceValue == null || isAlwaysShooting.boolValue == true)
		{
			EditorGUILayout.HelpBox("Caution no firePos assign!", MessageType.Warning);
		}

		//EditorGUILayout.LabelField("GameDev Settings", EditorStyles.boldLabel);
		//EditorGUILayout.PropertyField(isAlwaysShooting, new GUIContent("isAlwaysShooting"));

		//EditorGUILayout.LabelField("VFX", EditorStyles.boldLabel);
		//EditorGUILayout.PropertyField(firePos, new GUIContent("firePos"));


	}
}
