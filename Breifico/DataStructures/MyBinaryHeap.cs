namespace Breifico.DataStructures
{
    public class MyBinaryHeap<T>
    {
        private MyList<T> _internalList = new MyList<T>();

        public MyBinaryHeap() {}

        public void Insert(T item) {
            this._internalList.Add(item);
        }

    }
}
