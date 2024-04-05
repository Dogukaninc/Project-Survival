using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{

    // Sol týka bir kere basýnca yakýn dövüþ animasyonunu oynatacak
    // x'e basýnca baltayý beline koyacak
    // Bir vurma animasyonu bitmeden bir sonraki sol týk'ý algýlamayacak yani yakýn dövüþ animasyonu spamlanamayacak
    // Hasar verme ya animation event ile ya da ontrigger ile yapýlacak

    public ActiveWeapon.WeaponSlot meleeSlot;

    public float damageAmount;
    public bool canSwing;
    public string meleeName;

    [HideInInspector] public Animator rigController;
    //public Transform raycastDestination;

    //private float nextHitTime;

    //Ray ray;
    //RaycastHit hitInfo;

    public Transform damageCenter;
    public LayerMask damagableLayer;
    public float damageRadius;

    private Collider[] hitColliders = new Collider[10];

    void Start()
    {
        //Rig controller'ý equip de atadým
        canSwing = true;

    }

    private void Update()
    {

    }

    public void SwingMelee()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (canSwing)
            {
                rigController.Play("weapon_melee_attack", 1, 0f);
                canSwing = false;
                StartCoroutine(SwingDelay());
            }
        }
    }

    public void GiveDamage(float damage)//Animation Event
    {
        int numberOfColliders = Physics.OverlapSphereNonAlloc(damageCenter.position, damageRadius, hitColliders, damagableLayer);
        if (numberOfColliders > 0)
        {
            for (int i = 0; i < numberOfColliders; i++)
            {
                if (hitColliders[i] != null)
                {
                    if (hitColliders[i].TryGetComponent(out Health health))
                    {
                        Debug.Log("Give Damage Çalýþtý");
                        health.currentHealth -= damage;
                    }
                }
            }

        }
    }

    IEnumerator SwingDelay()
    {
        yield return new WaitForSeconds(0.5f);//Sallama animasyonunun uzunlugu kadar beklet
        canSwing = true;
    }


    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(transform.position + posOffSet, transform.localScale * sizeMultiplier);
    //}

}
