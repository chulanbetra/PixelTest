using UnityEngine;
using System.Collections;

public class TilesSettings : MonoBehaviour 
{
	public float TileWidth = 1f;
	public Color GridColor = Color.white;
	public MapBounds[] MapBounds;

	public Vector3 IndexToVector3(int i, int j)		
	{	
		return new Vector3(i * TileWidth + TileWidth * 0.5f, j * TileWidth + TileWidth * 0.5f, 0);
	}
	
	public Vector3 GetAlignedVector3(Vector3 v)
	{
		return new Vector3(Mathf.FloorToInt(v.x / TileWidth) * TileWidth + TileWidth * 0.5f, Mathf.FloorToInt(v.y / TileWidth) * TileWidth + TileWidth * 0.5f, 0);
	}

	void OnDrawGizmos()
	{
		GizmosDrawGrid();
	}

	void GizmosDrawGrid()
	{
		Vector3 vPos = Camera.current.transform.position;
		Gizmos.color = this.GridColor;
		
		for (float y = vPos.y - 800.0f; y < vPos.y + 800.0f; y += this.TileWidth)
		{
			Gizmos.DrawLine(new Vector3(-10000.0f, Mathf.Floor(y / this.TileWidth) * this.TileWidth, 0), 
			                new Vector3(10000.0f, Mathf.Floor(y / this.TileWidth) * this.TileWidth, 0));
		}
		
		for (float x = vPos.x - 1200.0f; x < vPos.x + 1200.0f; x += this.TileWidth)
		{
			Gizmos.DrawLine(new Vector3(Mathf.Floor(x / this.TileWidth) * this.TileWidth, -10000.0f, 0), 
			                new Vector3(Mathf.Floor(x / this.TileWidth) * this.TileWidth, 10000.0f, 0));
		}

		Gizmos.color = Color.yellow;
		foreach (MapBounds mapbounds in this.MapBounds)
		{
			Gizmos.DrawWireCube(mapbounds.Center, mapbounds.Size);
		}
	}
}
