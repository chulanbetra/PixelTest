using UnityEngine;

public sealed class MapSettings
{
	public static readonly MapSettings Instance = new MapSettings();
		
	public float TileWidth = 1f;
	
	MapSettings()
	{
	}	
	
	public static Vector3 IndexToVector3(int i, int j)		
	{		
		float fTileWidth = Instance.TileWidth;
		return new Vector3(i * fTileWidth + fTileWidth * 0.5f, j * fTileWidth + fTileWidth * 0.5f, 0);
	}
	
	public static Vector3 PointToVector3(Point pPoint)
	{
		float fTileWidth = Instance.TileWidth;
		return new Vector3(pPoint.X * fTileWidth + fTileWidth * 0.5f, pPoint.Y * fTileWidth + fTileWidth * 0.5f, 0);
	}

	public static Vector3 GetAlignedVector3(Vector3 v)
	{
		float fTileWidth = Instance.TileWidth;
		return new Vector3(Mathf.FloorToInt(v.x / fTileWidth) * fTileWidth + fTileWidth * 0.5f, Mathf.FloorToInt(v.y / fTileWidth) * fTileWidth + fTileWidth * 0.5f, 0);
	}
}
