/////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2021 Tegridy Ltd                                          //
// Author: Darren Braviner                                                 //
// Contact: db@tegridygames.co.uk                                          //
/////////////////////////////////////////////////////////////////////////////
//                                                                         //
// This program is free software; you can redistribute it and/or modify    //
// it under the terms of the GNU General Public License as published by    //
// the Free Software Foundation; either version 2 of the License, or       //
// (at your option) any later version.                                     //
//                                                                         //
// This program is distributed in the hope that it will be useful,         //
// but WITHOUT ANY WARRANTY.                                               //
//                                                                         //
/////////////////////////////////////////////////////////////////////////////
//                                                                         //
// You should have received a copy of the GNU General Public License       //
// along with this program; if not, write to the Free Software             //
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston,              //
// MA 02110-1301 USA                                                       //
//                                                                         //
/////////////////////////////////////////////////////////////////////////////

using System.Collections;
using UnityEngine;
using Tegridy.Tools;
namespace Tegridy.PuzzleMatch
{
    public class TegridyPuzzleMatchMenu : MonoBehaviour
    {
        public TegridyPuzzleMatchGUIMenu gameMenu; //menu ui
        public TegridyPuzzleMatch control; //used for tracking the game state
        
        GameObject[] prefabHolder; // used for keeping track of the level prefabs
        int maxLevel; //the max level the player can start
        int currentLevel; //the current level the player can play upto
        private void Start()
        {
            OpenLevelSelect();
        }
        public void OpenLevelSelect()
        {
            //Add some listeners and create the prefabs for our level select
            gameMenu.close.onClick.AddListener(() => CloseStartScreen());
            UITools.SetButtonText(gameMenu.close, LanguageController.quit);
            UITools.SetText(gameMenu.title, LanguageController.gameName);

            prefabHolder = UITools.DrawStraight(gameMenu.levelPrefab.gameObject, control.levels.Length, gameMenu.prefabSpacing, false, true, gameMenu.levels);
            for (int i = 0; i < prefabHolder.Length; i++)
            {
                int val = i;
                //setup the prefabs to display the level info
                TegridyPuzzleMatchGUILevelPrefab thisLevel = prefabHolder[val].GetComponent<TegridyPuzzleMatchGUILevelPrefab>();
                if (control.levels[i].levelIcon != null) thisLevel.levelPic.sprite = control.levels[val].levelIcon;
                UITools.SetText(thisLevel.title, LanguageController.level + (i + 1).ToString());
                UITools.SetText(thisLevel.score, LanguageController.score + "<br>" + control.levels[val].bestScore.ToString());
                UITools.SetText(thisLevel.clicks, LanguageController.clicks + "<br>" + control.levels[val].bestClicks.ToString());
                UITools.SetText(thisLevel.time, LanguageController.time + "<br>" + control.levels[val].bestTime.ToString());

                thisLevel.launch.onClick.AddListener(() => StartLevel(val));
                UITools.SetButtonText(thisLevel.launch, LanguageController.start);

                if (i > maxLevel) thisLevel.launch.interactable = false;
            }
            gameMenu.startScreen.SetActive(true);
        }
        private void CloseStartScreen()
        {
            gameMenu.startScreen.SetActive(false);
            gameMenu.close.onClick.RemoveAllListeners();
            if (prefabHolder != null)
            {
                foreach (GameObject old in prefabHolder)
                {
                    Destroy(old);
                }
                prefabHolder = null;
            }
        }
        private void StartLevel(int level)
        {
            //Start the new GUI and the game controller
            CloseStartScreen();
            control.StartMatch(level, true, this);
            currentLevel = level;
        }
        public void CloseLevel(float time, int click, int score, bool winner)
        {
            control.levels[currentLevel].ui.SetActive(false);
            if (winner)
            {
                //check to see if these are better than the old scores
                float newTime = control.levels[currentLevel].settings.matchTime - time;
                if (control.levels[currentLevel].bestTime == 0) control.levels[currentLevel].bestTime = time;
                if (newTime < control.levels[currentLevel].bestTime) control.levels[currentLevel].bestTime = newTime;

                //make sure clicks arent at zero
                if (control.levels[currentLevel].bestClicks == 0) control.levels[currentLevel].bestClicks = click;
                if (click < control.levels[currentLevel].bestClicks) control.levels[currentLevel].bestClicks = click;

                //check the final score
                if (score > control.levels[currentLevel].bestScore) control.levels[currentLevel].bestScore = score;

                if (currentLevel == maxLevel) maxLevel++;

                OpenLevelSelect();
            }
            else StartCoroutine(LevelFailed());
        }
        private IEnumerator LevelFailed()
        {
            //Display the game over info
            gameMenu.infoScreen.SetActive(true);

            UITools.SetText(gameMenu.infoTitle, LanguageController.failed);
            UITools.SetText(gameMenu.infoDescription, LanguageController.failedReasons[Random.Range(0, LanguageController.failedReasons.Length)]);

            yield return new WaitForSeconds(3f);
            gameMenu.infoScreen.SetActive(false);
            OpenLevelSelect();
        }
    }
}