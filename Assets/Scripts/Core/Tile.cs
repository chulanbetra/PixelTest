using UnityEngine;
using System.Collections;

// tile class
public class Tile : MonoBehaviour
{
	// tile accessibility flags
	public eTileFlag Flags;
	// tile direction flags
	public eDirection Directions;

	// tile mask
	public int TileMask;
	
	// specific object on tile (player, enemy, etc.)
	private GameObject TileObject;

	// Use this for initialization
	void Start() 
	{	
	}
	
	// Update is called once per frame
	void Update() 
	{	
	}

	//---------- Flag Functions ----------
	
	// check if mask has flag
	public bool HasFlag(eTileFlag f)
	{
		int iMask = (int)this.Flags;
		if ((iMask & (int)f) > 0)
		{
			return true;
		}
		return false;
	}
	
	// sets flag into tile mask
	public void SetFlag(eTileFlag f)
	{
		int iMask = (int)this.Flags;
		iMask |= (int)f;
		this.Flags = (eTileFlag)iMask;
	}
	
	// clears flag from tile mask
	public void ClearFlag(eTileFlag f)
	{
		int iMask = (int)this.Flags;
		iMask &= ~(int)f;
		this.Flags = (eTileFlag)iMask;
	}
	
	// toggle flag (turns it on if it was off and vice versa)
	public void Toggle(eTileFlag f)
	{
		int iMask = (int)this.Flags;
		iMask ^= (int)f;
		this.Flags = (eTileFlag)iMask;
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
