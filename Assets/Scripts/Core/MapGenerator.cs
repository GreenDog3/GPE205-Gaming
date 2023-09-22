using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] gridPrefabs;
    public int rows;
    public int cols;
    public int mapSeed;
    public float roomWidth = 25.0f;
    public float roomHeight = 25.0f;
    private Room[,] grid;

    public enum MapType{ Random, MapOfTheDay, SetSeed};
    public MapType mapType;

    private void Awake()
    {
        GenerateMap();
    }

    public GameObject RandomRoomPrefab()
    {
        return gridPrefabs[Random.Range(0, gridPrefabs.Length)];
    }

    public void GenerateMap()
    {
        //mapType = (MapType)GameManager.instance.typeOfMap;
        //mapSeed = (int)GameManager.instance.mapSeed;

        if (mapType == MapType.SetSeed)
        {
            //sets seed to number chosen
            Random.InitState(mapSeed);
        }
        else if (mapType == MapType.Random)
        {
            //seed randomly by using the clock since time is linear
            System.DateTime time;
            time = System.DateTime.Now;
            Random.InitState((int)time.Ticks);
        }
        else
        {
            //seed by the date
            Random.InitState((int)System.DateTime.Today.Ticks);
        }


        grid = new Room[rows, cols];
        for (int currentRow = 0; currentRow < rows; currentRow++)
        {
            //open vertical walls
            for (int currentCol = 0; currentCol < cols; currentCol++)
            {
                //find the locations we need to spawn rooms at
                float xPosition = roomWidth * currentCol;
                float zPosition = roomHeight * currentRow;
                Vector3 newPosition = new Vector3(xPosition, 0.0f, zPosition);
                //spawn the rooms in the locations we need to spawn the rooms at
                GameObject tempRoomObj = Instantiate (RandomRoomPrefab(), newPosition, Quaternion.identity) as GameObject;
                //set its parent
                tempRoomObj.transform.parent = this.transform;
                //Give it a name
                Room tempRoom = tempRoomObj.GetComponent<Room>();
                //put it in the array
                grid[currentCol, currentRow] = tempRoom;

                //open the walls
                if (currentRow == 0)
                {
                    //if we're on the bottom, open the north door
                    tempRoom.doorNorth.SetActive(false);
                }
                else if (currentRow == rows - 1)
                {
                    //if we're on top, open the bottom
                    tempRoom.doorSouth.SetActive(false);
                }
                else
                {
                    //if we're in the middle, open both
                    tempRoom.doorSouth.SetActive(false);
                    tempRoom.doorNorth.SetActive(false);
                }

                //open horizontal walls
                if (currentCol == 0)
                {
                    //if we're on the left, open to the right
                    tempRoom.doorEast.SetActive(false);
                }
                else if (currentCol == cols - 1)
                {
                    //if we're on the right, open to the left
                    tempRoom.doorWest.SetActive(false);
                }
                else
                {
                    //if we're in the middle, open both
                    tempRoom.doorEast.SetActive(false);
                    tempRoom.doorWest.SetActive(false);
                }
            }
        }
    }
}
