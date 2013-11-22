// Type: Dexcom.Common.AsynchronousQueue`1
// Assembly: Dexcom.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B95A59FD-1598-4EA8-B1F5-D812FE5A6802
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Dexcom.Common
{
  [DebuggerDisplay("Count = {Count}")]
  [Serializable]
  public class AsynchronousQueue<T> : IEnumerable<T>, ICollection, IEnumerable
  {
    private static T[] _emptyArray = new T[0];
    private const int _DefaultCapacity = 4;
    private const int _GrowFactor = 200;
    private const int _MinimumGrow = 4;
    private const int _ShrinkThreshold = 32;
    private T[] _array;
    private int _head;
    private int _size;
    [NonSerialized]
    private object _syncRoot;
    private int _tail;
    private int _version;
    private int _logicalHead;
    private int _logicalTail;

    public int LogicalFirstIndex
    {
      get
      {
        lock (this.SyncRoot)
          return this._logicalHead;
      }
    }

    public int LogicalLastIndex
    {
      get
      {
        lock (this.SyncRoot)
          return this._logicalTail;
      }
    }

    public int Count
    {
      get
      {
        lock (this.SyncRoot)
          return this._size;
      }
    }

    bool ICollection.IsSynchronized
    {
      get
      {
        return true;
      }
    }

    object ICollection.SyncRoot
    {
      get
      {
        if (this._syncRoot == null)
          Interlocked.CompareExchange(ref this._syncRoot, new object(), (object) null);
        return this._syncRoot;
      }
    }

    static AsynchronousQueue()
    {
    }

    public AsynchronousQueue()
    {
      this._array = AsynchronousQueue<T>._emptyArray;
    }

    public AsynchronousQueue(IEnumerable<T> collection)
    {
      if (collection == null)
        throw new ArgumentNullException("collection");
      this._array = new T[4];
      this._size = 0;
      this._version = 0;
      foreach (T obj in collection)
        this.Add(obj);
    }

    public AsynchronousQueue(int capacity)
    {
      if (capacity < 0)
        throw new ArgumentOutOfRangeException("Capacity must not be a negative value!");
      this._array = new T[capacity];
      this._head = 0;
      this._tail = 0;
      this._size = 0;
      this._logicalHead = 0;
      this._logicalTail = 0;
    }

    public void Clear()
    {
      lock (this.SyncRoot)
      {
        if (this._head < this._tail)
        {
          Array.Clear((Array) this._array, this._head, this._size);
        }
        else
        {
          Array.Clear((Array) this._array, this._head, this._array.Length - this._head);
          Array.Clear((Array) this._array, 0, this._tail);
        }
        this._head = 0;
        this._tail = 0;
        this._size = 0;
        this._logicalHead = 0;
        this._logicalTail = 0;
        ++this._version;
      }
    }

    public bool Contains(T item)
    {
      lock (this.SyncRoot)
      {
        int local_0 = this._head;
        int local_1 = this._size;
        EqualityComparer<T> local_2 = EqualityComparer<T>.Default;
        while (local_1-- > 0)
        {
          if ((object) item == null)
          {
            if ((object) this._array[local_0] == null)
              return true;
          }
          else if ((object) this._array[local_0] != null && local_2.Equals(this._array[local_0], item))
            return true;
          local_0 = (local_0 + 1) % this._array.Length;
        }
        return false;
      }
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
      lock (this.SyncRoot)
      {
        if (array == null)
          throw new ArgumentNullException("array");
        if (arrayIndex < 0 || arrayIndex > array.Length)
          throw new ArgumentOutOfRangeException("arrayIndex");
        if (array.Length - arrayIndex < this._size)
          throw new ArgumentException("Invalid offset length!");
        int local_1 = this._size;
        if (local_1 == 0)
          return;
        int local_2 = this._array.Length - this._head < local_1 ? this._array.Length - this._head : local_1;
        Array.Copy((Array) this._array, this._head, (Array) array, arrayIndex, local_2);
        int local_1_1 = local_1 - local_2;
        if (local_1_1 <= 0)
          return;
        Array.Copy((Array) this._array, 0, (Array) array, arrayIndex + this._array.Length - this._head, local_1_1);
      }
    }

    public T[] CopyToArray()
    {
      lock (this.SyncRoot)
      {
        T[] local_0 = (T[]) Array.CreateInstance(typeof (T), this._size);
        if (this._size != 0)
        {
          int local_1 = this._size;
          int local_2 = this._array.Length - this._head < local_1 ? this._array.Length - this._head : local_1;
          Array.Copy((Array) this._array, this._head, (Array) local_0, 0, local_2);
          int local_1_1 = local_1 - local_2;
          if (local_1_1 > 0)
            Array.Copy((Array) this._array, 0, (Array) local_0, this._array.Length - this._head, local_1_1);
        }
        return local_0;
      }
    }

    public T[] CopyToArray(int startingIndex)
    {
      lock (this.SyncRoot)
      {
        if (startingIndex < this._logicalHead)
          throw new ArgumentOutOfRangeException("startingIndex", string.Format("Specified starting index is less than current starting item. Specified={0}, LHead={1}, LTail={2}.", (object) startingIndex, (object) this._logicalHead, (object) this._logicalTail));
        if (startingIndex > this._logicalTail)
          throw new ArgumentOutOfRangeException("startingIndex", string.Format("Specified starting index is greater than the last item. Specified={0}, LHead={1}, LTail={2}.", (object) startingIndex, (object) this._logicalHead, (object) this._logicalTail));
        if (startingIndex == this._logicalTail)
          return (T[]) Array.CreateInstance(typeof (T), 0);
        int local_0 = startingIndex - this._logicalHead;
        if (local_0 == 0)
          return this.CopyToArray();
        int local_1 = this._size - local_0;
        int local_2 = (this._head + local_0) % this._array.Length;
        T[] local_3 = (T[]) Array.CreateInstance(typeof (T), local_1);
        if (local_1 != 0)
        {
          int local_4 = local_1;
          int local_5 = this._array.Length - local_2 < local_4 ? this._array.Length - local_2 : local_4;
          Array.Copy((Array) this._array, local_2, (Array) local_3, 0, local_5);
          int local_4_1 = local_4 - local_5;
          if (local_4_1 > 0)
            Array.Copy((Array) this._array, 0, (Array) local_3, this._array.Length - local_2, local_4_1);
        }
        return local_3;
      }
    }

    public T Remove()
    {
      lock (this.SyncRoot)
      {
        if (this._size == 0)
          throw new InvalidOperationException("Remove: AsynchronousQueue is empty!");
        T local_0 = this._array[this._head];
        this._array[this._head] = default (T);
        ++this._logicalHead;
        this._head = (this._head + 1) % this._array.Length;
        --this._size;
        ++this._version;
        return local_0;
      }
    }

    public int Remove(int howMany)
    {
      lock (this.SyncRoot)
      {
        if (this._size == 0)
          throw new InvalidOperationException("Remove: AsynchronousQueue is empty!");
        if (howMany > this._size)
          throw new InvalidOperationException("Remove: Attempt to remove more items than queue contains!");
        for (int local_0 = 0; local_0 < howMany; ++local_0)
        {
          this._array[this._head] = default (T);
          ++this._logicalHead;
          this._head = (this._head + 1) % this._array.Length;
          --this._size;
        }
        ++this._version;
        return this._size;
      }
    }

    public void Add(T item)
    {
      lock (this.SyncRoot)
      {
        if (this._size == this._array.Length)
        {
          int local_0 = this._array.Length * 200 / 100;
          if (local_0 < this._array.Length + 4)
            local_0 = this._array.Length + 4;
          this.SetCapacity(local_0);
        }
        this._array[this._tail] = item;
        ++this._logicalTail;
        this._tail = (this._tail + 1) % this._array.Length;
        ++this._size;
      }
    }

    private T GetElement(int i)
    {
      lock (this.SyncRoot)
        return this._array[(this._head + i) % this._array.Length];
    }

    public AsynchronousQueue<T>.Enumerator GetEnumerator()
    {
      lock (this.SyncRoot)
        return new AsynchronousQueue<T>.Enumerator(this);
    }

    public T GetFirst()
    {
      lock (this.SyncRoot)
      {
        if (this._size == 0)
          throw new InvalidOperationException("GetFirst: AsynchronousQueue is empty!");
        else
          return this._array[this._head];
      }
    }

    public T GetLast()
    {
      lock (this.SyncRoot)
        return this.GetAtLogicalIndex(this._logicalTail - 1);
    }

    public T GetAtLogicalIndex(int logicalIndex)
    {
      lock (this.SyncRoot)
      {
        if (this._size == 0)
          throw new InvalidOperationException("GetAtLogicalIndex: AsynchronousQueue is empty!");
        if (logicalIndex >= this._logicalHead && logicalIndex < this._logicalTail)
          return this._array[(this._head + (logicalIndex - this._logicalHead)) % this._array.Length];
        else
          throw new ArgumentOutOfRangeException("logicalIndex");
      }
    }

    private void SetCapacity(int capacity)
    {
      T[] objArray = new T[capacity];
      if (this._size > 0)
      {
        if (this._head < this._tail)
        {
          Array.Copy((Array) this._array, this._head, (Array) objArray, 0, this._size);
        }
        else
        {
          Array.Copy((Array) this._array, this._head, (Array) objArray, 0, this._array.Length - this._head);
          Array.Copy((Array) this._array, 0, (Array) objArray, this._array.Length - this._head, this._tail);
        }
      }
      this._array = objArray;
      this._head = 0;
      this._tail = this._size == capacity ? 0 : this._size;
      ++this._version;
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
      lock (this.SyncRoot)
        return (IEnumerator<T>) new AsynchronousQueue<T>.Enumerator(this);
    }

    void ICollection.CopyTo(Array array, int index)
    {
      lock (this.SyncRoot)
      {
        if (array == null)
          throw new ArgumentNullException("array");
        if (array.Rank != 1)
          throw new ArgumentException("Multi-dimensioned array not supported!");
        if (array.GetLowerBound(0) != 0)
          throw new ArgumentException("Lower bound of array is non-zero!");
        int local_0 = array.Length;
        if (index < 0 || index > local_0)
          throw new ArgumentOutOfRangeException("index");
        if (local_0 - index < this._size)
          throw new ArgumentException("Invalid offset length!");
        int local_1 = local_0 - index < this._size ? local_0 - index : this._size;
        if (local_1 == 0)
          return;
        try
        {
          int local_2 = this._array.Length - this._head < local_1 ? this._array.Length - this._head : local_1;
          Array.Copy((Array) this._array, this._head, array, index, local_2);
          int local_1_1 = local_1 - local_2;
          if (local_1_1 <= 0)
            return;
          Array.Copy((Array) this._array, 0, array, index + this._array.Length - this._head, local_1_1);
        }
        catch (ArrayTypeMismatchException exception_0)
        {
          throw new ArgumentException("Invalid array type!");
        }
      }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      lock (this.SyncRoot)
        return (IEnumerator) new AsynchronousQueue<T>.Enumerator(this);
    }

    public T[] ToArray()
    {
      lock (this.SyncRoot)
      {
        T[] local_0 = new T[this._size];
        if (this._size != 0)
        {
          if (this._head < this._tail)
          {
            Array.Copy((Array) this._array, this._head, (Array) local_0, 0, this._size);
            return local_0;
          }
          else
          {
            Array.Copy((Array) this._array, this._head, (Array) local_0, 0, this._array.Length - this._head);
            Array.Copy((Array) this._array, 0, (Array) local_0, this._array.Length - this._head, this._tail);
          }
        }
        return local_0;
      }
    }

    public void TrimExcess()
    {
      lock (this.SyncRoot)
      {
        if (this._size >= (int) ((double) this._array.Length * 0.9))
          return;
        this.SetCapacity(this._size);
      }
    }

    [Serializable]
    public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
    {
      private AsynchronousQueue<T> _q;
      private int _index;
      private int _version;
      private int _logicalOffset;
      private T _currentElement;

      public T Current
      {
        get
        {
          if (this._index >= 0)
            return this._currentElement;
          if (this._index == -1)
            throw new InvalidOperationException("Enumerator not started!");
          else
            throw new InvalidOperationException("Enumerator already exited!");
        }
      }

      object IEnumerator.Current
      {
        get
        {
          if (this._index >= 0)
            return (object) this._currentElement;
          if (this._index == -1)
            throw new InvalidOperationException("Enumerator not started!");
          else
            throw new InvalidOperationException("Enumerator already exited!");
        }
      }

      internal Enumerator(AsynchronousQueue<T> q)
      {
        this._q = q;
        this._version = this._q._version;
        this._index = -1;
        this._logicalOffset = this._q._logicalHead;
        this._currentElement = default (T);
      }

      public void Dispose()
      {
        this._index = -2;
        this._currentElement = default (T);
      }

      public bool MoveNext()
      {
        if (this._version != this._q._version)
          throw new InvalidOperationException("Enumerator aborted because collection has changed!");
        if (this._index == -2)
          return false;
        ++this._index;
        if (this._index == this._q._size)
        {
          this._index = -2;
          this._currentElement = default (T);
          return false;
        }
        else
        {
          this._currentElement = this._q.GetAtLogicalIndex(this._index + this._logicalOffset);
          return true;
        }
      }

      void IEnumerator.Reset()
      {
        if (this._version != this._q._version)
          throw new InvalidOperationException("Enumerator aborted because collection has changed!");
        this._index = -1;
        this._currentElement = default (T);
      }
    }
  }
}
