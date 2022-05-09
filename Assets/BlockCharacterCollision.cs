using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCharacterCollision : MonoBehaviour
{
    [SerializeField]
    public Collider2D characterCollider;
    public Collider2D characterBlockerCollider;
    private void Awake()
    {
        characterCollider = gameObject.GetComponent<Collider2D>();
    }
    void Start()
    {
        Physics2D.IgnoreCollision(characterCollider, characterBlockerCollider, true);
    }

}
