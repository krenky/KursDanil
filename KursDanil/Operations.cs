using System;
using System.Collections.Generic;
using System.Text;

namespace KursDanil
{
    [Serializable]
    class Operations
    {
        int sum;
        
        DateTime timeOperat;
        Operations next;

        internal Operations Next { get => next; set => next = value; }
        public DateTime TimeOperat { get => timeOperat; set => timeOperat = value; }
        public int Sum { get => sum; set => sum = value; }

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="sum1"></param>
        /// <param name="time"></param>
        public Operations(int sum1, DateTime time)
        {
            Sum = sum1;
            TimeOperat = time;
        }

        public Operations()
        {
        }
    }
}
