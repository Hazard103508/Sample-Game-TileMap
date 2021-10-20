using System;
using UnityEngine;
using UnityEngine.Events;

public class Gem : Tile
{
    public UnityEvent Collected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collected.Invoke();
        Destroy(this.gameObject);
    }
}
