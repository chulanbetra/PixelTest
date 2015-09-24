using UnityEngine;
using System.Collections;

public class AIHelper
{
	public static void RemoveAINodes(GameObject tilesObject)
	{
		foreach (AINode aiNode in tilesObject.GetComponentsInChildren<AINode>())
		{
			Object.DestroyImmediate(aiNode);
		}
	}

	public static void AddAINodes(GameObject tilesObject)
	{
		Transform transformTilesObject = tilesObject.transform;
		for (int i= 0; i < transformTilesObject.childCount; i++)
		{
			Transform tileObject = transformTilesObject.GetChild(i);
			if (tileObject != null && tileObject.gameObject != null)
			{
				tileObject.gameObject.AddComponent<AINode>();
			}
		}
	}
}
