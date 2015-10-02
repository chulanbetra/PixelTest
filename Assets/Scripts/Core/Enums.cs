using System.Collections;

// tile flags
public enum eTileFlag
{
	// player can move on this tile
	WALKABLE = 0x01,
	// AI can move on this tile
	AI_WALKABLE = 0x02,
	// dynamics (enemies, npcs, destroyable objects etc.)
	OBSTACLE = 0x04,
}

public enum eDirection
{
	Up = 0,
	Down = 1,
	Left = 2,
	Right = 3,
	Up_Left = 4,
	Up_Right = 5,
	Down_Left = 6,
	Down_Right = 7,
}