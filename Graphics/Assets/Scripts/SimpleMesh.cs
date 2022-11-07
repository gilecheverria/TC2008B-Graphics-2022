/*
Generate the geometry of an object using code

Gilberto Echeverria
2022-11-07
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]


public class SimpleMesh : MonoBehaviour
{
    Mesh mesh;

    Vector3[] verts;
    int[] faces;

    [SerializeField] float scaleDelta = 0.001f;

    float scaleFactor = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        verts = new Vector3[4];
        faces = new int[12];

        verts[0] = new Vector3(-1, 0, -1);
        verts[1] = new Vector3(1, 0, -1);
        verts[2] = new Vector3(0, 0, 2);
        verts[3] = new Vector3(0, 3, 0);

        faces[0] = 0;
        faces[1] = 1;
        faces[2] = 2;
        faces[3] = 0;
        faces[4] = 3;
        faces[5] = 1;
        faces[6] = 1;
        faces[7] = 3;
        faces[8] = 2;
        faces[9] = 2;
        faces[10] = 3;
        faces[11] = 0;

        // Give the infomation to the mesh
        mesh.vertices = verts;
        mesh.triangles = faces;

        mesh.RecalculateNormals();
    }

    // Update is called once per frame
    void Update()
    {
        Matrix4x4 scaler = HW_Transforms.ScaleMat(scaleFactor, scaleFactor, scaleFactor);
        Matrix4x4 move = HW_Transforms.TranslationMat(-3, 0, -5);

        Matrix4x4 composite = move * scaler;

        Vector3[] modified = new Vector3[verts.Length];
        for(int i=0; i<verts.Length; i++) {
            Vector4 temp = new Vector4(verts[i].x, verts[i].y, verts[i].z, 1);
            modified[i] = composite * temp;
        }

        mesh.vertices = modified;

        scaleFactor += scaleDelta;
    }
}
