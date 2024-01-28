using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private Material blurMaterial;

    [SerializeField]
    private SpriteRenderer background;

    private void Awake() {
        if(!blurMaterial) return;
        List<Material> materials = new();
        background.GetMaterials(materials);
        materials.Add(blurMaterial);
        background.SetMaterials(materials);
    }

    public void UpdateBackground(float progress) {
        if(!background) return;
        background.material.SetFloat("_Progress", progress);
    }

}
