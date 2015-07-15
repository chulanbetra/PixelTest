using UnityEngine;
using System.Collections;

[System.Serializable]
// tile class
public class Tile
{
	private int m_iLevel;
	
	public int Level
	{
		get
		{
			return m_iLevel;
		}
		set
		{
			m_iLevel = value;
		}
	}
	public eTileFlag Flag;
	// tile mask
	public int TileMask;
	
	// specific object on tile (player, enemy, etc.)
	private GameObject TileObject;
	
	// constructor
	public Tile()
	{
		TileMask = 0;
		TileObject = null;
	}
	
	//---------- Flag Functions ----------
	
	// check if mask has flag
	public bool HasFlag(eTileFlag f)
	{
		if ((TileMask & (int)f) > 0)
		{
			return true;
		}
		return false;
	}
	
	// sets flag into tile mask
	public void SetFlag(eTileFlag f)
	{
		TileMask |= (int)f;
	}
	
	// clears flag from tile mask
	public void ClearFlag(eTileFlag f)
	{
		TileMask &= ~(int)f;
	}
	
	// toggle flag (turns it on if it was off and vice versa)
	public void Toggle(eTileFlag f)
	{
		TileMask ^= (int)f;
	}
	//-------------------------------------
	
	public GameObject GetTileObject()
	{
		return TileObject;
	}
	
	public void SetTileObject(GameObject o)
	{
		TileObject = o;
	}
}
