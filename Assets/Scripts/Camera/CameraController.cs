using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{	
	public Camera m_pViewCamera;
	public float ZoomSpeed = 1.0f;
	public float ScrollSpeed = 15.0f;  
	// how many pixels are rendered within one unity unit
	float PixelsPerUnit;
	
	void Awake()
	{	
		m_pViewCamera = this.GetComponentInChildren<Camera>();
		PixelsPerUnit = (Screen.height / (m_pViewCamera.orthographicSize * 2));
	}
	
	// Use this for initialization
	void Start () 
	{		
	}
	
	// Update is called once per frame
	void Update () 
	{			
		//HandleCameraMovement();		
		//HandleCameraZoom();
	}
	
	// move camera according to input
	void HandleCameraMovement()
	{
		if (Input.GetKey(KeyCode.W) /*|| Input.mousePosition.y > Screen.height - 30.0f*/)
		{
			// move camera up	
			Vector3 vDir = this.transform.up * ScrollSpeed * Time.deltaTime;
			Vector3 vPos = this.transform.position + vDir;
			vPos.y = Mathf.Round(vPos.y * PixelsPerUnit) / PixelsPerUnit;
			//Debug.Log(vPos.y);			
			this.transform.position = vPos;
		}
		else if (Input.GetKey(KeyCode.S) /*|| Input.mousePosition.y < 30.0f*/)
		{
			// move camera down	
			Vector3 vDir = this.transform.up * -ScrollSpeed * Time.deltaTime;
			Vector3 vPos = this.transform.position + vDir;
			vPos.y = Mathf.Round(vPos.y * PixelsPerUnit) / PixelsPerUnit;
			//Debug.Log(vPos.y);			
			this.transform.position = vPos;
		}
		else if (Input.GetKey(KeyCode.A) /*|| Input.mousePosition.x < 30.0f*/)
		{
			// move camera left	
			Vector3 vDir = this.transform.right * -ScrollSpeed * Time.deltaTime;
			Vector3 vPos = this.transform.position + vDir;
			vPos.x = Mathf.Round(vPos.x * PixelsPerUnit) / PixelsPerUnit;
			//Debug.Log(vPos.x);			
			this.transform.position = vPos;
		}
		else if (Input.GetKey(KeyCode.D) /*|| Input.mousePosition.x > Screen.width - 30.0f*/)
		{
			// move camera right	
			Vector3 vDir = this.transform.right * ScrollSpeed * Time.deltaTime;
			Vector3 vPos = this.transform.position + vDir;
			vPos.x = Mathf.Round(vPos.x * PixelsPerUnit) / PixelsPerUnit;
			//Debug.Log(vPos.x);			
			this.transform.position = vPos;
		}
	}	
	
	// zoom camera according to input
	void HandleCameraZoom()
	{
		if (Input.GetKey(KeyCode.Z))
		{
			// zoom in	
			CameraZoom(-ZoomSpeed);
		}
		else if (Input.GetKey(KeyCode.X))
		{
			// zoom out 
			CameraZoom(ZoomSpeed);
		}
	}
	
	// zoom camera in or out, values are clamped
	void CameraZoom(float fZoom)
	{
		float fZoomLevel = m_pViewCamera.orthographicSize;
		m_pViewCamera.orthographicSize = Mathf.Clamp(fZoomLevel + fZoom, 5.0f, 50.0f);
	}
}
