using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public GameController gameController;
    public AudioClip gemSound;
    public AudioClip keySound;

    private Rigidbody2D rigid;
    private bool playerKilled = false;
    private int numberOfKeys = 0;

    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.freezeRotation = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!playerKilled)
        { 
            //Store the current horizontal input in the float moveHorizontal.
            float moveHorizontal = 0, moveVertical = 0;

            if (Input.GetKey(KeyCode.UpArrow)) moveVertical++;
            if (Input.GetKey(KeyCode.DownArrow)) moveVertical--;
            if (Input.GetKey(KeyCode.RightArrow)) moveHorizontal++;
            if (Input.GetKey(KeyCode.LeftArrow)) moveHorizontal--;

            Vector3 currentPos = transform.position;
            
            rigid.transform.Translate(new Vector2(moveHorizontal * Time.deltaTime * speed, moveVertical * Time.deltaTime * speed));
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision.");
        if (collision.gameObject.CompareTag("Door"))
        {
            // Check if a key has been grabbed
            if (numberOfKeys > 0)
            {
                collision.gameObject.SetActive(false);
                numberOfKeys--;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Gem"))
        {
            Debug.Log("Grabbable boi");
            collision.gameObject.SetActive(false);
            // Play gem sound:
            AudioSource.PlayClipAtPoint(gemSound, transform.position);
            // Tell game controller we collected a gem:
            gameController.GemCollected();
        }
        else if (collision.gameObject.CompareTag("Key"))
        {
            Debug.Log("Issa key");
            collision.gameObject.SetActive(false);
            // Play key sound:
            AudioSource.PlayClipAtPoint(keySound, transform.position);
            numberOfKeys++;
        }

        else if (collision.gameObject.CompareTag("Death"))
        {
            Debug.Log("Death");
            // TO-DO: Tell searcher to rotate towards player.
            collision.gameObject.transform.parent.GetComponent<Searchlight>().CatchPlayer(transform.position);
            // Start player death function:
            playerKilled = true;
            gameController.KillPlayer();
        }
    }
}
