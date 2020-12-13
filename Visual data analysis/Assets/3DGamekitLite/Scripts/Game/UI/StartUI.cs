﻿using System.Collections;
using System.Collections.Generic;
using Gamekit3D;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Gamekit3D
{
    public class StartUI : SaveData
    {
        public bool alwaysDisplayMouse;
        public GameObject pauseCanvas;
        public GameObject optionsCanvas;
        public GameObject controlsCanvas;
        public GameObject audioCanvas;
        public GameObject heatmapCanvas;

        protected bool m_InPause;
        protected PlayableDirector[] m_Directors;

        // UI bool ===========================================
        bool active_position = false;
        bool active_jump = false;
        bool active_attack = false;
        bool active_death = false;
        bool active_damaged = false;
        bool active_ui = false;
        bool active_enemy_damaged = false;
        bool active_enemy_killed = false;
        public List<GameObject> buttons = new List<GameObject>();
        // ===================================================


        void Start()
        {
            if (!alwaysDisplayMouse)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            m_Directors = FindObjectsOfType<PlayableDirector> ();
        }

        public void Quit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
		    Application.Quit();
#endif
        }

        public void ExitPause()
        {
            m_InPause = true;
            SwitchPauseState();
        }

        public void RestartLevel()
        {
            m_InPause = true;
            SwitchPauseState();
            SceneController.RestartZone();
        }

        void Update()
        {
            if (PlayerInput.Instance != null && PlayerInput.Instance.Pause)
            {
                SwitchPauseState();
            }           
        }

        protected void SwitchPauseState()
        {
            if (m_InPause && Time.timeScale > 0 || !m_InPause && ScreenFader.IsFading)
                return;

            if (!alwaysDisplayMouse)
            {
                Cursor.lockState = m_InPause ? CursorLockMode.Locked : CursorLockMode.None;
                Cursor.visible = !m_InPause;
            }

            for (int i = 0; i < m_Directors.Length; i++)
            {
                if (m_Directors[i].state == PlayState.Playing && !m_InPause)
                {
                    m_Directors[i].Pause ();
                }
                else if(m_Directors[i].state == PlayState.Paused && m_InPause)
                {
                    m_Directors[i].Resume ();
                }
            }
            
            if(!m_InPause)
                CameraShake.Stop ();

            if (m_InPause)
                PlayerInput.Instance.GainControl();
            else
                PlayerInput.Instance.ReleaseControl();

            Time.timeScale = m_InPause ? 1 : 0;

            if (pauseCanvas)
                pauseCanvas.SetActive(!m_InPause);

            if (optionsCanvas)
                optionsCanvas.SetActive(false);

            if (controlsCanvas)
                controlsCanvas.SetActive(false);

            if (audioCanvas)
                audioCanvas.SetActive(false);

            if (heatmapCanvas)
                heatmapCanvas.SetActive(false);


            if (!m_InPause)
            {
                //=========================================================================================
                Dictionary<string, object> myDic = new Dictionary<string, object>();

                //Type
                myDic.Add("Type", "OpenMenu");

                //Transform
                myDic.Add("PositionX", transform.position.x);
                myDic.Add("PositionY", transform.position.y);
                myDic.Add("PositionZ", transform.position.z);

                //TimeStamp
                myDic.Add("TimeStamp", Time.time);

                //PlayerID
                myDic.Add("PlayerID", /*PlayerData.player_id*/0);

                PlayerEventTrack.EventList.Add(myDic);
                Debug.Log("Player OpenMenu");
                //=========================================================================================
            }

            m_InPause = !m_InPause;
        }

        public void SaveDataButton()
        {
            SavePosition();
            SaveEvents();
            PlayerEventTrack.EventData = ReadData.Read("Saved_data");
            PlayerEventTrack.PositiontData = ReadData.Read("Position_data");

            //TODO: Christt stuff
        }

   
        public void PositionButton()
        {
            ResetButtonsHeatMap();
            active_position = !active_position;
            ActiveButtonHeatMap(active_position, "ShowPositionDataButtonCanvas");
        }
        public void DeathButton()
        {
            ResetButtonsHeatMap();
            active_death = !active_death;
            ActiveButtonHeatMap(active_death, "ShowDeathDataButtonCanvas");
        }
        public void AttackButton()
        {
            ResetButtonsHeatMap();
            active_attack = !active_attack;
            ActiveButtonHeatMap(active_attack, "ShowAttackDataButtonCanvas");
        }
        public void JumpButton()
        {
            ResetButtonsHeatMap();
            active_jump = !active_jump;
            ActiveButtonHeatMap(active_jump, "ShowJumpkDataButtonCanvas");
        }
        public void DamagedButton()
        {
            ResetButtonsHeatMap();
            active_damaged = !active_damaged;
            ActiveButtonHeatMap(active_damaged, "ShowDamagedDataButtonCanvas");       
        }
        public void EnemyKilledButton()
        {
            ResetButtonsHeatMap();
            active_enemy_killed = !active_enemy_killed;
            ActiveButtonHeatMap(active_enemy_killed, "ShowEnemyKilledDataButtonCanvas");       
        }
        public void EnemyDamagedButton()
        {
            ResetButtonsHeatMap();
            active_enemy_damaged = !active_enemy_damaged;
            ActiveButtonHeatMap(active_enemy_damaged, "ShowEnemyDamagedDataButtonCanvas");         
        }
        public void UIButton()
        {
            ResetButtonsHeatMap();
            active_ui = !active_ui;
            ActiveButtonHeatMap(active_ui, "ShowUiInteractionDataButtonCanvas");
        }      


        private void ChangeTextColor(bool active, TMP_Text textmeshPro)
        {
            if (active)
                textmeshPro.color = new Color32(0, 102, 204, 255);
            else
                textmeshPro.color = new Color32(255, 255, 255, 255);

        }

        private void ResetButtonsHeatMap()
        {
            foreach (var item in buttons)
            {
                TMP_Text textmeshPro = item.GetComponentInChildren<TMP_Text>();
                ChangeTextColor(false, textmeshPro);
            }

            active_position = false;
            active_jump = false;
            active_attack = false;
            active_death = false;
            active_damaged = false;
            active_ui = false;
            active_enemy_damaged = false;
            active_enemy_killed = false;
        }

        private void ActiveButtonHeatMap(bool active, string name)
        {
            foreach (var item in buttons)
            {
                if(item.name == name)
                {
                    TMP_Text textmeshPro = item.GetComponentInChildren<TMP_Text>();
                    ChangeTextColor(active, textmeshPro);
                }                
            }
        }
    }
}
