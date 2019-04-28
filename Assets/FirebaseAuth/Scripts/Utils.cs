﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

static public class Utils {

	static public string REMEMBER_ME = "REMEMBER_ME";
	static public string LOGGED = "LOGGED"; //1: Email	2: Google	3:Facebook
	static public int NONE = 0;
	static public int EM = 1;
	static public int GG = 2;
	static public int FB = 3;
	static public int TW = 4;
	static public int AM = 5;
	static public int PH = 6;
    public const string MatchEmailPattern =
        @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
        + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
        + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
        + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";
}
