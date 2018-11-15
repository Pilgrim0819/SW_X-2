using UnityEngine;
using System.Collections;

public class CameraHandler : MonoBehaviour {

    public float horizontalSpeed = 4.0f;
    public float verticalSpeed = 4.0f;
    public float moveSpeed = 15.0f;

    private float pitch = 0.0f;
    private float yaw = 0.0f;

    private bool rotateCamera = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            rotateCamera = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            rotateCamera = false;
        }

        if (rotateCamera)
        {
            yaw += horizontalSpeed * Input.GetAxis("Mouse X");
            pitch += verticalSpeed * Input.GetAxis("Mouse Y");
            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }

        if (Input.GetKey("w"))
        {
            transform.position += this.transform.forward * moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey("s"))
        {
            transform.position -= this.transform.forward * moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey("d"))
        {
            transform.position += this.transform.right * moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey("a"))
        {
            transform.position -= this.transform.right * moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey("space"))
        {
            transform.position += this.transform.up * moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey("c"))
        {
            transform.position -= this.transform.up * moveSpeed * Time.deltaTime;
        }
    }
}
