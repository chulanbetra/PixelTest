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
		CreateTiles();		
	}
	
	// Update is called once per frame
	void Update () 
	{	
	}
	
	void CreateTiles()
	{
		Random.seed = (int)System.DateTime.Now.Ticks; 
		GameObject pTiles = new GameObject("Tiles");
		int iTileCountXDiv2 = MapSettings.Instance.TileCountX / 2;
		int iTileCountZDiv2 = MapSettings.Instance.TileCountZ / 2;			
		for (int i = -iTileCountXDiv2; i < iTileCountXDiv2; i++)
		{
			for (int j = -iTileCountZDiv2; j < iTileCountZDiv2; j++)
			{			
				Point pPoint = new Point(i, j);
				m_pTiles.Add(pPoint, CreateTile());				
				GameObject pQuad = GetRandomTilePrefab();				
				if (pQuad != null)
				{
					pQuad.transform.parent = pTiles.transform;
					pQuad.transform.position = MapSettings.PointToVector3(pPoint);
				}
			}
		}
	}
	
	GameObject GetRandomTilePrefab()
	{
		int i = 2;//Random.Range(1,3);
		string sPath = "Prefabs/Tiles/TileGrass" + i;
		return Instantiate(Resources.Load(sPath)) as GameObject;
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
		
		// draw pathgraph nodes gizmo
		#region Nodes
		/*if (this.Tiles != null)
		{
			Gizmos.color = Color.green;
			Vector3 vMoveUp = Vector3.up * fTileWidth * 0.5f;
			foreach (var pTile in this.Tiles)
			{				
				Gizmos.DrawSphere(MapSettings.PointToVector3(pTile.Key) + vMoveUp, fTileWidth * 0.3f);
			}
		}*/	
		#endregion
	}
}


