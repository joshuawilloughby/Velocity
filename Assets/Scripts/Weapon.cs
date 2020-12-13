using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

namespace Com.Josh.Velocity
{
    public class Weapon : MonoBehaviourPunCallbacks
    {
        #region Variables

        public List<Gun> loadout;
        [HideInInspector] public Gun currentGunData;

        public Transform weaponParent;
        public GameObject bulletholePrefab;
        public LayerMask canBeShot;
        public bool isAiming = false;

        private float currentCooldown;
        private int currentIndex;
        public GameObject currentWeapon;

        private bool isReloading = false;

        #endregion

        #region Monobehaviour Callbacks

        private void Start()
        {
            foreach (Gun a in loadout) a.Initialize();
            Equip(0);
        }

        void Update()
        {
            if (photonView.IsMine && Input.GetKeyDown(KeyCode.Alpha1))
            {
                photonView.RPC("Equip", RpcTarget.All, 0);
            }

            if (photonView.IsMine && Input.GetKeyDown(KeyCode.Alpha2))
            {
                photonView.RPC("Equip", RpcTarget.All, 1);
            }

            if (photonView.IsMine && Input.GetKeyDown(KeyCode.Alpha3))
            {
                photonView.RPC("Equip", RpcTarget.All, 2);
            }

            if (currentWeapon != null)
            {
                if (photonView.IsMine)
                {
                    Aim(Input.GetMouseButton(1));

                    if (loadout[currentIndex].burst != 1)
                    {
                        if (Input.GetMouseButtonDown(0) && currentCooldown <= 0 && !isReloading)
                        {
                            if (loadout[currentIndex].FireBullet())
                            {
                                photonView.RPC("Shoot", RpcTarget.All);
                                Pump();
                            }
                            else
                            {
                                StartCoroutine(Reload(loadout[currentIndex].reload));
                            }
                        }
                    }
                    else
                    {
                        if (Input.GetMouseButton(0) && currentCooldown < 0 && !isReloading)
                        {
                            if (loadout[currentIndex].FireBullet())
                            {
                                photonView.RPC("Shoot", RpcTarget.All);
                            }
                            else
                            {
                                StartCoroutine(Reload(loadout[currentIndex].reload));
                            }
                        }
                    }

                    if (Input.GetKey(KeyCode.R) && !isReloading)
                    {
                        photonView.RPC("ReloadRPC", RpcTarget.All);
                    }

                    //cooldown
                    if (currentCooldown > 0)
                    {
                        currentCooldown -= Time.deltaTime;
                    }
                }

                //weapon position elasticity
                currentWeapon.transform.localPosition = Vector3.Lerp(currentWeapon.transform.localPosition, Vector3.zero, Time.deltaTime * 4f);
                     
            }
        }

        #endregion

        #region Private Methods

        [PunRPC]
        private void ReloadRPC()
        {
            StartCoroutine(Reload(loadout[currentIndex].reload));
        }


        IEnumerator Reload (float p_wait)
        {
            isReloading = true;

            if(currentWeapon.transform.Find("Anchor/Design/Reload").GetComponent<Animator>())
            {
                currentWeapon.transform.Find("Anchor/Design/Reload").GetComponent<Animator>().Play("Reload", 0, 0);
            }
            else
            {
                currentWeapon.SetActive(false);
            }
            
            yield return new WaitForSeconds(p_wait);

            loadout[currentIndex].Reload();
            currentWeapon.SetActive(true);
            isReloading = false;
        }

        private void Pump ()
        {
            if (currentWeapon.transform.Find("Anchor/Design/Pump").GetComponent<Animator>())
            {
                currentWeapon.transform.Find("Anchor/Design/Pump").gameObject.SetActive(true);

                currentWeapon.transform.Find("Anchor/Design/Pump").GetComponent<Animator>().Play("Pump", 0, 0);
            }
            else
            {
                return;
            }

            currentWeapon.transform.Find("Anchor/Design/Pump").gameObject.SetActive(false);
        }

