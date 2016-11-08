using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fiddler;
using System.Windows.Forms;
using System.Xml;

namespace MultipartFormDataInspector
{
    public class MultipartRequestInspector : Inspector2, IRequestInspector2
    {
        private bool isReadOnly;
        private byte[] binaryContent;

        private MultipartViewer viewControl;
        private static Logger log = new Logger(true);

        public override void AddToTab(TabPage o)
        {
            o.Text = "Multipart/form-data";

            //isReadOnly = true;

            // create and add xml tree view as initial control
            viewControl = new MultipartViewer();
            viewControl.BackColor = CONFIG.colorDisabledEdit;
            viewControl.Dock = DockStyle.Fill;
            o.Controls.Add(viewControl);
        }


        /// <summary>
        /// Update the controls with the new binary content
        /// </summary>
        private void UpdateControlContent()
        {
            try
            {
                List<PostParameter> lst = MultipartConverter.Convert(binaryContent);
                if (lst == null)
                {
                    this.Clear();
                }
                else
                {
                    viewControl.BindGrid(lst);
                }
            }
            catch (XmlException ex)
            {
                log.LogString(ex.ToString());
                viewControl.DisplayError(ex.Message);
            }
        }

        #region IBaseInspector2 members

        public override int GetOrder()
        {
            return 1;
        }

        public bool bDirty { get; set; }

        public bool bReadOnly
        {
            get { return isReadOnly; }
            set
            {
                isReadOnly = value;
                // show the correct control based on whether we're in view (read-only) or edit mode.
                //SwitchControl();
            }
        }

        public void Clear()
        {
            viewControl.Clear();
            bDirty = false;
            bReadOnly = true;
        }

        public byte[] body
        {
            get { return binaryContent; }
            set
            {
                // when fiddler updates this inspector's content, our controls' content
                binaryContent = value;
                UpdateControlContent();
            }
        }

        #region IRequestInspector2 members

        public HTTPRequestHeaders headers
        {
            get { return null; }
            set { }
        }

        #endregion

        #endregion
    }
}
