using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Com.Josh.Velocity
{
    public class Sway : MonoBehaviourPunCallbacks
    {
        #region Variables

        public float intensity;
        public float smooth;
        public bool isMine;

        private Quaternion origin_rotation;

        #endregion

        #region Monobehaviour Callbacks

        private void Start()
        {
            origin_rotation = transform.localRotation;
        }

        private void Update()
        {
            if(Pause.paused)
            {
                return;
            }

            UpdateSway();
        }

        #endregion

        #region Private Methods

        private void UpdateSway()
        {
            //controls
            float temp_x_mouse = Input.GetAxis("Mouse X");
            float temp_y_mouse = Input.GetAxis("Mouse Y");

            if(!isMine)
            {
                temp_x_mouse = 0;
                temp_y_mouse = 0;
            }

            //calculate target rotation
            Quaternion temp_x_adjustment = Quaternion.AngleAxis(-intensity * temp_x_mouse,Vector3.up);
            Quaternion temp_y_adjustment = Quaternion.AngleAxis(intensity * temp_y_mouse, Vector3.right);
            Quaternion target_rotation = origin_rotation * temp_x_adjustment * temp_y_adjustment;

            //rotate towards target rotation
            transform.localRotation = Quaternion.Lerp(transform.localRotation, target_rotation, Time.deltaTime * smooth);
        }

        #endregion
    }
}