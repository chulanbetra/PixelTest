using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AINode : MonoBehaviour 
{
	public Dictionary<eDirection, AINode> Neighbors;

	// Use this for initialization
	void Start () 
	{	
		Neighbors = new Dictionary<eDirection, AINode>();
		Neighbors.Add(eDirection.UP, null);
		Neighbors.Add(eDirection.DOWN, null);
		Neighbors.Add(eDirection.LEFT, null);
		Neighbors.Add(eDirection.RIGHT, null);
		Neighbors.Add(eDirection.UP_LEFT, null);
		Neighbors.Add(eDirection.UP_RIGHT, null);
		Neighbors.Add(eDirection.DOWN_LEFT, null);
		Neighbors.Add(eDirection.DOWN_RIGHT, null);
	}
	
	// Update is called once per frame
	void Update () 
	{	
	}
}
