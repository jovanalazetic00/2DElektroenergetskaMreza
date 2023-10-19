namespace PZ1.Model
{
    public class PowerEntity
    {

        // Read attributes from the XML file
        private long id;         //node, switch

        private string name;     //node, switch

        private double x;        //node, switch

        private double y;        //node, switch

        private double latitude;

        private double longitude;

        public long Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public double X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        public double Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }

        public double Latitude { get => latitude; set => latitude = value; }

        public double Longitude { get => longitude; set => longitude = value; }

        public PowerEntity() { }
    }
}
