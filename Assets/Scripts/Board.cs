using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int width;
    public int height;

    public GameObject tilePrefab;

    Tile[,] m_allTiles; // 2D array of tiles

    void Start()
    {
        m_allTiles = new Tile[width, height];
        SetUpTiles();
    }

    void SetUpTiles()
    {
        for (int i = 0; i < width; i++) // for x-axis
        {
            for (int j = 0; j < height; j++) // for y-axis
            {
                GameObject tile = Instantiate(tilePrefab, new Vector3(i, j, 0), Quaternion.identity) as GameObject; // Not sure if we need to cast this these days

                tile.name = "Tile (" + i + "," + j + ")";

                m_allTiles[i, j] = tile.GetComponent<Tile>();

                tile.transform.parent = transform;
            }
        }
    }
}
