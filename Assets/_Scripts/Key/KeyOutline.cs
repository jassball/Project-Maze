using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyOutline : MonoBehaviour
{
    public Material outlineMaterial;

    private Renderer rend;
    private Material[] originalMaterials;

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalMaterials = rend.materials;
    }

    public void ShowOutline()
    {
        Material[] newMaterials = new Material[originalMaterials.Length + 1];
        originalMaterials.CopyTo(newMaterials, 0);
        newMaterials[newMaterials.Length - 1] = outlineMaterial;
        rend.materials = newMaterials;
    }

    public void HideOutline()
    {
        rend.materials = originalMaterials;
    }
}
