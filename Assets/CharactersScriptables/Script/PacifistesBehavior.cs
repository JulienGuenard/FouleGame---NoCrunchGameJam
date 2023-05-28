using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacifistesBehavior : CharacterBehavior
{

    public void ChaseAnotherEntity()
    {
        if (anotherEntity.GetComponent<PacifistesBehavior>() != null)
        {
            Distance = Vector2.Distance(transform.position, anotherEntity.transform.position);
            Vector2 direction = anotherEntity.transform.position - transform.position;
            direction.Normalize();

            /// Pour que ça tourn de manière smoooooooooooooth
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.position = Vector2.MoveTowards(this.transform.position, anotherEntity.transform.position, Speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
    }
   
    // Start is called before the first frame update
    void Start()
    {
        LoadScriptable();
        Speed = data.MaxSpeed;
        Health = data.Health;
        DMG = data.DMG;
        IsAgressif = false;
        IsPacif = true;
        DetectZone = this.GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        DetectZone.radius = radius;
    }

    private void Update()
    {
        CheckHP();
    }

    protected override void LoadScriptable()
    {
        data = Resources.Load<CharacterData>("CharactersData/Pacifistes/Pacifos");
       base.CharacterData.Add(data);
       
    }

}
