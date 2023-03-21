using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCutout : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private Camera mainCamera;
    private Material material = null;
    
    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }

    public void Update()
    {
        if (material != null)
        {
            material.SetFloat("_Activate", 0f);
        }
        
        Vector2 cutoutPosition = mainCamera.WorldToViewportPoint(player.position);
        
        cutoutPosition.y /= (Screen.width / Screen.height);

        //Vector3 offset = player.position - transform.position;
        bool isSomethingHit = Physics.Linecast(transform.position, player.position, out RaycastHit raycastHitInfo,
            obstacleLayer);

        if (isSomethingHit)
        {
            material = raycastHitInfo.transform.GetComponent<Renderer>().material;
            
            material.SetFloat("_Activate", 1f);
            
            // material.SetVector("_CutoutPosition", cutoutPosition);
            // material.SetFloat("_CutoutSize", 0.1f);
            // material.SetFloat("_FalloffSize", 0.05f);
        }

        // for (int i = 0; i < hitObjects.Length; i++)
        // {
        //     Material[] materials = hitObjects[i].transform.GetComponent<Renderer>().materials;
        //
        //     for (int m = 0; m < materials.Length; m++)
        //     {
        //         materials[m].SetVector("_CutoutPosition", cutoutPosition);
        //         materials[m].SetFloat("_CutoutSize", 0.1f);
        //         materials[m].SetFloat("_FalloffSize", 0.05f);
        //     }
        // }
    }
}
