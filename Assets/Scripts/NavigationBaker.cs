using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;

public class NavigationBaker : MonoBehaviour
{
    public LayerMask navLayer;

    public GameObject[] sourceObjects; // NavMesh 생성에 사용될 지오메트리 오브젝트들

    private void Start()
    {
       sourceObjects = FindObjectsWithLayer(navLayer);

        // NavMesh 생성
        BuildNavMesh();
    }
    private GameObject[] FindObjectsWithLayer(LayerMask layer)
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        System.Collections.Generic.List<GameObject> filteredObjects = new System.Collections.Generic.List<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if ((layer.value & (1 << obj.layer)) > 0)
            {
                filteredObjects.Add(obj);
            }
        }

        return filteredObjects.ToArray();
    }
    private void BuildNavMesh()
    {
        NavMeshData navMeshData = new NavMeshData();

        NavMeshBuildSource[] sources = new NavMeshBuildSource[sourceObjects.Length];
        List<NavMeshBuildSource> listSources = new List<NavMeshBuildSource>();

        for (int i = 0; i < sourceObjects.Length; i++)
        {
            Mesh sourceMesh = sourceObjects[i].GetComponent<MeshFilter>().sharedMesh;
            Matrix4x4 sourceTransform = sourceObjects[i].transform.localToWorldMatrix;

            sources[i] = new NavMeshBuildSource();
            sources[i].shape = NavMeshBuildSourceShape.Mesh;
            sources[i].sourceObject = sourceMesh;
            sources[i].transform = sourceTransform;
            sources[i].area = NavMesh.GetSettingsByID(0).agentTypeID; // You may need to adjust this ID

            listSources.Add(sources[i]);
        }

        Bounds bounds = new Bounds(Vector3.zero, Vector3.one * 1000);
        bool v = NavMeshBuilder.UpdateNavMeshData(navMeshData, NavMesh.GetSettingsByID(0), listSources, bounds);
    }
}
