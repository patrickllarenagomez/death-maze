using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterScript : MonoBehaviour
{

    // Use this for initialization
    Animation animation;
    string[] animationName = { "WK_heavy_infantry_06_combat_walk",
                                "WK_heavy_infantry_04_charge",
                                "WK_heavy_infantry_05_combat_idle" };
    GameObject characterObject;
    Rigidbody characterRigidbody;

    float breath = 1f;
    float health = 1f;
    float RotationSpeed = 240.0f;
    float _runningSpeed = 2.0f;
    float time = 0;
    float timeRecoverBreath = 0;
    float timeInTrap = 0f;

    float needleEnterDamage = .03f;
    float needleStayDamage = .01f;
    float cutterEnterDamage = .04f;
    float cutterStayDamage = .02f;
    float sawEnterDamage = .04f;
    float sawStayDamage = .02f;
    float spearEnterDamage = .05f;
    float bladeEnterDamage = .04f;
    float axeEnterDamage = .10f;
    float greatAxeEnterDamage = .33f;

    public GameObject breathBar;
    public GameObject healthBar;
    public GameObject responseScreen;
    public GameObject escapeZone;


    private breathScript BreathScript;
    private healthScript HealthScript;
    private TypeOutScript responseScript;

    public Transform escapeTransform;

    public bool isCleared = false;

    AudioSource[] audios;
    AudioSource walking;
    AudioSource running;
    AudioSource damagetaken;

    bool audioOneIsPlaying = false;
    bool audioTwoIsPlaying = false;
    bool audioThreeIsPlaying = false;
    bool isCharacterDead = false;

    Renderer rend;

    void Start ()
    {
        animation = GetComponent<Animation>();
        characterObject = GameObject.Find("Knight");
        characterRigidbody = GetComponent<Rigidbody>();

        BreathScript = breathBar.GetComponent<breathScript>();
        HealthScript = healthBar.GetComponent<healthScript>();
        responseScript = responseScreen.GetComponent<TypeOutScript>();
        audios = characterObject.GetComponents<AudioSource>();
        rend = characterObject.GetComponent<Renderer>();
        escapeTransform = escapeZone.GetComponent<Transform>();
        walking = audios[0];
        running = audios[1];
        damagetaken = audios[2];

    }

    // Update is called once per frame
    void Update ()
    {
        if (characterRigidbody.IsSleeping())
            characterRigidbody.WakeUp();
        Movement();
    }

    void Movement()
    {
        // Get Input for axis
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Calculate the forward vector
        Vector3 camForward_Dir = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 move = v * camForward_Dir + h * Camera.main.transform.right;

        if (move.magnitude > 1f) move.Normalize();

        // Calculate the rotation for the player
        move = transform.InverseTransformDirection(move);

        // Get Euler angles
        float turnAmount = Mathf.Atan2(move.x, move.z);

        transform.Rotate(0, turnAmount * RotationSpeed * Time.deltaTime, 0);
        //walk forward
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.Space))
        {

            if (Input.GetKeyDown(KeyCode.W))
                walkingSFX();

            RecoverBreath();
            animation.Play(animationName[0]);
            transform.Translate(Vector3.forward * Time.deltaTime);

            //walk sidewards to the right
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * Time.deltaTime);
            }
            //walk sidewards to the left
            else if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * Time.deltaTime);
            }
        }
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A)
            || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.Space))
        {
            audioOneIsPlaying = false;
            walking.Pause();
            walking.loop = false;

            audioTwoIsPlaying = false;
            running.Pause();
            running.loop = false;
        }

        // backwards
        else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.Space))
        {
            if (Input.GetKeyDown(KeyCode.S))
                walkingSFX();

            RecoverBreath();
            animation.Play(animationName[0]);
            transform.Translate(-Vector3.back * Time.deltaTime);
            //back sidewards to the right
            if (Input.GetKey(KeyCode.D))
            {
                animation.Play(animationName[0]);
                transform.Translate(Vector3.right * Time.deltaTime);
            }
            //back sidewards to the left
            else if (Input.GetKey(KeyCode.A))
            {
                animation.Play(animationName[0]);
                transform.Translate(Vector3.left * Time.deltaTime);
            }
        }
        //left
        else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.Space))
        {
            if (Input.GetKeyDown(KeyCode.A))
                walkingSFX();

            RecoverBreath();
            animation.Play(animationName[0]);
            transform.Translate(Vector3.left * Time.deltaTime);
        }
        //right
        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.Space))
        {
            if (Input.GetKeyDown(KeyCode.D))
                walkingSFX();

            RecoverBreath();
            animation.Play(animationName[0]);
            transform.Translate(Vector3.right * Time.deltaTime);
        }

        //run
        else if (Input.GetKey(KeyCode.Space) && breath > .05)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                runningSFX();

            time += Time.deltaTime;
            if (time > .3)
            {
                breath -= .05f;
                time = 0;

                BreathScript.updateBreathBar(breath);
            }

            animation.Play(animationName[1]);
            transform.Translate(Vector3.forward * _runningSpeed * Time.deltaTime);

            // backwards
            if (Input.GetKey(KeyCode.S))
            {
                animation.Play(animationName[1]);
                transform.Translate(-Vector3.back * _runningSpeed * Time.deltaTime);

                //back sidewards to the right
                if (Input.GetKey(KeyCode.D))
                {
                    transform.Translate(Vector3.right * _runningSpeed * Time.deltaTime);
                }
                //back sidewards to the left
                else if (Input.GetKey(KeyCode.A))
                {
                    transform.Translate(Vector3.left * _runningSpeed * Time.deltaTime);
                }
            }
            //left
            else if (Input.GetKey(KeyCode.A))
            {
                animation.Play(animationName[1]);
                transform.Translate(Vector3.left * _runningSpeed * Time.deltaTime);
            }
            //right
            else if (Input.GetKey(KeyCode.D))
            {
                animation.Play(animationName[1]);
                transform.Translate(Vector3.right * _runningSpeed * Time.deltaTime);
            }
        }
        else if (Input.GetKeyDown(KeyCode.E))
            mark();
        else if (Input.GetKey(KeyCode.Escape))
            cheat_finish();
        //standby
        else
        {
            RecoverBreath();
            animation.Play(animationName[2]);
        }
    }


    void RecoverBreath()
    {
        timeRecoverBreath += Time.deltaTime;
        if (timeRecoverBreath > .1)
        {
            if (breath != 1 && breath <=1)
                breath += .02f;

            if(breath > 1)
                breath = 1;

            timeRecoverBreath = 0;

            BreathScript.updateBreathBar(breath);
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag == "escapeZone")
        {
            responseScript.FinalText = "STAGE CLEARED!";
            responseScript.On = true;
            isCleared = true;
            StartCoroutine(changeStage());
        }

        if (coll.gameObject.tag == "needleTrap")
        {
            health -= needleEnterDamage;
            updateHealth(health);
            GameObject obj = (GameObject)Instantiate(Resources.Load("Blood"), transform.position, transform.rotation);
            damageTakenSFX();
            Destroy(obj, 1.0f);
        }

        if (coll.gameObject.tag == "cutterTrap")
        {
            health -= cutterEnterDamage;
            updateHealth(health);
            GameObject obj = (GameObject)Instantiate(Resources.Load("Blood"), transform.position, transform.rotation);
            damageTakenSFX();
            Destroy(obj, 1.0f);
        }

        if (coll.gameObject.tag == "spear")
        {
            health -= spearEnterDamage;
            updateHealth(health);
            GameObject obj = (GameObject)Instantiate(Resources.Load("Blood"), transform.position, transform.rotation);
            damageTakenSFX();
            Destroy(obj, 1.0f);
        }

        if (coll.gameObject.tag == "blade")
        {
            health -= bladeEnterDamage;
            updateHealth(health);
            GameObject obj = (GameObject)Instantiate(Resources.Load("Blood"), transform.position, transform.rotation);
            damageTakenSFX();
            Destroy(obj, 1.0f);
        }

        if (coll.gameObject.tag == "axeTrap")
        {
            health -= axeEnterDamage;
            updateHealth(health);
            GameObject obj = (GameObject)Instantiate(Resources.Load("Blood"), transform.position, transform.rotation);
            damageTakenSFX();
            Destroy(obj, 1.0f);
        }

        if (coll.gameObject.tag == "greatAxeTrap")
        {
            health -= greatAxeEnterDamage;
            updateHealth(health);
            GameObject obj = (GameObject)Instantiate(Resources.Load("Blood"), transform.position, transform.rotation);
            damageTakenSFX();
            Destroy(obj, 1.0f);
        }

        if (health < 0 && isCharacterDead == false)
        {
            characterDeath();
            isCharacterDead = true;
        }
    }

    void OnCollisionStay(Collision coll)
    {
        timeInTrap += Time.deltaTime;
        if (coll.gameObject.tag == "needleTrap")
        {
            if (timeInTrap > .5f)
            {
                health -= needleStayDamage;
                updateHealth(health);
                GameObject obj = (GameObject)Instantiate(Resources.Load("Blood"), transform.position, transform.rotation);
                damageTakenSFX();
                Destroy(obj, 1.0f);
                timeInTrap = 0;
            }
        }

        if (coll.gameObject.tag == "cutterTrap")
        {
            if (timeInTrap > .5f)
            {
                health -= cutterStayDamage;
                updateHealth(health);
                GameObject obj = (GameObject)Instantiate(Resources.Load("Blood"), transform.position, transform.rotation);
                damageTakenSFX();
                Destroy(obj, 1.0f);
                timeInTrap = 0;
            }
        }

        if (coll.gameObject.tag == "sawTrap")
        {
            if (timeInTrap > .5f)
            {
                health -= sawStayDamage;
                updateHealth(health);
                GameObject obj = (GameObject)Instantiate(Resources.Load("Blood"), transform.position, transform.rotation);
                damageTakenSFX();
                Destroy(obj, 1.0f);
                timeInTrap = 0;
            }
        }

        if (health < 0 && isCharacterDead == false)
        {
            characterDeath();
            isCharacterDead = true;
        }
    }

    void updateHealth(float health)
    {
        if (health > 0)
            HealthScript.updateHealthBar(health);
        else
            HealthScript.updateHealthBar(0);
    }

    void walkingSFX()
    {
        if (!audioOneIsPlaying && !audioTwoIsPlaying)
        {
            walking.Play();
            walking.loop = true;
            audioOneIsPlaying = true;
        }
    }

    void runningSFX()
    {
        if (!audioTwoIsPlaying)
        {
            walking.Pause();
            running.Play();
            running.loop = true;
            audioTwoIsPlaying = true;
        }
    }

    void mark()
    {
        Instantiate(Resources.Load("arrow"), new Vector3(transform.position.x, -.12f, transform.position.z), transform.rotation);
    }

    void damageTakenSFX()
    {
        if (!audioThreeIsPlaying)
        {
            damagetaken.Play();
            audioThreeIsPlaying = true;
        }
        else
        {
            damagetaken.Pause();
            audioThreeIsPlaying = false;
        }
    }

    public void characterDeath()
    {
        if(isCleared == false)
        {
            responseScript.FinalText = "YOU LOSE.";
            responseScript.On = true;
            HealthScript.updateHealthBar(0);
            Instantiate(Resources.Load("Particle_System"), transform.position, transform.rotation);
            transform.localScale = new Vector3(0f,0f,0f);
            StartCoroutine(menuStage());
        }
    }

    void cheat_finish()
    {
        Vector3 escapeZoneCoords = escapeTransform.transform.position;
        characterRigidbody.transform.position = escapeZoneCoords;
    }

    IEnumerator menuStage()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);

    }

    IEnumerator changeStage()
    {
        yield return new WaitForSeconds(3);
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if ((currentScene + 1) < 6)
            SceneManager.LoadScene(currentScene + 1);
        else
            SceneManager.LoadScene(6);
    }
}