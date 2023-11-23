using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model
{
    public class WalletModel
    {
        public string Id { get; set; }
        public string name { get; set; }
        public int balance { get; set; }
        public int balanceHistory { get; set; }
    }
}
