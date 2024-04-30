using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManagement : MonoBehaviour
{
    public int health = 5;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public bool attackTriggered = false;
    [SerializeField] GameObject gameOver;

    [SerializeField] GameObject counterScript;
    private bool counterEnabled;

    private void Awake()
    {
        gameOver.SetActive(false);
    }
    void Update()
    {
        foreach (Image img in hearts)
        {
            img.sprite = emptyHeart;
        }
        for (int i = 0; i < health; i++)
        {
            hearts[i].sprite = fullHeart;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.transform.tag == "Enemy")
        {
            attackTriggered = true;
            counterEnabled = counterScript.GetComponent<CounterScript>().counterOn;
            if (counterEnabled == true)
            {
                Debug.Log("Damage Countered");
                StartCoroutine(DelayAttackTrigger());

            }
            else
            {
                attackTriggered = false;
                if (health != 0)
                {


                    health = health - 1;




                    if (health == 0)
                    {
                        gameOver.SetActive(true);


                    }
                    else
                    {
                        StartCoroutine(GetHurt());

                    }
                }


            }
        }

    }

        IEnumerator DelayAttackTrigger()
        {
            yield return null;
            attackTriggered = false;
        }

        IEnumerator GetHurt()
    {
        Physics2D.IgnoreLayerCollision(9, 7);
        yield return new WaitForSeconds(2);
        Physics2D.IgnoreLayerCollision(9, 7, false);
    }
}
