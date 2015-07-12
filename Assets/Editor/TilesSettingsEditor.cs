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
	private List<Sprite> tiles;
	private int selectedTile;
	private GameObject tilesGameObject;
	private bool leftMouseButtonDown = false;
	private bool rightMouseButtonDown = false;

	void OnEnable()
	{
		// obtain reference to script and object the editor is describing
		tilesSettings = (TilesSettings)target;
		tilesGameObject = tilesSettings.gameObject;

		// create tiles list from resources
		tiles = new List<Sprite>();
		string sTilesPath = Application.dataPath + "/Resources/Textures/Tiles/Grass/";
		foreach (string sTilePath in Directory.GetFiles(sTilesPath, "*.png"))
		{
			string sTileName = Path.GetFileNameWithoutExtension(sTilePath);
			Sprite pSprite = Resources.Load<Sprite>("Textures/Tiles/Grass/" + sTileName);
			if (pSprite != null)
			{
				tiles.Add(pSprite);
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
		selectedTile = GUILayout.SelectionGrid(selectedTile, tiles.Select(x => x.texture).ToArray(), 10, GUILayout.ExpandWidth(false));	
		GUILayout.EndVertical();
	}

	void OnSceneGUI(/*SceneView sceneview*/)
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

			if (leftMouseButtonDown)
			{
				// dragging with left button pressed
				if (pSpriteObject == null)
				{
					CreateTile(vPos);
				}
			}
			if (rightMouseButtonDown)
			{
				// dragging with right button pressed
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
		GameObject pSpriteObject = new GameObject();
		SpriteRenderer pRenderer = pSpriteObject.AddComponent<SpriteRenderer>();
		pRenderer.sortingLayerName = "Ground";
		pRenderer.sprite = tiles[selectedTile];
		pSpriteObject.name = tiles[selectedTile].name + "_" + vPos.x + "_" + vPos.y;
		pSpriteObject.transform.parent = tilesGameObject.transform;
		pSpriteObject.transform.position = new Vector3(vPos.x, vPos.y, 0);
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
