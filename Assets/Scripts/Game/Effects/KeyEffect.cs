using UnityEngine;

public class KeyEffect : MonoBehaviour
{
    [SerializeField] GameObject KeyEffects;
    private void OnDisable()
    {
        Vector3 PositionEffect = new Vector3(transform.position.x, 0F, transform.position.z);
        Instantiate(KeyEffects, PositionEffect, Quaternion.identity);
    }
}
