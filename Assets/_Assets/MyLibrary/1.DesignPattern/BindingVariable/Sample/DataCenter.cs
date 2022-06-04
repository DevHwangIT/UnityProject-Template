
namespace MyLibrary.Utility.Sample
{
    public class DataCenter
    {
        static DataCenter _instance;

        public static DataCenter Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DataCenter();
                return _instance;
            }
        }

        // Simple is the best. DataCenter convention is Example Code
        public ObservableValue<int> hp = new ObservableValue<int>(100);
        public ObservableValue<int> mp = new ObservableValue<int>(100);
    }
}
