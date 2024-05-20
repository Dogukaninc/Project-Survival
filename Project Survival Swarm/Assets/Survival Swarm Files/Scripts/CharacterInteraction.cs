using System;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterInteraction : MonoBehaviour
{
    public GameObject interactionInfoPanel;
    public TextMeshProUGUI infoText;
    
    Camera cam;
    
    public LayerMask layerMask;
    public float sizeMultiplier;
    
    private Vector3 detectBoxSize;
    private Vector3 posOffSet = new Vector3(0, 1, 0);

    Collider[] hitColliders = new Collider[10];


    private void Start()
    {
        cam = Camera.main;
        detectBoxSize = transform.localScale / 2 * sizeMultiplier;
    }

    private void FixedUpdate()
    {
        FindInteractables(transform.position + posOffSet);
    }
    
    private void LateUpdate()
    {
        InteractionPanelFacing();
    }
    
    void FindInteractables(Vector3 center)
    {

        int numberOfColliders = Physics.OverlapBoxNonAlloc(center, detectBoxSize, hitColliders, Quaternion.identity, layerMask);

        if (numberOfColliders > 0)
        {
            float[] distances = new float[numberOfColliders];
            for (int a = 0; a < numberOfColliders; a++)
            {
                distances[a] = (transform.position - hitColliders[a].transform.position).sqrMagnitude;
            }
            
            float closestDistance = distances.Min();//Dizide en kucuk degere sahip olan elemani ariyor.
            
            int closestIndex = Array.IndexOf(distances, closestDistance);
            Collider closestCollider = hitColliders[closestIndex];

            MeshRenderer c_renderer = closestCollider.GetComponent<MeshRenderer>();
            c_renderer.material.color = Color.green;

            if (closestCollider.TryGetComponent<IInteractable>(out IInteractable interactble))
            {
                interactionInfoPanel.gameObject.SetActive(true);
                interactionInfoPanel.transform.position = closestCollider.transform.position + new Vector3(0, 1, 0);

                InteractbleObject _Interactable = closestCollider.GetComponent<InteractbleObject>();
                infoText.text = _Interactable.objectName + _Interactable.infoText;
                interactble.Interact();
            }

            for (int i = 0; i < numberOfColliders; i++)//Yakin olan disindaki tum colliderlari uninteract yapiyor
            {
                if (hitColliders[i] != closestCollider)
                {
                    hitColliders[i].GetComponent<MeshRenderer>().material.color = Color.red;

                    if (hitColliders[i].TryGetComponent(out IInteractable _interactble))
                    {
                        _interactble.UnInteract();
                    }
                }
            }

            Debug.Log("En yakin obje: " + closestCollider.name, closestCollider.gameObject);
        }
        else
        {
            var _interactables = FindObjectsOfType(typeof(InteractbleObject));
            foreach (var item in _interactables)
            {
                item.GetComponent<IInteractable>().UnInteract();
            }

            foreach (var item in _interactables)
            {
                item.GetComponent<MeshRenderer>().material.color = Color.gray;
            }

            interactionInfoPanel.gameObject.SetActive(false);
            interactionInfoPanel.transform.position = transform.position;
            Debug.Log("Etkilesilebilir bir obje bulunamadi!!!");
        }
    
    }
    
    private void InteractionPanelFacing()
    {
        var rotation = cam.transform.rotation;
        interactionInfoPanel.transform.LookAt(interactionInfoPanel.transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + posOffSet, transform.localScale * sizeMultiplier);
        
    }

}
