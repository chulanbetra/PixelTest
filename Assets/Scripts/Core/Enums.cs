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