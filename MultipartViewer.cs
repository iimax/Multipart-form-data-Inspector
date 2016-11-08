using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MultipartFormDataInspector
{
    public partial class MultipartViewer : UserControl
    {
        public MultipartViewer()
        {
            InitializeComponent();
        }

        public void BindGrid(List<PostParameter> lst)
        {
            this.dataGridView1.DataSource = lst;
        }

        public void Clear()
        {
            List<PostParameter> lst = new List<PostParameter>();
            BindGrid(lst);
            DisplayError("Invalid content!");
        }

        public void DisplayError(string msg)
        {
            this.lblMsg.Text = msg;
        }


    }
}
