using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureTest : MonoBehaviour {
    private RawImage rawImage;
    public Coord[] players;
    public Coord[] enemies;
    public float intensityMax = 5;
    public int texResolution = 32;

    private Map<Color> map;
    private Texture2D tex;

    private void Start() {
        map = new Map<Color>(texResolution, texResolution, Color.black);
        tex = new Texture2D(texResolution, texResolution);

        tex.filterMode = FilterMode.Point;
        MapProcess(map);
        WriteOnTexture(map, tex);
        rawImage = GetComponent<RawImage>();
        rawImage.texture = tex;
    }

    private void Update() {
        map = new Map<Color>(texResolution, texResolution, Color.black);
        MapProcess(map);
        WriteOnTexture(map, tex);
        rawImage.texture = tex;
    }

    void MapProcess(Map<Color> map) {
        map.MapIter((v, c) => {
            foreach (var coord in players) {
                var dist = coord.Dist(c);
                var inf = Mathf.Clamp01((intensityMax - dist) / intensityMax);
                map[c] = new Color(map[c].r, 0, Mathf.Max(map[c].b, inf));
            }

            foreach (var coord in enemies) {
                var dist = coord.Dist(c);
                var inf = Mathf.Clamp01((intensityMax - dist) / intensityMax);
                map[c] = new Color(Mathf.Max(map[c].r, inf), 0, map[c].b);
            }

            var col = map[c];
            var g = col.b * col.r;
            map[c] = new Color(col.r, g, col.b);
        });
    }

    void WriteOnTexture(Map<Color> map, Texture2D tex) {
        map.MapIter((v, x, y) => {
            tex.SetPixel(x, y, v);
        });

        tex.Apply();
    }
}
