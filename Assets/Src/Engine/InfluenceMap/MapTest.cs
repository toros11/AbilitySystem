﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTest : MonoBehaviour {
	void Start () {
        var map = new Map<int>(32, 32, 0);
        print(map);
	}
}
