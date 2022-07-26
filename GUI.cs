using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace LTB_Verwaltung
{
    public partial class GUI : Form
    {
        private static GUI instance = null;
        private static readonly object padlock = new object();

        private readonly ComponentResourceManager resources = new ComponentResourceManager(typeof(GUI));
        private bool allowCheckChange = true;
        private int lastSelectorIndex;

        private GUI()
        {
            InitializeComponent();
        }

        public static GUI Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new GUI();
                    }
                    return instance;
                }
            }
        }

        public ListViewItem CheckItem(ListViewItem listViewItem)
        {
            if (bool.Parse(listViewItem.Text))
            {
                listViewItem.Checked = true;
            }

            listViewItem.Text = string.Empty;

            return listViewItem;
        }

#pragma warning disable IDE0017
        public void AppendItems(List<string[]> view)
        {
            allowCheckChange = false;

            lvIndexTable.Items.Clear();

            foreach (var arrayItem in view)
            {
                ListViewItem listViewItem = new ListViewItem(arrayItem);

                listViewItem.Tag = arrayItem[2];

                listViewItem = CheckItem(listViewItem);

                lvIndexTable.Items.Add(listViewItem);
            }

            allowCheckChange = !allowCheckChange;
        }

        public void ActivateInputs()
        {
            lvIndexTable.BeginUpdate();

            btnPrint.Enabled = true;
            btnSave.Enabled = true;
            cbShowNotOwned.Enabled = true;
            cbShowOwned.Enabled = true;
            ddEditionSelector.Enabled = true;
            ddEditionSelector.SelectedIndex = 0;

            lvIndexTable.EndUpdate();
        }

#pragma warning disable IDE1006
        public bool getCbShowOwnedState()
        {
            return cbShowOwned.Checked;
        }

        public bool getCbShowNotOwnedState()
        {
            return cbShowNotOwned.Checked;
        }

        public int getDdEditionSelectorIndex()
        {
            return ddEditionSelector.SelectedIndex - 1;
        }
        

        private void lvIndexTable_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (allowCheckChange)
            {
                LTB.Instance.ChangeLTB(e.Item.Tag.ToString(), e.Item.Checked, getDdEditionSelectorIndex());
            }
        }

        private void cbShowOwned_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShowOwned.Checked)
            {
                cbShowNotOwned.CheckedChanged -= cbShowNotOwned_CheckedChanged;
                cbShowNotOwned.Checked = false;
                cbShowNotOwned.CheckedChanged += cbShowNotOwned_CheckedChanged;

                AppendItems(LTB.Instance.GetSpecificLTB(LTB.Instance.GetCategory(getDdEditionSelectorIndex()), true));
            }
            else
            {
                AppendItems(LTB.Instance.GetCategory(getDdEditionSelectorIndex()));
            }
        }

        private void cbShowNotOwned_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShowNotOwned.Checked)
            {
                cbShowOwned.CheckedChanged -= cbShowOwned_CheckedChanged;
                cbShowOwned.Checked = false;
                cbShowOwned.CheckedChanged += cbShowOwned_CheckedChanged;

                AppendItems(LTB.Instance.GetSpecificLTB(LTB.Instance.GetCategory(getDdEditionSelectorIndex()), false));
            }
            else
            {
                AppendItems(LTB.Instance.GetCategory(getDdEditionSelectorIndex()));
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                LTB.Instance.SaveLTB(false, false, true);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

            MessageBox.Show(resources.GetString("btnPrint.MessageBox.Text"), resources.GetString("btnPrint.MessageBox.Title"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                LTB.Instance.LoadLTB();

                AppendItems(LTB.Instance.GetCategory(getDdEditionSelectorIndex()));

                ActivateInputs();
            }
            catch (System.Net.WebException)
            {
                FTP.Instance.OpenConfig();
                MessageBox.Show(resources.GetString("btnOpen.MessageBox.Text"), resources.GetString("btnOpen.MessageBox.Title"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                LTB.Instance.SaveLTB(true, true, false);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
            
            MessageBox.Show(resources.GetString("btnSave.MessageBox.Text"), resources.GetString("btnSave.MessageBox.Title"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ddEditionSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectorIndex = getDdEditionSelectorIndex();

            if (selectorIndex != lastSelectorIndex)
            {
                if (cbShowOwned.Checked)
                {
                    AppendItems(LTB.Instance.GetSpecificLTB(LTB.Instance.GetCategory(getDdEditionSelectorIndex()), true));
                }
                else if (cbShowNotOwned.Checked)
                {
                    AppendItems(LTB.Instance.GetSpecificLTB(LTB.Instance.GetCategory(getDdEditionSelectorIndex()), false));
                }
                else
                {
                    AppendItems(LTB.Instance.GetCategory(selectorIndex));
                }

                lastSelectorIndex = selectorIndex;
            }
        }

        private void form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (btnSave.Enabled && LTB.Instance.isDirty)
            {
                DialogResult saveFileDialog = MessageBox.Show(resources.GetString("form.MessageBox.Text"), resources.GetString("form.MessageBox.Title"), MessageBoxButtons.YesNo);

                if (saveFileDialog == DialogResult.Yes)
                {
                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        LTB.Instance.SaveLTB(true, true, false);
                    }
                    finally
                    {
                        Cursor.Current = Cursors.Default;
                    }
                }
            }

            CSV.Instance.DeleteCSV();
        }
    }
}
