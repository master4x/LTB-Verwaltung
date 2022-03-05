﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace LTB_Verwaltung
{
    public partial class Form : System.Windows.Forms.Form
    {
        private readonly ComponentResourceManager resources = new ComponentResourceManager(typeof(Form));
        private bool allowCheckChange = true;
        private int lastSelectorState;

        public Form()
        {
            InitializeComponent();
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
        private void lvIndexTable_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (allowCheckChange)
            {
                LTB.Instance.ChangeLTB(e.Item.Tag.ToString(), e.Item.Checked, ddEditionSelector.SelectedIndex - 1);
            }
        }

        private void cbShowOwned_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShowOwned.Checked)
            {
                cbShowNotOwned.Checked = false;

                AppendItems(LTB.Instance.GetSpecificItems(LTB.Instance.GetCategory(ddEditionSelector.SelectedIndex - 1), true));
            }
            else
            {
                AppendItems(LTB.Instance.GetCategory(ddEditionSelector.SelectedIndex - 1));
            }
        }

        private void cbShowNotOwned_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShowNotOwned.Checked)
            {
                cbShowOwned.Checked = false;

                AppendItems(LTB.Instance.GetSpecificItems(LTB.Instance.GetCategory(ddEditionSelector.SelectedIndex - 1), false));
            }
            else
            {
                AppendItems(LTB.Instance.GetCategory(ddEditionSelector.SelectedIndex - 1));
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            LTB.Instance.SaveLTB(false, true);

            MessageBox.Show(resources.GetString("btnPrint.MessageBox.Text"), resources.GetString("btnPrint.MessageBox.Title"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                LTB.Instance.LoadLTB();

                AppendItems(LTB.Instance.GetCategory(ddEditionSelector.SelectedIndex - 1));

                ActivateInputs();
            }
            catch (System.Net.WebException)
            {
                MessageBox.Show(resources.GetString("btnOpen.MessageBox.Text"), resources.GetString("btnOpen.MessageBox.Title"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            LTB.Instance.SaveLTB(true, false);

            MessageBox.Show(resources.GetString("btnSave.MessageBox.Text"), resources.GetString("btnSave.MessageBox.Title"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ddEditionSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectorState = ddEditionSelector.SelectedIndex - 1;

            if (selectorState != lastSelectorState)
            {
                AppendItems(LTB.Instance.GetCategory(selectorState));

                cbShowOwned.Checked = false;
                cbShowNotOwned.Checked = false;

                lastSelectorState = selectorState;
            }
        }

        private void form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (btnSave.Enabled & LTB.Instance.isDirty)
            {
                DialogResult saveFileDialog = MessageBox.Show(resources.GetString("form.MessageBox.Text"), resources.GetString("form.MessageBox.Title"), MessageBoxButtons.YesNo);

                if (saveFileDialog == DialogResult.Yes)
                {
                    LTB.Instance.SaveLTB(true, false);
                }
            }

            CSV.Instance.DeleteCSV();
        }
    }
}
