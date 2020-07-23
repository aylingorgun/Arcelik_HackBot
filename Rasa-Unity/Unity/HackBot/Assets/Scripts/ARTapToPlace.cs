using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ARTapToPlace : MonoBehaviour
{

    public GameObject gameObjectToInstantiate;
    private GameObject spawnedObject;
    private ARRaycastManager _arRaycastManager;
    private Vector2 touchPosition;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake(){
        _arRaycastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetTouchPosition(out Vector2 touchPosition){
        if(Input.touchCount > 0){
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }
    void Update()
    {
        if(!TryGetTouchPosition(out Vector2 touchPosition))
            return;
        
        if(_arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon)){
            var hitPose = hits[0].pose;
            
            if(spawnedObject == null){
                spawnedObject = Instantiate(gameObjectToInstantiate, hitPose.position, hitPose.rotation);
                spawnedObject.GetComponentInChildren<Renderer>().material.color = Color.black;
            } else {
                spawnedObject.transform.Rotate(0.0f, 90.0f, 0.0f, Space.World);
                spawnedObject.transform.position = hitPose.position;

                if(spawnedObject.GetComponentInChildren<Renderer>().material.color == Color.black)
                    spawnedObject.GetComponentInChildren<Renderer>().material.color = Color.red;
                else
                    spawnedObject.GetComponentInChildren<Renderer>().material.color = Color.black;
            }
        }
    }
}
