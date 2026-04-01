using UnityEngine;
using System.Collections.Generic;
using System;

public enum RandomType { Random, Seeded, MapOfTheDay };

public class MapGenerator : MonoBehaviour
{
    [Header("Random Data")]
    public RandomType randomType;
    public int seed = 27;

    [Header("TileData")]
    public List<Tile> availableTiles;
    public float tileWidth;
    public float tileLength;
    public int mapCols;
    public int mapRows;

    public Tile[,] grid;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set the seed value
        //InitializeRandom();

        // TODO: Delete, this is only for testing
        //GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeRandom()
    {
        switch ( randomType )
        {
            // Random
            case RandomType.Random:
                // Do nothing, since our random class automatically randomizes everything
                UnityEngine.Random.InitState( (int)DateTime.Now.Ticks );
            break;
            // Seeded
            case RandomType.Seeded:
                UnityEngine.Random.InitState(seed);
            break;
            // Map Of The Day
            case RandomType.MapOfTheDay:
                UnityEngine.Random.InitState( DateToInt( DateTime.Now.Date ) );
            break;
        }
    }

    public int DateToInt( DateTime date )
    {
        return date.Year + date.Month + date.Day;
    }

    public void GenerateMap()
    {
        // Create the grid array to hold our map
        grid = new Tile[ mapCols, mapRows ];

        // iterate through and generate all the map tiles
        for ( int currentRow = 0; currentRow < mapRows; currentRow++ )
        {
            for ( int currentCol = 0; currentCol < mapCols; currentCol++ )
            {
                // create a map tile
                Tile tempTile = Instantiate<Tile> (GetRandomTile()) as Tile;

                // Put it in the right position
                Vector3 correctPosition = Vector3.zero;
                correctPosition.z = currentRow * tileWidth;
                correctPosition.x = currentCol * tileLength;
                tempTile.transform.position = correctPosition;
                
                // Set parent to game object
                tempTile.transform.parent = gameObject.transform;

                tempTile.name = "Tile(" + currentCol + ", " + currentRow + ")";

                // Open the correct doors

                // Check if a row south of the current row exists
                if (currentRow - 1 >= 0)
                {
                    tempTile.doorSouth.SetActive(false);
                }
                // Check if a row north of the current row exists
                if (currentRow + 1 < mapRows)
                {
                    tempTile.doorNorth.SetActive(false);
                }

                // Check if a col west of the current col exists
                if ( currentCol - 1 >= 0)
                {
                    tempTile.doorWest.SetActive(false);
                }
                // Check if a col east of the current col exists
                if ( currentCol + 1 < mapCols)
                {
                    tempTile.doorEast.SetActive(false);
                }

                // Save it to the grid
                grid[ currentCol, currentRow ] = tempTile;

            }
        }
    }

    public Tile GetRandomTile()
    {
        int tileNumber = UnityEngine.Random.Range(0, availableTiles.Count );
        return availableTiles[tileNumber];
    }

}