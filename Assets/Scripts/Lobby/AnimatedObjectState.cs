using UnityEngine;

public class AnimatedObjectState : MonoBehaviour
{
    [SerializeField] private string _animationName;
    [SerializeField] private Animator _animator;
    void Start() => _animator = GetComponent<Animator>();
    public void StartAnimation() => _animator.SetBool(_animationName, true);
    public void FinishAnimation() => _animator.SetBool(_animationName, false);
}
