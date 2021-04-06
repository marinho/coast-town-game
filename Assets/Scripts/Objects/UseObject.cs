using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseObject : MonoBehaviour
{
    public Material mouseOverOutlineMaterial;
    public GameObject screenToShow;
    public bool isActive = true;

    private bool playerInRange = false;
    private Material initialMaterial;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ShowAttachedScreen();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && mouseOverOutlineMaterial != null && isActive)
        {
            var spriteRenderer = GetSpriteRenderer();
            initialMaterial = spriteRenderer.material;
            spriteRenderer.material = mouseOverOutlineMaterial;
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isActive)
        {
            var spriteRenderer = GetSpriteRenderer();
            spriteRenderer.material = initialMaterial;
            playerInRange = false;
        }
    }

    private void OnMouseDown()
    {
        if (isActive)
        {
            ShowAttachedScreen();
        }
    }

    private void ShowAttachedScreen()
    {
        if (playerInRange && screenToShow != null && isActive)
        {
            screenToShow.SetActive(true);
        }
    }

    private SpriteRenderer GetSpriteRenderer()
    {
        return gameObject.GetComponent<SpriteRenderer>();
    }
}
