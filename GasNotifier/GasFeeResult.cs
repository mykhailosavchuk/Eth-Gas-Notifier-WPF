using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasNotifier
{
    public class GasFeeResult
    {
        public string LastBlock { get; set; }
        public float SafeGasPrice { get; set; }
        public float ProposeGasPrice { get; set; }
        public float FastGasPrice { get; set; }
        public float suggestBaseFee { get; set; }
        public string gasUsedRatio { get; set; }
    }
}
