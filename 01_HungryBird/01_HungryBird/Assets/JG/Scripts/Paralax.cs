using UnityEngine;

public class Paralax : MonoBehaviour
{
    [SerializeField] Texture2D bg = null;
    [SerializeField] Vector2 scrollSpeed = new Vector2(0.5f, 0);

    MeshRenderer mr = null;

    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        mr.material.mainTexture = bg;
    }
    void LateUpdate()
    {
        if (mr.material.mainTexture != bg)
        {
            mr.material.mainTexture = bg;
        }
        mr.material.mainTextureOffset += scrollSpeed * Time.deltaTime;
    }
}