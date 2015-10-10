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
	}
	
	// Update is called once per frame
	void Update() 
	{	
		Vector3 vPlayerPosition = this.transform.position;
		HandleMovement(vPlayerPosition);
		UpdateCurrentTile(vPlayerPosition);
		UpdateCameraPosition(vPlayerPosition);
	}

	// move character according to input
	private void HandleMovement(Vector3 vPos)	{

		Vector2 vMoveAmount = GetMoveAmount();
		CheckCollisionX(vPos, ref vMoveAmount);
		CheckCollisionY(vPos, ref vMoveAmount);
		this.transform.Translate(vMoveAmount);
	}

	// check horizontal collisions and update move amount
	private void CheckCollisionX(Vector3 vPos, ref Vector2 vMoveAmount)
	{
		if (vMoveAmount.x != 0)
		{
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
	private void CheckCollisionY(Vector3 vPos, ref Vector2 vMoveAmount)
	{
		if (vMoveAmount.y != 0)
		{
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

	private void UpdateCurrentTile(Vector3 vPos)
	{
		float fTileWidthHalf = tilesSettings.TileWidth * 0.5f;
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

	private void UpdateCameraPosition(Vector3 vPos)
	{
		// find current map bounds
		MapBounds mapBounds = tilesSettings.MapBounds.FirstOrDefault(bounds => bounds.Contains(vPos));
		if (mapBounds != null)
		{
			// get camera extents
			float fCameraVerticalExtent = Camera.main.orthographicSize;
			float fCameraHorizontalExtent = fCameraVerticalExtent * Screen.width / Screen.height;

			// get boundary for camera
			float fLeftBound   = mapBounds.Min.x + fCameraHorizontalExtent;
			float fRightBound  = mapBounds.Max.x - fCameraHorizontalExtent;
			float fBottomBound = mapBounds.Min.y + fCameraVerticalExtent;
			float fTopBound    = mapBounds.Max.y - fCameraVerticalExtent;

			// calculate new camera position
			float fX = Mathf.Clamp(vPos.x, fLeftBound, fRightBound);
			float fY = Mathf.Clamp(vPos.y, fBottomBound, fTopBound);

			// set new camera position
			Camera.main.transform.position = new Vector3(fX, fY, Camera.main.transform.position.z);
		}
	}
}
