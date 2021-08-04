using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointsTotal : MonoBehaviour
{

    public List<int> centers;
    public int points = 0;

    // Start is called before the first frame update
    void Start()
    {
        centers = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addPoints(string conventionCenter)
    {
        if(!centers.Contains(int.Parse(conventionCenter)) && int.Parse(conventionCenter) > 2)
        {
            points += 1;
            centers.Add(int.Parse(conventionCenter));
        }
    }
}
