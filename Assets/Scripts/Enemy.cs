using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float hp;
    [SerializeField]
    private GameObject coin;
    [SerializeField]
    private GameObject heart;

    [SerializeField] private int damage;

    [SerializeField]
    private float moveSpeed;
    private float destroyPosition = -6f;

    public void setDamage(int damage)
    {
        this.damage = damage;
    }

    public int getDamage() { return this.damage; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;

        if (transform.position.y < destroyPosition)
            Destroy(gameObject);
    }

    public void setMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    public float getMoveSpeed()
    {
        return this.moveSpeed;
    }

    public void setDecreaseHp(int damage)
    {
        this.hp -= damage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Weapon")
        {
            Weapon weapon = other.gameObject.GetComponent<Weapon>();
            int damage = weapon.getDamage();

            setDecreaseHp(damage);
            if(this.hp <= 0)
            {
                Destroy(gameObject);
                Instantiate(coin, transform.position, Quaternion.identity);

                if(this.gameObject.tag == "Boss")
                {
                    bool isGame = GameManager.instance.SetGameClear();
                    Player player = FindObjectOfType<Player>();
                    player.setIsGame(isGame);
                }

                if(Random.Range(0,4) == 0)
                    Instantiate(heart, transform.position, Quaternion.identity);
            }

            Destroy(other.gameObject);
        }

    }
}
