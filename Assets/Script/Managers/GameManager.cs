using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    List<GameObject> zombieList;
    public int maxZombieCount = 10; //最多场景内同一时间 有多少个僵尸
    public float intervalTime = 3.0f; //间隔多长时间创建僵尸

    public GameObject zombiePrefab;

    float creatZombieTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        zombieList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckNeedCreateZombie();
    }

    void CheckNeedCreateZombie()
    {
        if (zombieList.Count < maxZombieCount)
        {
            if (creatZombieTime > 0)
            {
                creatZombieTime -= Time.deltaTime;
            }
            else
            {
                CreateZombie();
                creatZombieTime = intervalTime;
            }
        }
    }

    void CreateZombie()
    {
        //随机位置生成僵尸
        float x = Random.Range(1, 10);
        float z = Random.Range(1, 10);
        Vector3 point = new Vector3(x,0f,z);
        GameObject zombie = Instantiate(zombiePrefab,point,Quaternion.identity);
        zombieList.Add(zombie);
    }

    //场景内是否有被击飞的僵尸
    public bool HasFlyingZombie()
    {
        foreach (GameObject zombie in zombieList)
        {
            ZombieController ctrl = zombie.GetComponent<ZombieController>();
            if (ctrl.IsFlying())
            {
                return true;
            }
        }
        return false;
    }

    //按下R的时候 亚索需要面向僵尸释放大招
    public Vector3 GetFlyingZombieDirection()
    {
        foreach (GameObject zombie in zombieList)
        {
            ZombieController ctrl = zombie.GetComponent<ZombieController>();
            if (ctrl.IsFlying())
            {
                return zombie.gameObject.transform.position;
            }
        }
        return new Vector3(0f,0f,0f);
    }

    //按下R的时候 所有被击飞的僵尸 需要暂停行动 
    public bool StopAllFlyingZombie()
    {
        foreach (GameObject zombie in zombieList)
        {
            ZombieController ctrl = zombie.GetComponent<ZombieController>();
            ctrl.RemoveRigidbody();
        }
        return false;
    }

    //暂停所有被击飞的僵尸行动后 R结束 继续恢复行动
    public bool RestartAllFlyingZombie()
    {
        foreach (GameObject zombie in zombieList)
        {
            ZombieController ctrl = zombie.GetComponent<ZombieController>();
            if (ctrl.IsFlying())
            {
                ctrl.AddRigidbody(2f,2f);
                ctrl.Downing();
            }
        }
        return false;
    }
}
