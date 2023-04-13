using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MarkerTileManager : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] TileBase markerTile;

    public Vector3Int markedPosition;
    private Vector3Int oldPosition;
    private bool isShowable = false;

    private void Update()
    {
        if (!isShowable) { return; }
        tilemap.SetTile(oldPosition, null);
        tilemap.SetTile(markedPosition, markerTile);
        oldPosition = markedPosition;
    }

    internal void Show(bool isSelectable)
    {
        isShowable = isSelectable;
        tilemap.gameObject.SetActive(isShowable);
    }
}
