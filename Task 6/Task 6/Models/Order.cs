using System;

namespace Task_6.Models
{
    public class Order
    {
        
        public double kursi { get; set; }
        public int XelshekrulebaID { get; set; }
        public int PersonaliID { get; set; }
        public int ShemkvetiID { get; set; }
        public double GadasaxdeliL { get; set; }
        public double GadasaxdeliD { get; set; }
        public double GadaxdiliL { get; set; }
        public double GadaxdiliD { get; set; }
        public double ValiL { get; set; }
        public double ValiD { get; set; }
        public string Reason { get; set; }
        public DateTime TarigiDawyebis { get; set; }
        public DateTime? TarigiShesrulebis { get; set; }
        public DateTime? TarigiDamtavrebis { get; set; }
        public int DaysRemaining { get; set; }
        public string VisiMizezit { get; internal set; }
        public bool Shesruleba { get; internal set; }
    }
}
