using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCSVTest : MonoBehaviour {
    public TextAsset text;

    private void Start() {
        var map = Map<int>.ReadCSV(text.text);
        print(Map<int>.ToCSV(map));
    }
}
