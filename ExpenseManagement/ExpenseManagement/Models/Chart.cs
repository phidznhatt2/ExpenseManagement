using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseManagement.Models
{
    public class DataChart
    {
        public string name { get; set; }
            
        public double value { get; set; }

        public double percent { get; set; }
    }

    public class ItemDataChart
    {
        public List<DataChart> items { get; set; }
        public int totalRecords { get; set; }
    }

    public class RootChart
    {
        public bool isSuccessed { get; set; }
        public string message { get; set; }
        public ItemDataChart data { get; set; }
    }
}
