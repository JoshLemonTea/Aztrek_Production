using UnityEngine;

public class Tribute : MonoBehaviour
{
    public string tributeType;

    private bool _collected = false;
    private AudioSource _audioSource;

    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_collected)
        {
            _collected = true;
            other.gameObject.GetComponent<TributeManager>().CollectTribute(tributeType);
            _audioSource.Play();
            Destroy(gameObject, 0.9f);
            //gameObject.SetActive(false);
        }
    }
}
