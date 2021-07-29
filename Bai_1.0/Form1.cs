using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bai_1._0.Context;
using Bai_1._0.Models;

namespace Bai_1._0
{
    public partial class FrmMain : Form
    {
        private IServices.IServices _IServices;
        private Account _account;
        private Guid _ID;
        private bool _flag = false;
        public FrmMain()
        {
            InitializeComponent();
            _IServices = new ServicesAccounts();
            _IServices.GetList();
            loadData();
            _IServices.resultsYear().ForEach(x=> cbNamSinh.Items.Add(x));
        }
        #region Form method
        /// <summary>
        /// reset data trên form load lại dữ liệu trông datagridview
        /// </summary>
        void clearForm()
        {
            txtTaiKhoan.Text =
                txtMatKhau.Text =
                    txtXacNhanMatKhau.Text = default;
            cbNam.Checked =
                cbNu.Checked =
                    cbON.Checked =
                        cbOFF.Checked = false;
            cbNamSinh.Text = default;
            loadData();
        }
        /// <summary>
        /// lấy dữ liệu từ datagridview đổ ngược lên form
        /// </summary>
        private void dgvAcc_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvAcc.Rows[e.RowIndex];
                txtTaiKhoan.ReadOnly = true;
                txtTaiKhoan.Text = row.Cells[0].Value.ToString();
                txtMatKhau.Text = row.Cells[1].Value.ToString();
                txtXacNhanMatKhau.Text = row.Cells[1].Value.ToString();
                cbNamSinh.Text = row.Cells[2].Value.ToString();
                if (row.Cells[3].Value.ToString() == "Nam")
                {
                    cbNam.Checked = true;
                }
                else
                {
                    cbNu.Checked = true;
                }
                if (row.Cells[4].Value.ToString() == "On")
                {
                    cbON.Checked = true;
                }
                else
                {
                    cbOFF.Checked = true;
                }

                _ID = _IServices.resultGuidID(row.Cells[0].Value.ToString());
            }
        }

        /// <summary>
        /// lấy dữ liệu từ CSDL đổ lên form 
        /// </summary>
        public void loadData()
        {
            dgvAcc.Text = default;
            Type type = typeof(Account);
            int count = type.GetProperties().Length;
            dgvAcc.ColumnCount = count;
            dgvAcc.Columns[0].Name = "Tài Khoản";
            dgvAcc.Columns[1].Name = "Mật khẩu";
            dgvAcc.Columns[2].Name = "Năm sinh";
            dgvAcc.Columns[3].Name = "Giới Tính";
            dgvAcc.Columns[4].Name = "Trạng thái";
            dgvAcc.Rows.Clear();
            _IServices.ResultList().ForEach(x =>
            {
                dgvAcc.Rows.Add(x.UserAcc, x.PassAcc, x.YearOfBirthAcc, bool.Parse(x.SexAcc.ToString()) == true ? "Nam" : "Nữ", bool.Parse(x.StatusAcc.ToString()) == true ? "On" : "Off");
            });
        }
        /// <summary>
        /// vlidate thông tin trên form
        /// lấy thông tin nhận vào trên form rồi trả về một tài khoản
        /// </summary>
        /// <returns>Account</returns>
        Account ResultAcc(Guid ID)
        {
            _account = new Account
            {
                IdAcc = ID,
                PassAcc = txtMatKhau.Text,
                SexAcc = cbNam.Checked == true ? true : false,
                StatusAcc = cbON.Checked == true ? true : false,
                UserAcc = txtTaiKhoan.Text,
                YearOfBirthAcc = int.Parse(cbNamSinh.Text)
            };
            return _account;
        }
        /// <summary>
        /// bắt lỗi thông tin trên form rồi trả về đúng hoặc sai
        /// </summary>
        /// <returns>true or false</returns>
        bool validate()
        {
            // bắt lỗi ký tự
            if (txtTaiKhoan.Text.Length < 8 && txtMatKhau.Text.Length < 8 && txtTaiKhoan.Text.Length > 15 && txtMatKhau.Text.Length > 15)
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu phải lơn hơn 8 và bé hơn 15 ký tự .");
                return false;
            }
            // bắt lỗi mật khẩu không khớp
            if (txtMatKhau.Text != txtXacNhanMatKhau.Text)
            {
                MessageBox.Show("Mật khẩu kkhoong khớp với nhau.");
                return false;
            }
            //
            if (cbNam.Checked == false && cbNu.Checked == false)
            {
                MessageBox.Show("hãy chọn giới tính phù hợp.");
                return false;
            }

            if (cbON.Checked == false && cbOFF.Checked == false)
            {
                MessageBox.Show("Hãy chọn trạng thái tài khoản.");
                return false;
            }
            return true;
        }
        /// <summary>
        /// checkbox events click
        /// </summary>
        private void cbNam_CheckedChanged(object sender, EventArgs e)
        {
            if (cbNu.Checked) cbNu.Checked = false;
        }

        private void cbNu_CheckedChanged(object sender, EventArgs e)
        {
            if (cbNam.Checked) cbNam.Checked = false;
        }

        private void cbON_CheckedChanged(object sender, EventArgs e)
        {
            if (cbOFF.Checked) cbOFF.Checked = false;
        }

        private void cbOFF_CheckedChanged(object sender, EventArgs e)
        {
            if (cbON.Checked) cbON.Checked = false;
        }
        #endregion

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (validate() == false) return;
            MessageBox.Show(_IServices.AddAccount(ResultAcc(Guid.NewGuid())));
            loadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MessageBox.Show(_IServices.SaveAccount());
            clearForm();
            _flag = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            MessageBox.Show(_IServices.UpdateAccount(ResultAcc(_ID)));
            clearForm();
            _flag = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            MessageBox.Show(_IServices.DeleteAccount(ResultAcc(_ID)));
            clearForm();
            _flag = true;
        }
        DialogResult x = MessageBox.Show
        ("Lưu những thay đổi ?",
            "Thông Báo",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (x == DialogResult.Yes)
            {
                _IServices.SaveAccount();
                Application.Exit();
            }
            Application.Exit();
        }
    }
}
