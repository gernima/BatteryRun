using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float roadEndPoint;
    [SerializeField] private float zSpeed;
    [SerializeField] private float timeToDestroyCell = 0.05f;
    [SerializeField] private PlayerBody playerBody;
    private float velocity;

    private Camera mainCamera;

    private Transform playerTransform;

    private Vector3 firstMousePosition, firstPlayerPosition;

    private bool moveTheBall;

    
    private void Start()
    {
        mainCamera = Camera.main;
        playerTransform = this.transform;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            moveTheBall = true;
        } else if (Input.GetMouseButtonUp(0))
        {
            moveTheBall = false;
        }

        if (moveTheBall)
        {
            Plane newPlane = new Plane(Vector3.up, 0.8f);
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (newPlane.Raycast(ray, out var distance))
            {
                Vector3 newMousePosition = ray.GetPoint(distance) - firstMousePosition;
                Vector3 newPlayerPosition = newMousePosition + firstPlayerPosition;
                newPlayerPosition.x = Mathf.Clamp(newPlayerPosition.x, -roadEndPoint, roadEndPoint);
                playerTransform.position = new Vector3(
                    Mathf.SmoothDamp(playerTransform.position.x, newPlayerPosition.x, ref velocity, speed * Time.deltaTime), 
                    playerTransform.position.y, playerTransform.position.z);
            }
        }
    }
    private void FixedUpdate()
    {
        playerTransform.position += Vector3.forward * zSpeed * Time.fixedDeltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "cellObstacle")
        {
            Destroy(other.gameObject, timeToDestroyCell);
            playerBody.Grow();
        }
    }
}
