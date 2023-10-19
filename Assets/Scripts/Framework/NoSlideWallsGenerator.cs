using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NoSlideWallsGenerator : MonoBehaviour
{
    // [SerializeField] private GameObject noSlideWall;
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private TileBase[] groundTiles;

    private List<TileBase> _tiles = new List<TileBase>();

    private void Start()
    {
        FindGroundTiles();
    }

    private void FindGroundTiles()
    {
        // Get the bounds of the tilemap
        var bounds = groundTilemap.cellBounds;

        // Iterate through all positions in the bounds
        for (int x = bounds.x; x < bounds.x + bounds.size.x; x++)
        {
            for (int y = bounds.y; y < bounds.y + bounds.size.y; y++)
            {
                // Get the tile at the current position
                var tile = groundTilemap.GetTile(new Vector3Int(x, y, 0));

                // Check if the tile is not null (i.e., it's a TileBase)
                if (tile != null)
                {
                    // Do something with the TileBase
                    // Debug.Log("Found a TileBase at position: " + new Vector3Int(x, y, 0));
                    _tiles.Add(tile);
                }
            }
        }

        for (int index = 0; index < _tiles.Count; index++)
        {
            var tile = _tiles[index];
            var yes = tile == groundTiles[0] && tile == groundTiles[1] && tile == groundTiles[2];
            if (!yes)
            {
                _tiles.Remove(tile);
            }
            
            Debug.Log(_tiles[index]);
        }
    }
}
