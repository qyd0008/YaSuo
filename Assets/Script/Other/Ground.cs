using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public GameObject clickMove;
    void Start()
    {
        InputManager.Instance.OnMouseClicked += CreateClickMove;
    }

    private void CreateClickMove(Vector3 target)
    {
        target.y += 0.3f;
        GameObject move = Instantiate(clickMove,target,Quaternion.identity);
        Destroy(move,0.5f);
    }
}
