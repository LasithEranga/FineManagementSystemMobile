using FMSMob.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace FMSMob.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}