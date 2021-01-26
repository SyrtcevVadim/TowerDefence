using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    public Material DefaultTerrainMaterial;
    public Material SelectedTerrainMaterial;
    public MeshRenderer mr;

    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }

    private void OnMouseEnter()
    {
        mr.material = SelectedTerrainMaterial;
    }
    private void OnMouseExit()
    {
        mr.material = DefaultTerrainMaterial;
    }
}
