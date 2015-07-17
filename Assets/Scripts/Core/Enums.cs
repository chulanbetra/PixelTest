﻿using System.Collections;

// tile flags
public enum eTileFlag
{
	// player can move on this tile
	WALKABLE = 0x01,
	// AI can move o nthis tile
	AI_WALKABLE = 0x02,
	// dynamics (actors, destroyable objects etc)
	OBSTACLE = 0x04,
	// can be moved through by animation
	CRAWL_UNDER = 0x08,
	JUMP_OVER = 0x10,
}

// tile neighbor directions
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