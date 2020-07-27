using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-999)]
public class Player : MonoBehaviour
{
    private StateBase actualState;

    private Rigidbody2D rb;
    public Rigidbody2D Rb { get => rb; }

    private InputManager input;
    public InputManager Input { get => input; set => input = value; }

    private GameManager gameManager;

    //Stats
    [HideInInspector] public Vector2 lastDirection = Vector2.up;

    [SerializeField] private FloatSO speed;
    public FloatSO Speed { get => speed; }

    private float actualSpeed;
    private bool slowMode;

    [SerializeField] private IntSO maxErrors;

    [SerializeField] private IntSO day;

    [SerializeField] private IntSO coins;
    public IntSO Coins { get => coins; set => coins = value; }

    [SerializeField] private GameObject kidUI;
    [SerializeField] private TextMeshProUGUI textKidUI;

    private int carryingKidsNumber = 0;

    private int levelTotalKids = 0;

    [SerializeField] private IntSO actualKids;

    //Minigames
    [SerializeField] private IntSO combinationsNumber;
    public IntSO CombinationsNumber { get => combinationsNumber; set => combinationsNumber = value; }

    private CombinationsUI combinationsUI;
    public CombinationsUI CombinationsUI { get => combinationsUI; set => combinationsUI = value; }

    //Obstacles
    [SerializeField] LayerMask objectLayerMask;
    [SerializeField] LayerMask basketLayerMask;

    //Basket
    private GameObject basket;

    private bool holdingBasket;

    //Animations
    private Animator anim;
    public Animator Anim { get => anim; set => anim = value; }

    public RuntimeAnimatorController animControllerNoBasket;
    public AnimatorOverrideController animControllerBasket;

    [HideInInspector] public Transform infiniteDown;

    SpriteRenderer spriteRenderer;
    public SpriteRenderer SpriteRenderer { get => spriteRenderer; set => spriteRenderer = value; }
    public IntSO MaxErrors { get => maxErrors; set => maxErrors = value; }

    //Sounds
    [HideInInspector] public AudioSource audioSource;
    [SerializeField] private List<AudioClip> sonidoCogerKidsList;
    [SerializeField] private AudioClip[] footSteps;

    public AudioClip failMinigame;
    public AudioClip inputMinigame;
    public AudioClip takeBasket;
    public AudioClip dropBasket;
    public AudioClip takeCoin;
    public AudioClip insertCoin;
    public AudioClip destructObject;

    public AudioSource mainSongSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        infiniteDown = FindObjectOfType<InfiniteDown>().transform;
        anim = GetComponent<Animator>();
        input = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody2D>();
        combinationsUI = FindObjectOfType<CombinationsUI>();
        gameManager = FindObjectOfType<GameManager>();
        basket = FindObjectOfType<BasketController>().gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Main" && (day.Value == 0))
        {
            input.enabled = false;
            StartCoroutine(NoInput());
        } else
        {
            anim.runtimeAnimatorController = animControllerNoBasket;
        }

