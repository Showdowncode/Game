using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Audio;

public class Terrain : MonoBehaviour
{
    public GameObject currentBlockType;

    public float amp = 10f;
    public float freq = 10f;

    void Start()
    {
        generateTerrain();
    }

    void generateTerrain()
    {
        int cols = 100;
        int rows = 100;

        for (int x = 0; x < cols; x++)
        {
            for (int z = 0; z < rows; z++)
            {
                float y = Mathf.PerlinNoise(x / freq, z / freq) * amp;

                GameObject newBlock = GameObject.Instantiate(currentBlockType);
                newBlock.transform.position = new Vector3(x, y, z);

                // Legg til Box Collider til hver blokk
                BoxCollider collider = newBlock.AddComponent<BoxCollider>();
                // Juster størrelsen på collidere til å passe til blokken
                collider.size = new Vector3(1f, 1f, 1f);
            }
        }
    }
}
