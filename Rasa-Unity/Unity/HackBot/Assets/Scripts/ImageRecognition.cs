using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class ImageRecognition : MonoBehaviour
{
    private ARTrackedImageManager _arTrackedImageManager;

    void Awake() {
        _arTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
    }

    void OnEnable(){
        _arTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }

    void OnDisable(){
         _arTrackedImageManager.trackedImagesChanged -= OnImageChanged;
    }

    public void OnImageChanged(ARTrackedImagesChangedEventArgs args){
        foreach (var trackedImage in args.added){
            Debug.Log(trackedImage.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
