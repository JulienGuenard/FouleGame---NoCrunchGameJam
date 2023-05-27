using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JFoule : MonoBehaviour
{
    [SerializeField] private int uniteNumber;
    [SerializeField] private GameObject uniteGMB;
    [SerializeField] private Vector3 spreadOffset;
    [SerializeField] private GameObject chefGroup;


    // Start is called before the first frame update
    void Awake()
    {

        for(int i = 0; i < uniteNumber; i++)
        {
            Vector3 spread = new Vector3(Random.Range(-spreadOffset.x, spreadOffset.x), Random.Range(-spreadOffset.y, spreadOffset.y), 0);
            GameObject gmb = Instantiate(uniteGMB, transform.position + spread, Quaternion.identity);
            gmb.transform.parent = transform;

            if (i == 0)
            {
                uniteGMB.GetComponent<JUnite>().uniteType = JUniteType.Chef;
                chefGroup = uniteGMB;
            }



            if (i > 0)
            {
                uniteGMB.GetComponent<JUnite>().uniteType = JUniteType.Passif;
            }
            uniteGMB.GetComponent<JUnite>().chefGroup = chefGroup.transform;

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
