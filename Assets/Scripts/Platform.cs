using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    //General info
    private bool isMovingPlatform;

    [Header("Moving Platform")]
    [SerializeField] private bool doesMove;
    [SerializeField] private GameObject startPosition;
    [SerializeField] private GameObject endPosition;
    private Vector3 startPos;
    private Vector3 endPos;
    [Range(0.01f, 3)] [SerializeField] private float moveSpeed;
    private float movingPlatformTimer;

    [Header("Rotating Platform")]
    [SerializeField] private float rotationSpeed;

    [Header("Breaking Platform")]
    [SerializeField] private bool doesBreak;
    [SerializeField] private float breakTime;
    [SerializeField] private float respawnTime;
    [SerializeField] private AudioClip _breakSound;

    [Header("Trap Platform")]
    [SerializeField] private bool isTrap;
    [SerializeField] private int spikeDamageAmount;
    //Why is this not in the health script?
    [SerializeField] private int invulnerableTime;
    [SerializeField] private GameObject spikes;
    [SerializeField] private bool isReactionTrap;
    [SerializeField] private float trapSetOffTime;
    [SerializeField] private float trapDamageTime;
    [SerializeField] private float damageCooldown;
    [SerializeField] private AudioClip _trapTriggerSound;
    private bool canDamagePlayer = false;
    private bool spikeCoroutineStarted = false;
    private bool isCoolingDown;
    
    private AudioSource _audioSource;

    [Header("Bouncy Platform (Need to turn Main Collider to Trigger for this)")]
    [Tooltip("Turn main Box Collider to Trigger for this")]
    [SerializeField] private bool isBouncyPlatform;

    [SerializeField]
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        //Make the movespeed variable more intuitive
        moveSpeed = moveSpeed / 5;

        //See if we need to parent the player to the Platform
        if (doesMove)
        {
            isMovingPlatform = true;
        }
        if (rotationSpeed > 0)
        {
            isMovingPlatform = true;
        }

        startPos = startPosition.transform.position;
        endPos = endPosition.transform.position;

        _audioSource = GetComponent<AudioSource>();

        spikes.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (doesMove)
        {
            MovePlatform();
        }
        if (rotationSpeed > 0)
        {
            RotatePlatform();
        }

        if (canDamagePlayer && !isCoolingDown)
        {
            DamagePlayer();
        }

        if (isTrap && !isReactionTrap && !spikeCoroutineStarted)
        {
            StartCoroutine(ShootUpSpikes());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        bool playerInTrigger = other.gameObject.GetComponent<Player>() != null;
        if (isMovingPlatform && playerInTrigger)
        {
            other.transform.parent = gameObject.transform;
        }

        if (doesBreak && playerInTrigger)
        {
            StartCoroutine(BreakPlatform());

            _animator.SetBool("IsBreaking", true);
        }

        if (isTrap && isReactionTrap && playerInTrigger)
        {
            StartCoroutine(ShootUpSpikes());
        }

        if (isBouncyPlatform && playerInTrigger)
        {
            if (other.gameObject.GetComponent<CharacterController>().isGrounded == false)
            {
                Player player = other.gameObject.GetComponent<Player>();
                float playerLastGroundHeight = player.lastGroundHeight;
                LaunchPlayer(player, playerLastGroundHeight);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isMovingPlatform)
        {
            if (other.gameObject.GetComponent<Player>() != null)
            {
                other.transform.parent = null;
            }
        }
    }

    private void MovePlatform()
    {
        float period = CalculateLerpPeriod();
        Vector3 newPlatformPosition = Vector3.Lerp(startPos, endPos, period);
        transform.position = newPlatformPosition;
    }

    private float CalculateLerpPeriod()
    {
        float period = (Mathf.Sin((movingPlatformTimer + 0.75f) * (2 * Mathf.PI)) + 1) / 2;

        movingPlatformTimer += Time.deltaTime * moveSpeed;
        if (movingPlatformTimer > 1)
            movingPlatformTimer -= 1;

        return period;
    }

    private void RotatePlatform()
    {
        Vector3 newEulers = transform.eulerAngles;
        newEulers.y += rotationSpeed * Time.deltaTime;
        transform.eulerAngles = newEulers;
    }

    private IEnumerator BreakPlatform()
    {
        yield return new WaitForSeconds(breakTime);
        //Unparent the player if they are parented
        if (gameObject.transform.GetChild(0).GetComponent<Player>() != null)
        {
            gameObject.transform.GetChild(0).transform.parent = null;
        }
        SoundPitchRandomizer.PlaySoundWithRandomPitch(_audioSource, _breakSound, 0.6f, 0.1f);

        _animator.SetBool("IsBreaking", false);

        ChangeVisibility(false);
        yield return new WaitForSeconds(respawnTime);
        ChangeVisibility(true);
    }

    private IEnumerator ShootUpSpikes()
    {
        spikeCoroutineStarted = true;
        yield return new WaitForSeconds(trapSetOffTime);
        canDamagePlayer = true;
        spikes.SetActive(true);
        SoundPitchRandomizer.PlaySoundWithRandomPitch(_audioSource, _trapTriggerSound, 0.55f, 0.15f);
        yield return new WaitForSeconds(trapDamageTime);
        canDamagePlayer = false;
        spikeCoroutineStarted = false;
        spikes.SetActive(false);
    }

    private void DamagePlayer()
    {
        Collider[] hits = Physics.OverlapBox(transform.position + new Vector3(0, 0.5f, 0), transform.localScale / 2, Quaternion.Euler(transform.eulerAngles));
        //Damaged Player bool makes sure the player only gets damaged once
        bool damagedPlayer = false;
        foreach (Collider hit in hits)
        {
            if (hit.GetComponent<Player>() != null && damagedPlayer == false)
            {
                //Take Damage here
                Debug.Log("Ouch!");
                if (hit.GetComponent<Health>().CanTakeDamage && hit.GetComponent<Health>().CurrentHealth > 1)
                {
                    hit.GetComponent<Player>().JumpMovement(false);
                }
                hit.GetComponent<Health>().TakeDamage(spikeDamageAmount);
                hit.GetComponent<Health>().MakeInvulnerable(invulnerableTime);
                damagedPlayer = true;
                StartCoroutine(DamagePlayerCooldown());
            }
        }
    }

    private IEnumerator DamagePlayerCooldown()
    {
        isCoolingDown = true;
        yield return new WaitForSeconds(damageCooldown);
        isCoolingDown = false;
    }

    private void LaunchPlayer(Player player, float lastGroundHeight)
    {
        float launchForce = Mathf.Sqrt(-2f * player.GravityValue * lastGroundHeight) / 3f;
        player.Movement.y = launchForce;
        Debug.Log("Launched Player");
    }

    private void ChangeVisibility(bool state)
    {
        if (GetComponentInChildren<Player>() != null)
        {
            GetComponentInChildren<Player>().gameObject.transform.parent = null;
        }

        BoxCollider[] colliders = GetComponents<BoxCollider>();
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = state;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(state);
        }

        if (canDamagePlayer == false)
        {
            spikes.SetActive(false);
        }
    }
}
