using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class playercontrollertask2 : MonoBehaviour
{
    [SerializeField] int Score = 0;
    [SerializeField] float Timer = 60.0f;
    [SerializeField] bool CompletionStatus = false;

    [SerializeField] TextMeshProUGUI ScoreTextBox;
    [SerializeField] TextMeshProUGUI TimeTextBox;
    [SerializeField] TextMeshProUGUI CompletionStatusTextBox;
    
    [SerializeField] AudioSource coinsound;
    [SerializeField] AudioClip coinclip;
    [SerializeField] AudioClip GameOverClip;

     Rigidbody physicsBody;
    
    [SerializeField] float speed; // we will choose a random speed and store it here

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EditorApplication.Beep();
        Debug.Log(name); // this should print the objects name before it changes
        name = ToString();
        Debug.Log(name); // and this should print the objects name after it changes

        speed = Random.Range(5, 12); // picks a random number 1-12

        physicsBody = GetComponent<Rigidbody>();

        Score = PlayerPrefs.GetInt("PlayerScore");
    }

    // Update is called once per frame
    void Update()
    {
        if (CompletionStatus == false)
        {
            if (Keyboard.current.f1Key.wasPressedThisFrame)
            {
                PlayerPrefs.SetInt("playerscore", Score);
            }

            if (Keyboard.current.f2Key.wasPressedThisFrame)
            {
                Score = PlayerPrefs.GetInt("playerscore");
            }

            float zspeed = 0, xspeed = 0;
            zspeed = Keyboard.current.wKey.value - Keyboard.current.sKey.value;
            xspeed = Keyboard.current.dKey.value - Keyboard.current.aKey.value;


            Vector3 direction = new Vector3(xspeed, 0, zspeed) * Time.deltaTime;

            // physicsBody.linearVelocity =speed * direction;

            physicsBody.AddForce(new Vector3(xspeed, 0, zspeed) * speed);

            Timer = Timer - Time.deltaTime;
            Timer = Mathf.Clamp(Timer, 0, 60f);
            UpdateTextBox();


            float bouncespeed = speed / 4;
            float rightboundary = 3.75f;
            if (transform.position.x > rightboundary)
            {
                float newXspeed = -bouncespeed;
                float maintainzspeed = physicsBody.linearVelocity.z;
                physicsBody.linearVelocity = new Vector3(newXspeed, 0, maintainzspeed);
            }
            float leftboundary = 1.5f;
            if (transform.position.x < leftboundary)
            {
                float newXspeed = -bouncespeed;
                float maintainZspeed = physicsBody.linearVelocity.z;
                physicsBody.linearVelocity = new Vector3(newXspeed, 0, maintainZspeed);
            }


            Timer = Timer - Time.deltaTime;
            UpdateTextBox();


            ScoreTextBox.text = "Score : " + Score.ToString();
            TimeTextBox.text = "Time : " + Timer.ToString("F0");
            CompletionStatusTextBox.text = "Level complete? : " + CompletionStatus.ToString();


            Timer = Timer - Time.deltaTime;

            Timer = Mathf.Clamp(Timer, 0, 60f);
            if (Timer <= 0 && Score != 3)
            {
                Destroy(gameObject);
            }
            if (Score >= 3) // win condition met
            {
                CompletionStatus = true;
            }
            if (Timer <= 0 && !CompletionStatus)
            {
                Destroy(gameObject);
            }

            if (Keyboard.current.uKey.wasReleasedThisFrame)
            {
                transform.position = new Vector3(3, 0, 0);
            }
        }
        else
        {
            if (Keyboard.current.escapeKey.isPressed)
            {
                UnityEditor.EditorApplication.ExitPlaymode();
            }
            void UpdateTextBox()
            {

            }
        }
    }
    private void OnMouseDown()
    {
        addpoints(10);
        Debug.Log("Who Clicked my ball?!!");
        Score += 10;

        Destroy(gameObject); // Destroys the red ball object that our script is attached to 

        //Score += 10;
        addpoints(10);

        transform.position = new Vector3(-0.5f, 0, 0.5f); // adjusts the speed value if its too fast or too slow

        coinsound.Play();
    }
        void changename()
        {
            name = "Player ball"; // change the name of the object that has this script
        }
        void addpoints(int amount)
        {
            Score += amount;
        }
    
        private void OnTriggerEnter(Collider other)
    {
        if (coinsound != null)
        {
            coinsound.Play();
            addpoints(10);
            Destroy(other.gameObject);
        }
    }
}
