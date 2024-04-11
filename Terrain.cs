using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Audio;

public class Terrain : MonoBehaviour
{
    private GameObject currentBlockType;
    public GameObject[] blockTypes;

    public float amp = 10f;
    public float freq = 10f;

    private Vector3 myPos;

    void Start() {
        generateTerrain();
    }

    void generateTerrain()
{
    myPos = this.transform.position;
    
    int cols = 100;
    int rows = 100;

    for (int x = 0; x < cols; x++)
    {
        for (int z = 0; z < rows; z++)
        {
            float y = Mathf.PerlinNoise((myPos.x + x) / freq, (myPos.z + z) / freq) * amp;

            y = Mathf.Floor (y);

            if (y > amp/3) currentBlockType =blockTypes[1];
            else currentBlockType = blockTypes[0];

            GameObject newBlock = GameObject.Instantiate(currentBlockType);
            newBlock.transform.position = new Vector3(myPos.x + x, y, myPos.z + z);

            // Legg til Box Collider til hver blokk
            BoxCollider collider = newBlock.AddComponent<BoxCollider>();
            // Juster størrelsen på collidere til å passe til blokken
            collider.size = new Vector3(1f, 1f, 1f);
        }
    }
}
}
