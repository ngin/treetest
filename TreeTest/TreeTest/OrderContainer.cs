using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeTest
{
    public class OrderMetaData
    {
        public List<OrderEvent> AllEvents { get; set; }
        public OrderEvent Latest
        {
            get
            {
                return this.AllEvents.OrderByDescending(t => t.Date).First(); 
            }
        }
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string ComplexOrderId { get { return this.Latest.ComplexOrderId; } }
        public List<string> Links { get; set; }
    }

    public class OrderContainer
    {
        public List<Node<OrderMetaData>> Legs { get; set; }
    }

    public class OrderEvent
    {
        public OrderEvent(int id, int? parentId, string details, DateTime date, string complexOrderId)
        {
            Id = id;
            ParentId = parentId;
            Details = details;
            Date = date;
            ComplexOrderId = complexOrderId;
        }

        public string ComplexOrderId { get; set; }

        public int Id
        {
            get; set;
        }

        public int? ParentId
        {
            get; set;
        }

        public string Details
        {
            get; set;
        }

        public DateTime Date
        {
            get; set;
        }

        public override string ToString()
        {
            return $"Id={Id} ParnetOrderId={ParentId} ComplexOrderId={ComplexOrderId} Details={Details} Date={Date.Day}";
        }
    }
}
