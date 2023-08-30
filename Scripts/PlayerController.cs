using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    [Header("Audio Clips")]
    [SerializeField]

    private RotateToMouse rotateToMouse;
    private MovementCharacterController movement;

    private PlayerAnimatorController animator;
    private AudioSource audioSource;
    private WeaponAssaultRifle weapon;


    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        rotateToMouse = GetComponent<RotateToMouse>();
        movement = GetComponent<MovementCharacterController>();
        animator = GetComponent<PlayerAnimatorController>();
        audioSource = GetComponent<AudioSource>();
        weapon = GetComponentInChildren<WeaponAssaultRifle>();
    }

    private void Update()
    {
        UpdateRotate();
        UpdateMove();
        UpdateWeaponAction();
    }

    private void UpdateRotate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        rotateToMouse.UpdateRotate(mouseX, mouseY);
    }

    private void UpdateMove()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        if (x!=0 || z!=0)
        {
            animator.MoveSpeed = 1;

            if (audioSource.isPlaying == false)
            {
                audioSource.loop = true;
                audioSource.Play();
            }
        }
        else
        {
            animator.MoveSpeed = 0;

            if (audioSource.isPlaying == true) 
            {
                audioSource.Stop();
            }
        }
            
        movement.MoveTo(new Vector3(x,0,z));

    }

    private void UpdateWeaponAction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            weapon.StartWeaponAction();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            weapon.StopWeaponAction();
        }
    }
}

