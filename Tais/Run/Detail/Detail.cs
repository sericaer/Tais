﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tais.Run
{
    public interface IDetail
    {
        string name { get; }
        decimal value { get; }
    }

    class Detail : IDetail, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public string name { get; set; }

        public List<IDetail> subs = new List<IDetail>();

        public decimal value => subs.Sum(x => x.value);

        public Detail(string name)
        {
            this.name = name;
        }

        public Detail(string name, IEnumerable<IDetail> subs)
        {
            this.name = name;
            this.subs.AddRange(subs);
        }
    }

    class Detail_Leaf : IDetail
    {
        public string name { get; set; }

        public decimal value { get; set; }

        public Detail_Leaf(string name, decimal value)
        {
            this.name = name;
            this.value = value;
        }
    }
}
