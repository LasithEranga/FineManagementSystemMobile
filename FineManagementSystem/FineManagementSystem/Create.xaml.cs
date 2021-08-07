using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FineManagementSystem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Create : ContentPage
    {
        public Create()
        {
            InitializeComponent();
            OffenceList list = new OffenceList("scd","csd","dsc","dsc");
            ArrayList lis = list.GetTemplate();
            foreach (Label l in lis)
            {
               
            }
        }



    }
}