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
                var editCol = new DataGridViewButtonColumn { Name = "EditCol", HeaderText = "", Text = "✎", UseColumnTextForButtonValue = true, Width = 40 };
                dataGridView1.Columns.Add(editCol);
            }
            if (dataGridView1.Columns["DeleteCol"] == null)
            {
                var delCol = new DataGridViewButtonColumn { Name = "DeleteCol", HeaderText = "", Text = "✕", UseColumnTextForButtonValue = true, Width = 40 };
                dataGridView1.Columns.Add(delCol);
            }
        }

        private void btnCreateCategory_Click(object sender, EventArgs e)
        {
            string name = txtNewCategory.Text.Trim();
            if (string.IsNullOrEmpty(name)) return;

            if (_categoryService.CategoryExists(name))
            {
                MessageBox.Show("Категория уже существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            string name = dataGridView1.Rows[e.RowIndex].Cells["Название"].Value.ToString();

            if (dataGridView1.Columns[e.ColumnIndex].Name == "DeleteCol")
            {
                if (_categoryService.HasProducts(id))
                {
                    MessageBox.Show("Нельзя удалить категорию, в которой есть товары!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show($"Удалить «{name}»?", "Вопрос", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    _categoryService.DeleteCategory(id);
                    RefreshGrid();
                }
            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "EditCol")
            {
                string newName = Microsoft.VisualBasic.Interaction.InputBox("Введите новое название:", "Редактирование", name);
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
