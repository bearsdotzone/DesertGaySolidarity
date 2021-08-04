using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class mapper : MonoBehaviour
{
    List<int> procGenLocations;
    Dictionary<int, GameObject> procGenTiles;

    public int tilesToGenerate = 100;

    Dictionary<string, GameObject> tilePrefabs;

    // Start is called before the first frame update
    void Start()
    {
        procGenLocations = new List<int>();
        procGenTiles = new Dictionary<int, GameObject>();

        tilePrefabs = new Dictionary<string, GameObject>();
        GameObject[] tiles = Resources.LoadAll("procgenTiles/", typeof(GameObject)).Cast<GameObject>().ToArray();
        foreach(GameObject g in tiles)
        {
            tilePrefabs.Add(g.name, g);
        }
    }

    public static int flipped = 1;
    public int magnitude = 0;
    public float magnitudeDistance = 100f;
    int tileWidth = 10;
    int rearTiles = 50;

    // Update is called once per frame
    void Update()
    {
        float currentZ = this.transform.position.z;

        if(currentZ >= magnitudeDistance)
        {
            magnitude += 1;
            this.transform.position -= new Vector3(0f, 0f, magnitudeDistance);
            currentZ -= magnitudeDistance;
        }
        else if (currentZ <= -magnitudeDistance)
        {
            magnitude -= 1;
            this.transform.position += new Vector3(0f, 0f, magnitudeDistance);
            currentZ += magnitudeDistance;
        }

        int currentGrid = (((int)currentZ) / tileWidth) + (magnitude * ((int)magnitudeDistance / tileWidth));
        

        int lowerBound = currentGrid;
        int upperBound = currentGrid;

        if (flipped == 1)
        {
            lowerBound -= rearTiles;
            upperBound += tilesToGenerate;
        }
        else
        {
            lowerBound -= tilesToGenerate;
            upperBound += rearTiles;
        }

        //print(string.Format("current grid: {0} lowerBound: {1} upperBound: {2}", currentGrid, lowerBound, upperBound));


        for (int a = procGenLocations.Count - 1; a >= 0; a--)
        {
            int gridToRemove = procGenLocations[a];
            if (gridToRemove > upperBound || gridToRemove < lowerBound)
            {
                // A tile that is unnesseccarry

                procGenLocations.Remove(gridToRemove);
                Destroy(procGenTiles[gridToRemove]);
                procGenTiles.Remove(gridToRemove);
            }
        }

        for(int a = lowerBound; a < upperBound; a++)
        {
            if(!procGenLocations.Contains(a))
            {
                GameObject desertTile;

                if (a <= 0)
                {
                    tilePrefabs.TryGetValue("grassLand", out desertTile);
                }
                else if (a == 1)
                {
                    tilePrefabs.TryGetValue("yellowstone", out desertTile);
                }
                else if (a < 250)
                {
                    tilePrefabs.TryGetValue("grassLand", out desertTile);
                }
                else if (a % 133760 == 0)
                {
                    tilePrefabs.TryGetValue("yellowstone", out desertTile);
                }
                else if ((a % 133760 < 250) || (a % 133760 > 133510))
                {
                    tilePrefabs.TryGetValue("grassLand", out desertTile);
                }
                else if (a % 66880 == 0)
                {
                    tilePrefabs.TryGetValue("conventionCenter", out desertTile);
                }
                else
                {
                    tilePrefabs.TryGetValue("desert", out desertTile);
                }

                if (desertTile == null)
                {
                    Debug.LogError("tile error on " + currentGrid);
                    tilePrefabs.TryGetValue("plainTile", out desertTile);
                }

                float currentTileZ = (currentGrid % (magnitudeDistance / tileWidth)) * tileWidth;

                GameObject newTile = Instantiate(desertTile);
                newTile.transform.position = new Vector3(0, 0, currentTileZ + (a - currentGrid) * tileWidth);
                newTile.name = a + "";
                procGenLocations.Add(a);
                procGenTiles.Add(a, newTile);
            }
            else
            {
                GameObject toMove;
                procGenTiles.TryGetValue(a, out toMove);
                float positionBasedOnMagnitude = int.Parse(toMove.name) * tileWidth - (magnitudeDistance * magnitude);

                if (toMove.transform.position.z != positionBasedOnMagnitude)
                {
                    toMove.transform.position = new Vector3(0f, 0f, positionBasedOnMagnitude);
                }
            }
        }
    }
    
}
