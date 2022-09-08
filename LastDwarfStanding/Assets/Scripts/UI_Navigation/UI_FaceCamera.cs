using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_FaceCamera : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
        _camera = FindObjectOfType<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = _camera.transform.forward;
    }
}
