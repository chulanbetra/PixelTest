using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	public float MoveSpeed = 5.0f;  
	// Use this for initialization
	void Start() 
	{	
	}
	
	// Update is called once per frame
	void Update() 
	{	
		HandleMovement();
	}

	// move character according to input
	void HandleMovement()
	{
		if (Input.GetKey(KeyCode.W)) 
		{
			// move camera up	
			Vector3 vDir = this.transform.up * MoveSpeed * Time.deltaTime;
			Vector3 vPos = this.transform.position + vDir;
			//Debug.Log(vPos.y);			
			this.transform.position = vPos;
		}
		if (Input.GetKey(KeyCode.S)) 
		{
			// move camera down	
			Vector3 vDir = this.transform.up * -MoveSpeed * Time.deltaTime;
			Vector3 vPos = this.transform.position + vDir;
			//Debug.Log(vPos.y);			
			this.transform.position = vPos;
		}
		if (Input.GetKey(KeyCode.A))
		{
			// move camera left	
			Vector3 vDir = this.transform.right * -MoveSpeed * Time.deltaTime;
			Vector3 vPos = this.transform.position + vDir;
			//Debug.Log(vPos.x);			
			this.transform.position = vPos;
		}
		if (Input.GetKey(KeyCode.D)) 
		{
			// move camera right	
			Vector3 vDir = this.transform.right * MoveSpeed * Time.deltaTime;
			Vector3 vPos = this.transform.position + vDir;
			//Debug.Log(vPos.x);			
			this.transform.position = vPos;
		}
	}
}
