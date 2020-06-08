using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsBehavior : MonoBehaviour
{
    public Animator thisAnimator;

    public void PlayOpenOptionsMenu()
    {
        thisAnimator.Play("Options_Open_Anim");
    }
}
