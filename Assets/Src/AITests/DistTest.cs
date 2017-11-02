using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistTest : MonoBehaviour {
    public Coord a, b;

	void Start () {
        print(a.TileDist(b));
	}
}
