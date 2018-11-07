using dll.Models;

namespace dll.App_Classes
{
    public class DllContext
    {
        private eduadvisorContext baglanti;

        public eduadvisorContext Baglanti
        {
            get
            {
                baglanti = new eduadvisorContext();
                return baglanti;
            }
        }
    }
}
