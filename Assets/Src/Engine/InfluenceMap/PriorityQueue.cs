
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueueNode {
    public PriorityQueueNode() {
        tuple = new Tupple();
    }

    public PriorityQueueNode(Coord coord, float order) {
        this.tuple = new Tupple();
        this.tuple.coord = coord;
        this.tuple.order = order;
    }

    public class Tupple {
        public Coord coord;
        public float order;
    }

    public Tupple tuple;
    public PriorityQueueNode next = null;

}

public class PriorityQueue : IEnumerable<Coord> {
    public int Count { get; private set; }
    public bool Empty { get { return Count == 0; } }

    private PriorityQueueNode head = null;

    public void Enqueue(Coord val, float order) {
        var item = new PriorityQueueNode(val, order);

        if(head == null) {
            head = item;
        } else if(head.tuple.order >= order) {
            item.next = head;

            head = item;
        } else {
            PriorityQueueNode it = head;
            PriorityQueueNode previous_it = it;
            while(it != null && it.tuple.order < order) {
                previous_it = it;
                it = it.next;
            }
            previous_it.next = item;
            item.next = it;
        }

        Count++;
    }

    public Coord Dequeue() {
        if(head == null)
            return null;

        var val = head.tuple.coord;
        head = head.next;
        Count--;
        return val;
    }

    public Coord Peek() {
        if(head == null)
            return null;

        return head.tuple.coord;
    }

    public IEnumerator<Coord> GetEnumerator() {
        PriorityQueueNode it = head;

        while(it != null) {
            yield return it.tuple.coord;
            it = it.next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() {
        PriorityQueueNode it = head;

        while(it != null) {
            yield return it.tuple.coord;
            it = it.next;
        }
    }

    public Coord this[int i] {
        get {
            PriorityQueueNode it = head;
            while(i-- > 0) {
                it = it.next;
            }
            return it.tuple.coord;
        }
    }

}