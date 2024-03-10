using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UnitAnimation : MonoBehaviour
{
    protected Animator _animator;

    private float _animationTime;

    private const float DELAY_AFTER_ANIMATION = 0.0f;

    private const string ATTACK = "Attack";
    private const string ADD_SHIELD = "AddShield";
    private const string TAKE_DAMAGE = "TakeDamage";
    private const string DEATH = "Death";
    private const string START_OFFSET = "StartOffset";

    public void Init()
    {
        _animator = GetComponent<Animator>();
        _animator.SetFloat(START_OFFSET, Random.Range(0, 1f));
    }

    public Coroutine Attack()
    {
        return StartCoroutine(PlayAnimation(ATTACK));
    }

    public Coroutine AddShield()
    {
        return StartCoroutine(PlayAnimation(ADD_SHIELD));
    }

    public Coroutine TakeDamage()
    {
        return StartCoroutine(PlayAnimation(TAKE_DAMAGE));
    }

    public Coroutine Death()
    {
        return StartCoroutine(PlayAnimation(DEATH));
    }

    protected IEnumerator PlayAnimation(string trigger)
    {
        _animator.SetTrigger(trigger);
        _animationTime = _animator.GetCurrentAnimatorStateInfo(0).length + DELAY_AFTER_ANIMATION;

        yield return new WaitForSeconds(_animationTime);
    }
}
