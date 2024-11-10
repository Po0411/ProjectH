using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    // ĳ���� ī�޶� ����
    public Camera playerCamera;

    // �̵� �ӵ� ����
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;

    // ī�޶� ȸ�� ����
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    // �ɱ� ����
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 3f;

    private Vector3 moveDirection = Vector3.zero; // �÷��̾� �̵� ���� ����
    private float rotationX = 0; // ī�޶� X�� ȸ����
    private CharacterController characterController; // CharacterController ������Ʈ

    private bool canMove = true; // �̵� ���� ����

    void Start()
    {
        characterController = GetComponent<CharacterController>(); // CharacterController ������Ʈ ��������
        Cursor.lockState = CursorLockMode.Locked; // ���콺 Ŀ�� ���
        Cursor.visible = false; // ���콺 Ŀ�� �����
    }

    void Update()
    {
        // �̵� ���� ����
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // �޸��� ���� Ȯ��
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        // ���� �̵� �ӵ� ���
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;

        // �̵� ���� ������Ʈ
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        // ���� ó��
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // �߷� ����
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // �ɱ� ����
        if (Input.GetKey(KeyCode.R) && canMove)
        {
            characterController.height = crouchHeight; // ĳ���� ���� ����
            walkSpeed = crouchSpeed; // �ȱ� �ӵ� ����
            runSpeed = crouchSpeed;  // �޸��� �ӵ� ����
        }
        else
        {
            characterController.height = defaultHeight; // �⺻ ���̷� ����
            walkSpeed = 6f; // �⺻ �ȱ� �ӵ�
            runSpeed = 12f; // �⺻ �޸��� �ӵ�
        }

        // ĳ���� �̵�
        characterController.Move(moveDirection * Time.deltaTime);

        // ī�޶� ȸ��
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}
