using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    public SpriteAnimationManager animationDictionary;
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
        return false;
    }

    private bool AnimateShake()
    {
        return false;
    }

    private bool AnimateQuickZoom()
    {
        return false;
    }

    private bool AnimateStepBack()
    {
        return false;
    }

    private bool AnimateLower()
    {
        return false;
    }
}
