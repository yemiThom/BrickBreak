using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneBehaviour : MonoBehaviour
{
    void OnTriggerEnter(Collider col){
        if(col.gameObject.name.Contains("Ball")){
            Destroy(col.gameObject);
        }

        GameManager.Instance.LoseLife();
    }
}
