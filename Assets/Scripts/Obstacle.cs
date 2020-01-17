using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int lifeValue;
    public Collider playerCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other == playerCollider) {
            // update life
            FindObjectOfType<GameManager>().changeLife(lifeValue);
        }
    }
}
