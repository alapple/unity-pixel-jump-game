using UnityEngine;

namespace Effects
{
    public class DestroyAfterTime : MonoBehaviour
    {
        void Start()
        {
            Destroy(gameObject, 1f);
        }
    }
}