using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerToolsController : MonoBehaviour
{
    PlayerMovement player;
    Rigidbody2D rb;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float maxDistanceBetweenMouseAndPlayer;
    [SerializeField] TileData seedableTiles;
   

    [SerializeField] AudioSource axeSound;
    [SerializeField] AudioSource pickaxeSound;
    [SerializeField] AudioSource grassSound;

    private MarkerTileManager markerTileManager;
    private TileReader tileReader;
    private Vector3Int selectedPosition;
    private bool isSelectable;
    private PlayerToolBarController toolbarController;
    private Animator animator;

    private void Start()
    {
        markerTileManager = GameManager.instance.markerManager;
        tileReader = GameManager.instance.tileReader;
    }

    private void Awake()
    {
        player = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        toolbarController = GetComponent<PlayerToolBarController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        SelectTile();
        CheckIsSelectable();
        MarkTile();
        if (Input.GetMouseButtonDown(0) && !PauseGame.isPaused)
        {
            UseToolWithPhysics();
            UseToolOnGround();
        }
        if (Input.GetMouseButtonDown(1) && !PauseGame.isPaused)
        {
            TryHarvesting();
        }
    }

    private void SelectTile()
    {
        selectedPosition = tileReader.GetGridPosition(Input.mousePosition, true);

    }

    private void TryHarvesting()
    {
        if (!GameManager.instance.plantsManager.plants.ContainsKey((Vector2Int)selectedPosition)) return;
        PlantTile plantTile = GameManager.instance.plantsManager.plants[(Vector2Int)selectedPosition];
        grassSound.Play();
        if (plantTile.isGrownUp)
        {
            plantTile.DropResource();
        }

        GameManager.instance.plantsManager.plants.Remove((Vector2Int)selectedPosition);

        GameManager.instance.plantsTilemap.SetTile((Vector3Int)selectedPosition, null);
    }

    private void CheckIsSelectable()
    {
        Vector2 playerPosition = transform.position;
        Vector2 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isSelectable = Vector2.Distance(playerPosition, cameraPosition) < maxDistanceBetweenMouseAndPlayer;
        bool isPlantGround = GameManager.instance.hoeableTiles.Contains(tileReader.GetTileBase(selectedPosition));
        markerTileManager.Show((isSelectable && isPlantGround) || (isSelectable && toolbarController.CurrItem?.name == "Shovel"));
    }

    private void MarkTile()
    { 
        markerTileManager.markedPosition = selectedPosition;
    }

    private bool UseToolWithPhysics()
    {
        Vector2 position = rb.position + player.lastPosition * offsetDistance;

        Item item = toolbarController.CurrItem;
        if (!item || !item.onAction) return false;

        bool flag = item.onAction.OnApply(position);

        if (flag)
        {
            if (item.itemName == "Pickaxe")
            {
                animator.SetTrigger("Pickaxe");
                pickaxeSound.Play();
            }
            else if (item.itemName == "Axe")
            {
                axeSound.Play();
                animator.SetTrigger("Axe");
            }
            if (item.onItemUsage) item.onItemUsage.OnItemUsed(item, GameManager.instance.inventory);
        }
        return flag;
    }

    private void UseToolOnGround()
    {
        if (isSelectable)
        {
            Item item = toolbarController.CurrItem;
            if (!item || !item.onTilemapAction) return;
            
            bool flag = item.onTilemapAction.OnApplyToTilemap(selectedPosition, tileReader, item);
            if (flag)
            {
                if (item.itemName == "Hoe" || item.itemName == "Shovel")
                {
                    animator.SetTrigger("Pickaxe");
                }
                if (item.onItemUsage) item.onItemUsage.OnItemUsed(item, GameManager.instance.inventory);
            }
        }
    }
}
