using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public sealed class LevelData :MonoBehaviour
{
	public static LevelData Instance;
	
	private Dictionary<Point, Tile> m_pTiles;
	
	public Dictionary<Point, Tile> Tiles
	{
		get
		{
			return m_pTiles;
		}
		set
		{
			m_pTiles = value;
		}
	}

	// Use this for initialization
	void Start () 
	{	
	}
	
	void Awake()
	{
		Instance = this;
		m_pTiles = new Dictionary<Point, Tile>(new PointEqualityComparer());
	}
	
	// Update is called once per frame
	void Update () 
	{	
	}
	
	// create new tile with default flag and level
	Tile CreateTile()
	{
		Tile pTile = new Tile();
		pTile.Level = 0;
		pTile.SetFlag(eTileFlag.WALKABLE);
		return pTile;
	}
		
	void OnDrawGizmos()
	{
		float fTileWidth = MapSettings.Instance.TileWidth;	
				
		#region Tiles
		// draw tiles gizmo		
		if (this.Tiles != null)
		{
			Gizmos.color = Color.green;
			Vector3 vDir = Vector3.forward * fTileWidth * 0.5f;
			foreach (var pTile in this.Tiles)
			{				
				Gizmos.DrawSphere(MapSettings.PointToVector3(pTile.Key) + vDir, fTileWidth * 0.3f);
			}
		}	
		#endregion
	}
}


