using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterInteraction : MonoBehaviour
{
    public LayerMask layerMask;
    public float sizeMultiplier;

    private Vector3 detectBoxSize;
    private Vector3 posOffSet = new Vector3(0, 1, 0);

    private List<Collider> insideColliders = new List<Collider>();

    private void Start()
    {
        detectBoxSize = transform.localScale / 2 * sizeMultiplier;
    }

    private void FixedUpdate()
    {
        FindInteractables(transform.position + posOffSet);
    }

    void FindInteractables(Vector3 center)
    {
        int maxColliders = 10;
        Collider[] hitColliders = new Collider[maxColliders];
        int numberOfColliders = Physics.OverlapBoxNonAlloc(center, detectBoxSize, hitColliders, Quaternion.identity, layerMask);

        if (numberOfColliders > 0)
        {

            for (int i = 0; i < numberOfColliders; i++)
            {
                insideColliders.Add(hitColliders[i]);//Tespit edilen tum colliderları listeye ekle
            }


            float[] distances = new float[numberOfColliders];

            for (int a = 0; a < numberOfColliders; a++)
            {
                distances[a] = (transform.position - hitColliders[a].transform.position).sqrMagnitude;
            }

            float closestDistance = distances.Min();//Dizide en kucuk degere sahip olan elemani ariyor.
            Debug.Log("En kisa mesafe:" + closestDistance);

            int closestIndex = Array.IndexOf(distances, closestDistance);
            Collider closestCollider = hitColliders[closestIndex];

            MeshRenderer c_renderer = closestCollider.GetComponent<MeshRenderer>();
            c_renderer.material.color = Color.green;

            IInteractable _cInteractable = closestCollider.GetComponent<IInteractable>();
            _cInteractable.Interact();

            for (int i = 0; i < numberOfColliders; i++)//Yakin olan disindaki tum colliderlari kirmizi yapiyor.
            {
                if (hitColliders[i] != closestCollider)
                {
                    hitColliders[i].GetComponent<MeshRenderer>().material.color = Color.red;
                    IInteractable _ıınteractble = hitColliders[i].GetComponent<IInteractable>();
                    _ıınteractble.UnInteract();//Uzaktakilerin panelini kapat
                }
            }

            Debug.Log("En yakin obje: " + closestCollider.name, closestCollider.gameObject);
        }
        else
        {
            for (int i = 0; i < insideColliders.Count; i++)
            {
                insideColliders[i].GetComponent<MeshRenderer>().material.color = Color.gray;
                IInteractable _ıınteractble = insideColliders[i].GetComponent<IInteractable>();
                _ıınteractble.UnInteract();//Etkilesim alani disinda kalanlarin panelini kapat
            }

            insideColliders.Clear();
            Debug.Log("Etkilesilebilir bir obje bulunamadi!!!");
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + posOffSet, transform.localScale * sizeMultiplier);

    }



}
