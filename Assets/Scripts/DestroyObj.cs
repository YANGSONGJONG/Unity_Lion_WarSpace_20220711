using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NRSUNG
{
    public class DestroyObj : MonoBehaviour
    {
        public float DeleteTime;
        private void Update()
        {
            Destroy(gameObject,DeleteTime);
        }
       

    }
}

