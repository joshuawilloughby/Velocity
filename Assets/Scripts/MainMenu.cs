using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Josh.Velocity
{
    public class MainMenu : MonoBehaviour
    {
        public Launcher launcher;

        public Animator anim;

        #region Main tabs

        [Header("Solo")]
        public Text text1;
        public GameObject image1;

        [Header("Multiplayer")]
        public Text text2;
        public GameObject image2;

        [Header("Customise")]
        public Text text3;
        public GameObject image3;

        [Header("Store")]
        public Text text4;
        public GameObject image4;

        [Header("Quit")]
        public Text text5;
        public GameObject image5;

        #endregion

        #region Matchmaking Tabs

        [Header("Join Match")]
        public Text text6;
        public GameObject image6;

        [Header("Create Match")]
        public Text text7;
        public GameObject image7;

        [Header("Back")]
        public Text text8;
        public GameObject image8;

        #endregion

        #region Searching Tabs

        [Header("Cancel")]
        public Text text9;
        public GameObject image9;

        #endregion


        #region Store Tabs

        [Header("Close")]
        public Text text10;
        public GameObject image10;

        #endregion


        private void Start()
        {
            image1.SetActive(false);
            image2.SetActive(false);
            image3.SetActive(false);
            image4.SetActive(false);
            image5.SetActive(false);
            image6.SetActive(false);
            image7.SetActive(false);
            image8.SetActive(false);
            image9.SetActive(false);
            image10.SetActive(false);

            Pause.paused = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        public void JoinMatch()
        {
            launcher.Join();
        }
        public void CreateMatch()
        {
            launcher.Create();
        }


        #region HoverOvers

        public void HoverOverOption1()
        {
            text1.rectTransform.localPosition = new Vector3(0f, 0f, 0f);
            image1.SetActive(true);
        }

        public void HoverOverOption2()
        {
            text2.rectTransform.localPosition = new Vector3(0f, 0f, 0f);
            image2.SetActive(true);
        }

        public void HoverOverOption3()
        {
            text3.rectTransform.localPosition = new Vector3(0f, 0f, 0f);
            image3.SetActive(true);
        }

        public void HoverOverOption4()
        {
            text4.rectTransform.localPosition = new Vector3(0f, 0f, 0f);
            image4.SetActive(true);
        }
        public void HoverOverOption5()
        {
            text5.rectTransform.localPosition = new Vector3(0f, 0f, 0f);
            image5.SetActive(true);
        }

        public void HoverOverOption6()
        {
            text6.rectTransform.localPosition = new Vector3(0f, 0f, 0f);
            image6.SetActive(true);
        }

        public void HoverOverOption7()
        {
            text7.rectTransform.localPosition = new Vector3(0f, 0f, 0f);
            image7.SetActive(true);
        }

        public void HoverOverOption8()
        {
            text8.rectTransform.localPosition = new Vector3(0f, 0f, 0f);
            image8.SetActive(true);
        }

        public void HoverOverOption9()
        {
            text9.rectTransform.localPosition = new Vector3(0f, 0f, 0f);
            image9.SetActive(true);
        }

        public void HoverOverOption10()
        {
            anim.SetBool("FeaturedHoverOver", true);
        }

        public void HoverOverOption11()
        {
            anim.SetBool("CodeHoverOver", true);
        }

        public void HoverOverOption12()
        {
            anim.SetBool("NewHoverOver", true);
        }

        public void HoverOverOption13()
        {
            anim.SetBool("CratesHoverOver", true);
        }


        public void HoverOverOption14()
        {
            text10.rectTransform.localPosition = new Vector3(30f, 0f, 0f);
            image10.SetActive(true);
        }
        #endregion


        #region Hover Over Ends

        public void FinishHoverOverOption1()
        {
            text1.rectTransform.localPosition = new Vector3(-25f, 0f, 0f);
            image1.SetActive(false);
        }

        public void FinishHoverOverOption2()
        {
            text2.rectTransform.localPosition = new Vector3(-25f, 0f, 0f);
            image2.SetActive(false);
        }

        public void FinishHoverOverOption3()
        {
            text3.rectTransform.localPosition = new Vector3(-25f, 0f, 0f);
            image3.SetActive(false);
        }

        public void FinishHoverOverOption4()
        {
            text4.rectTransform.localPosition = new Vector3(-25f, 0f, 0f);
            image4.SetActive(false);
        }

        public void FinishHoverOverOption5()
        {
            text5.rectTransform.localPosition = new Vector3(-25f, 0f, 0f);
            image5.SetActive(false);
        }

        public void FinishHoverOverOption6()
        {
            text6.rectTransform.localPosition = new Vector3(-25f, 0f, 0f);
            image6.SetActive(false);
        }

        public void FinishHoverOverOption7()
        {
            text7.rectTransform.localPosition = new Vector3(-25f, 0f, 0f);
            image7.SetActive(false);
        }

        public void FinishHoverOverOption8()
        {
            text8.rectTransform.localPosition = new Vector3(-25f, 0f, 0f);
            image8.SetActive(false);
        }

        public void FinishHoverOverOption9()
        {
            text9.rectTransform.localPosition = new Vector3(-25f, 0f, 0f);
            image9.SetActive(false);
        }

        public void FinishHoverOverOption10()
        {
            anim.SetBool("FeaturedHoverOver", false);
        }

        public void FinishHoverOverOption11()
        {
            anim.SetBool("CodeHoverOver", false);
        }

        public void FinishHoverOverOption12()
        {
            anim.SetBool("NewHoverOver", false);
        }

        public void FinishHoverOverOption13()
        {
            anim.SetBool("CratesHoverOver", false);
        }

        public void FinishHoverOverOption14()
        {
            text10.rectTransform.localPosition = new Vector3(10f, 0f, 0f);
            image10.SetActive(false);
        }

        #endregion

        public void QuitGame()
        {
            //quit in the editor

            //EditorApplication.isPlaying = false;

            //else

            Application.Quit();
        }
    }

}
