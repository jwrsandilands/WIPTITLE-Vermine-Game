using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    public Animator animator;

    public bool PerformAnimation(SpriteAnimationEnum animation) =>
        animation switch
        {
            SpriteAnimationEnum.Hop => AnimateHop(),
            SpriteAnimationEnum.Wobble => AnimateWobble(),
            SpriteAnimationEnum.Shake => AnimateShake(),
            SpriteAnimationEnum.QuickZoom => AnimateQuickZoom(),
            SpriteAnimationEnum.StepBack => AnimateStepBack(),
            SpriteAnimationEnum.Lower => AnimateLower(),
            _ => false,
        };

    private bool AnimateHop()
    {
        animator.Play("Hop");
        return true;
    }

    private bool AnimateWobble()
    {
        animator.Play("Wobble");
        return false;
    }

    private bool AnimateShake()
    {
        animator.Play("Shake");
        return false;
    }

    private bool AnimateQuickZoom()
    {
        animator.Play("QuickZoom");
        return false;
    }

    private bool AnimateStepBack()
    {
        animator.Play("StepBack");
        return false;
    }

    private bool AnimateLower()
    {
        animator.Play("Lower");
        return false;
    }
}
