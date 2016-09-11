using UnityEngine;
using System.Collections;

public class NPCmovement : MonoBehaviour {

    public Vector3 startMovement;
    public Rigidbody baseBody;

    public float randomMovementRadius = 2.0f;
    public float movementDuration = 1.0f;
    public float movementIntervalTime = 2.0f;

    void Start(){
        startMovement = baseBody.transform.position;

        StartCoroutine("KeepMovingRandom");
    }

    void MoveToRandomPosition(){
        StartCoroutine("TweenMovement", movementDuration);
    }

    IEnumerator TweenMovement(float moveDuration){

        float timer = moveDuration;
        float time = 0.0f;

        Vector2 randCirclePos = Random.insideUnitCircle;
        Vector3 newPosition = startMovement + new Vector3(randCirclePos.x, 0, randCirclePos.y);

        while (time < timer)
        {
            Vector3 lerpedPosition = Vector3.Lerp(baseBody.transform.position, newPosition, time/timer);

            baseBody.MovePosition(lerpedPosition);

            time += Time.deltaTime;
            yield return 0;
        }

        baseBody.MovePosition(newPosition);
    }

    IEnumerator KeepMovingRandom() {
        while (true)
        {
            MoveToRandomPosition();
            yield return new WaitForSeconds(movementIntervalTime);
        }
    }
}
