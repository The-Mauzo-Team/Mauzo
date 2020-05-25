using System.Drawing;

namespace Desktop.Templates
{
    class User
    {
        private int Id 
        {
            get { return Id; }
            set { Id = value; }
        }

        private string Username
        {
            get { return Username; }
            set { Username = value; }
        }

        private string Password
        {
            get { return Password; }
            set { Password = value; }
        }

        private string Firstname 
        {
            get { return Firstname; }
            set { Firstname = value; }
        }

        private string Lastname
        {
            get { return Lastname; }
            set { Lastname = value; }
        }

        private string Email 
        {
            get { return Email; }
            set { Email = value; }
        }

        private bool IsAdmin 
        {
            get { return IsAdmin; }
            set { IsAdmin = value; }
        }

        private Bitmap UserPic 
        {
            get { return UserPic; }
            set { UserPic = value; }
        }
    }
}