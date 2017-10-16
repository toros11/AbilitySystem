using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASTest : MonoBehaviour {
    public TextAsset text;

    private void Start() {
        var map = Map<int>.ReadCSV(text.text);
        var pf = new PathFind();
        print(map);
        var path = pf.FindShortestPah(map, new Coord(0, 0), new Coord(2, 2));
        print(path.Count);
        foreach (var c in path)
            print(c);
    }
}
