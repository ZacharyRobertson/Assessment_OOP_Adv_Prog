using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;
    private float deathDelay = .2f;

    private bool purified = false;
    private Enemy thisEnemy;
    private Material rend;
    // Use this for initialization
    void Awake()
    {
        currentHealth = maxHealth;
        thisEnemy = GetComponent<Enemy>();
        rend = GetComponent<Material>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            StartCoroutine(Purify());
        }
    }
    public void TakeDamage(int damage, Player attackingPlayer)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            attackingPlayer.score += thisEnemy.score;
        }
    }

    IEnumerator Purify()
    {
        purified = true;
        thisEnemy.behaviourIndex = Enemy.Behaviour.IDLE;
        Color c = rend.color;
        c.a = 0.5f;
        #region Sin Math
        //Set an Index and Amplitude for the X/Y Axes as 
        float sinIndex = 0;
        float amplitudeX = 10.0f;
        float amplitudeY = 5.0f;
        float omegaX = 1.0f;
        float omegaY = 5.0f;

        sinIndex += Time.deltaTime;
        float x = amplitudeX * Mathf.Cos(omegaX * sinIndex);
        float y = Mathf.Abs(amplitudeY * Mathf.Sin(omegaY * sinIndex));
        transform.localPosition = new Vector3(x, y, 0);
        #endregion
        yield return new WaitForSeconds(deathDelay);
        Destroy(gameObject);
    }
    
}
