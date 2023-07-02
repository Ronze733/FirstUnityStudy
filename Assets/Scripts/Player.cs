using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int coin = 0;
    private int point = 0;

    private bool isGame = true;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private GameObject[] weapons;

    [SerializeField]
    private int[] weaponTiming;

    [SerializeField]
    private Transform weaponPosition;


    private float shootInterval = 0.3f;
    private float lastShotTime = 0f;

    [SerializeField]
    private int maxHp = 5;
    private int hp;

    private void Start()
    {
        hp = maxHp;
        GameManager.instance.ShowHp(this.getHp());
    }

    // Update is called once per frame
    void Update()
    {
        /*float horizontalInput = Input.GetAxisRaw("Horizontal");
        // float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 moveTo = new Vector3(horizontalInput, 0f, 0f);

        transform.position += moveTo * moveSpeed * Time.deltaTime;*/

        /*Vector3 moveTo = new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.position -= moveTo;
        } else if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.position += moveTo; 
        }*/

        //Debug.Log(Input.mousePosition);

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float toX = Mathf.Clamp(mousePos.x, -3f, 3f);
        float toY = Mathf.Clamp(mousePos.y, -4f, -1f);

        transform.position = new Vector3(toX, toY, transform.position.z);

        if(isGame)
            Shoot();
    }

    void Shoot()
    {
        if (Time.time - lastShotTime > shootInterval)
        {
            int index = 0;

            for (int i = 5; i > 0; i--)
            {
                if (this.coin >= this.weaponTiming[i])
                {
                    index = i;
                    Weapon weapon = this.weapons[index].gameObject.GetComponent<Weapon>();
                    shootInterval = weapon.getShootInterval();
                    break;
                }
            }

            Instantiate(weapons[index], weaponPosition.position, Quaternion.identity);
            lastShotTime = Time.time;
        }
    }

    public void setHp(int changeHp)
    {
        this.hp += changeHp;
        if(this.hp >= maxHp)
            this.hp = maxHp;

        if (this.hp <= 0)
            this.hp = 0;
    }

    public int getHp() { return hp; }

    public int getCoin() { return coin; }
    public void setCoin(int coin)
    {
        this.coin += coin;
    }

    public int getPoint() { return point; }

    public void setPoint(int point) { this.point = point; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            int damage = enemy.getDamage();

            this.setHp(-1 * damage);
            GameManager.instance.ShowHp(this.getHp());

            if(this.hp <= 0)
            {
                Debug.Log("GameOver");
                GameManager.instance.SetGameOver();
                Destroy(gameObject);
            }
        } else if(other.gameObject.tag == "Coin")
        {
            DropItem coin = other.gameObject.GetComponent<DropItem>();
            int price = coin.getValue();
            this.setCoin(price);
            GameManager.instance.IncreaseCoin(this.getCoin());

            Destroy(other.gameObject);

        } else if(other.gameObject.tag == "Heart")
        {
            DropItem heart = other.gameObject.GetComponent<DropItem>();
            int hearVal = heart.getValue();
            this.setHp(hearVal);
            GameManager.instance.ShowHp(this.getHp());

            Destroy(other.gameObject);
        }
    }

    public bool getIsGame()
    {
        return this.isGame;
    }

    public void setIsGame(bool isGame)
    {
        this.isGame = isGame;
    }
}
