using UnityEngine;

public class ButtonState : MonoBehaviour
{
    public Animator _animator;
    void Start() => _animator = GetComponent<Animator>();
    public void StartAnimation() => _animator.SetBool("IsPointerEnter", true);
    public void FinishAnmation() => _animator.SetBool("IsPointerEnter", false);
}
