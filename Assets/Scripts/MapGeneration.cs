using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;


public class MapGeneration : MonoBehaviour
{
    // Start is called before the first frame update
    public ProBuilderMesh cube;
    public Material material;
    void Start()
    {
        
        /* IList<Face> cubefaces = new List<Face>();
        cubefaces = cube.faces; */

        

        ExtrudeElements.Extrude(cube, cube.faces, ExtrudeMethod.IndividualFaces, 1f);
        Debug.Log("FaceCount: " + cube.faces.Count);
        for(int i = 0; i < cube.faces.Count; i++){
            Debug.Log("Face: "+cube.faces[i]);
            cube.faces[i].Reverse();
        }
        


    }

    // Update is called once per frame
    void Update()
    {



    }

    void generateBlock()
    {
        Vector3 test = new Vector3(1, 2, 3);
        Vector3 test2 = new Vector3(0, 0, 0);
        cube = ShapeGenerator.GenerateCube(PivotLocation.Center, test);
        cube.GetComponent<MeshRenderer>().material = material;
        cube.transform.position = test2;
        /* cube.triangles = mesh.triangles.Reverse().ToArray(); */
    }
}
