using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player; // reference to the player's transform component

    private bool shouldFollowPlayer = true; // flag to track if the camera should follow the player
    private void Update()
    {
        if (shouldFollowPlayer && player != null) // check if the camera should follow the player and if the player is not null
        {
            transform.position = new Vector3(player.position.x, player.position.y, transform.position.z); // move the camera to the player's position
        }
        
    }

    public void StopFollowingPlayer()
    {
        shouldFollowPlayer = false;
    }
}
