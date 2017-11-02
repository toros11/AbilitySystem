using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfluenceMapTexture : MonoBehaviour {
    public int intensityMax = 5;
    public TextAsset textMap;

    private RawImage rawImage;
    private Map<int> rawMap;

    private Map<Color> colorMap;
    private Texture2D tex;

    private void Start() {
        rawImage = GetComponent<RawImage>();
        rawMap = Map<int>.ReadCSV(textMap.text);
        colorMap = new Map<Color>(rawMap.width, rawMap.height, Color.black);
        tex = new Texture2D(rawMap.height, rawMap.width);
        tex.filterMode = FilterMode.Point;
        rawImage.texture = tex;
        MapProcess(colorMap);
        WriteOnTexture(colorMap, tex);
    }

    private void Update() {
        rawMap = Map<int>.ReadCSV(textMap.text);
        colorMap = new Map<Color>(rawMap.width, rawMap.height, Color.black);
        MapProcess(colorMap);
        WriteOnTexture(colorMap, tex);
        rawImage.texture = tex;
    }

    void MapProcess(Map<Color> map) {
        rawMap.MapIter((v, coord) => {
            if(v == 1) {

                Fill(coord, (c, t) => {
                    var col = colorMap[c];
                    col.b += t;
                    col.b = Mathf.Clamp01(col.b);
                    colorMap[c] = col;
                }, intensityMax);
            }

            else if (v == 2) {

                Fill(coord, (c, t) => {
                    var col = colorMap[c];

                    col.r += t;
                    col.r = Mathf.Clamp01(col.r);
                    colorMap[c] = col;
                }, intensityMax);
            }

            else if(v < 0) {
                colorMap[coord] = Color.white;
            }
        });
    }

    struct MapPath {
        public Coord coord;
        public int cost;

        public MapPath(Coord coord, int cost) {
            this.coord = coord;
            this.cost = cost;
        }
    }

    void Fill(Coord coord, Action<Coord, float> onEach, int range) {
        var closedSet = new HashSet<Coord>();
        var openSet = new Queue<MapPath>();
        openSet.Enqueue(new MapPath(coord, 0));

        while (openSet.Count > 0) {
            var c = openSet.Dequeue();

            if (!closedSet.Contains(c.coord) && c.cost < range && rawMap[c.coord] >= 0) {
                closedSet.Add(c.coord);
                onEach(c.coord, 1 - c.cost / (float)range);
                Coord next;

                next = c.coord + Coord.right;
                if(!closedSet.Contains(next) && rawMap.IsInMapRange(next))
                    openSet.Enqueue(new MapPath(next, c.cost+1));

                next = c.coord + Coord.left;
                if (!closedSet.Contains(next) && rawMap.IsInMapRange(next))
                    openSet.Enqueue(new MapPath(next, c.cost + 1));

                next = c.coord + Coord.up;
                if (!closedSet.Contains(next) && rawMap.IsInMapRange(next))
                    openSet.Enqueue(new MapPath(next, c.cost + 1));

                next = c.coord + Coord.down;
                if (!closedSet.Contains(next) && rawMap.IsInMapRange(next))
                    openSet.Enqueue(new MapPath(next, c.cost + 1));
            }           
        }        
    }

    void WriteOnTexture(Map<Color> map, Texture2D tex) {
        map.MapIter((v, x, y) => {
            tex.SetPixel(y, map.width - x - 1, v);
        });

        tex.Apply();
    }
}
