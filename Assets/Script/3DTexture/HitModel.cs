using UnityEngine;
using System.Collections;

public class HitModel : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastSelect(Input.mousePosition);
        }
    }

    private void RaycastSelect(Vector3 screenPoint)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPoint);
        RaycastHit hit;

        // 射线判定
        if (Physics.Raycast(ray, out hit) && hit.transform.gameObject != null && hit.transform.gameObject.GetComponent<MeshFilter>())
        {
            
            Renderer  r = hit.transform.gameObject.GetComponent<Renderer>();
            Texture2D texture = r.material.mainTexture as Texture2D;
            Debug.Log(hit.textureCoord);
            Color color = texture.GetPixel(Mathf.FloorToInt(hit.textureCoord.x * texture.width), Mathf.FloorToInt(hit.textureCoord.y * texture.height));
            Debug.Log(color.r * 255);
            Debug.Log(color.g * 255);
            Debug.Log(color.b * 255);
            Debug.Log(color.a * 255);
        }
    }

    //private Vector2 getUVPoint
}
