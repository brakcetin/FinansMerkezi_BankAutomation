﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinansMerkezi
{
    internal class Informations
    {
        private string accountNo;
        private string name;
        private decimal balance;

        public string AccountNo
        {
            get { return accountNo; }
            set { accountNo = value; }
        }
        public decimal Balance
        {
            get { return balance; }
            set { balance = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
