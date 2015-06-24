using System.Collections;

// tile flags
public enum eTileFlag
{
	NONE = 0x00,
	// flags for moving
	WALKABLE = 0x01,	
	// dynamics (actors, destroyable objects etc)
	OBSTACLE = 0x2,
	// can be moved through by animation
	CRAWL_UNDER = 0x4,
	JUMP_OVER = 0x8,
}

// nodes neighbor direction
public enum eDirection
{
	UP = 0,
	DOWN = 1,
	LEFT = 2,
	RIGHT = 3,
	UP_LEFT = 4,
	UP_RIGHT = 5,
	DOWN_LEFT = 6,
	DOWN_RIGHT = 7,
}