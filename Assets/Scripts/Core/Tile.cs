using UnityEngine;
using System.Collections;

// tile class
public class Tile : MonoBehaviour
{
	// tile mask
	public int TileMask;

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
		return (this.TileMask & (int)f) > 0;
	}
	
	// sets flag into tile mask
	public void SetFlag(eTileFlag f)
	{
		this.TileMask |= (int)f;
	}
	
	// clears flag from tile mask
	public void ClearFlag(eTileFlag f)
	{
		this.TileMask &= ~(int)f;
	}
	
	// toggle flag (turns it on if it was off and vice versa)
	public void Toggle(eTileFlag f)
	{
		this.TileMask ^= (int)f;
	}
}
