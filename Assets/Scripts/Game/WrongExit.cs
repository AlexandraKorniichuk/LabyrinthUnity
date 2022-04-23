using UnityEngine;

public class WrongExit : MonoBehaviour
{
    [SerializeField] GameObject Particles;
    private AudioSource audioSource;
    void Start() => audioSource = GetComponent<AudioSource>();
    public void ShowExitIsWrong()
    {
        Instantiate(Particles, new Vector3(transform.position.x, 1f, transform.position.z), Quaternion.identity);
        audioSource.Play();
    }
}
