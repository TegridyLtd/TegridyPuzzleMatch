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
using TMPro;
using UnityEngine.UI;
namespace Tegridy.PuzzleMatch
{
    public class TegridyPuzzleMatchGUILevel : MonoBehaviour
    {
        [Header("UI Elements")]
        public RectTransform problemRect;
        public RectTransform solutionRect;
        public Image[] problem;
        public Image[] solution;
        public Button[] buttons;
        public Button close;

        [Header("Game Info")]
        public TextMeshProUGUI time;
        public TextMeshProUGUI score;
        public TextMeshProUGUI clicks;
        public TextMeshProUGUI title;
        public TextMeshProUGUI round;

        [Header("Problem Colours")]
        public Color32[] colors;

        [Header("Puzzle Rotations")]
        public Vector3[] rotationsProblem;
        public Vector3[] rotationsSolution;
    }
}