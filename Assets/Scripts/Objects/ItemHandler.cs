using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemKind
{
    furniture,
    food
}

public class ItemHandler : MonoBehaviour
{
    public string name;
    public int price;
    public string description;
    public ItemKind kind;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
