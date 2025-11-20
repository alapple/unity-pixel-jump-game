using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Environment
{
    public class BackgroundController : MonoBehaviour
    {

        private float _startPos, _length;
        [SerializeField]
        private GameObject cam;
        [SerializeField]
        private float parallaxFactor;
        private void Start()
        {
            _startPos = transform.position.x;
            _length = GetComponent<SpriteRenderer>().bounds.size.x;
        }

        void FixedUpdate()
        {
            float distance = cam.transform.position.x * parallaxFactor;
            float movement = cam.transform.position.x * (1 - parallaxFactor);
            transform.position = new Vector3(_startPos + distance, transform.position.y, transform.position.z);
            if (movement > _startPos + _length) _startPos += _length;
            else if (movement < _startPos - _length) _startPos -= _length;
        }
    }
}