using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Com.Josh.Velocity
{
    public class Look : MonoBehaviourPunCallbacks
    {
        #region Variables

        public static bool cursorLocked = true;

        public Transform player;
        public Transform cams;
        public Transform weapon;

        public float xSensitivity;
        public float ySensitivity;
        public float maxAngle;

        private Quaternion camCenter;

        #endregion

        #region Monobehaviour Callbacks

        void Start()
        {
            camCenter = cams.localRotation; //set rotation origin for cameras to camCenter
        }

        void Update()
        {
            if (!photonView.IsMine)
            {
                return;
            }

            if(Pause.paused)
            {
                return;
            }

            SetY();
            SetX();

            UpdateCursorLock();
        }

        #endregion

        #region Private Methods

        void SetY()
        {
            float temp_input = Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime;
            Quaternion temp_adjust = Quaternion.AngleAxis(temp_input, -Vector3.right);
            Quaternion temp_delta = cams.localRotation * temp_adjust;

            if (Quaternion.Angle(camCenter, temp_delta) < maxAngle)
            {
                cams.localRotation = temp_delta;
            }

            weapon.rotation = cams.rotation;
        }

        void SetX()
        {
            float temp_input = Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime;
            Quaternion temp_adjust = Quaternion.AngleAxis(temp_input, Vector3.up);
            Quaternion temp_delta = player.localRotation * temp_adjust;
            player.localRotation = temp_delta;
        }

        void UpdateCursorLock()
        {
            if (cursorLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

            }
        }

    #endregion
    }
}

