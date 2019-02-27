using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMonsterBehaviour : MonoBehaviour
{
    [SerializeField]
    MonsterModel monsterModel;
    public MonsterModel MonsterModel { get { return monsterModel; } }
    public MonsterContext MonsterContext { private set; get; }

    public Animator _Animator { get; private set; }

    private OperatorModel operatorModel;

    void Awake()
    {
        MonsterContext = new MonsterContext();
        _Animator = GetComponent<Animator>();
    }

    void Start()
    {
 
        operatorModel.monsterBehaviour = this;
        operatorModel.monsterUI.Init();

        monsterModel.maxHP = monsterModel.hp;
    }

    private void FixedUpdate()
    {
        
    }

    // 召喚時のモーション
    public void GetSummonMotion(Vector3 targetPos, Vector3 enemyPos)
    {
        StartCoroutine(SummonMotion(targetPos,enemyPos));
    }

    IEnumerator SummonMotion(Vector3 targetPos, Vector3 enemyPos)
    {
        while (true)
        {
            //while(MonsterManager.Instance.PlayerMonsterBehaviour == null|| MonsterManager.Instance.ComputerMonsterBehaviour == null)
            //{
            //    yield return null;
            //}



            //var distance = Vector3.Distance(transform.position, targetPos);
            //while (distance >= 0.1f)
            //{
            //    distance = Vector3.Distance(transform.position, targetPos);
            //    Vector3 targetDir = new Vector3(targetPos.x, transform.position.y, targetPos.z) - transform.position;
            //    Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 100, 0f);
            //    transform.rotation = Quaternion.LookRotation(newDir);

            //    var rb = GetComponent<Rigidbody>();

            //    rb.AddForce(transform.forward * 2);
            //    yield return null;
            //}

            //if (distance < 0.1)
            {
                transform.rotation = Quaternion.LookRotation(enemyPos - transform.position);  
            }
            
            break;
        }
        yield return null;
    }
}
