using UnityEngine;
using UnityEditor;
using System.Collections;

public class AIMenu : ScriptableObject  
{
	[MenuItem("AI/Generate Pathgraph")]
	static void GeneratePathgraph()
	{
		GameObject tilesObject = GameObject.Find("Tiles");
		if (tilesObject != null)
		{
			PathgraphGizmo pPathgraphGizmo = tilesObject.GetComponent<PathgraphGizmo>();
			if (pPathgraphGizmo != null)
			{
				pPathgraphGizmo.CanDrawGizmo = false;
				AIHelper.RemoveAINodes(tilesObject);
				AIHelper.AddAINodes(tilesObject);
				AIHelper.ConnectAINodes(tilesObject);
				pPathgraphGizmo.CanDrawGizmo = true;
			}
		}
	}
}
