﻿using UnityEngine;
using UnityEngine.UI;

public class FlyThrough : MonoBehaviour {

    public float lookSpeed = 15.0f;
    public float moveSpeed = 15.0f;
    public float rotationX = 0.0f;
    public float rotationY = 0.0f;

    public Text modeText;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update() {
        // only rotate and move camera when cursor is locked
        if (!Cursor.visible) {
            RotateCamera();
            MoveCamera();
        }

        // toggle lock cursor
        if (Input.GetKeyDown(KeyCode.Escape)) {
            ToggleLockCursor();
        }
    }

    void RotateCamera() {
        // rotation
        rotationX += Input.GetAxis("Mouse X") * lookSpeed;
        rotationY += Input.GetAxis("Mouse Y") * lookSpeed;
        rotationY = Mathf.Clamp(rotationY, -90, 90);
        transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
        transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);
    }

    void MoveCamera() {
        // horizontal movement
        transform.position += transform.forward * moveSpeed * Input.GetAxis("Vertical");
        transform.position += transform.right * moveSpeed * Input.GetAxis("Horizontal");

        // vertical movement
        if (Input.GetKey(KeyCode.LeftShift)) {
            transform.position += Vector3.down * moveSpeed * 1f;
        }

        if (Input.GetKey(KeyCode.Space)) {
            transform.position += Vector3.up * moveSpeed * 1f;
        }
    }

    void ToggleLockCursor() {
        if (Cursor.visible) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        } else {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
