using Breifico.DataStructures;

namespace Breifico.TestApp
{
    public class Node<T>
    {
        public string Name { get; set; }
        private MyList<Link<T>> _links;

        public Node() {
            this._links = new MyList<Link<T>>();
        }
    }

    public class Link<T>
    {
        public int Cost { get; private set; }

        public Link(int cost) {
            this.Cost = cost;
        }

        public MyList<Node<T>> Neighbors { get; private set; }
    }



    internal class Program
    {
        private static void Main(string[] args) {
            var number = 123654;
            //var x = new NumberBaseConverter();
            //var y = x.ToBase(number, 16, 8);
        }
    }
}