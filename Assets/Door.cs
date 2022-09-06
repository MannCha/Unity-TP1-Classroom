using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool _trigger = false;

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log($"I am touching {other.name}!");
        _trigger = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        Debug.Log($"{other.name} has left the trigger.");
        _trigger = false;
    }

    private void Update() {
        if (_trigger) {
            if (Inputs.IsMovingUp()) {
                Debug.Log($"Entering door!");
            }
        }
    }
}
