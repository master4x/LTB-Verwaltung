namespace LTB_Verwaltung
{
    partial class GUI
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUI));
            this.lvIndexTable = new System.Windows.Forms.ListView();
            this.chOwnership = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chPublication = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chEdition = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chNote = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.ddEditionSelector = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.cbShowNotOwned = new System.Windows.Forms.CheckBox();
            this.cbShowOwned = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvIndexTable
            // 
            this.lvIndexTable.CheckBoxes = true;
            this.lvIndexTable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chOwnership,
            this.chNumber,
            this.chTitle,
            this.chPublication,
            this.chEdition,
            this.chNote});
            resources.ApplyResources(this.lvIndexTable, "lvIndexTable");
            this.lvIndexTable.FullRowSelect = true;
            this.lvIndexTable.GridLines = true;
            this.lvIndexTable.HideSelection = false;
            this.lvIndexTable.Name = "lvIndexTable";
            this.lvIndexTable.UseCompatibleStateImageBehavior = false;
            this.lvIndexTable.View = System.Windows.Forms.View.Details;
            this.lvIndexTable.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvIndexTable_ItemChecked);
            // 
            // chOwnership
            // 
            resources.ApplyResources(this.chOwnership, "chOwnership");
            // 
            // chNumber
            // 
            resources.ApplyResources(this.chNumber, "chNumber");
            // 
            // chTitle
            // 
            resources.ApplyResources(this.chTitle, "chTitle");
            // 
            // chPublication
            // 
            resources.ApplyResources(this.chPublication, "chPublication");
            // 
            // chEdition
            // 
            resources.ApplyResources(this.chEdition, "chEdition");
            // 
            // chNote
            // 
            resources.ApplyResources(this.chNote, "chNote");
            // 
            // btnOpen
            // 
            resources.ApplyResources(this.btnOpen, "btnOpen");
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ddEditionSelector
            // 
            this.ddEditionSelector.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.ddEditionSelector.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            resources.ApplyResources(this.ddEditionSelector, "ddEditionSelector");
            this.ddEditionSelector.FormattingEnabled = true;
            this.ddEditionSelector.Items.AddRange(new object[] {
            resources.GetString("ddEditionSelector.Items"),
            LTB.Instance.categories[0],
            LTB.Instance.categories[1],
            LTB.Instance.categories[2],
            LTB.Instance.categories[3],
            LTB.Instance.categories[4],
            LTB.Instance.categories[5],
            LTB.Instance.categories[6]});
            this.ddEditionSelector.Name = "ddEditionSelector";
            this.ddEditionSelector.SelectedIndexChanged += new System.EventHandler(this.ddEditionSelector_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.cbShowNotOwned);
            this.panel1.Controls.Add(this.cbShowOwned);
            this.panel1.Controls.Add(this.btnOpen);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.ddEditionSelector);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // btnPrint
            // 
            resources.ApplyResources(this.btnPrint, "btnPrint");
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // cbShowNotOwned
            // 
            resources.ApplyResources(this.cbShowNotOwned, "cbShowNotOwned");
            this.cbShowNotOwned.Name = "cbShowNotOwned";
            this.cbShowNotOwned.UseVisualStyleBackColor = true;
            this.cbShowNotOwned.CheckedChanged += new System.EventHandler(this.cbShowNotOwned_CheckedChanged);
            // 
            // cbShowOwned
            // 
            resources.ApplyResources(this.cbShowOwned, "cbShowOwned");
            this.cbShowOwned.Name = "cbShowOwned";
            this.cbShowOwned.UseVisualStyleBackColor = true;
            this.cbShowOwned.CheckedChanged += new System.EventHandler(this.cbShowOwned_CheckedChanged);
            // 
            // GUI
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvIndexTable);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "GUI";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.form_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListView lvIndexTable;
        private System.Windows.Forms.ColumnHeader chNumber;
        private System.Windows.Forms.ColumnHeader chTitle;
        private System.Windows.Forms.ColumnHeader chPublication;
        private System.Windows.Forms.ColumnHeader chEdition;
        private System.Windows.Forms.ColumnHeader chNote;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox ddEditionSelector;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox cbShowOwned;
        private System.Windows.Forms.CheckBox cbShowNotOwned;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.ColumnHeader chOwnership;
    }
}

