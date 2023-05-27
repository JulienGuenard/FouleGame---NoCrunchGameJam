using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JPlayer : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private JFoule foule;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        transform.position += input * speed * Time.deltaTime;
    }
}
