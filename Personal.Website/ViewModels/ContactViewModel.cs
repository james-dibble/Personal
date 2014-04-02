namespace Personal.Website.ViewModels
{
    public class ContactViewModel
    {
        public ContactViewModel(string tweet)
        {
            this.Twitter = tweet;
        }

        public string Twitter { get; private set; } 
    }
}