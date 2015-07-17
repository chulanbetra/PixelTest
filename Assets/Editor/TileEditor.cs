using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Tile))]
public class TileEditor : Editor 
{
	private Tile tile;

	void OnEnable()
	{
		tile = (Tile)target;
	}

	public override void OnInspectorGUI()
	{
		tile.Flags = (eTileFlag)EditorGUILayout.EnumMaskField("Flags", tile.Flags);		
		tile.Directions = (eDirection)EditorGUILayout.EnumMaskField("Directions", tile.Directions);		
	}
}
