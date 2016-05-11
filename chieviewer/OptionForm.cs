using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chieviewer
{
    public partial class OptionForm : Form
    {
        public OptionForm()
        {
            InitializeComponent();
            ClearAllInput();
            listBoxNgWord.Items.Clear();
            listBoxNgName.Items.Clear();
            DataBase db = new DataBase();
            List<NgListModel> ngWords = db.GetNgWordList(DataBase.NgType.Word);
            listBoxNgWord.Items.AddRange(ngWords.ToArray());
            List<NgListModel> ngNames = db.GetNgWordList(DataBase.NgType.Name);
            listBoxNgName.Items.AddRange(ngNames.ToArray());
            buttonUpdateNgName.Enabled = false;
            buttonUpdateNgWord.Enabled = false;
        }

        /***************************************/
        // NGネームタブ
        /***************************************/

        // NGネームタブの「OK」ボタン
        private void buttonNgNameOk_Click(object sender, EventArgs e)
        {
            ClearAllInput();
            this.Dispose();
        }

        // NGネームリスト内のアイテムが選択されたとき
        private void listBoxNgName_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelNgNameError.Text = "";
            buttonUpdateNgName.Enabled = true;

            NgListModel selectedItem = ((ListBox)sender).SelectedItem as NgListModel;
            if (selectedItem == null) return;
            textBoxNgName.Text = selectedItem.Word;
            textBoxNgName.Tag = selectedItem.Id;
            checkBoxNgNameRegex.Checked = selectedItem.Regex;
        }

        // NGネームタブの「追加」ボタン
        private void buttonAddNgName_Click(object sender, EventArgs e)
        {
            labelNgNameError.Text = "";

            if (string.IsNullOrEmpty(textBoxNgName.Text)) return;

            foreach (NgListModel item in listBoxNgName.Items)
            {
                if (item.Word == textBoxNgName.Text)
                {
                    labelNgNameError.Text = $"「{textBoxNgName.Text}」は既に登録されています。";
                    return;
                }
            }

            DataBase db = new DataBase();
            Int64 lastInsertRowId = db.AddNgWord(DataBase.NgType.Name, textBoxNgName.Text, checkBoxNgNameRegex.Checked);
            NgListModel newItem = new NgListModel();
            newItem.Id = (int)lastInsertRowId;
            newItem.Type = (int)DataBase.NgType.Name;
            newItem.Regex = checkBoxNgNameRegex.Checked;
            newItem.Word = textBoxNgName.Text;
            listBoxNgName.Items.Add(newItem);
            textBoxNgName.Text = "";
            textBoxNgName.Tag = null;
        }

        // NGネームタブの「更新」ボタン
        private void buttonUpdateNgName_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxNgName.Text)) return;

            int selectedIndex = listBoxNgName.SelectedIndex;
            NgListModel selectedItem = listBoxNgName.SelectedItem as NgListModel;

            DataBase db = new DataBase();
            db.UpdateNgWord(DataBase.NgType.Name, selectedItem.Id, textBoxNgName.Text, checkBoxNgNameRegex.Checked);
            // 更新後、リフレッシュ
            listBoxNgName.Items.Clear();
            List<NgListModel> ngNames = db.GetNgWordList(DataBase.NgType.Name);
            listBoxNgName.Items.AddRange(ngNames.ToArray());
            listBoxNgName.SelectedIndex = selectedIndex;
        }

        // NGネームタブの「削除」ボタン
        private void buttonDeleteNgName_Click(object sender, EventArgs e)
        {
            labelNgNameError.Text = "";

            if (listBoxNgName.SelectedItem == null)
            {
                return;
            }
            NgListModel selectedItem = (NgListModel)listBoxNgName.SelectedItem;
            DataBase db = new DataBase();
            db.DeleteNgWordList(DataBase.NgType.Name, selectedItem.Id);
            int index = listBoxNgName.SelectedIndex;
            listBoxNgName.Items.Remove(selectedItem);

            if (index > 0)
            {
                listBoxNgName.SelectedIndex = index - 1;
            }
            else if (listBoxNgName.Items.Count > 0)
            {
                listBoxNgName.SelectedIndex = 0;
            }
            if(listBoxNgName.Items.Count == 0)
            {
                buttonUpdateNgName.Enabled = false;
            }
        }


        /***************************************/
        // NGワードタブ
        /***************************************/

        // NGワードタブの「OK」ボタン
        private void buttonNgWordOk_Click(object sender, EventArgs e)
        {
            ClearAllInput();
            this.Dispose();
        }

        // NGワードリスト内のアイテムが選択されたとき
        private void listBoxNgWord_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelNgWordError.Text = "";
            buttonUpdateNgWord.Enabled = true;

            NgListModel selectedItem = ((ListBox)sender).SelectedItem as NgListModel;
            if (selectedItem == null) return;
            textBoxNgWord.Text = selectedItem.Word;
            textBoxNgWord.Tag = selectedItem.Id;
            checkBoxNgWordRegex.Checked = selectedItem.Regex;
        }

        // NGワードタブの「追加」ボタン
        private void buttonAddNgWord_Click(object sender, EventArgs e)
        {
            labelNgWordError.Text = "";

            if (string.IsNullOrEmpty(textBoxNgWord.Text)) return;

            foreach(NgListModel item in listBoxNgWord.Items)
            {
                if (item.Word == textBoxNgWord.Text)
                {
                    labelNgWordError.Text = $"「{textBoxNgWord.Text}」は既に登録されています。";
                    return;
                }
            }

            DataBase db = new DataBase();
            Int64 lastInsertRowId = db.AddNgWord(DataBase.NgType.Word, textBoxNgWord.Text, checkBoxNgWordRegex.Checked);
            NgListModel newItem = new NgListModel();
            newItem.Id = (int)lastInsertRowId;
            newItem.Type = (int)DataBase.NgType.Word;
            newItem.Regex = checkBoxNgWordRegex.Checked;
            newItem.Word = textBoxNgWord.Text;
            listBoxNgWord.Items.Add(newItem);
            textBoxNgWord.Text = "";
            textBoxNgWord.Tag = null;
        }

        // NGワードタブの「更新」ボタン
        private void buttonUpdateNgWord_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxNgWord.Text)) return;

            int selectedIndex = listBoxNgWord.SelectedIndex;
            NgListModel selectedItem = listBoxNgWord.SelectedItem as NgListModel;
            DataBase db = new DataBase();
            db.UpdateNgWord(DataBase.NgType.Word, selectedItem.Id, textBoxNgWord.Text, checkBoxNgWordRegex.Checked);
            // 更新後、リフレッシュ
            listBoxNgWord.Items.Clear();
            List<NgListModel> ngWords = db.GetNgWordList(DataBase.NgType.Word);
            listBoxNgWord.Items.AddRange(ngWords.ToArray());
            listBoxNgWord.SelectedIndex = selectedIndex;
        }

        // NGワードタブの「削除」ボタン
        private void buttonDeleteNgWord_Click(object sender, EventArgs e)
        {
            labelNgWordError.Text = "";

            if (listBoxNgWord.SelectedItem == null)
            {
                return;
            }
            NgListModel selectedItem = (NgListModel)listBoxNgWord.SelectedItem;
            DataBase db = new DataBase();
            db.DeleteNgWordList(DataBase.NgType.Word, selectedItem.Id);
            int index = listBoxNgWord.SelectedIndex;
            listBoxNgWord.Items.Remove(selectedItem);

            if (index > 0)
            {
                listBoxNgWord.SelectedIndex = index - 1;
            }
            else if(listBoxNgWord.Items.Count > 0)
            {
                listBoxNgWord.SelectedIndex = 0;
            }
        }

        // タブ切り替え時
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearAllInput();
        }

        // 初期化処理
        private void ClearAllInput()
        {
            textBoxNgName.Text = "";
            textBoxNgWord.Text = "";
            textBoxNgName.Tag = null;
            textBoxNgWord.Tag = null;
            labelNgWordError.Text = "";
            labelNgNameError.Text = "";
            listBoxNgName.ClearSelected();
            listBoxNgWord.ClearSelected();
            buttonUpdateNgName.Enabled = false;
            buttonUpdateNgWord.Enabled = false;
        }


    }
}
