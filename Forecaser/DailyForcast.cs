using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecaser {
    class DailyForcast {

        public double lat { get; set; }
        public double lon { get; set; }
        public List<daily> daily { get; set; }
    }

    public class temp {
        public double day { get; set; }
    }

    public class weather {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class daily {
        public double dt { get; set; }
        public temp temp { get; set; }
        public double pressure { get; set; }
        public double humidity { get; set; }
        public double speed { get; set; }
        public List<weather> weather { get; set; }

    }
}
