using System;
using System.Reflection;
using System.Windows.Forms;

namespace SeeSharpTools.JY.Localization
{
    public static class Localization
    {
        #region SetAllLang
        /// <summary>
        /// Set language
        /// </summary>
        /// <param name="lang">language:zh-CN, en-US</param>
        private static void SetAllLang(string lang)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);
            Form frm = null;

            string name = "MainForm";

            frm = (Form)Assembly.Load("CameraTest").CreateInstance(name);

            if (frm != null)
            {
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager();
                resources.ApplyResources(frm, "$this");
                AppLang(frm, resources);
            }
        }
        #endregion

        #region SetLang
        /// <summary>
        /// Set Winform display language, 
        /// </summary>
        /// <param name="lang">language name such as zh-CN, en-US, should be the same as reource file name</param>
        /// <param name="form">the form you need to set</param>
        /// <param name="formType">the type of the form </param>
        public static void SetLang(string lang, Form form, Type formType)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);
            if (form != null)
            {
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(formType);
                resources.ApplyResources(form, "$this");
                AppLang(form, resources);
            }
        }
        #endregion

        #region AppLang for control
        /// <summary>
        ///  loop set the propery of the control
        /// </summary>
        /// <param name="control"></param>
        /// <param name="resources"></param>
        private static void AppLang(Control control, System.ComponentModel.ComponentResourceManager resources)
        {
            if (control is MenuStrip)
            {
                resources.ApplyResources(control, control.Name);
                MenuStrip ms = (MenuStrip)control;
                if (ms.Items.Count > 0)
                {
                    foreach (ToolStripMenuItem c in ms.Items)
                    {
                        AppLang(c, resources);
                    }
                }
            }

            foreach (Control c in control.Controls)
            {
                resources.ApplyResources(c, c.Name);
                AppLang(c, resources);
            }
        }
        #endregion

        #region AppLang for menuitem
        /// <summary>
        /// set the toolscript 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="resources"></param>
        private static void AppLang(ToolStripMenuItem item, System.ComponentModel.ComponentResourceManager resources)
        {
            if (item is ToolStripMenuItem)
            {
                resources.ApplyResources(item, item.Name);
                ToolStripMenuItem tsmi = (ToolStripMenuItem)item;
                if (tsmi.DropDownItems.Count > 0)
                {
                    foreach (ToolStripMenuItem c in tsmi.DropDownItems)
                    {
                        AppLang(c, resources);
                    }
                }
            }
        }
        #endregion
    }
}
