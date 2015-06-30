using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class TileEditor : EditorWindow
{
	private List<Sprite> tiles;
	private int selectedTile;
	private GameObject tilesContainer;

	bool Is2DMode
	{
		get
		{
			return EditorSettings.defaultBehaviorMode == EditorBehaviorMode.Mode2D;
		}
	}

	[MenuItem ("Window/Tile Editor %e")]
	static void OpenTileEditor() 
	{
		GetWindow<TileEditor>("Tile Editor");
	}

	void OnEnable()
	{
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

		// get container for tiles
		tilesContainer = GameObject.Find("Tiles");
		if (tilesContainer == null)
		{
			tilesContainer = new GameObject("Tiles");
		}

		SceneView.onSceneGUIDelegate += OnSeceneGUI;
	}

	void OnDisable()
	{
		SceneView.onSceneGUIDelegate -= OnSeceneGUI;
	}

	void OnGUI()
	{
		if (!Is2DMode)
		{
			GUILayout.Label("Switch Scene View to 2D mode!");
		}
		else
		{
			selectedTile = GUILayout.SelectionGrid(selectedTile, tiles.Select(x => x.texture).ToArray(), 10, GUILayout.ExpandWidth(false));		
		}
	}

	public void OnSeceneGUI(SceneView sceneView)
	{
		int controlID = GUIUtility.GetControlID(FocusType.Passive);
		Event e = Event.current;
		if (e.type == EventType.MouseDown)
		{
			GUIUtility.hotControl = controlID;
			e.Use();

			Vector3 vPos = Camera.current.ScreenToWorldPoint(new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight, 0));
			vPos = MapSettings.GetAlignedVector3(vPos);
			GameObject pSpriteObject = GetGameObjectFromPosition(vPos);

			if (e.button == 0)
			{
				// left click
				if (pSpriteObject == null)
				{
					CreateTile(vPos);
				}
			}
			else if (e.button == 1)
			{
				// right click
				if (pSpriteObject != null)
				{
					DestroyImmediate(pSpriteObject);
				}
			}
		}
		if (e.type == EventType.MouseUp)
		{
			GUIUtility.hotControl = 0;
		}
	}

	void CreateTile(Vector3 vPos)
	{
		GameObject pSpriteObject = new GameObject();
		SpriteRenderer pRenderer = pSpriteObject.AddComponent<SpriteRenderer>();
		pRenderer.sortingLayerName = "Ground";
		pRenderer.sprite = tiles[selectedTile];
		pSpriteObject.name = tiles[selectedTile].name + "_" + vPos.x + "_" + vPos.y;
		pSpriteObject.transform.parent = tilesContainer.transform;
		pSpriteObject.transform.position = new Vector3(vPos.x, vPos.y, 0);
	}

	GameObject GetGameObjectFromPosition(Vector3 vPos)
	{
		if (tilesContainer != null)
		{
			for (int i = 0; i < tilesContainer.transform.childCount; i++)
		    {
				Transform pChild = tilesContainer.transform.GetChild(i);
				if (pChild.position == vPos)
				{
					return pChild.gameObject;
				}
		    }
		}
		return null;
	}
}
