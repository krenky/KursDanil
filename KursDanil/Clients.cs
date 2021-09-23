using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;



namespace KursDanil 
{
    [Serializable]
    class Clients 
    {
        public string _name;
        public Operations _operations;
        /// <summary>
        /// конструктор  
        /// </summary>
        /// <param name="name"></param>
        /// <param name="balance"></param>
        public Clients(string name, int balance, DateTime time)
        {
            _operations = new Operations(balance, time);
            Name = name;
            
            
        }

        public Clients()
        {
        }

        /// добавление 
        /// </summary>
        /// <param name="Item">Добавляемый</param>
        /// <param name="ItemNext">Элемент после которого добавляют</param>
        /// <returns></returns>
        public bool AddOperation(int sum, DateTime time)
        {
            Operations localOper;
            if (Balance() + sum < 0)
            {
                return false;
            }
            Operations current = _operations;
            if (current.Next == null)
            {
                if (_operations.TimeOperat > time)
                {
                    _operations.Next = new Operations(sum, time);
                    return true;
                }
                else
                {
                    localOper = _operations;
                    _operations = new Operations(sum, time);
                    _operations.Next = localOper;
                    return true;
                }
            }
            if (current.TimeOperat < time)
            {
                localOper = _operations;
                _operations = new Operations(sum, time);
                _operations.Next = localOper;
                return true;
            }
            do
            {
                if (current.Next == null)
                {
                    current.Next = new Operations(sum, time);
                    return true;
                }
                else
                {
                    if (current.TimeOperat > time && current.Next.TimeOperat < time)
                    {
                        localOper = current.Next;
                        current.Next = new Operations(sum, time);
                        current.Next.Next = localOper;
                        return true;
                    }
                    else
                    {
                        current = current.Next;
                    }
                }
            } while (current != null);
            return false;
            //Operations current = _operations;
            //if(current.TimeOperat < time)
            //{
            //    localOper = current;
            //    current = new Operations(sum, time);
            //    localOper.Next = current;
            //}
        }
        /// добавление 
        /// </summary>
        /// <param name="Item">Добавляемый</param>
        /// <param name="ItemNext">Элемент после которого добавляют</param>
        /// <returns></returns>
        public bool AddOperation(Operations operations)
        {
            Operations localOper;
            if (Balance() + operations.Sum < 0)
            {
                return false;
            }
            Operations current = _operations;
            if (current.Next == null)
            {
                if (_operations.TimeOperat > operations.TimeOperat)
                {
                    _operations.Next = operations;
                    return true;
                }
                else
                {
                    localOper = _operations;
                    _operations = operations;
                    _operations.Next = localOper;
                    return true;
                }
            }
            if (current.TimeOperat < operations.TimeOperat)
            {
                localOper = _operations;
                _operations = operations;
                _operations.Next = localOper;
                return true;
            }
            do
            {
                if (current.Next == null)
                {
                    current.Next = operations;
                    return true;
                }
                else
                {
                    if (current.TimeOperat > operations.TimeOperat && current.Next.TimeOperat < operations.TimeOperat)
                    {
                        localOper = current.Next;
                        current.Next = operations;
                        current.Next.Next = localOper;
                        return true;
                    }
                    else
                    {
                        current = current.Next;
                    }
                }
            } while (current != null);
            return false;
            //Operations current = _operations;
            //if(current.TimeOperat < time)
            //{
            //    localOper = current;
            //    current = new Operations(sum, time);
            //    localOper.Next = current;
            //}
        }
        /// <summary>
        /// Удаление элемента
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public bool DelOpereation(DateTime time)
        {
            if (_operations.TimeOperat.Equals(time))
            {
                _operations = _operations.Next;
                return true;
            }
            Operations prev = _operations;
            Operations current = _operations.Next;
            while(current.Next != null)
            {
                if (current.TimeOperat.Equals(time))
                {
                    prev.Next = current.Next;
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// состояние счета на текущи момент
        /// </summary>
        /// <returns></returns>
        public int Balance()
        {
            int a=0;
            Operations current = _operations;
            while (current!=null)
            {
                a += current.Sum;
                current = current.Next;
                
            }
            return a;
        }
        //public void AddOperation(Operations Item)
        //{
        //    operations = AddOperation(Item,operations);
        //}
        public string Name { get => _name; set => _name = value; }
        [JsonIgnore]
        public int ActualBalance => Balance();

        public Operations Operations { get => _operations; set => _operations = value; }
        public List<Operations> GetOperations { get => ToList();
            set
            {
                foreach(Operations i in value)
                {
                    AddOperation(i);
                }
            }
        }

        /// <summary>
        /// вывод информации текущем клиенте
        /// </summary>
        /// <returns></returns>
        public string Info()
        {
            return $"Имя:{_name} - баланс: {Balance()}\n";
        }
        private List<Operations> ToList()
        {
            List<Operations> operations = new List<Operations>();
            Operations current = _operations;
            while (current != null)
            {
                operations.Add(current);
                current = current.Next;
            }
            return operations;
        }

        //public IEnumerator GetEnumerator()
        //{
        //    Operations current = _operations;
        //    while (current != null)
        //    {
        //        yield return current;
        //        current = current.Next;
        //    }
        //}
    }
}
