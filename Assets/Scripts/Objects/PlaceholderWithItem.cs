using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceholderWithItem : MonoBehaviour
{
    public GameObject item;
    public bool placed = false;

    // Update is called once per frame
    void Update()
    {
        if (item != null)
        {
            item.SetActive(placed);
            item.GetComponent<SpriteRenderer>().sortingOrder = 1;
            GetComponent<UseObject>().isActive = !placed;
        }
    }
}
