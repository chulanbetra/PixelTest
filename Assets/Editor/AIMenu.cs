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
			AIHelper.RemoveAINodes(tilesObject);
			AIHelper.AddAINodes(tilesObject);
		}
	}


}
