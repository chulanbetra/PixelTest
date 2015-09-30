using UnityEngine;
using System.Collections;
using System.Linq;

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
				AINode aiNode = tileObject.gameObject.AddComponent<AINode>();
				aiNode.Init();
			}
		}
	}

	public static void ConnectAINodes(GameObject tilesObject)
	{
		AINode[] aiNodes = tilesObject.GetComponentsInChildren<AINode>();
		foreach (AINode aiNode in aiNodes)
		{
			Vector3 vPos = aiNode.transform.position;
			// up
			aiNode.Neighbors[eDirection.UP] = aiNodes.Where(node => node.transform.position == vPos + Vector3.up).FirstOrDefault();
			// down
			aiNode.Neighbors[eDirection.DOWN] = aiNodes.Where(node => node.transform.position == vPos - Vector3.up).FirstOrDefault();
			// left
			aiNode.Neighbors[eDirection.LEFT] = aiNodes.Where(node => node.transform.position == vPos - Vector3.right).FirstOrDefault();
			// right
			aiNode.Neighbors[eDirection.RIGHT] = aiNodes.Where(node => node.transform.position == vPos + Vector3.right).FirstOrDefault();
			// up-left
			aiNode.Neighbors[eDirection.UP_LEFT] = aiNodes.Where(node => node.transform.position == vPos + Vector3.up - Vector3.right).FirstOrDefault();
			// up-right
			aiNode.Neighbors[eDirection.UP_RIGHT] = aiNodes.Where(node => node.transform.position == vPos + Vector3.up + Vector3.right).FirstOrDefault();
			// down-left
			aiNode.Neighbors[eDirection.DOWN_LEFT] = aiNodes.Where(node => node.transform.position == vPos - Vector3.up - Vector3.right).FirstOrDefault();
			// down-right
			aiNode.Neighbors[eDirection.DOWN_RIGHT] = aiNodes.Where(node => node.transform.position == vPos - Vector3.up + Vector3.right).FirstOrDefault();
		}
	}
}
