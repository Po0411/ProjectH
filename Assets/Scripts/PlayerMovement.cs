using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    // 캐릭터 카메라 설정
    public Camera playerCamera;

    // 이동 속도 설정
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;

    // 카메라 회전 설정
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    // 앉기 설정
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 3f;

    private Vector3 moveDirection = Vector3.zero; // 플레이어 이동 방향 벡터
    private float rotationX = 0; // 카메라 X축 회전값
    private CharacterController characterController; // CharacterController 컴포넌트

    private bool canMove = true; // 이동 가능 여부

    void Start()
    {
        characterController = GetComponent<CharacterController>(); // CharacterController 컴포넌트 가져오기
        Cursor.lockState = CursorLockMode.Locked; // 마우스 커서 잠금
        Cursor.visible = false; // 마우스 커서 숨기기
    }

    void Update()
    {
        // 이동 방향 설정
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // 달리기 상태 확인
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        // 현재 이동 속도 계산
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;

        // 이동 방향 업데이트
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        // 점프 처리
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // 중력 적용
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // 앉기 설정
        if (Input.GetKey(KeyCode.R) && canMove)
        {
            characterController.height = crouchHeight; // 캐릭터 높이 변경
            walkSpeed = crouchSpeed; // 걷기 속도 조정
            runSpeed = crouchSpeed;  // 달리기 속도 조정
        }
        else
        {
            characterController.height = defaultHeight; // 기본 높이로 설정
            walkSpeed = 6f; // 기본 걷기 속도
            runSpeed = 12f; // 기본 달리기 속도
        }

        // 캐릭터 이동
        characterController.Move(moveDirection * Time.deltaTime);

        // 카메라 회전
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}
