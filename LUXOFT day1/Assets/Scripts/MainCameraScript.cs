using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraScript : MonoBehaviour {
    private const float cameraOffset = -10.0f;
    public Camera playerCamera;

    // Use this for initialization
    void Start ()
    {
        playerCamera = GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
        CameraFollow();
	}

    void CameraFollow()
    {
        /*
        var cam = gameObject.GetComponent<Camera>();
        var playerPosX = gameObject.transform.position.x;
        var playerPosY = gameObject.transform.position.y;
        
        playerCamera.transform.position = new Vector3(playerPosX, playerPosY, cameraOffset);
        Debug.Log(string.Format("x = {1}, y = {2}",  playerPosX, playerPosY));
        */
    }
}
