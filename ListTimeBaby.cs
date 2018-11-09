using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListTimeBaby : MonoBehaviour {

    public GameObject cubePrefab;

    Color[] colors = { Color.black, Color.red, Color.blue, Color.yellow };

    GameObject[] cubes = new GameObject[5];

    float spacer = 0.5f;

	// Use this for initialization
	void Start () {

        for (int i = 0; i < cubes.Length; i++) {

            GameObject cube = Instantiate(cubePrefab);
            cubes[i] = cube;
            cube.transform.position = new Vector3(i + (spacer * i), cube.transform.position.y);
            cube.GetComponent<Renderer>().material.color = colors[i];
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
