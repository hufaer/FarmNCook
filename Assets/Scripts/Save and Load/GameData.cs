using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

[System.Serializable]
public class GameData
{
    public int playerMoney;

    public Vector3 playerPosition;

    public List<ItemSlot> slots;

    public List<TilemapData> tilemaps;

    public List<PlantData> plants;

    public float timeOfDay;

    public List<Vector3> deletedObjects;

    //public List<WorldObject> worldObjects;

    public List<bool> recipesUnlocked;

    public int day;

    public int dialogueId;

    public GameData()
    {
        playerMoney = 0;
        playerPosition = new Vector3(-9, 6);
        slots = new List<ItemSlot>(new ItemSlot[32]);
        plants = new List<PlantData>();
        timeOfDay = 43200f;
        //worldObjects = new List<WorldObject>();
        recipesUnlocked = new List<bool>() { false, false, false, false, false};
        deletedObjects = new List<Vector3>();
        day = 0;
        dialogueId = 0;
}
}
