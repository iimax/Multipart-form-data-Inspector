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
        private List<PostParameter> _data = null;
        public void BindGrid(List<PostParameter> lst)
        {
            _data = lst;
            this.dataGridView1.DataSource = lst;
            DisplayError(string.Empty);
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

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (_data != null)
            {
                string template = txtTemplate.Text;
                StringBuilder sb = new StringBuilder();
                foreach (var item in _data)
                {
                    sb.AppendFormat(template, item.Name, IgnoreValueOf(item.Name, item.Value));
                    sb.AppendLine();
                }
                Clipboard.SetText(sb.ToString());
                DisplayError("copied!");
            }
        }

        private object IgnoreValueOf(string name, object val)
        {
            if (name == "__EVENTVALIDATION" || name == "__VIEWSTATE")
            {
                return string.Empty;
            }
            return val;
        }

    }
}
