using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class PlantTile
{
    public int time;
    public Plant plant;
    public int currGrowthStage;
    public bool isGrownUp;

    private float spread = 0.6f;
    public Vector2Int tilePosition;

    public void DropResource()
    {
        int count = plant.count;
        Debug.Log(count);
        while (count > 0)
        {
            count--;

            Vector3 position = new Vector3(tilePosition.x, tilePosition.y);
            position.x += spread * UnityEngine.Random.value - spread / 2;
            position.y += spread * UnityEngine.Random.value - spread / 2;

            SpawnItem.instance.Spawn(position, plant.dropPlant, 1);
        }
    }
}

public class PlantsManager : TimeManager
{
    [SerializeField] TileBase hoedTile;
    [SerializeField] Tilemap tilemap;

    public Dictionary<Vector2Int, PlantTile> plants = new Dictionary<Vector2Int, PlantTile>();

    private void Start()
    {
        //plants = new Dictionary<Vector2Int, PlantTile>();
        //hoedTiles = new List<Vector2Int>();
        action += Tick;
        Init();
    }

    public void Tick()
    { 
        foreach (var dictPlant in plants)
        {
            if (dictPlant.Value.isGrownUp) { continue; }
            dictPlant.Value.time += 1;

            if (dictPlant.Value.time >= dictPlant.Value.plant.growthPhaseTime[dictPlant.Value.currGrowthStage])
            {
                tilemap.SetTile((Vector3Int)dictPlant.Key, dictPlant.Value.plant.growingTiles[dictPlant.Value.currGrowthStage]);
                dictPlant.Value.currGrowthStage++;
            }

            if (dictPlant.Value.time >= dictPlant.Value.plant.timeToGrow)
            {
                Debug.Log("Growed up");
                dictPlant.Value.isGrownUp = true;
            }
        }
    }

    internal bool Check(Vector3Int position)
    {
        return tilemap.GetTile(position) == hoedTile && !plants.ContainsKey((Vector2Int)position);
    }

    internal void Seed(Vector3Int position, Plant plant) 
    {
        PlantTile plantTile = new PlantTile();
        plants.Add((Vector2Int)position, plantTile);
        plantTile.tilePosition = (Vector2Int)position;

        tilemap.SetTile(position, plant.growingTiles[0]);
        plants[(Vector2Int)position].plant = plant;
    }

    public void Plow(Vector3Int position)
    {
        if (tilemap.GetTile(position) == hoedTile)
        {
            return;
        }

        StartCoroutine(Hoe(position));
    }

    public IEnumerator Hoe(Vector3Int position)
    {
        yield return new WaitForSeconds(0.6f);
        tilemap.SetTile(position, hoedTile);
    }
}
