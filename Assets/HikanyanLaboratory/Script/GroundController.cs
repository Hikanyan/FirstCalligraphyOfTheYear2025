using UnityEngine;

namespace HikanyanLaboratory.Script
{
    public class GroundController : MonoBehaviour
    {
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                GetComponent<Renderer>().material.color = Color.blue;
            }
        }
    }
}