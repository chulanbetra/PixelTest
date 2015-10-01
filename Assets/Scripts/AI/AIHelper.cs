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
			aiNode.Neighbors[0] = aiNodes.Where(node => node.transform.position == vPos + Vector3.up).FirstOrDefault();
			// down
			aiNode.Neighbors[1] = aiNodes.Where(node => node.transform.position == vPos - Vector3.up).FirstOrDefault();
			// left
			aiNode.Neighbors[2] = aiNodes.Where(node => node.transform.position == vPos - Vector3.right).FirstOrDefault();
			// right
			aiNode.Neighbors[3] = aiNodes.Where(node => node.transform.position == vPos + Vector3.right).FirstOrDefault();
			// up-left
			if (aiNode.Neighbors[0] != null && aiNode.Neighbors[0].Tile != null && aiNode.Neighbors[0].Tile.HasFlag(eTileFlag.AI_WALKABLE) &&
			    aiNode.Neighbors[2] != null && aiNode.Neighbors[2].Tile != null && aiNode.Neighbors[2].Tile.HasFlag(eTileFlag.AI_WALKABLE))
			{
				aiNode.Neighbors[4] = aiNodes.Where(node => node.transform.position == vPos + Vector3.up - Vector3.right).FirstOrDefault();
			}
			// up-right
			if (aiNode.Neighbors[0] != null && aiNode.Neighbors[0].Tile != null && aiNode.Neighbors[0].Tile.HasFlag(eTileFlag.AI_WALKABLE) &&
			    aiNode.Neighbors[3] != null && aiNode.Neighbors[3].Tile != null && aiNode.Neighbors[3].Tile.HasFlag(eTileFlag.AI_WALKABLE))
			{
				aiNode.Neighbors[5] = aiNodes.Where(node => node.transform.position == vPos + Vector3.up + Vector3.right).FirstOrDefault();
			}
			// down-left
			if (aiNode.Neighbors[1] != null && aiNode.Neighbors[1].Tile != null && aiNode.Neighbors[1].Tile.HasFlag(eTileFlag.AI_WALKABLE) &&
			    aiNode.Neighbors[2] != null && aiNode.Neighbors[2].Tile != null && aiNode.Neighbors[2].Tile.HasFlag(eTileFlag.AI_WALKABLE))
			{
				aiNode.Neighbors[6] = aiNodes.Where(node => node.transform.position == vPos - Vector3.up - Vector3.right).FirstOrDefault();
			}
			// down-right
			if (aiNode.Neighbors[1] != null && aiNode.Neighbors[1].Tile != null && aiNode.Neighbors[1].Tile.HasFlag(eTileFlag.AI_WALKABLE) &&
			    aiNode.Neighbors[3] != null && aiNode.Neighbors[3].Tile != null && aiNode.Neighbors[3].Tile.HasFlag(eTileFlag.AI_WALKABLE))
			{
				aiNode.Neighbors[7] = aiNodes.Where(node => node.transform.position == vPos - Vector3.up + Vector3.right).FirstOrDefault();
			}
		}
	}
}
