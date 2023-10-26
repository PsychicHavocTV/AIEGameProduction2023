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
        //transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        playerCamera.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        m_mouseX += Input.GetAxisRaw("Mouse X") * mouseSense * Time.deltaTime;
        m_mouseY -= Input.GetAxisRaw("Mouse Y") * mouseSense * Time.deltaTime;

        if (m_mouseX >= 360.0f)
            m_mouseX -= 360.0f;
        else if (m_mouseX < 0.0f)
            m_mouseX += 360.0f;

        m_mouseY = Mathf.Clamp(m_mouseY, -89.0f, 89.0f);

        playerCamera.transform.rotation = Quaternion.Euler(new Vector3(m_mouseY, m_mouseX, 0));

        m_inputX = Input.GetAxisRaw("Horizontal");
        m_inputZ = Input.GetAxisRaw("Vertical");

        Vector3 move = Vector3.zero;
        move = m_inputX * playerCamera.transform.right + m_inputZ * playerCamera.transform.forward;
        move.y = 0;

        controller.Move(move.normalized * speed * Time.deltaTime);

        m_velocity.y++;
    }
}
