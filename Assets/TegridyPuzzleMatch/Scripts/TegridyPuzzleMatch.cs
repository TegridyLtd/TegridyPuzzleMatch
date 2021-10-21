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
using System.Collections.Generic;
using UnityEngine;
using System;
using Tegridy.Tools;
namespace Tegridy.PuzzleMatch
{
    public class TegridyPuzzleMatch : MonoBehaviour
    {
        public ButtonLevel[] levels;
        public TegridyPuzzleMatchAudio defaultAudio;

        //References the level we are using
        int currentLevel;
        TegridyPuzzleMatchGUILevel gameUI;
        TegridyPuzzleMatchSettings options;
        TegridyPuzzleMatchAudio sound;

        //used for keeping track
        int[] currentColor;
        int currentRound;
        int clicks;
        int score;
        float finalTime;
        bool miniComplete;
        float currCountdownValue;

        //systems
        AudioSource audioSource;
        TegridyPuzzleMatchMenu hostMenu;
        bool returnMenu;
        public void Awake()
        {
            for (int i = 0; i < levels.Length; i++)
            {
                levels[i].results = new List<TegridyPuzzleMatchResults>();
                if (levels[i].audio.click == null) levels[i].audio.click = defaultAudio.click;
                if (levels[i].audio.matchComplete == null) levels[i].audio.matchComplete = defaultAudio.matchComplete;
                if (levels[i].audio.roundComplete == null) levels[i].audio.roundComplete = defaultAudio.roundComplete;
                if (levels[i].audio.matchFailed == null) levels[i].audio.matchFailed = defaultAudio.matchFailed;
                levels[i].ui.SetActive(false);
            }
        }
        public void StartMatch(int level, bool return2Menu, TegridyPuzzleMatchMenu menu)
        {
            if (audioSource == null) audioSource = this.gameObject.AddComponent<AudioSource>();
            hostMenu = menu;
            returnMenu = return2Menu;
            currentLevel = level;
            gameUI = levels[level].ui;
            options = levels[level].settings;
            sound = levels[level].audio;

            //get our player UI
            Array.Resize(ref currentColor, gameUI.buttons.Length);

            //set up the game variable
            currentRound = 0;
            miniComplete = false;
            clicks = 0;
            score = options.startingScore;

            //set the starting strings
            UITools.SetText(gameUI.title, levels[level].levelName);
            UITools.SetText(gameUI.clicks, LanguageController.clicks + clicks.ToString());
            UITools.SetText(gameUI.score, LanguageController.score  + score.ToString());
            UITools.SetText(gameUI.time, LanguageController.time);
            UITools.SetText(gameUI.round, LanguageController.round + (currentRound + 1).ToString() + "/" + options.rounds.ToString());
            UITools.SetButtonText(gameUI.close, LanguageController.quit);

            //make sure we have enough colours for the settings
            if (options.dificulty > gameUI.colors.Length) options.dificulty = gameUI.colors.Length;
            SetDefaultcolor();
            AddListeners();
            GeneratePuzzle();

            //set the timer if we are using one for this level and enable the UI
            if (options.timer == true) StartCoroutine(StartCountdown(options.matchTime));
            gameUI.SetActive(true);
        }
        private void CloseUp()
        {
            StopAllCoroutines();
            RemoveListeners();
            if (returnMenu) hostMenu.CloseLevel(finalTime, clicks, score, miniComplete);
            else CustomCloseUp();
        }
        private void CustomCloseUp()
        {
            //what todo when the puzzle is close
        }
        #region UIControl
        private void AddListeners()
        {
            for (int i = 0; i < gameUI.buttons.Length; i++)
            {
                int buttonValue = i;
                gameUI.buttons[i].onClick.AddListener(() => ChangeColor(buttonValue));
            }
            gameUI.close.onClick.AddListener(() => CloseUp());
        }
        private void RemoveListeners()
        {
            for (int i = 0; i < gameUI.buttons.Length; i++)
            {
                gameUI.buttons[i].onClick.RemoveAllListeners();
            }
            gameUI.close.onClick.RemoveAllListeners();
        }
        private void SetDefaultcolor()
        {
            for (int i = 0; i < gameUI.problem.Length; i++)
            {
                currentColor[i] = 0;
                gameUI.problem[i].color = gameUI.colors[currentColor[i]];
            }
        }
        private void ChangeColor(int button)
        {
            currentColor[button]++;
            if (currentColor[button] == options.dificulty) currentColor[button] = 0;

            gameUI.problem[button].color = gameUI.colors[currentColor[button]];
            clicks++;
            score -= options.clickDecrease;

            UITools.SetText(gameUI.clicks, LanguageController.clicks  + clicks.ToString());
            UITools.SetText(gameUI.score, LanguageController.score  + score.ToString());
            AudioTools.PlayOneShot(sound.click, audioSource);
            CheckPuzzle();
        }
        #endregion
        #region GameControl
        private void GeneratePuzzle()
        {
            if (options.increaseDificulty == true)
            {
                options.dificulty++;
                if (options.dificulty >= gameUI.colors.Length - 1)
                {
                    //increase colors
                    options.dificulty = gameUI.colors.Length - 1;
                    //if we maxed dificulty turn on rotation

                    if (options.rotateSolution == true)
                    {
                        //if weve maxed again turn on more rotation
                        if (options.rotateProblem == true)
                        {
                            //if we maxed again....
                            options.resetColors = false;
                        }
                        options.rotateProblem = true;
                    }
                    options.rotateSolution = true;
                }
            }

            //generate new puzzle
            for (int i = 0; i < gameUI.solution.Length; i++)
            {
                gameUI.solution[i].color = gameUI.colors[UnityEngine.Random.Range(0, options.dificulty)];
            }
            //rotate puzzle
            if (options.rotateProblem == true)
            {
                gameUI.problemRect.Rotate(gameUI.rotationsProblem[UnityEngine.Random.Range(0, gameUI.rotationsProblem.Length)]);
            }
            if (options.rotateSolution == true)
            {
                gameUI.solutionRect.Rotate(gameUI.rotationsProblem[UnityEngine.Random.Range(0, gameUI.rotationsSolution.Length)]);
            }

            //reset default colors
            if (options.resetColors == true)
            {
                SetDefaultcolor();
            }
        }
        private void CheckPuzzle()
        {
            //check if we have a match
            bool match = true;
            for (int i = 0; i < gameUI.problem.Length; i++)
            {
                if (gameUI.problem[i].color != gameUI.solution[i].color)
                {
                    match = false;
                }
            }
            //if we do
            if (match == true)
            {
                //move to next round
                currentRound++;
                if (currentRound > options.rounds)
                {
                    //no more rounds
                    PuzzleComplete();
                }
                else
                {
                    //round rewards and generate new puzzle
                    RoundComplete();
                    GeneratePuzzle();
                }
            }
        }
        private void PuzzleComplete()
        {
            AudioTools.PlayOneShot(sound.matchComplete, audioSource);
            miniComplete = true;
            LogResults();
            CloseUp();
        }
        private void PuzzleFailed()
        {
            AudioTools.PlayOneShot(sound.matchFailed, audioSource);
            miniComplete = false;
            LogResults();
            CloseUp();
        }
        private void RoundComplete()
        {
            LogResults();
            if (options.resetTime == true)
            {
                StopCoroutine(StartCountdown(0f));
                currCountdownValue = options.matchTime;
                StartCoroutine(StartCountdown(options.matchTime));
            }
            UITools.SetText(gameUI.round, LanguageController.round + ": " + (currentRound + 1).ToString() + "/" + options.rounds.ToString());
            finalTime = currCountdownValue;
            AudioTools.PlayOneShot(sound.matchComplete, audioSource);
        }
        private IEnumerator StartCountdown(float countdownValue)
        {
            currCountdownValue = countdownValue;
            while (currCountdownValue > 0)
            {
                UITools.SetText(gameUI.score, LanguageController.score  + score.ToString());
                UITools.SetText(gameUI.time, LanguageController.time + currCountdownValue.ToString("F0"));

                yield return new WaitForSeconds(0.1f);
                currCountdownValue -= 0.1f;
                score -= options.timeDecrease;

            }
            PuzzleFailed();
        }
        private void LogResults()
        {
            TegridyPuzzleMatchResults results = new TegridyPuzzleMatchResults();
            results.score = score;
            results.time = currCountdownValue;
            results.round = currentRound;
            results.maxRound = levels[currentLevel].settings.rounds;
            results.clicks = clicks;
            results.winner = miniComplete;
            levels[currentLevel].results.Add(results);
        }
        #endregion
    }
}