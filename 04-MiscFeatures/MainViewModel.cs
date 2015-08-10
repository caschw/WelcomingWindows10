using System;

namespace _04_MiscFeatures
{
    public class MainViewModel
    {
        public string Greeting { get { return "Hello ThatConference!"; } }

        public string Time { get { return DateTime.Now.ToString("HH:mm:ss"); } }
    }
}
