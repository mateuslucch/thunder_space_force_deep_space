using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {


    [SerializeField] int shieldHealt = 200;
    [SerializeField] int fixedHealt = 200;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "EnemyThings")
        {
            DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
            if (!damageDealer) { return; }

            ProcessHit(damageDealer); //mantém damageDealer, se não não é chamado no metodo
        }
       
    }
    private void ProcessHit(DamageDealer damageDealer)
    {
        shieldHealt -= damageDealer.GetDamage(); //subtrai de healt o que tem no damagedealer (script usado nos projéteis)
        damageDealer.Hit();
        if (shieldHealt <= 0)
        {
            shieldHealt = fixedHealt;
            gameObject.SetActive(false);

        }
    }
}
