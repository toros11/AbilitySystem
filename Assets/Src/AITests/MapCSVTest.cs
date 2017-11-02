using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapCSVTest : MonoBehaviour {
    public TextAsset text;
    private RawImage rawImage;


    private void Start() {
        rawImage = GetComponent<RawImage>();
        var map = Map<int>.ReadCSV(text.text);
        var tex = new Texture2D(map.width, map.height);
        tex.filterMode = FilterMode.Point;

        map.MapIter((v, coord) => {
            tex.SetPixel(coord.y, map.width - coord.x - 1, Color.Lerp(Color.black, Color.white, v / 9f));
        });

        tex.Apply();

        rawImage.texture = tex;
    }
}
