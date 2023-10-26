using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Camera playerCamera;
    [SerializeField]
    private GameObject playerBody;
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private float mouseSense = 200;
    [SerializeField]

    private float speed = 100;
    private float m_mouseX = 0;
    private float m_mouseY = 0;
    private float m_inputX = 0;
    private float m_inputZ = 0;

    private Vector3 m_velocity;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        playerCamera.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        m_mouseX += Input.GetAxis("Mouse X") * mouseSense * Time.deltaTime;
        m_mouseY += Input.GetAxis("Mouse Y") * mouseSense * Time.deltaTime;

        playerCamera.transform.rotation = Quaternion.Euler(new Vector3(m_mouseY, m_mouseX, 0));
        playerBody.transform.rotation = Quaternion.Euler(new Vector3(0, m_mouseX, 0));

        m_inputX = Input.GetAxis("Horizontal");
        m_inputZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * m_inputX + transform.forward * m_inputZ;
        move.y = 0;

        controller.Move(move * speed * Time.deltaTime);
        m_velocity.y++;
        //transform.position -= move * speed * Time.deltaTime;
    }
}
