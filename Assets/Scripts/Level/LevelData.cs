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
}


