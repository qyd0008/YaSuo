using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q3WindTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider collider) 
    {
        if (collider.gameObject.tag == "Enemy")
        {
            Debug.Log("collider.gameObject.name : " + collider.gameObject.name);
            GameObject zombie = collider.gameObject;
            ZombieController ctrl = zombie.GetComponent<ZombieController>();
            Debug.Log("击飞 " + zombie.name);
            if (!ctrl.IsFlying())
            {
                ctrl.Flying();
            }
        }
    }
}
