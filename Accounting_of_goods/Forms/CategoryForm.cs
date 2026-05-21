namespace WinFormsApp1
{
    public partial class dgvCategories : Form
    {
        private readonly ICategoryService _categoryService;

        public dgvCategories(ICategoryService categoryService)
        {
            InitializeComponent();
            _categoryService = categoryService;
        }

        private void dgvCategories_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowHeadersVisible = false;
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            dataGridView1.DataSource = _categoryService.GetCategoriesForGrid();
            EnsureButtons();
        }

        private void EnsureButtons()
        {
            if (dataGridView1.Columns["EditCol"] == null)
            {
                var editCol = new DataGridViewButtonColumn { Name = "EditCol", HeaderText = "", Text = "вњЏпёЏ", UseColumnTextForButtonValue = true, Width = 40 };
                dataGridView1.Columns.Add(editCol);
            }
            if (dataGridView1.Columns["DeleteCol"] == null)
            {
                var delCol = new DataGridViewButtonColumn { Name = "DeleteCol", HeaderText = "", Text = "рџ—‘пёЏ", UseColumnTextForButtonValue = true, Width = 40 };
                dataGridView1.Columns.Add(delCol);
            }
        }

        private void btnCreateCategory_Click(object sender, EventArgs e)
        {
            string name = txtNewCategory.Text.Trim();
            if (string.IsNullOrEmpty(name)) return;

            if (_categoryService.CategoryExists(name))
            {
                MessageBox.Show("РљР°С‚РµРіРѕСЂРёСЏ СѓР¶Рµ СЃСѓС‰РµСЃС‚РІСѓРµС‚!", "РћС€РёР±РєР°", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _categoryService.AddCategory(name);
            txtNewCategory.Clear();
            RefreshGrid();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = (int)dataGridView1.Rows[e.RowIndex].Cells["ID"].Value;
            string name = dataGridView1.Rows[e.RowIndex].Cells["РќР°Р·РІР°РЅРёРµ"].Value.ToString();

            if (dataGridView1.Columns[e.ColumnIndex].Name == "DeleteCol")
            {
                if (_categoryService.HasProducts(id))
                {
                    MessageBox.Show("РќРµР»СЊР·СЏ СѓРґР°Р»РёС‚СЊ РєР°С‚РµРіРѕСЂРёСЋ, РІ РєРѕС‚РѕСЂРѕР№ РµСЃС‚СЊ С‚РѕРІР°СЂС‹!", "РћС€РёР±РєР°", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show($"РЈРґР°Р»РёС‚СЊ В«{name}В»?", "Р’РѕРїСЂРѕСЃ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    _categoryService.DeleteCategory(id);
                    RefreshGrid();
                }
            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "EditCol")
            {
                string newName = Microsoft.VisualBasic.Interaction.InputBox("Р’РІРµРґРёС‚Рµ РЅРѕРІРѕРµ РЅР°Р·РІР°РЅРёРµ:", "Р РµРґР°РєС‚РёСЂРѕРІР°РЅРёРµ", name);
                if (!string.IsNullOrEmpty(newName) && newName != name)
                {
                    _categoryService.UpdateCategoryName(id, newName);
                    RefreshGrid();
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
    }
}
