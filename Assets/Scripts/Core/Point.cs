using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Point
{
	public int X;
	public int Y;
	
	public Point(int x, int y)
	{
		X = x;
		Y = y;
	}
}

public class PointEqualityComparer : IEqualityComparer<Point>
{
    public bool Equals(Point vPoint1, Point vPoint2)
    {
        return vPoint1.X == vPoint2.X && vPoint1.Y == vPoint2.Y;
    }

    public int GetHashCode(Point vPoint)
    {
        return vPoint.X ^ vPoint.Y;
    }
}
