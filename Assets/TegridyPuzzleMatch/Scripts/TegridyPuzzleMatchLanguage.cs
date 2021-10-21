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

namespace Tegridy.PuzzleMatch
{
    public static class LanguageController
    {
        public static string gameName = "PuzzleMatch";
        public static string score = "Score: ";
        public static string time = "Time: ";
        public static string round = "Round: ";
        public static string clicks = "Clicks: ";
        public static string failed = "Failed";
        public static string[] failedReasons = {"You Lose", "You Suck", "Better Luck Next Time", "Etc etc" };
        public static string level = "Level: ";
        public static string start = "Start";
        public static string quit = "Exit";
    }
}