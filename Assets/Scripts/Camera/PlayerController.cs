using UnityEngine;
using System.Collections;
using System.Linq;

public class PlayerController : MonoBehaviour 
{
	public float MoveSpeed = 5.0f;  
	public LayerMask CollisionLayerMask;

	private AINode currentNode;
	private Sprite sprite;
	private SpriteRenderer spriteRenderer;
	private TilesSettings tilesSettings;
	private Vector2 colliderOffset;
	private Vector2 coliderSize;

	//private Transform char1;
	//private Transform char2;

	// Use this for initialization
	void Start() 
	{	
		// get collider data
		BoxCollider2D boxCollider2D = this.GetComponent<BoxCollider2D>(); 
		if(boxCollider2D != null)
		{
			colliderOffset = boxCollider2D.offset;
			coliderSize = boxCollider2D.size;
		}

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

		//char1 = GameObject.Find("char1").transform;
		//char2 = GameObject.Find("char2").transform;
	}
	
	// Update is called once per frame
	void Update() 
	{	
		HandleMovement();
		UpdateCurrentTile();

		// moving other characters behind player like train (Albion style), nned to fix collisions
		/*Vector3 dir1 = this.transform.position - char1.position;
		dir1.Normalize();
		Vector3 dir2 = char1.position - char2.position;
		dir2.Normalize();
		char1.position = Vector3.Lerp(char1.position, this.transform.position - dir1 * 0.5f, 0.1f);
		char2.position = Vector3.Lerp(char2.position, char1.position - dir2 * 0.5f, 0.1f);*/
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
		Vector2 vMoveAmount = GetMoveAmount();
		CheckCollisionX(ref vMoveAmount);
		CheckCollisionY(ref vMoveAmount);
		this.transform.Translate (vMoveAmount);
	}

	// check horizontal collisions and update move amount
	private void CheckCollisionX(ref Vector2 vMoveAmount)
	{
		if (vMoveAmount.x != 0)
		{
			Vector3 vPos = this.transform.position;
			float fDir = Mathf.Sign(vMoveAmount.x);
			Vector2 rayDirection = new Vector2(fDir, 0);
			for (int i = 0; i < 3; i++)
			{
				float fX = vPos.x + colliderOffset.x + coliderSize.x * 0.5f * fDir;
				float fY = vPos.y + colliderOffset.y + coliderSize.x * 0.5f * (i - 1);
				Vector2 rayOrigin = new Vector2(fX, fY);
				Debug.DrawRay(rayOrigin, rayDirection);
				
				RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, Mathf.Abs(vMoveAmount.x), CollisionLayerMask);
				if (hit.collider != null)
				{
					Debug.DrawRay(rayOrigin, rayDirection, Color.yellow);
					vMoveAmount.x = 0;
					break;
				}
			}
		}
	}

	// check vertical collisions and update move amount
	private void CheckCollisionY(ref Vector2 vMoveAmount)
	{
		if (vMoveAmount.y != 0)
		{
			Vector3 vPos = this.transform.position;
			float fDir = Mathf.Sign(vMoveAmount.y);
			Vector2 rayDirection = new Vector2(0, fDir);
			for (int i = 0; i < 3; i++)
			{
				float fX = vPos.x + colliderOffset.x + coliderSize.x * 0.5f * (i - 1);
				float fY = vPos.y + colliderOffset.y + coliderSize.x * 0.5f * fDir;
				Vector2 rayOrigin = new Vector2(fX, fY);
				Debug.DrawRay(rayOrigin, rayDirection);
				
				RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, Mathf.Abs(vMoveAmount.y), CollisionLayerMask);
				if (hit.collider != null)
				{
					Debug.DrawRay(rayOrigin, rayDirection, Color.yellow);
					vMoveAmount.y = 0;		
					break;
				}
			}
		}
	}

	// return move vector based on keys pressed
	private Vector2 GetMoveAmount()
	{
		Vector2 vDir = Vector2.zero;
		// right
		if (Input.GetKey(KeyCode.D)) 
		{				
			vDir.x = MoveSpeed * Time.deltaTime;
		}
		// left
		if (Input.GetKey(KeyCode.A))
		{
			vDir.x = -MoveSpeed * Time.deltaTime;
		}
		// up
		if (Input.GetKey(KeyCode.W)) 
		{				
			vDir.y = MoveSpeed * Time.deltaTime;
		}
		// down
		if (Input.GetKey(KeyCode.S)) 
		{
			vDir.y = -MoveSpeed * Time.deltaTime;
		}
		return vDir;
	}
}
