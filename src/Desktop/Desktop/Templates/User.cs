using System.Drawing;

namespace Desktop.Templates
{
    class User
    {
        public int Id 
        {
            get { return Id; }
            set { Id = value; }
        }

        public string Username
        {
            get { return Username; }
            set { Username = value; }
        }

        public string Password
        {
            get { return Password; }
            set { Password = value; }
        }

        public string Firstname 
        {
            get { return Firstname; }
            set { Firstname = value; }
        }

        public string Lastname
        {
            get { return Lastname; }
            set { Lastname = value; }
        }

        public string Email 
        {
            get { return Email; }
            set { Email = value; }
        }

        public bool IsAdmin 
        {
            get { return IsAdmin; }
            set { IsAdmin = value; }
        }

        public Bitmap UserPic 
        {
            get { return UserPic; }
            set { UserPic = value; }
        }
    }
}