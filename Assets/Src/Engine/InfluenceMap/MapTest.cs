using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTest : MonoBehaviour {
    public TextAsset text;

	void Start () {
        var map = Map<int>.ReadCSVRaw(text.text);
        print(map);
	}
}
