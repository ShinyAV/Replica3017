using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float xMax = 0.0F;
    public float yMax = 0.0F;
    public float xMin = 0.0F;
    public float yMin = 0.0F;
    public float currentX = 0.0f;
    public float currentY = 0.0f;
    public float sensibility = 0.5f;
    public float zoomLevel;
    public float minZoomLevel = 5f;
    public float maxZoomLevel = 15f;
    public float zoomSensibility = 0.5f;
    public Camera playerCamera;
    public Player player;

    // Use this for initialization
    void Start ()
    {
        player = this.GetComponent<Player>();
        this.transform.position = player.myUnits[player.selectedUnit].GetComponent<Unit>().transform.position;
        currentX = this.transform.position.x;
        currentY = this.transform.position.y;
        zoomLevel = 15;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (player.isMovingPhase == false && player.isAttacking == false && player.isUsingAbility == false && player.isUsingTactic == false && player.isMenuOpen == false) 
        {
            //movimento della telecamera
            currentX += (Input.GetAxis("LeftStickHorizontalPlayer" + player.playerNumber) - Input.GetAxis("LeftStickVerticalPlayer" + player.playerNumber)) * sensibility;
            currentY -= (Input.GetAxis("LeftStickVerticalPlayer" + player.playerNumber) + Input.GetAxis("LeftStickHorizontalPlayer" + player.playerNumber)) * sensibility;
            //settaggio dei limiti del movimento
            currentY = Mathf.Clamp(currentY, yMin, yMax);
            currentX = Mathf.Clamp(currentX, xMin, xMax);
            playerCamera.GetComponent<Camera>().transform.position = new Vector2(currentX, currentY);
            //zoom telecamera
            zoomLevel -= (Input.GetAxis("RightTriggerPlayer" + player.playerNumber)) * zoomSensibility;
            zoomLevel += (Input.GetAxis("LeftTriggerPlayer" + player.playerNumber)) * zoomSensibility;
            //limiti zoom
            zoomLevel = Mathf.Clamp(zoomLevel, minZoomLevel, maxZoomLevel);
            playerCamera.GetComponent<Camera>().orthographicSize = zoomLevel;

        }

    }
}
