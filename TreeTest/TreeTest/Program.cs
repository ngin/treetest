using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Test();
            Console.ReadLine();
        }

        public static IEnumerable<OrderEvent> GetOrders()
        {
            return new List<OrderEvent>
            {
                new OrderEvent(2, 1, "Sales Child", DateTime.Now.AddDays(1), null),
                new OrderEvent(3, 2, "Exec Parent", DateTime.Now.AddDays(2), null),
                new OrderEvent(4, 3, "Exec Child 1", DateTime.Now.AddDays(3), null),
                new OrderEvent(1, null, "Sales Parent", DateTime.Now.AddDays(0), null),
            };
        }

        public static void Test()
        {
            var orders = GetOrders();

            var rootNodes = Node<OrderEvent>.CreateTree(orders, o => o.Id, o => o.ParentId);
            var rootNode = rootNodes.FirstOrDefault();

            var leaf = rootNode.Descendants.Single(n => n.Value.Details == "Exec Child 1");

            leaf.AddFirstSibling(new OrderEvent(6, null, "Exec Child 2", DateTime.Now.AddDays(4), null));
            leaf.AddFirstSibling(new OrderEvent(7, null, "Exec Child 3", DateTime.Now.AddDays(5), null));

            Console.WriteLine(rootNode.ToString(t => t.ToString()));

            var minRouteTime = rootNode.Leaves.Min(t => t.Value.Date);

            rootNode.ForEach((n, l) => 
            {
                n.Value.Date = minRouteTime;
                //if (l == 3)
                //{
                //    n.Value.Date = n.Value.Date.AddDays(1);
                //}
            });

            Console.WriteLine(rootNode.ToString(t => t.ToString()));
        }
    }
}
