using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipBehavior : MonoBehaviour
{
    public Transform MyTransform;
    public bool IsFlipping;
    public float TimeStarted;
    public float TimeTakenDuringLerp = .25f;
    public Quaternion StartRotation;
    public Quaternion GoalRotation;

    public void Flip()
    {
        IsFlipping = true;
        TimeStarted = Time.time;

        StartRotation = MyTransform.rotation;
        var eulerAngleRotation = MyTransform.eulerAngles + (180f * Vector3.up);
        GoalRotation = Quaternion.Euler(eulerAngleRotation);
    }

    private void OnDisable()
    {
        MyTransform.rotation = Quaternion.Euler(Vector3.zero);
        IsFlipping = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (IsFlipping)
        {
            float timeSinceStarted = Time.time - TimeStarted;
            float percentageComplete = timeSinceStarted / TimeTakenDuringLerp;
            MyTransform.rotation = Quaternion.Slerp(StartRotation, GoalRotation, percentageComplete);
            if (percentageComplete >= 1.0f)
            {
                MyTransform.rotation = Quaternion.Euler(Vector3.zero);
                IsFlipping = false;
            }
        }
    }
}
