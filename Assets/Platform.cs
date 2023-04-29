using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    //General info
    private bool isMovingPlatform;

    [Header("Moving Platform")]
    [Tooltip("Only 2 Locations are supported for now")]
    [SerializeField] private List<Vector3> platformLocations;
    [Range(0.01f, 3)] [SerializeField] private float moveSpeed;
    private float movingPlatformTimer;

    [Header("Rotating Platform")]
    [SerializeField] private float rotationSpeed;

    [Header("Breaking Platform")]
    [SerializeField] private bool doesBreak;
    [SerializeField] private float breakTime;

    [Header("Trap Platform")]
    [SerializeField] private bool isTrap;
    [SerializeField] private GameObject spikes;
    [SerializeField] private bool isReactionTrap;
    [SerializeField] private float trapSetOffTime;
    [SerializeField] private float trapDamageTime;
    [SerializeField] private float damageCooldown;
    private bool canDamagePlayer = false;
    private bool spikeCoroutineStarted = false;
    private bool isCoolingDown;

    [Header("Bouncy Platform (Need to turn Main Collider to Trigger for this)")]
    [Tooltip("Turn main Box Collider to Trigger for this")]
    [SerializeField] private bool isBouncyPlatform;

    // Start is called before the first frame update
    void Start()
    {
        if (platformLocations.Count == 1 || platformLocations.Count > 2)
        {
            Debug.Log("You need either 0 or 2 Platform Locations, 0 if the Platform doesn't move");
        }

        //See if we need to parent the player to the Platform
        if (platformLocations.Count > 0)
        {
            isMovingPlatform = true;
        }
        if (rotationSpeed > 0)
        {
            isMovingPlatform = true;
        }

        spikes.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (platformLocations.Count == 2)
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
        Vector3 newPlatformPosition = Vector3.Lerp(platformLocations[0], platformLocations[1], period);
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
        Destroy(gameObject);
    }

    private IEnumerator ShootUpSpikes()
    {
        spikeCoroutineStarted = true;
        yield return new WaitForSeconds(trapSetOffTime);
        canDamagePlayer = true;
        spikes.SetActive(true);
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
        float launchForce = Mathf.Sqrt(-2f * player.GravityValue * lastGroundHeight);
        player._movement.y = launchForce;
        Debug.Log("Launched Player");
    }
}