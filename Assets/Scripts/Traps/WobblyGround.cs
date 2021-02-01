using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class WobblyGround : MonoBehaviour
{
    [SerializeField] private float timeUntilBreak;
    [SerializeField] private float timeUntilReset;
    
    public readonly float wiggleDistance = 0.025f;
    private Rigidbody2D _body;
    private Vector3 _basePosition;
    private bool _treaddedOn;
    
    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _basePosition = transform.position;
        _body.bodyType = RigidbodyType2D.Static;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Break());
        }
    }

    private IEnumerator Break()
    {
        if (!_treaddedOn)
        {
            _treaddedOn = true;
            yield return new WaitForSeconds(timeUntilBreak);
            _treaddedOn = false;
            _body.bodyType = RigidbodyType2D.Dynamic;
            yield return new WaitForSeconds(timeUntilReset);
            ResetFloor();
        }
    }

    private void ResetFloor()
    {
        _body.bodyType = RigidbodyType2D.Static;
        transform.position = _basePosition;
    }

    private void FixedUpdate()
    {
        if (_treaddedOn)
        {
            transform.position = _basePosition + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * wiggleDistance;
        }
    }
}
