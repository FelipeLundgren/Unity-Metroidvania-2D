using System;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    protected Animator animator;

    protected void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    protected abstract void Update();

}
