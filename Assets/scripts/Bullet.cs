using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    UnityEvent onSpawn, onDeath, onContact;
    public float speed, seekerRange, life = .01f;
    GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        OnSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        #region homing on enemies
        //if homing then slowly turn towards the nearest enemy
        if (GameManager.instance.GetModifiers().Contains(Modifiers.homing))
        {
            GetNearestEnemy();
            if (target != null)
            {
                Vector3 targetPos = target.transform.position;
                targetPos.y = transform.position.y;

                transform.forward = Vector3.Slerp(transform.forward, -(transform.position - targetPos), 0.05f);
            }  
        }
        #endregion
    }

    IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(life);
        OnDeath();
    }

    void OnSpawn()
    {
        onSpawn?.Invoke();
        StartCoroutine(LifeTimer());
    }
    void OnContact()
    {
        onContact?.Invoke();
    }
    void OnDeath()
    {
        onDeath?.Invoke();
        Destroy(this.gameObject);
    }
    void GenerateExplosion()
    {

    }
    void GetNearestEnemy()
    {
        GameObject closest =null;
        var colliders = Physics.OverlapSphere(transform.position, seekerRange, LayerMask.GetMask("Enemy"));
        Debug.Log($"Scan Count:{colliders.Length}");
        foreach (var item in colliders)
        {
            if(closest == null)
            {
                closest = item.gameObject;
            } else
            {
                if(Vector3.Distance(closest.transform.position, transform.position) > Vector3.Distance(item.transform.position, transform.position))
                    closest = item.gameObject;
            }
        }
        target = closest;
    }
}
