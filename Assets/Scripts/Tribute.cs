using UnityEngine;

public class Tribute : MonoBehaviour
{
    public string tributeType;

    private AudioSource _audioSource;

    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<TributeManager>().CollectTribute(tributeType);
            _audioSource.Play();
            gameObject.SetActive(false);
        }
    }
}
