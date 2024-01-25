using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePickup : MonoBehaviour
{
    private float toTimeSpent, fromTimeSpent;
    public float toDuration, fromDuration;
    public float offset;
    private Vector3 rotateTo;
    public GameObject focusPoint;
    public GameObject awayPoint;

    void FixedUpdate()
    {
        rotateTo = new Vector3(focusPoint.GetComponentInParent<Transform>().eulerAngles.x,
                               focusPoint.GetComponentInParent<Transform>().eulerAngles.y + offset,
                               focusPoint.GetComponentInParent<Transform>().eulerAngles.z);

        gameObject.transform.eulerAngles = rotateTo;

        if (toTimeSpent < toDuration)
        {
            focusPoint.GetComponentInParent<MoveCamera>().isAnimationPlaying = true;

            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, focusPoint.transform.position, toTimeSpent / toDuration);
            toTimeSpent += Time.deltaTime;
        }
        else if(fromTimeSpent < fromDuration)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, awayPoint.transform.position, fromTimeSpent / fromDuration);
            fromTimeSpent += Time.deltaTime;
        }
        else
        {
            focusPoint.GetComponentInParent<MoveCamera>().isAnimationPlaying = false;

            Destroy(gameObject);
        }
    }
}
