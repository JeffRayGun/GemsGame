using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rigid;

    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.freezeRotation = true;
    }

    // Update is called once per frame
    void FixedUpdate()
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision.");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            Debug.Log("Grabbable boi");
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Death"))
        {
            Debug.Log("Death");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
