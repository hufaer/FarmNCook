using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class TilemapData
{
    public string key;
    public List<TileInfo> tiles = new List<TileInfo>();
}

[Serializable]
public class TileInfo
{
    public TileBase tile;
    public Vector3Int position;

    public TileInfo(TileBase tile, Vector3Int position)
    {
        this.tile = tile;
        this.position = position;
    }
}

[Serializable]
public class PlantData
{
    public Vector2Int position;
    public PlantTile plantTile;

    public PlantData(Vector2Int position, PlantTile plantTile)
    {
        this.position = position;
        this.plantTile = plantTile;
    }
}

[Serializable]
public class WorldObject
{
    public GameObject worldObject;
    public Vector3 position;

    public WorldObject(GameObject obj, Vector3 pos)
    {
        worldObject = obj;
        position = pos;
    }
}

public class GameManager : MonoBehaviour, IDataPersistence
{
    public static GameManager instance;

    [SerializeField] Item shovel;


    private void Awake()
    {
        instance = this;
    }

    Dictionary<string, Tilemap> tilemaps = new Dictionary<string, Tilemap>();
    private void Start()
    {
        tilemaps.Add(tilemap.name, tilemap);
        tilemaps.Add(plantsTilemap.name, plantsTilemap);

    }

    public void LoadData(GameData gameData)
    {
        foreach (PlantData plantData in gameData.plants)
        {
            plantsManager.plants.Add(plantData.position, plantData.plantTile);
        }

        Debug.Log(gameData.plants.Count);

        List<TilemapData> data = gameData.tilemaps;

        foreach (var tilemapData in data)
        {
            if (!tilemaps.ContainsKey(tilemapData.key))
            {
                Debug.Log("Tilemap wtf");
                continue;
            }

            var map = tilemaps[tilemapData.key];
            map.ClearAllTiles();

            if (tilemapData.tiles != null && tilemapData.tiles.Count > 0)
            {
                foreach (TileInfo tile in tilemapData.tiles)
                {
                    map.SetTile(tile.position, tile.tile);
                }
            }
        }

        inventory.slots = gameData.slots;
    }

    public void SaveData(ref GameData gameData)
    {
        Debug.Log("Save gamemanager");
        gameData.plants = new List<PlantData>();
        foreach (var pair in plantsManager.plants)
        {
            gameData.plants.Add(new PlantData(pair.Key, pair.Value));
        }

        List<TilemapData> data = new List<TilemapData>();

        gameData.slots = new List<ItemSlot>();
        foreach (ItemSlot slot in inventory.slots)
        {
            gameData.slots.Add(slot);
        }


        foreach (var obj in tilemaps)
        {
            TilemapData tilemapData = new TilemapData();
            tilemapData.key = obj.Key;

            BoundsInt bounds = obj.Value.cellBounds;
            for (int x = bounds.xMin; x < bounds.xMax; x++)
            {
                for (int y = bounds.yMin; y < bounds.yMax; ++y)
                {
                    Vector3Int position = new Vector3Int(x, y, 0);
                    TileBase tile = obj.Value.GetTile(position);

                    if (tile != null)
                    {
                        TileInfo tileInfo = new TileInfo(tile, position);
                        tilemapData.tiles.Add(tileInfo);
                    }
                }
            }
            data.Add(tilemapData);
        }

        gameData.tilemaps = data;
    }

    public GameObject player;

    public ItemContainer inventory;

    public ItemDragDropController dragDrop;

    public BaseItemPanel toolbar;

    public MarkerTileManager markerManager;

    public TileReader tileReader;

    public PlantsManager plantsManager;

    public DayTimeController timeController;

    public List<TileBase> hoeableTiles;

    public Tilemap plantsTilemap;

    public Tilemap tilemap;

    public AlchemyManager alchemyManager;

    public GameObject hintPanel;
    public TMPro.TextMeshProUGUI nameText;
    public TMPro.TextMeshProUGUI priceText;

    public List<RecipePanel> recipePanels;

    public SavePrefabsPosition savePosition;
}
