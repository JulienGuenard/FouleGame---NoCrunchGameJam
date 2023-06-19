using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public static AttackManager instance;
    List<GameObject> attackList = new List<GameObject>();
    List<GameObject> attackListUsed = new List<GameObject>();
    public GameObject attackGMB;
    public Transform objPulling;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Update()
    {
        
    }

    public void SpawnNewAttack()
    {
        GameObject atk = Instantiate(attackGMB, new Vector3(999,999,0), Quaternion.identity);
        AttackManager.instance.attackList.Add(atk);
        atk.transform.parent = objPulling;
    }

    public void SpawnAttackAtPos(Vector3 pos, FlockAgent targetAttacked, int damage)
    {
        if (attackList.Count == 0) return;

        GameObject atk = attackList[0];
        
        atk.transform.position = pos;

        attackList.Remove(atk);
        attackListUsed.Add(atk);
        

        atk.GetComponent<DestroyAfterTime>().delay = 1f;
        atk.GetComponent<DestroyAfterTime>().StartCoroutine("DestroyAfterDelay");

        AttackTarget atkTarget = atk.GetComponent<AttackTarget>();
        atkTarget.target = targetAttacked;
        atkTarget.damage = damage;
    }

    public void BackToPullingList(GameObject obj)
    {
        obj.transform.position = new Vector3(999, 999, 0);
        attackListUsed.Remove(obj);
        attackList.Add(obj);
    }
}
