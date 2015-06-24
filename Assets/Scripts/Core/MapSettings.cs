using UnityEngine;

public sealed class MapSettings
{
	public static readonly MapSettings Instance = new MapSettings();
		
	public float TileWidth = 1f;
	public int TileCountX = 30;
	public int TileCountZ = 30;
	public int TileZIndex = 50;
	
	MapSettings()
	{
	}	
	
	public static Vector3 IndexToVector3(int i, int j)		
	{		
		float fTileWidth = Instance.TileWidth;
		return new Vector3(i * fTileWidth + fTileWidth * 0.5f, j * fTileWidth + fTileWidth * 0.5f, Instance.TileZIndex);
	}
	
	public static Vector3 PointToVector3(Point pPoint)
	{
		float fTileWidth = Instance.TileWidth;
		return new Vector3(pPoint.X * fTileWidth + fTileWidth * 0.5f, pPoint.Y * fTileWidth + fTileWidth * 0.5f, Instance.TileZIndex);
	}
}
