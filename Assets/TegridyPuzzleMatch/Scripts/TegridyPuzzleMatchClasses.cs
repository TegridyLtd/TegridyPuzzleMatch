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

using UnityEngine;
using System.Collections.Generic;
namespace Tegridy.PuzzleMatch
{
    [System.Serializable] public class TegridyPuzzleMatchAudio
    {
        public AudioClip[] click;
        public AudioClip[] roundComplete;
        public AudioClip[] matchComplete;
        public AudioClip[] matchFailed;
    }
    [System.Serializable] public class ButtonLevel
    {
        [Header("Settings")]
        public Sprite levelIcon;
        public string levelName;
        public TegridyPuzzleMatchGUILevel ui;
        public TegridyPuzzleMatchSettings settings;
        public TegridyPuzzleMatchAudio audio;

        [Header("Info")]
        public int bestScore;
        public float bestTime;
        public int bestClicks;
        public List<TegridyPuzzleMatchResults> results;
    }
    [System.Serializable] public class TegridyPuzzleMatchSettings
    {
        [Header("MiniGameSettings")]
        public int startingScore;
        public int clickDecrease;
        public int timeDecrease;

        [Header("Round Setting")]
        public int rounds;
        public int dificulty;
        public bool increaseDificulty;
        public bool rotateProblem;
        public bool rotateSolution;
        public bool resetColors;
        public bool timer;
        public float matchTime;
        public bool resetTime;
    }

    [System.Serializable] public class TegridyPuzzleMatchResults
    {
        public int lvl;
        public bool winner;
        public int round;
        public int maxRound;
        public float time;
        public int clicks;
        public int score;
    }
}