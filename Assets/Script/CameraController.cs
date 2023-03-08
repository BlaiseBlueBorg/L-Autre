using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5;
    private float lookUpMax = 30f;
    private float lookDownMax = -60f;

    private Vector2 rotation = Vector2.zero;
    [SerializeField] private Transform parent;
    [SerializeField] private Transform player;
    [SerializeField] private float distanceFromPlayer;
    [SerializeField] private LayerMask obstacleLayer; 

    private float initialFOV;


    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(parent);
    }
    
    // Update is called once per frame
    void Update()
    {
        rotation.x += Input.GetAxis("Mouse X") * rotationSpeed; //move up and down
        rotation.y += Input.GetAxis("Mouse Y") * rotationSpeed; //move left and right

        rotation.y = Mathf.Clamp(rotation.y, lookDownMax, lookUpMax);
        
        Quaternion xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
        Quaternion yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

        transform.localRotation = xQuat * yQuat;
        
        transform.position = parent.position - transform.forward * distanceFromPlayer;
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.blue;
    //     Gizmos.DrawLine(transform.position, player.position);
    //
    //     Physics.Linecast(transform.position, parent.position);
    //     bool isSomethingHit = Physics.Linecast(transform.position, player.position, out RaycastHit raycastHitInfo,
    //         obstacleLayer);
    //
    //     if (isSomethingHit)
    //     {
    //         Vector3 pointWhereHisOccured = raycastHitInfo.point;
    //         Gizmos.color = Color.red;
    //         Gizmos.DrawSphere(pointWhereHisOccured, radius:1);
    //     }
    // }
}

/*public Vector3 LineCastStart;
    public Vector3 LineCastEnd;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(LineCastStart, radius:0.2f);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(LineCastEnd, radius:0.2f);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(LineCastStart, LineCastEnd);

        Physics.Linecast(LineCastStart, LineCastEnd);

        bool isSomethingHit = Physics.Linecast(LineCastStart, LineCastEnd, out RaycastHit raycastHitInfo);

        if(isSomethingHit)
        {
            Vector3 pointWhereHitOccured = raycastHitInfo.point;
            Gizmos.color = Color.white; 
            Gizmos.DrawSphere(Vector3.zero, radius:1);

        }*/