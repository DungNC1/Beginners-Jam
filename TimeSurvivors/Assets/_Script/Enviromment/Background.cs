using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class Background : MonoBehaviour
{
    public Tilemap tilemap;
    public Transform player;
    public Tilemap propTilemap;
    public Tile[] propTiles;
    [SerializeField] private float propSpawnChance = 0.1f;
    public Tile groundTile;
    public int chunkSize = 16;
    public int loadRadius = 3;

    private Vector2Int lastPlayerChunk;
    private HashSet<Vector2Int> loadedChunks = new HashSet<Vector2Int>();

    void Start()
    {
        Debug.Log("Prop spawn chance: " + propSpawnChance);
        LoadChunksAround(WorldToChunkCoords(player.position));
    }


    void Update()
    {
        Vector2Int currentChunk = WorldToChunkCoords(player.position);
        if (currentChunk != lastPlayerChunk)
        {
            lastPlayerChunk = currentChunk;
            LoadChunksAround(currentChunk);
        }
    }

    Vector2Int WorldToChunkCoords(Vector3 worldPos)
    {
        return new Vector2Int(
            Mathf.FloorToInt(worldPos.x / chunkSize),
            Mathf.FloorToInt(worldPos.y / chunkSize)
        );
    }

    void LoadChunksAround(Vector2Int centerChunk)
    {
        for (int x = -loadRadius; x <= loadRadius; x++)
        {
            for (int y = -loadRadius; y <= loadRadius; y++)
            {
                Vector2Int chunkCoord = new Vector2Int(centerChunk.x + x, centerChunk.y + y);
                if (!loadedChunks.Contains(chunkCoord))
                {
                    GenerateChunk(chunkCoord);
                    loadedChunks.Add(chunkCoord);
                }
            }
        }
    }

    void GenerateChunk(Vector2Int chunkCoord)
    {
        Vector3Int tileOffset = new Vector3Int(chunkCoord.x * chunkSize, chunkCoord.y * chunkSize, 0);

        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                Vector3Int tilePos = tileOffset + new Vector3Int(x, y, 0);

                tilemap.SetTile(tilePos, groundTile);

                if (propTiles.Length > 0 && Random.value < propSpawnChance)
                {
                    Tile randomProp = propTiles[Random.Range(0, propTiles.Length)];
                    propTilemap.SetTile(tilePos, randomProp);
                    Debug.Log("Placing prop at " + tilePos + ": " + randomProp.name);
                }
            }
        }
    }

}
