using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WhatsAppCloneMainProject.Models;
using WhatsAppCloneMainProject.ViewModels;

namespace WhatsAppCloneMainProject.Controls
{
    public partial class ContactContent : UserControl
    {
        public ContactContentViewModel ContactContentViewModel { get; set; } = new ContactContentViewModel();
        public ContactContent(Contact contact)
        {
            InitializeComponent();
            this.DataContext = ContactContentViewModel;

            ContactContentViewModel.User = contact.ContactUser;
            this.DateAdded = contact.DateAdded;
            this.Email = ContactContentViewModel.User.Email;
            this.UserName = ContactContentViewModel.User.Username;
        }



        public DateTime DateAdded
        {
            get { return (DateTime)GetValue(DateAddedProperty); }
            set { SetValue(DateAddedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DateAdded.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DateAddedProperty =
            DependencyProperty.Register("DateAdded", typeof(DateTime), typeof(ContactContent), new PropertyMetadata(null));



        public string Email
        {
            get { return (string)GetValue(EmailProperty); }
            set { SetValue(EmailProperty, value); }
        }

        public static readonly DependencyProperty EmailProperty =
            DependencyProperty.Register("Email", typeof(string), typeof(ContactContent), new PropertyMetadata(null));



        public string UserName
        {
            get { return (string)GetValue(UserNameProperty); }
            set { SetValue(UserNameProperty, value); }
        }
        public static readonly DependencyProperty UserNameProperty =
            DependencyProperty.Register("UserName", typeof(string), typeof(ContactContent), new PropertyMetadata(null));




    }
}
