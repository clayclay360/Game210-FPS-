using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCubeScript : MonoBehaviour
{
    public Transform sphereRef;
    public float speed;
    
    // Update is called once per frame
    void Update()
    {

        #region

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        #endregion

        float sphereRadius = sphereRef.localScale.x / 2;

        //cube vertics
        Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
        Vector3[] vertices = mesh.vertices;

        for(int v = 0; v < vertices.Length; v++)
        {
            Vector3 vertexWorldPos = transform.TransformPoint(vertices[v]);

            if(Vector3.Distance(vertexWorldPos,sphereRef.position) < sphereRadius)
            {
                Debug.Log("Cube is colliding");
                return;
            }
        }

        Debug.Log("Cube not colliding");
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered with" + other.name);
    }
}
