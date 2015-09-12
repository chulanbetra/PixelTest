using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

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
		foreach (var value in Enum.GetValues(typeof(eTileFlag)))
		{
			eTileFlag flag = (eTileFlag)value;
			bool hasFlag = EditorGUILayout.Toggle(value.ToString(), tile.HasFlag(flag));
			if (hasFlag)
			{
				tile.SetFlag(flag);
			}
			else
			{
				tile.ClearFlag(flag);
			}
		}
	}
}
