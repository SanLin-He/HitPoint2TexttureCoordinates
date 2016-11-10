using UnityEngine;
using System.Collections.Generic;

public class CreatCollider : MonoBehaviour
{
    public bool Show;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //Mesh mesh = GetComponent<MeshFilter>().mesh;
        //Vector3[] vertices = mesh.vertices;
        //int i = 0;
        //while (i < vertices.Length)
        //{
        //    vertices[i] += Vector3.up * Time.deltaTime;
        //    i++;
        //}
        //mesh.vertices = vertices;
        //mesh.RecalculateBounds();
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        foreach (ContactPoint contact in collisionInfo.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
    }

    // Summary:
    //     The closest point to the bounding box of the attached collider.
    //
    // Parameters:
    //   position:
    public void OnTriggerStay(Collider other)
    {
        //if (Show)
        //    DebugVertices(other.gameObject);

        

    }



    public void DebugVertices(GameObject other)
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        var vertices = mesh.vertices;
        var otherVertices = other.GetComponent<MeshFilter>().mesh.vertices;
        var newVertices = new List<Vector3>();
        foreach (var vertice in vertices)
        {
            var pos = transform.TransformPoint(vertice);
            foreach (var v in otherVertices)
            {

                var p = other.transform.TransformPoint(v);
                if (pos.Equals(p))
                {
                    newVertices.Add(vertice);
                }
            }

        }

        CreatGameObject(newVertices);
    }

    public void CreatGameObject(List<Vector3> newVertices)
    {
        GameObject go = new GameObject();
        MeshFilter mf = go.AddComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        mf.mesh = mesh;
        mesh.SetVertices(newVertices);
        mesh.RecalculateBounds();

        go.AddComponent<MeshCollider>();
        go.AddComponent<MeshRenderer>();
    }
}
