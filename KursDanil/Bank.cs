using System;
using System.ComponentModel;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json.Serialization;


namespace KursDanil
{
    [Serializable]
    class Bank
    {
        Clients[] _clients;
        int _count;
        int _Head, _Tail;
        Clients NullClient = new Clients("Not Found", 0, new DateTime());
        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="count1">размер массива</param>
        public Bank(int _Count)
        {
            _count = _Count;
            _clients = new Clients[_Count];
            _Head = -1;
            _Tail = -1;
        }
        public Bank(int count, Clients[] clients, int head, int tail)
        {
            _count = count;
            _clients = clients;
            _Head = head;
            _Tail = tail;
        }

        public Bank()
        {
        }

        /// <summary>
        /// добавление клиента
        /// </summary>
        /// <param name="name"></param>
        /// <param name="sum"></param>
        public void Add(string name, int sum, DateTime time)
        {
            Clients сlient = new Clients(name, sum, time);
            if (!isFull())
            {
                if (_Head == -1) _Head = 0;
                _Tail++;
                _clients[_Tail] = сlient;
            }
            else
            {
                Array.Resize(ref _clients, _clients.Length * 2);
                _count = _count * 2;
                Add(name, sum, time);
            }
        }
        /// <summary>
        /// добавление клиента
        /// </summary>
        /// <param name="name"></param>
        /// <param name="sum"></param>
        public void Add(Clients client)
        {
            if (!isFull())
            {
                if (_Head == -1) _Head = 0;
                _Tail++;
                _clients[_Tail] = client;
            }
            else
            {
                Array.Resize(ref _clients, _clients.Length * 2);
                _count = _count * 2;
                Add(client);
            }
        }
        /// <summary>
        /// сдвиг
        /// </summary>
        private void Push()
        {
            for (int i = _Head; i < _Tail-1; i++)
            {
                _clients[i] = _clients[i+1];
            }
            _clients[_Tail] = null;
            _Tail--;
        }
        /// <summary>
        /// удаление
        /// </summary>
        public void Delete()
        {
            if (!isEmpty())
                Push();
            else _clients[0] = null;
        }
        /// <summary>
        /// изменение баланса
        /// </summary>
        /// <param name="index"></param>
        /// <param name="sum"></param>
        public bool ChangeBalance(int index, int sum, DateTime time)
        {
            if (index != -1)
            {
                return _clients[index].AddOperation(sum, time);
            }
            return false;
        }
        /// <summary>
        /// вывод всей информации
        /// </summary>
        /// <returns></returns>
        public string PrintAllInfo()
        {
            string Info = "";
            for(int i = _Head; i <= _Tail; i++)
            {
                Info =Info + _clients[i].Info();
            }
            return Info; 
        }
        /// <summary>
        /// вывод информации по нужному клиенту
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string PrintInfo(int index)
        {
            if (index != -1)
                return _clients[index].Info();
            else
                return "Не найдено";
        }
        /// <summary>
        /// поиск по имени 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        //public int Search(string name)
        //{
        //    for(int i = _Head; i <= _Tail; i++)
        //    {
        //        if(_clients[i].Name == name)
        //        {
        //            return i;
        //        }
        //    }
        //    return -1;
        //}
        public Clients Search(string name)
        {
            for (int i = _Head; i <= _Tail; i++)
            {
                if (_clients[i].Name == name)
                {
                    return _clients[i];
                }
            }
            throw new Exception($"Client =>{name} not found");
        }
        /// <summary>
        /// проверка на заполненность
        /// </summary>
        /// <returns></returns>
        public bool isFull()
        {
            if ((_Head == _Tail + 1) || (_Head == 0 && _Tail == _count - 1))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// проверка на пустоту
        /// </summary>
        /// <returns></returns>
        public bool isEmpty()
        {
            return _Head == _Tail;
        }
        

        public Clients[] Clients { get => _clients; set => _clients = value; }
        public int Count { get => _count; set => _count = value; }
        public int Head { get => _Head; set => _Head = value; }
        public int Tail { get => _Tail; set => _Tail = value; }
    }
}
