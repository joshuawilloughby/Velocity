using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Josh.Velocity
{
    public class MenuUIController : MonoBehaviour
    {
        public Animator animator;

        void Start()
        {
            animator.SetBool("Create Match", false);
        }

        public void CreateMenu()
        {
            animator.SetBool("Create Match", true);
        }
    }
}