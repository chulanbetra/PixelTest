using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class MapBounds
{
	public Vector3 Center;
	public Vector3 Size;

	public Vector3 Min
	{
		get
		{
			return Center - Extents;
		}
	}

	public Vector3 Max
	{
		get
		{
			return Center + Extents;
		}
	}

	public Vector3 Extents
	{
		get
		{
			return Size * 0.5f;
		}
	}

	public bool Contains(Vector3 vPos)
	{
		return (vPos.x >= Min.x 
		    && vPos.x <= Max.x
		    && vPos.y >= Min.y
		    && vPos.y <= Max.y
		    && vPos.z >= Min.z
		        && vPos.z <= Max.z);
	}
}
