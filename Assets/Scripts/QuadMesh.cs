using UnityEngine;

public class QuadMesh : MonoBehaviour
   
{
    [SerializeField] private Material material;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        Mesh mesh = new Mesh();
        meshRenderer.material = material;
        mesh.vertices = new Vector3[]
        {
            new Vector3(0, 0, 0),//0
            new Vector3(1, 0, 0),//1
            new Vector3(1, 1, 0),//2
            new Vector3(0, 1, 0)//3
        };
        mesh.triangles = new int[]
        {
            0, 3, 1,//Triangle 1
            3, 2, 1 //Triangle 2
        };
        meshFilter.mesh = mesh;
    }
    private void Awake()
    {
    

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
