using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diceScript : MonoBehaviour
{

    List<GameObject> diceObjects;
    public GameObject diceLord;
    public Sprite[] diceSprites;
    pointsTotal pt;

    // Start is called before the first frame update
    void Start()
    {
        diceObjects = new List<GameObject>();
        pt = transform.parent.GetComponent<pointsTotal>();
    }

    int lastPoints = 0;

    // Update is called once per frame
    void Update()
    {
        int amount = pt.points;
        int numDice = (int) Mathf.Ceil((float)amount / 6.0f);

        if (diceObjects.Count < numDice)
        {
            GameObject newDice = Instantiate(diceLord, transform);
            newDice.transform.localPosition = new Vector3((numDice - 1) * -0.1f, 0f, 0f);
            diceObjects.Add(newDice);
            lastPoints = amount;
        }

        if(amount > lastPoints)
        {
            int spriteIndex = (amount % 6) - 1;
            if(spriteIndex == -1)
            {
                spriteIndex = 5;
            }

            diceObjects[diceObjects.Count - 1].GetComponentInChildren<SpriteRenderer>().sprite = diceSprites[spriteIndex];
            lastPoints = amount;
        }
    }
}