        [PunRPC]
        void Equip(int p_ind)
        {
            if (currentWeapon != null)
            {
                if (isReloading)
                {
                    StopCoroutine("Reload");
                }
                Destroy(currentWeapon);
            }

            currentIndex = p_ind;

            GameObject temp_newEquipment = Instantiate(loadout[p_ind].prefab, weaponParent.position, weaponParent.rotation, weaponParent) as GameObject;
            temp_newEquipment.transform.localPosition = Vector3.zero;
            temp_newEquipment.transform.localEulerAngles = Vector3.zero;
            temp_newEquipment.GetComponent<Sway>().isMine = photonView.IsMine;

            currentWeapon = temp_newEquipment;
            currentGunData = loadout[p_ind];
        }

        public bool Aim(bool p_isAiming)
        {
            if (!currentWeapon)
            {
                return false;
            }

            if (isReloading)
            {
                p_isAiming = false;
            }

            isAiming = p_isAiming;
            Transform temp_anchor = currentWeapon.transform.Find("Anchor");
            Transform temp_state_ads = currentWeapon.transform.Find("States/ADS");
            Transform temp_state_hip = currentWeapon.transform.Find("States/Hip");

            if (p_isAiming)
            {
                //aim
                temp_anchor.position = Vector3.Lerp(temp_anchor.position,temp_state_ads.position, Time.deltaTime * loadout[currentIndex].aimSpeed);
            }
            else
            {
                //hip
                temp_anchor.position = Vector3.Lerp(temp_anchor.position, temp_state_hip.position, Time.deltaTime * loadout[currentIndex].aimSpeed);
            }

            return p_isAiming;
        }

        [PunRPC]
        void Shoot()
        {
            Transform temp_spawn = transform.Find("Cameras/Normal Camera");

            //cooldown
            currentCooldown = loadout[currentIndex].firerate;

            for (int i = 0; i < Mathf.Max(1, currentGunData.pellets); i++)
            {
                //bloom
                Vector3 temp_bloom = temp_spawn.position + temp_spawn.forward * 1000f;
                temp_bloom += Random.Range(-loadout[currentIndex].bloom, loadout[currentIndex].bloom) * temp_spawn.up;
                temp_bloom += Random.Range(-loadout[currentIndex].bloom, loadout[currentIndex].bloom) * temp_spawn.right;
                temp_bloom += temp_spawn.position;
                temp_bloom.Normalize();

                //raycast
                RaycastHit temp_hit = new RaycastHit();
                if (Physics.Raycast(temp_spawn.position, temp_bloom, out temp_hit, 1000f, canBeShot))
                {
                    GameObject temp_newHole = Instantiate(bulletholePrefab, temp_hit.point + temp_hit.normal * 0.001f, Quaternion.identity) as GameObject;
                    temp_newHole.transform.LookAt(temp_hit.point + temp_hit.normal);
                    Destroy(temp_newHole, 5f);

                    if (photonView.IsMine)
                    {
                        //shooting other player on network
                        if (temp_hit.collider.transform.gameObject.layer == 11)
                        {
                            //RPC Call to damage player
                            temp_hit.collider.transform.root.gameObject.GetPhotonView().RPC("TakeDamage", RpcTarget.All, loadout[currentIndex].damage);
                        }
                    }
                }
            }
            
            //gun fx
            currentWeapon.transform.Rotate(-loadout[currentIndex].recoil, 0, 0);
            currentWeapon.transform.position -= currentWeapon.transform.forward * loadout[currentIndex].kickback;
        }

        [PunRPC]
        private void TakeDamage(int p_damage)
        {
            GetComponent<Player>().TakeDamage(p_damage);
        }

        #endregion

        #region Public Methods

        public void RefreshAmmo (Text p_text)
        {
            int temp_clip = loadout[currentIndex].GetClip();
            int temp_stash = loadout[currentIndex].GetStash();

            p_text.text = temp_clip.ToString("00") + " / " + temp_stash.ToString("00");
        }

        #endregion
    }
}