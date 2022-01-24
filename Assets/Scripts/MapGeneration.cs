using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class MapGeneration : MonoBehaviour
{
    // Start is called before the first frame update
    public ProBuilderMesh cube;
    public Material material;
    void Start()
    {
        Vector3 test = new Vector3(1, 2, 3);
        Vector3 test2 = new Vector3(0, 0, 0);
        cube = ShapeGenerator.GenerateCube(PivotLocation.Center, test);
        cube.GetComponent<MeshRenderer>().material = material;
        cube.transform.position = test2;


        // Update is called once per frame
        void Update()
        {



        }

    }
}
