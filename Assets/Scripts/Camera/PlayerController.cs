using UnityEngine;
using System.Collections;
using System.Linq;

public class PlayerController : MonoBehaviour 
{
	public float MoveSpeed = 5.0f;  

	private AINode currentNode;
	private Sprite sprite;
	private SpriteRenderer spriteRenderer;
	private TilesSettings tilesSettings;

	// Use this for initialization
	void Start() 
	{	
		// get sprite renderer component and sprite
		spriteRenderer = this.GetComponent<SpriteRenderer>();
		if (spriteRenderer != null)
		{
			sprite = spriteRenderer.sprite;
		}

		// get current player tile
		Vector3 vPos = this.transform.position;
		tilesSettings = GameObject.FindObjectOfType(typeof(TilesSettings)) as TilesSettings;
		if (tilesSettings != null)
		{
			float fTileWidthHalf = tilesSettings.TileWidth * 0.5f;
			AINode[] aiNodes = tilesSettings.GetComponentsInChildren<AINode>();
			currentNode = aiNodes.FirstOrDefault(node => Mathf.Abs(node.transform.position.x - vPos.x) <= fTileWidthHalf && 
			                                     Mathf.Abs(node.transform.position.y - vPos.y) <= fTileWidthHalf);
		}
	}
	
	// Update is called once per frame
	void Update() 
	{	
		HandleMovement();
		UpdateCurrentTile();
	}

	private void UpdateCurrentTile()
	{
		float fTileWidthHalf = tilesSettings.TileWidth * 0.5f;
		Vector3 vPos = this.transform.position;
		Vector3 vNodePos = currentNode.transform.position;
		if (vPos.x > vNodePos.x + fTileWidthHalf)
		{
			if (vPos.y > vNodePos.y + fTileWidthHalf)
			{
				currentNode = currentNode.GetNeighbor(eDirection.Up_Right);
			}
			else if (vPos.y < vNodePos.y - fTileWidthHalf)
			{
				currentNode = currentNode.GetNeighbor(eDirection.Down_Right);
			}
			else
			{
				currentNode = currentNode.GetNeighbor(eDirection.Right);
			}
		}
		else if (vPos.x < vNodePos.x - fTileWidthHalf)
		{
			if (vPos.y > vNodePos.y + fTileWidthHalf)
			{
				currentNode = currentNode.GetNeighbor(eDirection.Up_Left);
			}
			else if (vPos.y < vNodePos.y - fTileWidthHalf)
			{
				currentNode = currentNode.GetNeighbor(eDirection.Down_Left);
			}
			else
			{
				currentNode = currentNode.GetNeighbor(eDirection.Left);
			}
		}
		else
		{
			if (vPos.y > vNodePos.y + fTileWidthHalf)
			{
				currentNode = currentNode.GetNeighbor(eDirection.Up);
			}
			else if (vPos.y < vNodePos.y - fTileWidthHalf)
			{
				currentNode = currentNode.GetNeighbor(eDirection.Down);
			}
			else
			{
				// no change
			}
		}
	}

	// move character according to input
	private void HandleMovement()
	{
		if (Input.GetKey(KeyCode.W)) 
		{				
			Vector3 vDir = this.transform.up * MoveSpeed * Time.deltaTime;
			Vector3 vPos = this.transform.position + vDir;
			this.transform.position = CheckUpCollision(vPos);;
		}
		if (Input.GetKey(KeyCode.S)) 
		{
			Vector3 vDir = this.transform.up * -MoveSpeed * Time.deltaTime;
			Vector3 vPos = this.transform.position + vDir;
			this.transform.position = CheckDownCollision(vPos);
		}
		if (Input.GetKey(KeyCode.A))
		{
			Vector3 vDir = this.transform.right * -MoveSpeed * Time.deltaTime;
			Vector3 vPos = this.transform.position + vDir;
			this.transform.position = CheckLeftCollision(vPos);
		}
		if (Input.GetKey(KeyCode.D)) 
		{				
			Vector3 vDir = this.transform.right * MoveSpeed * Time.deltaTime;
			Vector3 vPos = this.transform.position + vDir;
			this.transform.position = CheckRightCollision(vPos);
		}
	}
	// TODO: diagonal tiles
	private Vector3 CheckUpCollision(Vector3 vPos)
	{
		Vector3 vNodePos = currentNode.transform.position;
		if (currentNode.GetNeighbor(eDirection.Up))
		{
			return vPos;
		}
		else
		{
			vPos.y = vNodePos.y - tilesSettings.TileWidth * 0.5f;
			return vPos;
		}
	}

	private Vector3 CheckDownCollision(Vector3 vPos)
	{
		Vector3 vNodePos = currentNode.transform.position;
		if (currentNode.GetNeighbor(eDirection.Down) || vPos.y > vNodePos.y - tilesSettings.TileWidth * 0.5f)
		{
			return vPos;
		}
		else
		{
			vPos.y = vNodePos.y - tilesSettings.TileWidth * 0.5f;
			return vPos;
		}
	}

	private Vector3 CheckLeftCollision(Vector3 vPos)
	{
		Vector3 vNodePos = currentNode.transform.position;
		AINode aiNode = currentNode.GetNeighbor(eDirection.Left);
		if (aiNode != null || vPos.x > vNodePos.x)
		{
			Debug.Log("vPos.x " + vPos.x);
			Debug.Log("vNodePos.x " + vNodePos.x);
			Debug.Log("no collision");
			return vPos;
		}
		else
		{
			Debug.Log("vPos.x " + vPos.x);
			Debug.Log("vNodePos.x " + vNodePos.x);
			Debug.Log("collision");
			vPos.x = vNodePos.x;
			return vPos;
		}
	}

	private Vector3 CheckRightCollision(Vector3 vPos)
	{
		Vector3 vNodePos = currentNode.transform.position;
		if (currentNode.GetNeighbor(eDirection.Right) || vPos.x < vNodePos.x)
		{
			return vPos;
		}
		else
		{
			vPos.x = vNodePos.x;
			return vPos;
		}
	}
}
