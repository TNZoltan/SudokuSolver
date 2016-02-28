using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics {
    class MyList<T> {
        private T[] myArray; 
        private int count;
        public MyList() {
            myArray = new T[100];
            count = 0;
        }
        public MyList(int size) {
            if (size > 0) {
                myArray = new T[size];
                count = 0;
            }
        }
        public int Count {
            get
            {
                return count;
            }
        }
        public void Add(T elm) {
            if (elm != null && count<myArray.Length) {
                myArray[count] = elm;
                ++count;
            }
        }
        public T GetElementByIndex(int idx) {
            if (idx >= 0 && idx < count) {
                return myArray[idx];
            }
            return default(T);
        }
        public T GetLastElement() {
            if (count > 0) {
                return myArray.Last();
            }
            return default(T);
        }
        public T GetFirstElement() {
            if (count > 0) {
                return myArray.First();
            }
            return default(T);
        }
        public void RemoveLastElement() {
            if (count >0) {
                myArray[count-1] = default(T);
                --count;
            }
        }
        public void RemoveFirstElement() {
            if (count>0) {
                for (int i = 0; i < count - 1; ++i) {
                    myArray[i] = myArray[i + 1];
                }
                myArray[count - 1] = default(T);
                count--;
            }
        }
        public void InsertElementAt(int idx, T elm) {
            if (elm!=null && count<myArray.Length && idx >=0 && idx < count) {
                for (int i = count; i>idx;--i) {
                    myArray[i] = myArray[i - 1];
                }
                myArray[idx] = elm;
                ++count;
            }
        }
    }
}
