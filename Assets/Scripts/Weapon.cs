using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private float destroyPosition;
    [SerializeField]
    private float shootInterval;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        if(transform.position.y > destroyPosition)
        {
            Destroy(gameObject);
        }
    }

    public int getDamage()
    {
        return damage;
    }

    public float getShootInterval()
    {
        return shootInterval;
    }

    public void setShootInterval(float shootInterval)
    {
        this.shootInterval = shootInterval;
    }
}
