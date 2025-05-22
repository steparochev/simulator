using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Animation anim;
    [SerializeField] private LayerMask layer;
    [SerializeField] private float distance;
    [SerializeField] private GameObject cam;
    void Start()
    {
        anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
