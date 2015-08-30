using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

[CustomEditor(typeof(TilesSettings))]
public class TilesSettingsEditor : Editor
{
	private TilesSettings tilesSettings;
	private List<SpriteRenderer> tiles;
	private int selectedTile;
	private GameObject tilesGameObject;
	private bool leftMouseButtonDown = false;
	private bool rightMouseButtonDown = false;

	void OnEnable()
	{
		// obtain reference to script and object the editor is describing
		tilesSettings = (TilesSettings)target;
		tilesGameObject = tilesSettings.gameObject;

		CreateTilesList();
	}

	private void CreateTilesList()
	{
		tiles = new List<SpriteRenderer>();
		string sTilesPath = Application.dataPath + "/Resources/Prefabs/Tiles/";
		foreach (string sTilePath in Directory.GetFiles(sTilesPath, "*.prefab", SearchOption.AllDirectories))
		{
			// leave only part of the path after Resources folder
			int indexOfResourcesSubstring = sTilePath.IndexOf("Resources");
			if (indexOfResourcesSubstring >= 0)
			{
				indexOfResourcesSubstring += "Resources/".Length;
				string sResourcePath = sTilePath.Substring(indexOfResourcesSubstring);
				// trim prefab file extension and change backslashes to forward slashes (unity needs forward slash when loading resources)
				sResourcePath = sResourcePath.Replace(".prefab", "").Replace('\\', '/');
				// load prefab and get sprite renderer component
				GameObject pTilePrefab = Resources.Load<GameObject>(sResourcePath);
				if (pTilePrefab != null)
				{
					SpriteRenderer pSpriteRenderer = pTilePrefab.GetComponent<SpriteRenderer>();
					if (pSpriteRenderer != null)
					{
						tiles.Add(pSpriteRenderer);
					}
				}
			}
		}
	}

	public override void OnInspectorGUI()
	{
		// tile width property editor
		GUILayout.BeginHorizontal();
		GUILayout.Label("Tile Width: ");
		tilesSettings.TileWidth = EditorGUILayout.Slider(tilesSettings.TileWidth, 1f, 100f);
		GUILayout.EndHorizontal();

		// grid color property editor
		GUILayout.BeginHorizontal();
		GUILayout.Label("Grid Color: ");
		tilesSettings.GridColor = EditorGUILayout.ColorField(tilesSettings.GridColor);
		GUILayout.EndHorizontal();

		// tiles selection property editor
		GUILayout.BeginVertical();
		GUILayout.Space(30);
		GUILayout.Label("Tiles: ");
		selectedTile = GUILayout.SelectionGrid(selectedTile, tiles.Select(x => x.sprite.texture).ToArray(), 8, GUILayout.ExpandWidth(false));	
		GUILayout.EndVertical();
	}

	void OnSceneGUI()
	{
		int controlID = GUIUtility.GetControlID(FocusType.Passive);
		Event e = Event.current;

		// mouse down event
		if (e.isMouse && e.type == EventType.MouseDown)
		{
			GUIUtility.hotControl = controlID;
			e.Use();

			// check mouse buttons pressed
			if (e.button == 0)
			{
				leftMouseButtonDown = true;

			}
			else if (e.button == 1)
			{
				rightMouseButtonDown = true;

			}
		}

		// mouse drag event
		if (e.isMouse && e.type == EventType.MouseDrag)
		{
			Vector3 vPos = Camera.current.ScreenToWorldPoint(new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight, 0));
			vPos = tilesSettings.GetAlignedVector3(vPos);
			GameObject pSpriteObject = GetGameObjectFromPosition(vPos);

			// dragging with left button pressed
			if (leftMouseButtonDown)
			{
				// create tile when none exists on that position
				if (pSpriteObject == null)
				{
					CreateTile(vPos);
				}
			}
			// dragging with right button pressed
			if (rightMouseButtonDown)
			{
				// delete tile when one exists on that position
				if (pSpriteObject != null)
				{
					DestroyImmediate(pSpriteObject);
				}
			}
		}

		// mouse up event
		if (e.isMouse && e.type == EventType.MouseUp)
		{
			GUIUtility.hotControl = 0;
			leftMouseButtonDown = false;
			rightMouseButtonDown = false;
		}
	}

	void CreateTile(Vector3 vPos)
	{
		SpriteRenderer pSpriteRenderer = tiles[selectedTile];
		GameObject pTile = GameObject.Instantiate(pSpriteRenderer.gameObject, vPos, Quaternion.identity) as GameObject;
		if (pTile != null)
		{
			pTile.name = pSpriteRenderer.sprite.texture.name + "_" + vPos.x + "_" + vPos.y;
			pTile.transform.parent = tilesGameObject.transform;
		}
	}

	GameObject GetGameObjectFromPosition(Vector3 vPos)
	{
		if (tilesGameObject != null)
		{
			for (int i = 0; i < tilesGameObject.transform.childCount; i++)
		    {
				Transform pChild = tilesGameObject.transform.GetChild(i);
				if (pChild.position == vPos)
				{
					return pChild.gameObject;
				}
		    }
		}
		return null;
	}
}
