using UnityEngine;
using System.Collections;

public class PathgraphGizmo : MonoBehaviour 
{
	public bool CanDrawGizmo;

	// Use this for initialization
	void Start () 
	{	
	}
	
	// Update is called once per frame
	void Update () 
	{	
	}

	void OnDrawGizmos()
	{
		GizmosDrawPathgraph();
	}	
	
	void GizmosDrawPathgraph()
	{
		TilesSettings pTilesSettings = this.GetComponent<TilesSettings>();
		if (pTilesSettings != null && CanDrawGizmo)
		{
			float fTileWidth = pTilesSettings.TileWidth;
			foreach (AINode aiNode in this.gameObject.GetComponentsInChildren<AINode>())
			{
				Gizmos.color = Color.green;
				Gizmos.DrawSphere(aiNode.transform.position, fTileWidth / 3);

				Gizmos.color = Color.red;
				foreach (AINode aiNeighbor in aiNode.Neighbors.Values)
				{
					if (aiNeighbor != null)
					{
						Gizmos.DrawLine(aiNode.transform.position, aiNeighbor.transform.position);
					}
				}
			}
		}
	}
}
