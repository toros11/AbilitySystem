using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Map<T> : IEnumerable<T> {
    public int width { get { return map.GetLength(0); } }
    public int height { get { return map.GetLength(1); } }
    T[,] map;

    public Map(int x, int y) {
        map = new T[x, y];
    }

    public Map(int x, int y, T defaultValue) {
        map = new T[x, y];
        for(int i = 0; i < x; i++) {
            for(int j = 0; j < y; j++) {
                map[i, j] = defaultValue;
            }
        }

    }

    public Map(T[,] map) {
        this.map = map;
    }

    public T this[int x, int y] {
        get {
            return map[x, y];
        }
        set {
            map[x, y] = value;
        }
    }

    public T this[Coord coord] {
        get {
            return map[coord.x, coord.y];
        }
        set {
            map[coord.x, coord.y] = value;
        }
    }


    public Vector3 CoordToWorldPoint(Coord tile) {
        return new Vector3(-width / 2 + .5f + tile.x, -3.5f, -height / 2 + .5f + tile.y);
    }

    public Coord WorldPointToCoord(Vector3 pos) {
        return new Coord(Mathf.FloorToInt(pos.x + width / 2 - .5f), Mathf.FloorToInt(pos.z + height / 2 - .5f));
    }

    public IEnumerator<T> GetEnumerator() {
        for(int x = 0; x < width; x++) {
            for(int y = 0; y < width; y++) {
                yield return map[x, y];
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator() {
        for(int x = 0; x < width; x++) {
            for(int y = 0; y < width; y++) {
                yield return map[x, y];
            }
        }
    }

    /// <summary>
    /// Iterates over all neighbors of a coord
    /// </summary>
    /// <param name="coord"></param>
    /// <param name="onEach"></param>
    public void NeighborIter(Coord coord, UnityAction<T, int, int> onEach) {
        for(int x = coord.x - 1; x <= coord.x + 1; x++) {
            for(int y = coord.y - 1; y <= coord.y + 1; y++) {
                if(map.IsInMapRange(x, y) && (y == coord.y || x == coord.x)) {
                    onEach.Invoke(map[x, y], x, y);
                }
            }
        }
    }

    /// <summary>
    /// Iterates over all neighbors of a coord
    /// </summary>
    /// <param name="coord"></param>
    /// <param name="onEach"></param>
    public void NeighborIter(Coord coord, UnityAction<T, Coord> onEach) {
        for(int x = coord.x - 1; x <= coord.x + 1; x++) {
            for(int y = coord.y - 1; y <= coord.y + 1; y++) {
                if(map.IsInMapRange(x, y) && (y == coord.y || x == coord.x)) {
                    onEach.Invoke(map[x, y], new Coord(x, y));
                }
            }
        }
    }

    /// <summary>
    /// Count all obstacles around the coord
    /// </summary>
    /// <param name="coord"></param>
    /// <param name="obstacle"></param>
    /// <returns></returns>
    public int ObstacleInNeighborHood(Coord coord, T obstacle) {
        int obstacles = 0;
        for(int x = coord.x - 1; x <= coord.x + 1; x++) {
            for(int y = coord.y - 1; y <= coord.y + 1; y++) {
                if(map.IsInMapRange(x, y)) {
                    obstacles += ((map[x, y].Equals(obstacle)) ? 1 : 0);
                }
            }
        }

        return obstacles;
    }

    /// <summary>
    /// Iterates on the map
    /// </summary>
    /// <param name="onEach"></param>
    public void MapIter(UnityAction<T, int, int> onEach) {
        for(int x = 0; x < map.GetLength(0); x++) {
            for(int y = 0; y < map.GetLength(1); y++) {
                onEach.Invoke(map[x, y], x, y);
            }
        }
    }

    /// <summary>
    /// Iterates on the map
    /// </summary>
    /// <param name="onEach"></param>
    public void MapIter(UnityAction<T, Coord> onEach) {
        for (int x = 0; x < map.GetLength(0); x++) {
            for (int y = 0; y < map.GetLength(1); y++) {
                onEach.Invoke(map[x, y], new Coord(x, y));
            }
        }
    }
    
    public bool IsInMapRange(int x, int y) {
        return x >= 0 && x < map.GetLength(0) && y >= 0 && y < map.GetLength(1);
    }

}

[Serializable]
public struct Coord {
    public int x;
    public int y;

    public Coord(int x, int y) {
        this.x = x;
        this.y = y;
    }

    public float DistSqrt(Coord other) {
        return Mathf.Pow(x - other.x, 2) + Mathf.Pow(y - other.y, 2);
    }

    public float Dist(Coord other) {
        return Mathf.Sqrt(DistSqrt(other));
    }

    public bool Equals(Coord other) {
        return x == other.x && y == other.y;
    }

    public override string ToString() {
        return "{ " + x + ", " + y + " }";
    }
}

public static class MapExtention {
    public static bool IsInMapRange<T>(this T[,] map, int x, int y) {
        return x >= 0 && x < map.GetLength(0) && y >= 0 && y < map.GetLength(1);
    }
}