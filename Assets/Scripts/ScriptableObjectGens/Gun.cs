using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Josh.Velocity
{
    [CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
    public class Gun : ScriptableObject
    {
        #region Variables

        public new string name;
        public int damage;
        public int ammo;
        public int burst; // 0 - semi | 1 - auto | 2 - burst
        public int pellets;
        public int clipsize;
        public float firerate;
        public float bloom;
        public float recoil;
        public float kickback; 
        public float aimSpeed;
        public float reload;
        
        public GameObject prefab;

        private int stash; //current ammo
        private int clip; //current clip

        public void Initialize()
        {
            stash = ammo;
            clip = clipsize;
        }

        public bool FireBullet()
        {
            if (clip > 0)
            {
                clip -= 1;
                return true;
            }
            else return false;
        }

        public void Reload()
        {
            stash += clip;
            clip = Mathf.Min(clipsize, stash);
            stash -= clip; 
        }

        public int GetStash() { return stash; }
        public int GetClip() { return clip; }

        #endregion

        #region Monobehaviour Callbacks



        #endregion

        #region Private Methods



        #endregion
    }
}

