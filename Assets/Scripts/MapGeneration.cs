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

    Vector3 size = new Vector3(1, 2, 3);
    Vector3 position = new Vector3(0, 0, 0);
    void Start()
    {


        generateBlock(size, position);

        Debug.Log("FaceCount: " + cube.faces.Count);

        for (int i = 0; i < cube.faces.Count; i++)
        {
            Debug.Log("Face: " + cube.faces[i]);
            cube.faces[i].Reverse();
        }


        /* IList<Face> cubefaces = new List<Face>();
        cubefaces.Add(cube.faces[0]); */
        /* IEnumerable<Face> chosenFace = new IEnumerable<Face>(); */
        /* ExtrudeElements.Extrude(cube, cubefaces, ExtrudeMethod.IndividualFaces, 1f);
        SurfaceTopology.ConformNormals(cube, cube.faces); */
    }

    // Update is called once per frame
    void Update()
    {



    }

    void generateBlock(Vector3 size, Vector3 position)
    {
        cube = ShapeGenerator.GenerateCube(PivotLocation.Center, size);
        cube.GetComponent<MeshRenderer>().material = material;
        cube.transform.position = position;
    }
}
