using UnityEngine;
using System.Collections;

public class GetPiexlColor : MonoBehaviour
{
    public Texture2D heightmap;
    public Vector3 size = new Vector3(100, 10, 100);

    void Update()
    {

        Debug.Log(heightmap.width);
        int x = Mathf.FloorToInt(transform.position.x / size.x * heightmap.width);
        int z = Mathf.FloorToInt(transform.position.z / size.z * heightmap.height);
        Vector3 pos = transform.position;
        pos.y = heightmap.GetPixel(x, z).grayscale * size.y;
        transform.position = pos;
        Debug.Log(pos);
    }
}
