using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor_HeroKnight : MonoBehaviour {

    private int m_ColCount = 0;
    public bool attackSensor;
    private float m_DisableTimer;

    private void OnEnable()
    {
        m_ColCount = 0;
    }

    public bool State()
    {
        if (m_DisableTimer > 0)
            return false;
        return m_ColCount > 0;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (attackSensor)
        {
            if(other.CompareTag("Enemy") && other is not CircleCollider2D){
                EnemyController enemy = other.GetComponent<EnemyController>();
                if (enemy != null)
                {
                    enemy.TakenHit();
                }
            }
        }
        else if (other is not CircleCollider2D){
            m_ColCount++;
        }
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (attackSensor)
        {
            
        }
        else if (other is not CircleCollider2D){
            m_ColCount--;
        }
    }

    void Update()
    {
        m_DisableTimer -= Time.deltaTime;
    }

    public void Disable(float duration)
    {
        m_DisableTimer = duration;
    }
}
