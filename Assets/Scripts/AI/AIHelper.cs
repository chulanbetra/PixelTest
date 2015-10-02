using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AIHelper
{
	public static void RemoveAINodes(GameObject tilesObject)
	{
		Transform transformTilesObject = tilesObject.transform;
		for (int i = 0; i < transformTilesObject.childCount; i++)
		{
			Transform tileObject = transformTilesObject.GetChild(i);
			foreach (AINode aiNode in tileObject.GetComponents<AINode>())
			{
				Object.DestroyImmediate(aiNode);
			}
		}
	}

	public static void AddAINodes(GameObject tilesObject)
	{
		Transform transformTilesObject = tilesObject.transform;
		for (int i = 0; i < transformTilesObject.childCount; i++)
		{
			Transform tileObject = transformTilesObject.GetChild(i);
			if (tileObject != null && tileObject.gameObject != null)
			{
				// create AI nodes only on ai walkable tiles
				Tile tile = tileObject.GetComponent<Tile>();
				if (tile != null && tile.HasFlag(eTileFlag.AI_WALKABLE))
				{
					tileObject.gameObject.AddComponent<AINode>();
				}
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
			aiNode.SetNeighbor(eDirection.Up, aiNodes.Where(node => node.transform.position == vPos + Vector3.up).FirstOrDefault());
			// down
			aiNode.SetNeighbor(eDirection.Down, aiNodes.Where(node => node.transform.position == vPos - Vector3.up).FirstOrDefault());
			// left
			aiNode.SetNeighbor(eDirection.Left, aiNodes.Where(node => node.transform.position == vPos - Vector3.right).FirstOrDefault());
			// right
			aiNode.SetNeighbor(eDirection.Right, aiNodes.Where(node => node.transform.position == vPos + Vector3.right).FirstOrDefault());
			// up-left
			if (aiNode.GetNeighbor(eDirection.Up) != null && aiNode.GetNeighbor(eDirection.Left) != null)
			{
				aiNode.SetNeighbor(eDirection.Up_Left, aiNodes.Where(node => node.transform.position == vPos + Vector3.up - Vector3.right).FirstOrDefault());
			}
			// up-right
			if (aiNode.GetNeighbor(eDirection.Up) != null && aiNode.GetNeighbor(eDirection.Right) != null)
			{
				aiNode.SetNeighbor(eDirection.Up_Right, aiNodes.Where(node => node.transform.position == vPos + Vector3.up + Vector3.right).FirstOrDefault());
			}
			// down-left
			if (aiNode.GetNeighbor(eDirection.Down) != null && aiNode.GetNeighbor(eDirection.Left) != null)
			{
				aiNode.SetNeighbor(eDirection.Down_Left, aiNodes.Where(node => node.transform.position == vPos - Vector3.up - Vector3.right).FirstOrDefault());
			}
			// down-right
			if (aiNode.GetNeighbor(eDirection.Down) != null && aiNode.GetNeighbor(eDirection.Right) != null)
			{
				aiNode.SetNeighbor(eDirection.Down_Right, aiNodes.Where(node => node.transform.position == vPos - Vector3.up + Vector3.right).FirstOrDefault());
			}
		}
	}
}
