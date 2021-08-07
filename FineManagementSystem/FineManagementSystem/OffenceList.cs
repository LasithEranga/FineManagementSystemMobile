using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FineManagementSystem
{
    class OffenceList
    {
        string offence;
        string offenceid;
        string offencedate;
        string offencetime;
        static Label loffence = new Label();
        static Label loffenceid = new Label();
        static Label loffencedate = new Label();
        static Label loffencetime = new Label();

        public OffenceList(string offence, string offenceid, string offencedate, string offencetime)
        {
            this.offence = offence;
            this.offenceid = offenceid;
            this.offencedate = offencedate;
            this.offencetime = offencetime;
            update();
        }

        public ArrayList GetTemplate() {

            ArrayList arlist = new ArrayList();
            arlist.Add(loffence);
            arlist.Add(loffenceid);
            arlist.Add(loffencedate);
            arlist.Add(loffencetime);

            return arlist;

        }

        public void update()
        {
            loffence.Text = offence;
            loffenceid.Text = offenceid;
            loffencedate.Text = offencedate;
            loffencetime.Text = offencetime;

        }





    }
}
