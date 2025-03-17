using System;
using System.Collections.Generic;

// 최소 힙(MinHeap) 제네릭 클래스 (T는 IComparable<T> 인터페이스를 구현해야 함)
public class MinHeap<T> where T : IComparable<T>
{
    private List<T> heap = new List<T>();

    // 힙에 저장된 요소의 개수를 반환
    public int Count { get { return heap.Count; } }

    // 새로운 요소를 힙에 추가
    public void Insert(T item)
    {
        heap.Add(item);
        HeapifyUp(heap.Count - 1); // 삽입한 요소를 올바른 위치로 이동
    }

    // 최소값(루트 노드)을 제거하고 반환
    public T RemoveMin()
    {
        if (heap.Count == 0)
            throw new InvalidOperationException("Heap is empty.");

        T min = heap[0];  // 루트 노드가 최소값
        heap[0] = heap[heap.Count - 1]; // 마지막 요소를 루트로 옮김
        heap.RemoveAt(heap.Count - 1);  // 마지막 요소 제거
        HeapifyDown(0);                 // 옮긴 요소를 올바른 위치로 이동
        return min;
    }

    // 삽입한 요소를 올바른 위치로 올리는 메서드 (상향 조정)
    private void HeapifyUp(int index)
    {
        while (index > 0)
        {
            int parent = (index - 1) / 2;
            // 현재 요소가 부모보다 작으면 자리 교환
            if (heap[index].CompareTo(heap[parent]) < 0)
            {
                T temp = heap[index];
                heap[index] = heap[parent];
                heap[parent] = temp;
                index = parent;
            }
            else
            {
                break;
            }
        }
    }

    // 루트 요소를 올바른 위치로 내리는 메서드 (하향 조정)
    private void HeapifyDown(int index)
    {
        int lastIndex = heap.Count - 1;
        while (index < heap.Count)
        {
            int left = index * 2 + 1;
            int right = index * 2 + 2;
            int smallest = index;

            if (left <= lastIndex && heap[left].CompareTo(heap[smallest]) < 0)
                smallest = left;
            if (right <= lastIndex && heap[right].CompareTo(heap[smallest]) < 0)
                smallest = right;

            if (smallest != index)
            {
                T temp = heap[index];
                heap[index] = heap[smallest];
                heap[smallest] = temp;
                index = smallest;
            }
            else
            {
                break;
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        MinHeap<int> minHeap = new MinHeap<int>();

        // 힙에 요소 추가
        minHeap.Insert(5);
        minHeap.Insert(3);
        minHeap.Insert(8);
        minHeap.Insert(1);
        minHeap.Insert(2);

        Console.WriteLine("MinHeap에서 최소값부터 순서대로 제거:");
        // 힙이 빌 때까지 최소값을 제거하며 출력 (항상 최소값이 먼저 나옴)
        while (minHeap.Count > 0)
        {
            Console.WriteLine(minHeap.RemoveMin());
        }
    }
}
