using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 2.5f;
    public float drag = 1.5f;

    private Transform character;
    private Vector2 mouseDirection;
    private Vector2 smoothing;
    private Vector2 result;

    private void Awake()
    {
        character = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
    }

    // Update is called once per frame
    void Update()
    {
        mouseDirection = new Vector2(Input.GetAxisRaw("Mouse X") * sensitivity, Input.GetAxisRaw("Mouse Y") * sensitivity);
        smoothing = Vector2.Lerp(smoothing, mouseDirection, 1 / drag);
        result += smoothing;
        result.y = Mathf.Clamp(result.y, -80, 80);

        transform.localRotation = Quaternion.AngleAxis(-result.y, Vector3.right);
        character.rotation = Quaternion.AngleAxis(result.x, character.up);

    }
}
