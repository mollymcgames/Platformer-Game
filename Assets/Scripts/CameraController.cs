using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player; // reference to the player's transform component
    private void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z); // move the camera to the player's position
        
    }
}
