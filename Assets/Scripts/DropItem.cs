using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField]
    private int value;

    private float destroyPosition = -6f;

    // Start is called before the first frame update
    void Start()
    {
        jump();
    }

    void jump()
    {
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();

        float randomJumpForce = Random.Range(4f, 8f);
        Vector2 jumpVelocity = Vector2.up * randomJumpForce;
        jumpVelocity.x = Random.Range(-2f, 2f);
        rigidBody.AddForce(jumpVelocity, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < destroyPosition)
            Destroy(gameObject);

        if(transform.position.x > 5 || transform.position.x < -5)
            Destroy(gameObject);
    }

    public void setValue(int value)
    {
        this.value = value;
    }

    public int getValue() { return this.value;}
}
