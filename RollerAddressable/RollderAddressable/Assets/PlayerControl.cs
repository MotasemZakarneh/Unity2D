using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] float forceMag;
    [SerializeField] GameObject coinSFXObj;
    [SerializeField] GameObject enemySFXObj;
    [SerializeField] int coinCount;
    [SerializeField] string nextLevelName;
    [SerializeField] string lostLevelName;

    Vector2 dir;
    Rigidbody2D rb2d;
    int score;
    // Start is called before the first frame update
    void Start()
    {
        dir = new Vector2(0, 0);
        score = 0;
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        dir.x = 0;
        dir.y = 0;
        if (Input.GetKey(KeyCode.W))
        {
            dir.y = 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            dir.x = -1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            dir.y = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            dir.x = 1;
        }
    }
    void FixedUpdate()
    {
        rb2d.AddForce(GetForce());
    }
    void OnTriggerEnter2D(Collider2D visitor)
    {
        Coin visitorC = visitor.GetComponent<Coin>();
        if(visitorC != null)
        {
            score = score + 1;
            AudioSource coinSFX = coinSFXObj.GetComponent<AudioSource>();
            coinSFX.Play();
            Destroy(visitorC.gameObject);
            print(score);
            if(score>=coinCount)
            {
                SceneManager.LoadScene(nextLevelName);
            }
        }

        Enemy visitorE = visitor.GetComponent<Enemy>();
        if(visitorE != null)
        {
            AudioSource enemySFX = enemySFXObj.GetComponent<AudioSource>();
            enemySFX.Play();
            Destroy(visitorE.gameObject);
            print("I Hit Enmemy");

            Invoke("LoadLostLevel", 0.4f);
        }
    }
    Vector2 GetForce()
    {
        Vector2 f = new Vector2(0, 0);
        f = dir * forceMag;
        return f;
    }
    void LoadLostLevel()
    {
        SceneManager.LoadScene(lostLevelName);
    }

}
