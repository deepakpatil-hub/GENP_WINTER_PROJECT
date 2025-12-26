using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

    public int mapSizeInChunks = 6;
    public int chunkSize = 16, chunkHeight = 128;
    public int waterThreshold = 50;
    public float noiseScale = 0.03f;
    public GameObject chunkPrefab;
    public Vector2Int mapSeedOffset;

    public TerrainGenerator terrainGenerator;

    Dictionary<Vector3Int, ChunkData> chunkDataDictionary = new Dictionary<Vector3Int, ChunkData>();
    Dictionary<Vector3Int, ChunkRenderer> chunkDictionary = new Dictionary<Vector3Int, ChunkRenderer>();

    public void GenerateWorld() {
        chunkDataDictionary.Clear();
        foreach (ChunkRenderer chunk in chunkDictionary.Values) {
            Destroy(chunk.gameObject);
        }
        chunkDictionary.Clear();

        for(int x = 0; x < mapSizeInChunks; x++) {
            for (int z = 0; z < mapSizeInChunks; z++) {

                ChunkData data = new ChunkData(chunkSize,chunkHeight, this,new Vector3Int(x*chunkSize,0,z*chunkSize));
                //GenerateVoxels(data);
                ChunkData newData = terrainGenerator.GenerateChunkData(data, mapSeedOffset);
                chunkDataDictionary.Add(data.worldPosition,data);
            }
        }

        foreach (ChunkData data in chunkDataDictionary.Values) {
            MeshData meshData = Chunk.GetChunkMeshData(data);
            GameObject chunkObject = Instantiate(chunkPrefab, data.worldPosition, Quaternion.identity);
            ChunkRenderer chunkRenderer = chunkObject.GetComponent<ChunkRenderer>();
            chunkDictionary.Add(data.worldPosition, chunkRenderer);
            chunkRenderer.InitializeChunk(data);
            chunkRenderer.UpdateChunk(meshData);
        }
    }

    private void GenerateVoxels(ChunkData chunkData) {
        
    }

    public BlockType GetBlockFromChunkCoordinates(ChunkData chunkData, int x, int y, int z) { 
        Vector3Int pos = Chunk.ChunkPositionFromBlockCoordinates(this, x,y,z);
        ChunkData containerChunk = null;

        chunkDataDictionary.TryGetValue(pos, out containerChunk);

        if (containerChunk == null) {
            return BlockType.Nothing;
        }
        Vector3Int positionInChunkCoordinates = Chunk.GetPositionInChunkCoordinates(containerChunk,new Vector3Int(x,y,z));
        return Chunk.GetBlockFromChunkCoordinates(containerChunk,positionInChunkCoordinates);
    }
}