        ConfigSpeed();
        holdingBasket = false;
        ChangeState(new StateIdle(this));
    }

    private IEnumerator NoInput()
    {
        yield return new WaitForSeconds(10f);

        anim.runtimeAnimatorController = animControllerNoBasket;
        input.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        actualState.Tick();
    }

    private void FixedUpdate()
    {
        actualState.FixedTick();
    }

    public void ChangeState(StateBase state)
    {
        if (actualState != null)
            actualState.OnStateExit();

        actualState = state;

        if (actualState != null)
            actualState.OnStateEnter();
    }

    public void Move()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = footSteps[UnityEngine.Random.Range(0, footSteps.Length)];
            audioSource.PlayOneShot(footSteps[UnityEngine.Random.Range(0, footSteps.Length)]);
        }

        Vector2 velocity = new Vector2(input.horizontal, input.vertical).normalized * actualSpeed;
        rb.velocity = velocity;
    }

    public void DestructObject()
    {
        audioSource.PlayOneShot(destructObject);
        DestructibleObject dObject = GetDestructibleObject();
        if (dObject)
            dObject.DestroyObject();
    }

    public void TakeKid()
    {
        if (TouchingKid())
        {
            int sonidoAleatorio = UnityEngine.Random.Range(0, sonidoCogerKidsList.Count);
            audioSource.PlayOneShot(sonidoCogerKidsList[sonidoAleatorio]);
            carryingKidsNumber++;
            Destroy(GetKid().gameObject);
            kidUI.SetActive(true);
            textKidUI.text = carryingKidsNumber.ToString();
        }
    }

    public void StopMovement()
    {
        rb.velocity = Vector2.zero;

        if (infiniteDown)
            transform.SetParent(infiniteDown);
    }

    public bool CheckMovement()
    {
        if (input.horizontal != 0 || input.vertical != 0)
            return true;

        return false;
    }

    public void CheckBasket()
    {
        if (input.interact)
        {
            if (holdingBasket)
            {
                if (infiniteDown)
                    basket.transform.SetParent(infiniteDown.transform);
                else
                    basket.transform.SetParent(null);
                holdingBasket = false;

                anim.runtimeAnimatorController = animControllerNoBasket;

                basket.SetActive(true);
                audioSource.PlayOneShot(dropBasket);
            }
            else if (TouchingBasket())
            {
                if (!gameManager.GameStarted)
                {
                    gameManager.StartGame();
                    infiniteDown.GetComponent<InfiniteDown>().CanMove = true;
                    mainSongSource.Play();
                }

                basket.transform.SetParent(transform); //The parent is again the player
                basket.transform.localPosition = new Vector3(0, 0.75f, 0);
                holdingBasket = true;
                basket.SetActive(false);

                if (carryingKidsNumber > 0)
                {
                    levelTotalKids += carryingKidsNumber;
                    carryingKidsNumber = 0;
                    kidUI.SetActive(false);
                }

                anim.runtimeAnimatorController = animControllerBasket;
                audioSource.PlayOneShot(takeBasket);

            }

            ConfigSpeed();
        }
    }

    private void ConfigSpeed()
    {
        if (holdingBasket && !slowMode)
        {
            actualSpeed = speed.Value * 0.8f;

        }
        else if (slowMode)
        {
            actualSpeed = speed.Value * 0.5f;
        }
        else
        {
            actualSpeed = speed.Value;
        }
    }

    public bool CheckKid()
    {
        if (input.interact)
        {
            if (TouchingKid() && !holdingBasket)
                return true;
        }

        return false;
    }

    public bool CheckDestructibleObject()
    {
        if (input.interact)
        {
            if (TouchingDestructibleObject() && !holdingBasket)
                return true;
        }

        return false;
    }

    public bool CheckUpgradeMachine()
    {
        if (input.interact)
        {
            if (TouchingUpgradeMachine() && !holdingBasket)
                return true;
        }

        return false;
    }

    public bool CheckClown()
    {
        if (input.interact)
        {
            if (TouchingClown() && !holdingBasket)
                return true;
        }

        return false;
    }

    public bool CheckOpenTutorial()
    {
        if (input.interact)
        {
            if (TouchingTutorial() && !holdingBasket)
                return true;
        }

        return false;
    }

    public OpenTutorial GetTutorial()
    {
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, Vector2.up, transform.localScale.y + 1f, objectLayerMask);

        if (raycast)
        {
            OpenTutorial tutorial = raycast.transform.gameObject.GetComponent<OpenTutorial>();
            if (tutorial)
                return tutorial;
        }

        return null;
    }

    //Raycasts
    private bool TouchingBasket()
    {
        Collider2D raycast = Physics2D.OverlapCircle(transform.position, 1.1f, basketLayerMask);
        //RaycastHit2D raycast = Physics2D.Raycast(transform.position, lastDirection, transform.localScale.y + 1f, objectLayerMask);

        if (raycast)
        {
            if (raycast.transform.gameObject.GetComponent<BasketController>())
                return true;
        }

        return false;
    }

    private bool TouchingClown()
    {
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, Vector2.up, transform.localScale.y + 1f, objectLayerMask);

        if (raycast)
        {
            if (raycast.transform.gameObject.GetComponent<ClownTalk>())
                return true;
        }

        return false;
    }

    private bool TouchingKid() //Sorry for that
    {
        Collider2D raycast = Physics2D.OverlapCircle((transform.position - Vector3.up * 0.25f), 0.85f, objectLayerMask);
        //RaycastHit2D raycast = Physics2D.Raycast(transform.position, lastDirection, transform.localScale.y, objectLayerMask);

        if (raycast)
        {
            if (raycast.transform.gameObject.GetComponent<KidController>())
                return true;
        }

        return false;
    }

    private bool TouchingUpgradeMachine()
    {
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, Vector2.up, transform.localScale.y, objectLayerMask);

        if (raycast)
        {
            if (raycast.transform.gameObject.GetComponent<UpgradeMachineController>())
                return true;
        }

        return false;
    }

    public bool TouchingDestructibleObject()
    {
        Collider2D raycast = Physics2D.OverlapCircle((transform.position - Vector3.up * 0.25f), 0.85f, objectLayerMask);
        //RaycastHit2D raycast = Physics2D.Raycast(transform.position, lastDirection, transform.localScale.y, objectLayerMask);

        if (raycast)
        {
            if (raycast.transform.gameObject.GetComponent<DestructibleObject>())
                return true;
        }

        return false;
    }

    public bool TouchingTutorial()
    {
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, Vector2.up, transform.localScale.y, objectLayerMask);
        //RaycastHit2D raycast = Physics2D.Raycast(transform.position, lastDirection, transform.localScale.y, objectLayerMask);

        if (raycast)
        {
            if (raycast.transform.gameObject.GetComponent<OpenTutorial>())
                return true;
        }

        return false;
    }

    public UpgradeMachineController GetUpgradeMachine()
    {
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, Vector2.up, transform.localScale.y, objectLayerMask);

        if (raycast)
        {
            UpgradeMachineController controller = raycast.transform.gameObject.GetComponent<UpgradeMachineController>();
            if (controller)
                return controller;
        }

        return null;
    }

    public ClownTalk GetClownTalk()
    {
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, Vector2.up, transform.localScale.y + 1f, objectLayerMask);

        if (raycast)
        {
            ClownTalk clown = raycast.transform.gameObject.GetComponent<ClownTalk>();
            if (clown)
                return clown;
        }

        return null;
    }

    public KidController GetKid()
    {
        Collider2D raycast = Physics2D.OverlapCircle((transform.position - Vector3.up * 0.25f), 0.85f, objectLayerMask);
        //RaycastHit2D raycast = Physics2D.Raycast(transform.position, lastDirection, transform.localScale.y, objectLayerMask);

        if (raycast)
        {
            KidController controller = raycast.transform.gameObject.GetComponent<KidController>();
            if (controller)
                return controller;
        }

        return null;
    }

    public DestructibleObject GetDestructibleObject()
    {
        Collider2D raycast = Physics2D.OverlapCircle((transform.position - Vector3.up * 0.25f), 0.85f, objectLayerMask);
        //RaycastHit2D raycast = Physics2D.Raycast(transform.position, lastDirection, transform.localScale.y, objectLayerMask);

        if (raycast)
        {
            DestructibleObject dObject = raycast.transform.gameObject.GetComponent<DestructibleObject>();
            if (dObject)
                return dObject;
        }

        return null;
    }

    public void Die()
    {
        ChangeState(new StateFinish(this));
        actualKids.Value += levelTotalKids;

        gameManager.Dead();
        input.enabled = false;
    }

    //Collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            audioSource.PlayOneShot(takeCoin);
            coins.Value++;
            Destroy(collision.gameObject);
            return;
        }

        if (collision.CompareTag("Final"))
        {
            gameManager.FinishGame();
            infiniteDown = null;
            ChangeState(new StateFinish(this));
            Die();
            return;
        }

        if (collision.CompareTag("Hole"))
        {
            transform.parent = null;
            ChangeState(new StateFalling(this, collision.GetComponent<Hole>().GetHolePoint()));
            return;
        }

        if (collision.CompareTag("Slow"))
        {
            slowMode = true;
            ConfigSpeed();
            return;
        }

        if (collision.CompareTag("Death"))
        {
            Die();
            return;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Slow"))
        {
            slowMode = false;
            ConfigSpeed();
        }
    }

}
