using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed;
    void Update()
    {
        Vector3 newPosition = transform.position;
        Vector3 pVelocity = new Vector3();

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            pVelocity += new Vector3((moveSpeed * -1), 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            pVelocity += new Vector3(moveSpeed, 0, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
            pVelocity += new Vector3(0, 0, moveSpeed);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
            pVelocity += new Vector3(0, 0, (moveSpeed * -1));
        }

        Vector3 p = pVelocity * Time.deltaTime;

        transform.Translate(p);
        newPosition.x = transform.position.x;
        newPosition.z = transform.position.z;
        transform.position = newPosition;
    }
}
