using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BricksBehaviour : MonoBehaviour
{
    public GameObject brickParticles;

    public BrickToughness brickToughness;
    public enum BrickToughness{
        One,
        Two,
        Three
    }

    int gotHit, maxHitPoints, scoreToGive;

    void CalculateHits(){
        switch (brickToughness){
            case BrickToughness.One:
                maxHitPoints = 1;
                scoreToGive = 100;
                break;
            case BrickToughness.Two:
                maxHitPoints = 2;
                scoreToGive = 150;
                break;
            case BrickToughness.Three:
                maxHitPoints = 3;
                scoreToGive = 250;
                break;
        }
    }

    void OnCollisionEnter(Collision other){
        gotHit++;

        CalculateHits();

        if(other.gameObject.name.Contains("Ball") && (gotHit == maxHitPoints)){
            Instantiate(brickParticles, transform.position, Quaternion.identity);
            GameManager.Instance.DestroyBrick(scoreToGive);
            Destroy(gameObject);
        }
    }
}
