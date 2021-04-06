using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;
    private Rigidbody2D rigidBody;
    private Vector3 change;
    private Animator animator;
    private float counterToSavePosition;

    private static int timeToSavePositionInSeconds = 3;
    private Transform transform;

    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();

        LoadInitialPosition();
    }

    void LoadInitialPosition()
    {
        var savedPosition = new Vector3(
            PlayerPrefs.GetFloat(PlayerPrefKeys.LatestPositionX),
            PlayerPrefs.GetFloat(PlayerPrefKeys.LatestPositionY),
            transform.position.z
        );
        transform.position = savedPosition;
        animator.SetFloat("moveX", savedPosition.x);
        animator.SetFloat("moveY", savedPosition.y);
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        UpdateAnimationAndMove();
        SaveLatestPosition();
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    void MoveCharacter()
    {
        change.Normalize();
        rigidBody.MovePosition(
            transform.position + change * speed * Time.deltaTime
        );
    }

    void SaveLatestPosition()
    {
        //PlayerPrefKeys
        counterToSavePosition += Time.deltaTime;
        if (counterToSavePosition >= timeToSavePositionInSeconds)
        {
            counterToSavePosition = counterToSavePosition % timeToSavePositionInSeconds;
            PlayerPrefs.SetFloat(PlayerPrefKeys.LatestPositionX, transform.position.x);
            PlayerPrefs.SetFloat(PlayerPrefKeys.LatestPositionY, transform.position.y);
        }
    }
}
