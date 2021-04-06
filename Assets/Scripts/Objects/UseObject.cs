using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseObject : MonoBehaviour
{
    public Sprite spriteMouseOut;
    public Sprite spriteMouseOver;
    public GameObject screenToShow;

    private bool playerInRange = false;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ShowAttachedScreen();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && spriteMouseOver != null)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = spriteMouseOver;
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && spriteMouseOut != null)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = spriteMouseOut;
            playerInRange = false;
        }
    }

    private void OnMouseDown()
    {
        ShowAttachedScreen();
    }

    private void ShowAttachedScreen()
    {
        if (playerInRange && screenToShow != null)
        {
            screenToShow.SetActive(true);
        }
    }
}
