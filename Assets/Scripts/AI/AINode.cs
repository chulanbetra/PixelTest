using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AINode : MonoBehaviour 
{
	// 0 - up
	// 1 - down
	// 2 - left
	// 3 - right
	// 4 - up left
	// 5 - up right
	// 6 - down left
	// 7 - down right
	public AINode[] Neighbors = new AINode[8];

	public Tile Tile
	{
		get
		{
			return this.gameObject.GetComponent<Tile>();
		}
	}

	public void SetNeighbor(eDirection dir, AINode aiNode)
	{
		Neighbors[(int)dir] = aiNode;
	}

	public AINode GetNeighbor(eDirection dir)
	{
		return Neighbors[(int)dir];
	}

	void Awake()
	{
	}

	// Use this for initialization
	void Start () 
	{	
	}
	
	// Update is called once per frame
	void Update () 
	{	
	}
}
